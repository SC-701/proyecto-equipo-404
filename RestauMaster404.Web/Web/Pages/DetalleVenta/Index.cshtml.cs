using System.Net;
using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.DetalleVenta
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        public List<DetalleVentaResponse> DetalleVentas { get; set; } = new();
        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        public async Task OnGet(Guid? id)
        {
            await CargarDetalleVentasAsync(id);
        }

        private async Task CargarDetalleVentasAsync(Guid? id)
        {
            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerDetallesVenta").Replace("{0}", id.ToString()); ;

                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);

                var respuesta = await cliente.GetAsync(endpoint);

                if (respuesta.StatusCode == HttpStatusCode.OK)
                {
                    var json = await respuesta.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    DetalleVentas = JsonSerializer.Deserialize<List<DetalleVentaResponse>>(json, opciones);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al cargar los detalles de la venta: " + ex.Message);
            }
        }
    }
}
