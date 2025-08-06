using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface IDetalleVentaController
    {
        Task<IActionResult> ObtenerPorVenta(Guid IdVenta);
        Task<IActionResult> Obtener(Guid Id);
        Task<IActionResult> Agregar(DetalleVentaRequest detalle);
        Task<IActionResult> Editar(Guid Id, DetalleVentaRequest detalle);
        Task<IActionResult> Eliminar(Guid Id);
    }
}
