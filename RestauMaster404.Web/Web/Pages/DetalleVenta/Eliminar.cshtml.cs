using System.Net;
using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.DetalleVenta
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

        public DetalleVentaResponse DetalleVenta { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id == Guid.Empty)
            {
                return RedirectToPage("Index");
            }

            try
            {
                string endpointMetodo = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerDetalleVenta");
                string obtenerVentaEndpoint = string.Format(endpointMetodo, Id);

                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

                var respuesta = await cliente.GetAsync(obtenerVentaEndpoint);

                if (respuesta.StatusCode == HttpStatusCode.OK)
                {
                    var json = await respuesta.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var detalleVenta = JsonSerializer.Deserialize<DetalleVentaResponse>(json, opciones);

                    if (detalleVenta == null)
                    {
                        return RedirectToPage("Index");
                    }
                    DetalleVenta = detalleVenta;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No se pudo obtener el detalle de la venta para su eliminaci�n.");
                    return RedirectToPage("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurri� un error al cargar el detalle de la venta.");
                return RedirectToPage("Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Id == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "ID de detalle venta no v�lido.");
                await OnGetAsync();
                return Page();
            }

            try
            {
                string endpointMetodo = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarDetalleVenta");
                string eliminarDetalleVentaEndpoint = string.Format(endpointMetodo, Id);

                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

                var respuesta = await cliente.DeleteAsync(eliminarDetalleVentaEndpoint);

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToPage("Index");
                }
                else
                {
                    if (respuesta.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        ModelState.AddModelError(string.Empty, "Ocurri� un error al eliminar el detalle de la venta.");
                    }

                    await OnGetAsync();
                    return Page();
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurri� un error inesperado al eliminar el detalle de la venta seleccionada.");
                await OnGetAsync();
                return Page();
            }
        }
    }
}
