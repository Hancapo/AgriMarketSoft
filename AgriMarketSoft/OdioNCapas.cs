using Agri.Business;
using Agri.Connect;
using Agri.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AgriMarketSoft
{
    public class OdioNCapas
    {
        private ConnectSQL ctql = new();
        Business b = new();
        public List<Producto> GetProductoList()
        {
            List<Producto> listaProducto = new();

            string sqlcommand = "select * from producto";

            foreach (DataRow dr in ctql.SqltoDataTable(sqlcommand).Rows)
            {
                Producto producto = new();
                producto.IdProducto = Convert.ToInt32(dr["idproducto"]);
                producto.NombreProducto = dr["nombreproducto"].ToString();
                producto.Stock = Convert.ToInt32(dr["stock"]);
                producto.IdCategoria = Convert.ToInt32(dr["idcategoria"]);
                producto.RutProveedor = dr["rutproveedor"].ToString();
                try
                {
                    producto.Imagen = ToImage((byte[])dr["foto"]);

                }
                catch
                {
                    producto.Imagen = new BitmapImage(new Uri("https://www.eglsf.info/wp-content/uploads/image-missing.png"));
                }

                try
                {
                    producto.Precio = Convert.ToInt32(dr["precio"]);

                }
                catch
                {
                    producto.Precio = 0;

                }

                try
                {
                    producto.Descripcion = dr["descripcion"].ToString();

                }
                catch
                {
                    producto.Descripcion = "No hay descripción disponible";

                }

                try
                {
                    producto.Medida = dr["medida"].ToString();


                }
                catch
                {
                    producto.Medida = "Sin datos";

                }




                listaProducto.Add(producto);
            }

            return listaProducto;
        }

        public bool CreateProducto(Producto producto)
        {
            string sqlcommand = ($"INSERT INTO PRODUCTO VALUES ({b.CalculateID("idproducto", "producto")},{producto.NombreProducto},{producto.Stock},{producto.IdCategoria},{producto.Descripcion},{producto.Medida},{producto.Precio},{producto.Imagen},{producto.RutProveedor})");
            try
            {
                ctql.RunSqlNonQuery(sqlcommand);
                return true;
            }
            catch
            {
                return false;
                
            }
        }


        public static BitmapImage ToImage(byte[] array)
        {
            var ms = new MemoryStream(array);
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnDemand;
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}
