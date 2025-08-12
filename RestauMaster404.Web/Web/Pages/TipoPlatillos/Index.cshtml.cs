using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace Web.Pages.TipoPlatillos
{
    [Authorize(Roles = "1, 3")]
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public List<TipoPlatilloResponse> TiposPlatillo { get; set; } = new();

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGetAsync()
        {
            await CargarTiposPlatilloAsync();
        }

        private async Task CargarTiposPlatilloAsync()
        {
            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTiposPlatillo");

                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);

                var respuesta = await cliente.GetAsync(endpoint);

                if (respuesta.StatusCode == HttpStatusCode.OK)
                {
                    var json = await respuesta.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    TiposPlatillo = JsonSerializer.Deserialize<List<TipoPlatilloResponse>>(json, opciones);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "La API no devolvió una respuesta exitosa.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al cargar los tipos de platillo: " + ex.Message);
            }
        }
    }
}