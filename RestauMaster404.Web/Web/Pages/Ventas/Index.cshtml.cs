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
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        public List<VentaResponse> Ventas { get; set; } = new();

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        public async Task OnGet()
        {
            await CargarVentasAsync();
        }

        private async Task CargarVentasAsync()
        {
            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerVentas");

                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);

                var respuesta = await cliente.GetAsync(endpoint);

                if (respuesta.StatusCode == HttpStatusCode.OK)
                {
                    var json = await respuesta.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    Ventas = JsonSerializer.Deserialize<List<VentaResponse>>(json, opciones);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al cargar las ventas: " + ex.Message);
            }
        }

    }
}
