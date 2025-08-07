using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Servicios
{
    public interface IConversorImagenServicio
    {
        byte[]? ConvertirBase64AByteArray(string base64);
    }
}
