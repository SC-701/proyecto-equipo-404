using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface IVentaController
    {
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener(Guid Id);
        Task<IActionResult> Agregar(VentaRequest venta);
        Task<IActionResult> Editar(Guid Id, VentaRequest venta);
        Task<IActionResult> Eliminar(Guid Id);
    }
}