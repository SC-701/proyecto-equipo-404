using Abstracciones.Interfaces.Reglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using Abstracciones.Modelos;
using System.Net;

namespace Web.Pages.Dashboard
{
    public class PlatilloVendido
    {
        public string NombrePlatillo { get; set; }
        public int CantidadVendida { get; set; }
    }

    [Authorize(Roles = "1, 2")]
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public int TiposPlatilloTotal { get; private set; }

        public double VentasDelMesTotal { get; private set; }

        public List<PlatilloVendido> PlatillosMasVendidos { get; private set; } = new List<PlatilloVendido>();

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGetAsync()
        {
            await Task.WhenAll(
                CargarConteoTiposPlatilloAsync(),
                ProcesarDatosDeVentasAsync()
            );
        }

        private async Task CargarConteoTiposPlatilloAsync()
        {
            try
            {
                var endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ContarTiposPlatillo");

                using var cliente = new HttpClient();

                var token = HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "Token")?.Value;
                if (!string.IsNullOrWhiteSpace(token))
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var respuesta = await cliente.GetAsync(endpoint);
                if (respuesta.IsSuccessStatusCode)
                {
                    var contenido = await respuesta.Content.ReadAsStringAsync();
                    TiposPlatilloTotal = int.TryParse(contenido, out var n) ? n : 0;
                }
                else
                {
                    TiposPlatilloTotal = 0;
                    ModelState.AddModelError(string.Empty, $"API {(int)respuesta.StatusCode} {respuesta.ReasonPhrase} en ContarTiposPlatillo.");
                }
            }
            catch (Exception ex)
            {
                TiposPlatilloTotal = 0;
                ModelState.AddModelError(string.Empty, "Error al contar tipos de platillo: " + ex.Message);
            }
        }

        private async Task ProcesarDatosDeVentasAsync()
        {
            VentasDelMesTotal = 0.0;
            PlatillosMasVendidos.Clear();

            var token = HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "Token")?.Value;
            if (string.IsNullOrWhiteSpace(token))
            {
                return;
            }

            try
            {
                var endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerVentas");
                using var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var respuesta = await cliente.GetAsync(endpoint);
                if (respuesta.IsSuccessStatusCode)
                {
                    var json = await respuesta.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var ventas = JsonSerializer.Deserialize<List<VentaResponse>>(json, opciones);

                    if (ventas != null)
                    {
                        var ventasDelMes = ventas.Where(v => v.Fecha.Year == DateTime.Now.Year && v.Fecha.Month == DateTime.Now.Month);

                        VentasDelMesTotal = ventasDelMes.Sum(v => v.Total);

                        var platillosVendidos = new Dictionary<string, int>();

                        foreach (var venta in ventasDelMes)
                        {
                            var detallesEndpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerDetallesVenta");
                            var detallesUrl = string.Format(detallesEndpoint, venta.Id);
                            var respuestaDetalles = await cliente.GetAsync(detallesUrl);

                            if (respuestaDetalles.IsSuccessStatusCode)
                            {
                                var jsonDetalles = await respuestaDetalles.Content.ReadAsStringAsync();
                                var detalles = JsonSerializer.Deserialize<List<DetalleVentaResponse>>(jsonDetalles, opciones);
                                if (detalles != null)
                                {
                                    foreach (var detalle in detalles)
                                    {
                                        if (platillosVendidos.ContainsKey(detalle.NombrePlatillo))
                                        {
                                            platillosVendidos[detalle.NombrePlatillo] += detalle.Cantidad;
                                        }
                                        else
                                        {
                                            platillosVendidos[detalle.NombrePlatillo] = detalle.Cantidad;
                                        }
                                    }
                                }
                            }
                        }

                        PlatillosMasVendidos = platillosVendidos.OrderByDescending(kvp => kvp.Value)
                                                               .Take(5)
                                                               .Select(kvp => new PlatilloVendido { NombrePlatillo = kvp.Key, CantidadVendida = kvp.Value })
                                                               .ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al procesar los datos de ventas: " + ex.Message);
            }
        }
    }
}