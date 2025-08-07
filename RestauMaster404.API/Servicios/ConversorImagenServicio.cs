using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstracciones.Interfaces.Servicios;

namespace Servicios
{
    public class ConversorImagenServicio : IConversorImagenServicio
    {
        public byte[]? ConvertirBase64AByteArray(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
                return null;

            try
            {
                return Convert.FromBase64String(base64);
            }
            catch
            {
                return null;
            }
        }
    }
}
