using Agri.Business;
using Agri.Connect;
using Agri.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AgriMarketSoft
{
    /// <summary>
    /// Lógica de interacción para TiendaList.xaml
    /// </summary>
    public partial class TiendaList : Page
    {

        Business b = new();
        ConnectSQL ctql = new();
        List<string> CategoriaFiltro = new();

        public TiendaList()
        {
            InitializeComponent();
            cbFiltros.ItemsSource = new List<string>() { "Ordenar de la A a la Z", "Ordenar de la Z a la A","De menor a mayor precio", "De mayor a menor precio" };
            LoadListViews();
            
        }

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

        public static BitmapImage ToImage(byte[] array)
        {
            var ms = new System.IO.MemoryStream(array);
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnDemand;
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
        private void LoadListViews()
        {

            //lBoxProductos.ItemsSource = lBoxProductos.ItemsSource.Cast<Producto>().ToList().OrderBy(x => x.NombreProducto).ToList();

            List<Categoria> categorias = new();
            categorias = b.GetCategoriaList();
            List<Region> regions = new();
            regions = b.GetRegionesList();
            List<Proveedor> proveedors = new();
            proveedors = b.GetProveedoresList();

            categorias.Insert(0, new Categoria() { NombreCategoria = "Todos" });
            regions.Insert(0, new Region() { NombreRegion = "Todos" });
            proveedors.Insert(0, new Proveedor() { Nombre = "Todos" });
            //Categoria ListView
            cbCategoria.ItemsSource = categorias.Select(x => x.NombreCategoria);
            //Regiones ListView
            cbRegiones.ItemsSource = regions.Select(x => x.NombreRegion);
            //Proveedores ListView
            cbProveedores.ItemsSource = proveedors.Select(x => x.Nombre);

            cbCategoria.SelectedIndex = 0;
            cbRegiones.SelectedIndex = 0;
            cbProveedores.SelectedIndex = 0;

            lBoxProductos.ItemsSource = GetProductoList();



        }


        private void cbFiltros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbFiltros.SelectedIndex)
            {
                case 0:
                    try
                    {
                        lBoxProductos.ItemsSource = GetProductoList();

                        lBoxProductos.ItemsSource = lBoxProductos.ItemsSource.Cast<Producto>().ToList().OrderBy(x => x.NombreProducto).ToList();

                    }
                    catch
                    {

                    }

                    break;
                case 1:
                    lBoxProductos.ItemsSource = GetProductoList();

                    lBoxProductos.ItemsSource = lBoxProductos.ItemsSource.Cast<Producto>().ToList().OrderBy(x => x.NombreProducto).Reverse().ToList();   
                    break;
                case 2:
                    lBoxProductos.ItemsSource = GetProductoList();

                    lBoxProductos.ItemsSource = lBoxProductos.ItemsSource.Cast<Producto>().ToList().OrderBy(x => x.Precio).ToList();
                    break;
                case 3:
                    lBoxProductos.ItemsSource = GetProductoList();

                    lBoxProductos.ItemsSource = lBoxProductos.ItemsSource.Cast<Producto>().ToList().OrderBy(x => x.Precio).Reverse().ToList();
                    break;

                default:
                    break;
            }
        }

        private void cbCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCategoria.Text != "Todos")
            {
                try
                {
                    lBoxProductos.ItemsSource = GetProductoList();
                    lBoxProductos.ItemsSource = lBoxProductos.ItemsSource.Cast<Producto>().ToList().Where(x => x.Categoria == cbCategoria.Text);

                }
                catch
                {

                    
                }

            }
            else
            {
                lBoxProductos.ItemsSource = GetProductoList();

            }
        }

        private void cbProveedores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            //lBoxProductos.ItemsSource = lBoxProductos.ItemsSource.Cast<Producto>().ToList().Where(x => x.Categoria == cbCategoria.Text);

        }

        private void cbRegiones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //lBoxProductos.ItemsSource = lBoxProductos.ItemsSource.Cast<Producto>().ToList().Where(x => x.Categoria == cbCategoria.Text);

        }

        private void ProductoCard_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var win = f
        }
    }
}
