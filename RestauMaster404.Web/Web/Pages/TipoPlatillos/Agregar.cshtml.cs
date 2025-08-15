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
    public class AgregarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty]
        public TipoPlatilloBase TipoPlatillo { get; set; } = new();

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarTipoPlatillo");
                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

                var contenido = new StringContent(JsonSerializer.Serialize(TipoPlatillo), System.Text.Encoding.UTF8, "application/json");
                var respuesta = await cliente.PostAsync(endpoint, contenido);

                if (respuesta.StatusCode == HttpStatusCode.Created)
                {
                    return RedirectToPage("Index");
                }
                else
                {
                    var error = await respuesta.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, "Error de la API: " + error);
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurri� un error al agregar el tipo de platillo: " + ex.Message);
                return Page();
            }
        }
    }
}