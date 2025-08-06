using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class PlatilloBase
    {
        [Required(ErrorMessage = "La propiedad nombre es requerida")]
        [StringLength(100, ErrorMessage = "La propiedad nombre debe tener como máximo 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La propiedad precio es requerida")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a cero")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "La propiedad stock es requerida")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a cero")]
        public int Stock { get; set; }
        [Display(Name = "Imagen")]
        [StringLength(300, ErrorMessage = "La URL de la imagen es muy larga")]
        public string? ImagenUrl { get; set; }

    }

    public class PlatilloRequest : PlatilloBase
    {
        [Required(ErrorMessage = "La propiedad IdTipoPlatillo es requerida")]
        public Guid IdTipoPlatillo { get; set; }

        [Required(ErrorMessage = "La propiedad IdEstado es requerida")]
        public Guid IdEstado { get; set; }
        public byte[] Imagen { get; set; }
    }

    public class PlatilloResponse : PlatilloBase
    {
        public Guid Id { get; set; }
        public string TipoPlatillo { get; set; }
        public string Estado { get; set; }
    }

    public class PlatilloDetalle : PlatilloResponse
    {
        public Guid IdTipoPlatillo { get; set; }
        public Guid IdEstado { get; set; }
    }
}
