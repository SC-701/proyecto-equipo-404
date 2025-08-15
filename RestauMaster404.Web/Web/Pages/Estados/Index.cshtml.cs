using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Estados
{
    [Authorize(Roles = "1")]
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public List<EstadoResponse> Estados { get; set; } = new();

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGetAsync()
        {
            await CargarEstadosAsync();
        }

        private async Task CargarEstadosAsync()
        {
            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerEstados");

                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);

                var respuesta = await cliente.GetAsync(endpoint);

                if (respuesta.StatusCode == HttpStatusCode.OK)
                {
                    var json = await respuesta.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    Estados = JsonSerializer.Deserialize<List<EstadoResponse>>(json, opciones);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "La API no devolvió una respuesta exitosa.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al cargar los estados: " + ex.Message);
            }
        }
    }
}