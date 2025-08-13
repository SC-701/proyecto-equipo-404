using Abstracciones.Interfaces.Reglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.Dashboard
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public int TiposPlatilloTotal { get; private set; }

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGetAsync()
        {
            await CargarConteoTiposPlatilloAsync();
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
    }
}
