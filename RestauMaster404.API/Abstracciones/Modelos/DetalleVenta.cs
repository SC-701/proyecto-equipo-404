using System;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class DetalleVentaBase
    {
        [Required(ErrorMessage = "La propiedad cantidad es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "La propiedad precio unitario es requerida")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor a cero")]
        public double PrecioUnitario { get; set; }

        [Required(ErrorMessage = "La propiedad subtotal es requerida")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El subtotal debe ser mayor a cero")]
        public double SubTotal { get; set; }
    }

    public class DetalleVentaRequest : DetalleVentaBase
    {
        [Required(ErrorMessage = "La propiedad IdVenta es requerida")]
        public Guid IdVenta { get; set; }

        [Required(ErrorMessage = "La propiedad IdPlatillo es requerida")]
        public Guid IdPlatillo { get; set; }
    }

    public class DetalleVentaResponse : DetalleVentaBase
    {
        public Guid Id { get; set; }
        public string NombrePlatillo { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
    }

    public class DetalleVentaDetalle : DetalleVentaResponse
    {
        public Guid IdPlatillo { get; set; }
        public Guid IdVenta { get; set; }
    }
}
