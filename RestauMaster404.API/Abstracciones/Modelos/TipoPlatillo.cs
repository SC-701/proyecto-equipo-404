using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class TipoPlatilloBase
    {
        [Required(ErrorMessage = "El nombre del tipo de platillo es requerido")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 60 caracteres")]
        public string Nombre { get; set; }
    }

    public class TipoPlatilloRequest : TipoPlatilloBase
    {
        public Guid Id { get; set; }
    }

    public class TipoPlatilloResponse : TipoPlatilloBase
    {
        public Guid Id { get; set; }
    }
}
