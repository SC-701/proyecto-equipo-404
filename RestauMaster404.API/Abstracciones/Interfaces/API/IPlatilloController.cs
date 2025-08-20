using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface IPlatilloController
    {
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener(Guid Id);
        Task<IActionResult> Agregar(PlatilloRequest platillo);
        Task<IActionResult> Editar(Guid Id, PlatilloRequest platillo);
        Task<IActionResult> Eliminar(Guid Id);
    }
}