using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface ITipoPlatilloController
    {
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener(Guid Id);
        Task<IActionResult> Agregar(TipoPlatilloRequest tipoPlatillo);
        Task<IActionResult> Editar(Guid Id, TipoPlatilloRequest tipoPlatillo);
        Task<IActionResult> Eliminar(Guid Id);
    }
}
