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
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public PlatilloRequest Platillo { get; set; } = new();

        public List<SelectListItem> TiposPlatillo { get; set; } = new();
        public List<SelectListItem> Estados { get; set; } = new();

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id == Guid.Empty)
            {
                return RedirectToPage("Index");
            }

            string endpointMetodo = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerPlatillo");
            string obtenerPlatilloEndpoint = string.Format(endpointMetodo, Id);

            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

            var respuesta = await cliente.GetAsync(obtenerPlatilloEndpoint);

            if (!respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }

            var json = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var detalle = JsonSerializer.Deserialize<PlatilloDetalle>(json, opciones);

            if (detalle == null)
            {
                return RedirectToPage("Index");
            }

            Platillo.Nombre = detalle.Nombre;
            Platillo.Precio = detalle.Precio;
            Platillo.Stock = detalle.Stock;
            Platillo.IdTipoPlatillo = detalle.IdTipoPlatillo;
            Platillo.IdEstado = detalle.IdEstado;

            await CargarTiposPlatilloAsync(Platillo.IdTipoPlatillo);
            await CargarEstadosAsync(Platillo.IdEstado);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
           
            if (!ModelState.IsValid)
            {
                await CargarTiposPlatilloAsync(Platillo.IdTipoPlatillo);
                await CargarEstadosAsync(Platillo.IdEstado);
                return Page();
            }

            string endpointMetodo = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarPlatillo");
            string editarPlatilloEndpoint = string.Format(endpointMetodo, Id);

            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

            var contenido = new StringContent(JsonSerializer.Serialize(Platillo), System.Text.Encoding.UTF8, "application/json");
            var respuesta = await cliente.PutAsync(editarPlatilloEndpoint, contenido);

            if (!respuesta.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al actualizar el platillo.");
                return Page();
            }

            return RedirectToPage("Index");
        }

        private async Task CargarTiposPlatilloAsync(Guid? idSeleccionado = null)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTiposPlatillo");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

            var respuesta = await cliente.GetAsync(endpoint);
            if (!respuesta.IsSuccessStatusCode)
            {
                TiposPlatillo = new List<SelectListItem>();
                return;
            }

            var json = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var tipos = JsonSerializer.Deserialize<List<TipoPlatilloResponse>>(json, opciones) ?? new List<TipoPlatilloResponse>();

            TiposPlatillo = tipos.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Nombre,
                Selected = t.Id == idSeleccionado
            }).ToList();
        }

        private async Task CargarEstadosAsync(Guid? idSeleccionado = null)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerEstados");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

            var respuesta = await cliente.GetAsync(endpoint);
            if (!respuesta.IsSuccessStatusCode)
            {
                Estados = new List<SelectListItem>();
                return;
            }

            var json = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var estados = JsonSerializer.Deserialize<List<EstadoResponse>>(json, opciones) ?? new List<EstadoResponse>();

            Estados = estados.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Nombre,
                Selected = e.Id == idSeleccionado
            }).ToList();
        }
    }
}