using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agri.Core
{
    public class Proveedor
    {
        public string RutProveedor { get; set; }
        public string Nombre { get; set; }
        public string Contacto { get; set; }
        public float Longitud { get; set; }
        public float Latitud { get; set; }


        public bool Seleccionado { get; set; }

    }
}
