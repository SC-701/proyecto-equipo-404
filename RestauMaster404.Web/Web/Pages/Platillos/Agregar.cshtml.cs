using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Platillos
{
    [Authorize(Roles = "1, 3")]

    public class AgregarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty]
        public PlatilloRequest Platillo { get; set; } = new();

        [BindProperty]
        public List<SelectListItem> TiposPlatillo { get; set; } = new();

        [BindProperty]
        public List<SelectListItem> Estados { get; set; } = new();

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<IActionResult> OnGet()
        {
            await CargarTiposPlatilloAsync();
            await CargarEstadosAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await CargarTiposPlatilloAsync();
                await CargarEstadosAsync();
                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarPlatillo");

            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);
            //^^^ Esto es lo de autenticación.
            var contenido = new StringContent(JsonSerializer.Serialize(Platillo), System.Text.Encoding.UTF8, "application/json");
            var respuesta = await cliente.PostAsync(endpoint, contenido);

            if (!respuesta.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Ocurrió un error al guardar el platillo.");
                await CargarTiposPlatilloAsync();
                await CargarEstadosAsync();
                return Page();
            }

            return RedirectToPage("Index");
        }

        private async Task CargarTiposPlatilloAsync()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTiposPlatillo");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);
            //^^^ Esto es lo de autenticación.
            var respuesta = await cliente.GetAsync(endpoint);

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var json = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var tipos = JsonSerializer.Deserialize<List<TipoPlatilloResponse>>(json, opciones);

                TiposPlatillo = tipos.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Nombre
                }).ToList();
            }
        }

        private async Task CargarEstadosAsync()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerEstados");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);
            //^^^ Esto es lo de autenticación.
            var respuesta = await cliente.GetAsync(endpoint);

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var json = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var estados = JsonSerializer.Deserialize<List<EstadoResponse>>(json, opciones);

                Estados = estados.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nombre
                }).ToList();
            }
        }
    }
}
