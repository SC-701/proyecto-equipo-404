using System.Net;
using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.Text;

namespace Web.Pages.Ventas
{
    [Authorize(Roles = "1, 2")]
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
            var token = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value;
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Login");
            }

            if (Id == Guid.Empty)
            {
                return RedirectToPage("Index");
            }

            try
            {
                string endpointMetodo = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerVenta");
                string obtenerVentaEndpoint = string.Format(endpointMetodo, Id);

                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

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
            var token = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value;
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Login");
            }

            if (Id == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "ID de la venta no válido.");
                await OnGetAsync();
                return Page();
            }

            try
            {
                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                string endpointDetalles = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerDetallesVenta");
                string obtenerDetallesEndpoint = string.Format(endpointDetalles, Id);
                var respuestaObtenerDetalles = await cliente.GetAsync(obtenerDetallesEndpoint);

                if (respuestaObtenerDetalles.StatusCode == HttpStatusCode.OK)
                {
                    var jsonDetalles = await respuestaObtenerDetalles.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var detallesVenta = JsonSerializer.Deserialize<List<Abstracciones.Modelos.DetalleVentaResponse>>(jsonDetalles, opciones);

                    if (detallesVenta != null)
                    {
                        string endpointEliminarDetalle = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarDetalleVenta");
                        foreach (var detalle in detallesVenta)
                        {
                            string eliminarDetalleEndpoint = string.Format(endpointEliminarDetalle, detalle.Id);
                            var respuestaEliminarDetalle = await cliente.DeleteAsync(eliminarDetalleEndpoint);

                            if (!respuestaEliminarDetalle.IsSuccessStatusCode)
                            {
                                var error = await respuestaEliminarDetalle.Content.ReadAsStringAsync();
                                ModelState.AddModelError(string.Empty, $"Error al eliminar el detalle con ID {detalle.Id}: {error}");
                                await OnGetAsync();
                                return Page();
                            }
                        }
                    }
                }
                else if (respuestaObtenerDetalles.StatusCode != HttpStatusCode.NoContent)
                {
                    var error = await respuestaObtenerDetalles.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Ocurrió un error al obtener los detalles de la venta: {error}");
                    await OnGetAsync();
                    return Page();
                }

                string endpointVenta = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarVenta");
                string eliminarVentaEndpoint = string.Format(endpointVenta, Id);

                var respuestaVenta = await cliente.DeleteAsync(eliminarVentaEndpoint);

                if (respuestaVenta.IsSuccessStatusCode)
                {
                    return RedirectToPage("Index");
                }
                else
                {
                    var error = await respuestaVenta.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(error))
                    {
                        ModelState.AddModelError(string.Empty, "Ocurrió un error al eliminar la venta. El servidor no proporcionó un mensaje de error detallado.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"Ocurrió un error al eliminar la venta: {error}");
                    }
                    await OnGetAsync();
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Ocurrió un error inesperado al eliminar la venta seleccionada: {ex.Message}");
                await OnGetAsync();
                return Page();
            }
        }
    }
}
