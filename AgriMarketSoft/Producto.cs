using Agri.Connect;
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
        private ConnectSQL cs = new();

        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Stock { get; set; }
        public string? Descripcion { get; set; }
        public string? Medida { get; set; }
        public int? Precio { get; set; }
        public BitmapImage? Imagen { get; set; }

        public string RutProveedor { get; set; }

        public string NombreProveedor => cs.RunSqlExecuteScalar($"SELECT nombreproveedor FROM PROVEEDOR where rutproveedor = '{RutProveedor}'").ToString();

        public string ContactoProveedor => cs.RunSqlExecuteScalar($"SELECT contacto FROM PROVEEDOR where rutproveedor = '{RutProveedor}'").ToString();
        public float LatitudP => Convert.ToSingle(cs.RunSqlExecuteScalar($"SELECT latitud FROM PROVEEDOR where rutproveedor = '{RutProveedor}'"));
        public float LongitudP => Convert.ToSingle(cs.RunSqlExecuteScalar($"SELECT longitud FROM PROVEEDOR where rutproveedor = '{RutProveedor}'"));
        public int IdCategoria { get; set; }

        public string Categoria => cs.RunSqlExecuteScalar($"SELECT Categoria.nombre FROM PRODUCTO INNER JOIN Categoria ON Producto.idcategoria = Categoria.idcategoria WHERE Producto.idcategoria = {IdCategoria}").ToString();

        public double? PrecioSinIva => (Convert.ToDouble(Precio) * 0.89);

    }
}
