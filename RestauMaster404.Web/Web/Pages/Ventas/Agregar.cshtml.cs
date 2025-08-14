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
    public class AgregarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarVenta");

                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

                var nuevaVenta = new VentaRequest
                {
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.TimeOfDay,
                    Total = 100
                };

                var contenido = new StringContent(JsonSerializer.Serialize(nuevaVenta), System.Text.Encoding.UTF8, "application/json");
                var respuesta = await cliente.PostAsync(endpoint, contenido);

                if (respuesta.StatusCode == HttpStatusCode.Created)
                {
                    var json = await respuesta.Content.ReadAsStringAsync();

                    if (string.IsNullOrWhiteSpace(json))
                    {
                        return RedirectToPage("Index");
                    }

                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var ventaCreada = JsonSerializer.Deserialize<VentaResponse>(json, opciones);

                    return RedirectToPage("Detalles", new { id = ventaCreada.Id });
                }
                else
                {
                    var error = await respuesta.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, "Error de la API al crear la venta: " + error);
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al crear la venta: " + ex.Message);
                return Page();
            }
        }
    }
}