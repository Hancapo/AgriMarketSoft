using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Agri.Core
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Stock { get; set; }
        public string? Descripcion { get; set; }
        public string? Medida { get; set; }
        public int? Precio { get; set; }
        public BitmapImage? Imagen { get; set; }


        public double PrecioSinIva => (Convert.ToDouble(Precio) * 0.89);

    }
}
