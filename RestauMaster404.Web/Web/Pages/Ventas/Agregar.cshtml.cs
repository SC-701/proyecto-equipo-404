using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Ventas
{
    public class AgregarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        [BindProperty]
        public VentaRequest Venta { get; set; } = new();

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        public void OnGet()
        {
        }
    }
}
