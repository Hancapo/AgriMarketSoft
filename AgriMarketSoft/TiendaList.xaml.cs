using Agri.Business;
using Agri.Connect;
using Agri.Core;
using MaterialDesignThemes.Wpf;
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
using Region = Agri.Core.Region;

namespace AgriMarketSoft
{
    /// <summary>
    /// Lógica de interacción para TiendaList.xaml
    /// </summary>
    public partial class TiendaList : Page
    {
        OdioNCapas onc = new();
        Business b = new();
        ConnectSQL ctql = new();
        List<string> CategoriaFiltro = new();

        public TiendaList()
        {
            InitializeComponent();
            cbFiltros.ItemsSource = new List<string>() { "Ordenar de la A a la Z", "Ordenar de la Z a la A","De menor a mayor precio", "De mayor a menor precio" };
            LoadListViews();
            
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

            lBoxProductos.ItemsSource = onc.GetProductoList();



        }


        private void cbFiltros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbFiltros.SelectedIndex)
            {
                case 0:
                    try
                    {
                        lBoxProductos.ItemsSource = onc.GetProductoList();

                        lBoxProductos.ItemsSource = lBoxProductos.ItemsSource.Cast<Producto>().ToList().OrderBy(x => x.NombreProducto).ToList();

                    }
                    catch
                    {

                    }

                    break;
                case 1:
                    lBoxProductos.ItemsSource = onc.GetProductoList();

                    lBoxProductos.ItemsSource = lBoxProductos.ItemsSource.Cast<Producto>().ToList().OrderBy(x => x.NombreProducto).Reverse().ToList();   
                    break;
                case 2:
                    lBoxProductos.ItemsSource = onc.GetProductoList();

                    lBoxProductos.ItemsSource = lBoxProductos.ItemsSource.Cast<Producto>().ToList().OrderBy(x => x.Precio).ToList();
                    break;
                case 3:
                    lBoxProductos.ItemsSource = onc.GetProductoList();

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
                    lBoxProductos.ItemsSource = onc.GetProductoList();
                    lBoxProductos.ItemsSource = lBoxProductos.ItemsSource.Cast<Producto>().ToList().Where(x => x.Categoria == cbCategoria.Text);

                }
                catch
                {

                    
                }

            }
            else
            {
                lBoxProductos.ItemsSource = onc.GetProductoList();

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
            Producto cardo = ((Card)sender).DataContext as Producto;
            NavigationService.Navigate(new ProductoView(cardo));
        }
    }
}
