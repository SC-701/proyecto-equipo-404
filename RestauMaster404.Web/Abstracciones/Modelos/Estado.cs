using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class EstadoBase
    {
        [Required(ErrorMessage = "El nombre del estado es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres")]
        public string Nombre { get; set; }
    }

    public class EstadoRequest : EstadoBase
    {
        public Guid Id { get; set; }
    }

    public class EstadoResponse : EstadoBase
    {
        public Guid Id { get; set; }
    }
}
