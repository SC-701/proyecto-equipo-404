using System.Net;
using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Ventas
{
    [Authorize(Roles = "1, 2")]
    public class DetallesModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public VentaResponse Venta { get; set; } = new();
        public List<DetalleVentaResponse> DetallesVenta { get; set; } = new();
        public List<PlatilloResponse> Platillos { get; set; } = new();

        [BindProperty]
        public DetalleVentaRequest NuevoDetalle { get; set; } = new();

        public DetallesModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id == Guid.Empty)
            {
                return RedirectToPage("Index");
            }
            await CargarDatosAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Id == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "ID de venta no válido.");
                return RedirectToPage("Index");
            }

            // CARGA LA VENTA ANTES DE USAR SUS PROPIEDADES
            await CargarDatosAsync();

            try
            {
                NuevoDetalle.IdVenta = Id;
                string platilloEndpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerPlatillo");
                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

                var respuestaPlatillo = await cliente.GetAsync(string.Format(platilloEndpoint, NuevoDetalle.IdPlatillo));
                if (!respuestaPlatillo.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Error al obtener el precio del platillo.");
                    return Page();
                }
                var platilloJson = await respuestaPlatillo.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var platillo = JsonSerializer.Deserialize<PlatilloDetalle>(platilloJson, opciones);

                NuevoDetalle.PrecioUnitario = platillo.Precio;
                NuevoDetalle.SubTotal = NuevoDetalle.Cantidad * NuevoDetalle.PrecioUnitario;

                string agregarDetalleEndpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarDetalleVenta");
                var contenido = new StringContent(JsonSerializer.Serialize(NuevoDetalle), System.Text.Encoding.UTF8, "application/json");
                var respuestaDetalle = await cliente.PostAsync(agregarDetalleEndpoint, contenido);

                if (respuestaDetalle.StatusCode == HttpStatusCode.Created)
                {
                    await RecalcularTotalVentaAsync(Id);
                    return RedirectToPage(new { id = Id });
                }
                else
                {
                    var error = await respuestaDetalle.Content.ReadAsStringAsync();

                    if (string.IsNullOrWhiteSpace(error))
                    {
                        ModelState.AddModelError(string.Empty, "La API respondió con un error, pero sin un mensaje detallado. Por favor, revise el log del servidor.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error de la API al agregar el detalle: " + error);
                    }
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error: " + ex.Message);
                return Page();
            }
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

                string ventaEndpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerVenta");
                var respuestaVenta = await cliente.GetAsync(string.Format(ventaEndpoint, Id));
                if (respuestaVenta.IsSuccessStatusCode)
                {
                    var json = await respuestaVenta.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        Venta = JsonSerializer.Deserialize<VentaResponse>(json, opciones);
                    }
                }

                string detallesEndpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerDetallesVenta");
                var respuestaDetalles = await cliente.GetAsync(string.Format(detallesEndpoint, Id));
                if (respuestaDetalles.IsSuccessStatusCode)
                {
                    var json = await respuestaDetalles.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        DetallesVenta = JsonSerializer.Deserialize<List<DetalleVentaResponse>>(json, opciones);
                    }
                }

                string platillosEndpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerPlatillos");
                var respuestaPlatillos = await cliente.GetAsync(platillosEndpoint);
                if (respuestaPlatillos.IsSuccessStatusCode)
                {
                    var json = await respuestaPlatillos.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        Platillos = JsonSerializer.Deserialize<List<PlatilloResponse>>(json, opciones);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al cargar los datos: " + ex.Message);
            }
        }

        private async Task RecalcularTotalVentaAsync(Guid ventaId)
        {
            if (Venta == null)
            {
                ModelState.AddModelError(string.Empty, "Error: No se pudo cargar la información de la venta para actualizar el total.");
                return;
            }

            try
            {
                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

                string detallesEndpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerDetallesVenta");
                var respuestaDetalles = await cliente.GetAsync(string.Format(detallesEndpoint, ventaId));

                double nuevoTotal = 0;
                if (respuestaDetalles.IsSuccessStatusCode)
                {
                    var json = await respuestaDetalles.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var detallesActualizados = JsonSerializer.Deserialize<List<DetalleVentaResponse>>(json, opciones);

                        nuevoTotal = detallesActualizados.Sum(d => d.SubTotal);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al obtener los detalles de la venta para recalcular el total.");
                    return;
                }

                string ventaEndpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarVenta");

                var ventaAActualizar = new
                {
                    Id = ventaId,
                    Fecha = Venta.Fecha,
                    Hora = Venta.Hora,
                    Total = nuevoTotal
                };

                var contenido = new StringContent(JsonSerializer.Serialize(ventaAActualizar), System.Text.Encoding.UTF8, "application/json");

                var respuestaPut = await cliente.PutAsync(string.Format(ventaEndpoint, ventaId), contenido);

                if (!respuestaPut.IsSuccessStatusCode)
                {
                    var error = await respuestaPut.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(error))
                    {
                        ModelState.AddModelError(string.Empty, "Error de la API al actualizar el total de la venta. El servidor no proporcionó un mensaje de error.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error de la API al actualizar el total: " + error);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al recalcular y actualizar el total: " + ex.Message);
            }
        }
    }
}