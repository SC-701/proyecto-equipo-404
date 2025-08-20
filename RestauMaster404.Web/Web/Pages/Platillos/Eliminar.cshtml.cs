using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Platillos
{
    [Authorize(Roles = "1, 3")]
    public class EliminarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EliminarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public PlatilloResponse Platillo { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id == Guid.Empty)
            {
                return RedirectToPage("Index");
            }

            string endpointMetodo = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerPlatillo");
            string obtenerPlatilloEndpoint = string.Format(endpointMetodo, Id);

            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

            var respuesta = await cliente.GetAsync(obtenerPlatilloEndpoint);

            if (!respuesta.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "No se pudo obtener el platillo para su eliminación.");
                return RedirectToPage("Index");
            }

            var json = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var detalle = JsonSerializer.Deserialize<PlatilloResponse>(json, opciones);

            if (detalle == null)
            {
                return RedirectToPage("Index");
            }

            Platillo = detalle;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Id == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "ID de platillo no válido.");
                return Page();
            }

            try
            {
                string endpointMetodo = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarPlatillo");
                string eliminarPlatilloEndpoint = string.Format(endpointMetodo, Id);

                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

                var respuesta = await cliente.DeleteAsync(eliminarPlatilloEndpoint);

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToPage("Index");
                }
                else
                {
                    if (respuesta.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        ModelState.AddModelError(string.Empty, "Error: El platillo se encuentra asociado a una o varias ventas y no puede ser eliminado.");
                    }
                    else
                    {
                        var error = await respuesta.Content.ReadAsStringAsync();
                        ModelState.AddModelError(string.Empty, $"Ocurrió un error al eliminar el platillo: {error}");
                    }

                    await OnGetAsync();
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Ocurrió un error inesperado al eliminar el platillo: {ex.Message}");
                await OnGetAsync();
                return Page();
            }
        }
    }
}