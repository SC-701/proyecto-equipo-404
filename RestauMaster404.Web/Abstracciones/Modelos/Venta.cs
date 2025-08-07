using System;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class VentaBase
    {
        [Required(ErrorMessage = "La propiedad fecha es requerida")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La propiedad hora es requerida")]
        public TimeSpan Hora { get; set; }

        [Required(ErrorMessage = "La propiedad total es requerida")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor a cero")]
        public double Total { get; set; }
    }

    public class VentaRequest : VentaBase
    {

    }

    public class VentaResponse : VentaBase
    {
        public Guid Id { get; set; }
    }
}