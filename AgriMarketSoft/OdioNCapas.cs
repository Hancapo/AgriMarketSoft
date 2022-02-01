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

            SqlCommand ProcProd = new("CrearProducto", ctql.SqlConnection);

            ProcProd.CommandType = CommandType.StoredProcedure;

            ProcProd.Parameters.AddWithValue("@IdPro", SqlDbType.Int).Value = producto.IdProducto;
            ProcProd.Parameters.AddWithValue("@NombrePro", SqlDbType.VarChar).Value = producto.NombreProducto;
            ProcProd.Parameters.AddWithValue("@StockPro", SqlDbType.Int).Value = producto.Stock;
            ProcProd.Parameters.AddWithValue("@IdCategoria", SqlDbType.Int).Value = producto.IdCategoria;
            ProcProd.Parameters.AddWithValue("@DecripPro", SqlDbType.VarChar).Value = producto.Descripcion;
            ProcProd.Parameters.AddWithValue("@MedidaPro", SqlDbType.VarChar).Value = producto.Medida;
            ProcProd.Parameters.AddWithValue("@PrecioPro", SqlDbType.Int).Value = producto.Precio;
            ProcProd.Parameters.AddWithValue("@RutProveedor", SqlDbType.VarChar).Value = producto.rutproveedor;

            if (producto.ImagenByte != null)
            {
                ProcProd.Parameters.AddWithValue("@FotoPro", SqlDbType.Image).Value = producto.ImagenByte;

            }
            else
            {
                ProcProd.Parameters.AddWithValue("@FotoPro", SqlDbType.Image).Value = null;

            }


            try
            {
                ProcProd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }





            string sqlcommand = ($"INSERT INTO PRODUCTO (idproducto, nombreproducto, stock, idcategoria, descripcion, medida, precio, rutproveedor) VALUES ({b.CalculateID("idproducto", "producto")},'{producto.NombreProducto}',{producto.Stock},{producto.IdCategoria},'{producto.Descripcion}','{producto.Medida}',{producto.Precio},'{producto.RutProveedor}')");
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





        public List<Producto> ListarProductosFromProveedor(string correo)
        {
            List<Producto> listaProducto = new();

            string sqlCommand = $"SELECT Producto.idproducto as idProducto, Producto.nombreproducto as nombreProducto, Producto.stock as stock, Categoria.nombre as nombreCategoria, Producto.descripcion as DescripcionProducto, Producto.medida as MedidaProducto,  Producto.precio as precio FROM Producto inner join Categoria on Producto.idcategoria = Categoria.idcategoria INNER JOIN Proveedor ON Producto.rutproveedor = Proveedor.rutproveedor  INNER JOIN Usuario ON Usuario.rutusuario = Proveedor.rutproveedor  where usuario.correo = '{correo}'";

            foreach (DataRow dr in ctql.SqltoDataTable(sqlCommand).Rows)
            {
                Producto p = new()
                {
                    IdProducto = Convert.ToInt32(dr["idProducto"]),
                    NombreProducto = dr["nombreProducto"].ToString(),
                    Stock = Convert.ToInt32(dr["stock"]),
                    NombreCategoria = dr["nombreCategoria"].ToString(),
                    Descripcion = dr["DescripcionProducto"].ToString(),
                    Medida = dr["MedidaProducto"].ToString(),
                    Precio = Convert.ToInt32(dr["precio"]),

                };

                listaProducto.Add(p);

            }

            return listaProducto;
        }



        public bool EliminarProducto(Producto pro)
        {
            SqlCommand cmd = new("EliminarProducto", ctql.SqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdProd", SqlDbType.Int).Value = pro.IdProducto;

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch 
            {

                return false;   
            }
        }
    }
}
