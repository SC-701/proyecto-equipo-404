using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface IEstadoController
    {
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener(Guid Id);
        Task<IActionResult> Agregar(EstadoRequest estado);
        Task<IActionResult> Editar(Guid Id, EstadoRequest estado);
        Task<IActionResult> Eliminar(Guid Id);
    }
}