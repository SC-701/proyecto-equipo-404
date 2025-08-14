using System.Net;
using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Ventas
{
    public class EliminarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EliminarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public VentaResponse Venta { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id == Guid.Empty)
            {
                return RedirectToPage("Index");
            }

            try
            {
                string endpointMetodo = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerVenta");
                string obtenerVentaEndpoint = string.Format(endpointMetodo, Id);

                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

                var respuesta = await cliente.GetAsync(obtenerVentaEndpoint);

                if (respuesta.StatusCode == HttpStatusCode.OK)
                {
                    var json = await respuesta.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var detalleVenta = JsonSerializer.Deserialize<VentaResponse>(json, opciones);

                    if (detalleVenta == null)
                    {
                        return RedirectToPage("Index");
                    }
                    Venta = detalleVenta;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No se pudo obtener la venta para su eliminación.");
                    return RedirectToPage("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al cargar la venta.");
                return RedirectToPage("Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Id == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "ID de la venta no válido.");
                await OnGetAsync();
                return Page();
            }

            try
            {
                string endpointMetodo = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarVenta");
                string eliminarVentaEndpoint = string.Format(endpointMetodo, Id);

                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

                var respuesta = await cliente.DeleteAsync(eliminarVentaEndpoint);

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToPage("Index");
                }
                else
                {
                    if (respuesta.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        ModelState.AddModelError(string.Empty, "Error: La venta cuenta con detalles, por lo tanto no se puede Eliminar");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Ocurrió un error al eliminar el la venta.");
                    }

                    await OnGetAsync();
                    return Page();
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error inesperado al eliminar la venta seleccionada.");
                await OnGetAsync();
                return Page();
            }
        }
    }
}
