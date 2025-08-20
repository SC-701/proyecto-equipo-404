using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Estados
{
    [Authorize(Roles = "1")]
    public class EliminarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EliminarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public EstadoResponse Estado { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id == Guid.Empty)
            {
                return RedirectToPage("Index");
            }

            try
            {
                string endpointMetodo = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerEstado");
                string obtenerEstadoEndpoint = string.Format(endpointMetodo, Id);

                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

                var respuesta = await cliente.GetAsync(obtenerEstadoEndpoint);

                if (respuesta.StatusCode == HttpStatusCode.OK)
                {
                    var json = await respuesta.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var detalle = JsonSerializer.Deserialize<EstadoResponse>(json, opciones);

                    if (detalle == null)
                    {
                        return RedirectToPage("Index");
                    }
                    Estado = detalle;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No se pudo obtener el estado para su eliminación.");
                    return RedirectToPage("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al cargar el estado.");
                return RedirectToPage("Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Id == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "ID de estado no válido.");
                await OnGetAsync();
                return Page();
            }

            try
            {
                string endpointMetodo = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarEstado");
                string eliminarEstadoEndpoint = string.Format(endpointMetodo, Id);

                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

                var respuesta = await cliente.DeleteAsync(eliminarEstadoEndpoint);

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToPage("Index");
                }
                else
                {
                    if (respuesta.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        ModelState.AddModelError(string.Empty, "Error: El Estado se encuentra asociado a un Platillo");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Ocurrió un error al eliminar el estado.");
                    }

                    await OnGetAsync();
                    return Page();
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error inesperado al eliminar el estado.");
                await OnGetAsync();
                return Page();
            }
        }
    }
}