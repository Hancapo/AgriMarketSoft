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

        static Business b = new();
        ConnectSQL ctql = new();
        List<Categoria> DropCategoria = b.GetCategoriaList();
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
                Producto producto = new()
                {
                    IdProducto = Convert.ToInt32(dr["idproducto"]),
                    NombreProducto = dr["nombreproducto"].ToString(),
                    Stock = Convert.ToInt32(dr["stock"]),
                    Imagen = ToImage((byte[])dr["foto"]),
                    Precio = Convert.ToInt32(dr["precio"]),
                    Descripcion = dr["descripcion"].ToString(),
                    Medida = dr["medida"].ToString()
                };

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
            lBoxProductos.ItemsSource = GetProductoList();

            lBoxProductos.ItemsSource = lBoxProductos.ItemsSource.Cast<Producto>().ToList().OrderBy(x => x.NombreProducto).ToList();    

            //Categoria ListView
            lvCategorias.ItemsSource = DropCategoria;
            //Regiones ListView
            lvRegiones.ItemsSource = b.GetRegionesList();
            //Proveedores ListView
            lvProveedores.ItemsSource = b.GetProveedoresList();

            SelectAllCategoria();
            SelectAllProveedores();
            SelectAllRegiones();


        }

        private void SelectAllProveedores()
        {
            lvProveedores.ItemsSource = lvProveedores.ItemsSource.Cast<Proveedor>().ToList().Select(c => { c.Seleccionado = true; return c; });
        }

        private void DeselectAllProveedores()
        {
            lvProveedores.ItemsSource = lvProveedores.ItemsSource.Cast<Proveedor>().ToList().Select(c => { c.Seleccionado = false; return c; });
        }

        private void SelectAllRegiones()
        {
            lvRegiones.ItemsSource = lvRegiones.ItemsSource.Cast<Region>().ToList().Select(c => { c.Seleccionado = true; return c; });
        }

        private void DeselectAllRegiones()
        {
            lvRegiones.ItemsSource = lvRegiones.ItemsSource.Cast<Region>().ToList().Select(c => { c.Seleccionado = false; return c; });
        }
        private void SelectAllCategoria()
        {
            lvCategorias.ItemsSource = lvCategorias.ItemsSource.Cast<Categoria>().ToList().Select(c => { c.Seleccionado = true; return c; });
        }

        private void DeselectAllCategoria()
        {
            lvCategorias.ItemsSource = lvCategorias.ItemsSource.Cast<Categoria>().ToList().Select(c => { c.Seleccionado = false; return c; });


        }

        private void cbSelectCategory_Unchecked(object sender, RoutedEventArgs e)
        {
            CategoriaFiltro.Remove((sender as CheckBox).Content.ToString());


        }

        private void cbSelectCategory_Checked(object sender, RoutedEventArgs e)
        {
            CategoriaFiltro.Add((sender as CheckBox).Content.ToString());

        }

        private void btnSelectAllCategory_Click(object sender, RoutedEventArgs e)
        {
            SelectAllCategoria();
        }

        private void btnDeselectAllCategory_Click(object sender, RoutedEventArgs e)
        {
            DeselectAllCategoria();
        }

        private void btnSelectAllProviders_Click(object sender, RoutedEventArgs e)
        {
            SelectAllProveedores();
        }

        private void btnSelectAllRegions_Click(object sender, RoutedEventArgs e)
        {
            SelectAllRegiones();
        }

        private void btnDeselectAllRegions_Click(object sender, RoutedEventArgs e)
        {
            DeselectAllRegiones();
        }

        private void btnDeselectAllProviders_Click(object sender, RoutedEventArgs e)
        {
            DeselectAllProveedores();
        }

        private void cbFiltros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbFiltros.SelectedIndex)
            {
                case 1:
                    lBoxProductos.ItemsSource = lBoxProductos.ItemsSource.Cast<Producto>().ToList().OrderBy(x => x.NombreProducto).Reverse().ToList();   
                    break;
                case 2:
                    lBoxProductos.ItemsSource = lBoxProductos.ItemsSource.Cast<Producto>().ToList().OrderBy(x => x.Precio).ToList();
                    break;
                case 3:
                    lBoxProductos.ItemsSource = lBoxProductos.ItemsSource.Cast<Producto>().ToList().OrderBy(x => x.Precio).Reverse().ToList();
                    break;

                default:
                    break;
            }
        }
    }
}
