using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Reportes
{
    public class ConteoPorTipoPlatillo
    {
        public Guid TipoId { get; set; }
        public string TipoNombre { get; set; }
        public int cantidadPlatillos { get; set; }
    }
}
