﻿using Agri.Business;
using Agri.Connect;
using Agri.Core;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using MessageBox = System.Windows.Forms.MessageBox;

namespace AgriMarketSoft
{
    /// <summary>
    /// Lógica de interacción para TiendaView.xaml
    /// </summary>
    public partial class TiendaView : Page
    {
        Business b = new Business();
        int userType = -1;
        Usuario u;
        OdioNCapas onc = new();
        ConnectSQL ctql = new();
        List<string> CategoriaFiltro = new();
        List<string> ProveedorFiltro = new();
        List<string> RegionFiltro = new();
        public TiendaView(Usuario userlog)
        {
            userType = userlog.idTipo;
            u = userlog;
            InitializeComponent();
            cbFiltros.ItemsSource = new List<string>() { "Ordenar de la A a la Z", "Ordenar de la Z a la A", "De menor a mayor precio", "De mayor a menor precio" };
            LoadListViews();
            ModoUsuario();
        }

        
        private void ModoUsuario()
        {


            switch (userType)
            {
                case 1:

                    lbTipo.Content = $"Cliente: {u.nombres}";
                    btnAgregarProducto.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    btnAgregarProducto.Visibility = Visibility.Visible;

                    lbTipo.Content = $"Proveedor: {u.nombres}";

                    break;
                default:
                    break;
            }
        }

        private void btnAgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            btnAgregarProducto.Visibility =Visibility.Hidden;
            NavigationService.Navigate(new AddProducto(u.rutusuario));
        }


        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de querer cerrar sesión?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                NavigationService.GoBack();
            }
        }

        private void LoadListViews()
        {

            lBoxProductos.ItemsSource = onc.GetProductoList();


            //Categoria ListView
            lvCategorias.ItemsSource = b.GetCategoriaList();
            //Regiones ListView
            lvRegiones.ItemsSource = b.GetRegionesList();
            //Proveedores ListView
            lvProveedores.ItemsSource = b.GetProveedoresList();



        }


        private void ProductoCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Producto cardo = ((Card)sender).DataContext as Producto;
            NavigationService.Navigate(new ProductoView(cardo));
        }

        private List<Producto> FiltrosCategoria(List<Producto> productos, List<string> CategoriasAFiltrar)
        {

            List<List<Producto>> Filtrados = new();
            List<Producto> ListaResultante = new();


            Filtrados.AddRange(CategoriasAFiltrar.Select(CategoriaNombre => productos.Where(x => x.Categoria == CategoriaNombre).ToList()));


            foreach (var ListaAnidada in Filtrados)
            {
                ListaResultante.AddRange(ListaAnidada);
            }


            return ListaResultante.DistinctBy(x => x.IdProducto).ToList();
        }

        private List<Producto> FiltrosProveedor(List<Producto> productos, List<string> ProveedoresAFiltrar)
        {
            List<List<Producto>> Filtrados = new();
            List<Producto> ListaResultante = new();


            Filtrados.AddRange(ProveedoresAFiltrar.Select(ProveedorNombre => productos.Where(x => x.NombreProveedor == ProveedorNombre).ToList()));


            foreach (var ListaAnidada in Filtrados)
            {
                ListaResultante.AddRange(ListaAnidada);
            }


            return ListaResultante.DistinctBy(x => x.IdProducto).ToList();
        }

        private List<Producto> FiltrosRegiones(List<Producto> productos, List<string> RegionesAFiltrar)
        {
            List<List<Producto>> Filtrados = new();
            List<Producto> ListaResultante = new();


            Filtrados.AddRange(RegionesAFiltrar.Select(RegionesNombre => productos.Where(x => x.ProductoRegion == RegionesNombre).ToList()));


            foreach (var ListaAnidada in Filtrados)
            {
                ListaResultante.AddRange(ListaAnidada);
            }


            return ListaResultante.DistinctBy(x => x.IdProducto).ToList();
        }

        private List<Producto> ReturnListaProductoFiltrada(List<Producto> productosCat, List<Producto> productosProv, List<Producto> productosReg)
        {
            List<Producto> TodosLosFiltros = new();
            TodosLosFiltros.AddRange(productosCat);
            TodosLosFiltros.AddRange(productosProv);
            TodosLosFiltros.AddRange(productosReg);
            return TodosLosFiltros.DistinctBy(x => x.IdProducto).ToList();
        }

        private void cbSelectCategory_Unchecked(object sender, RoutedEventArgs e)
        {

            CategoriaFiltro.Clear();


            //if (lvCategorias.Items.Cast<Categoria>().Any(x=> x.Seleccionado == true))
            //{




            //}
            //else
            //{
            //    //lBoxProductos.ItemsSource = onc.GetProductoList();

            //}
            foreach (var item in lvCategorias.Items)
            {
                var CategoriaLista = ((Categoria)item);

                if (CategoriaLista.Seleccionado)
                {
                    if (!CategoriaFiltro.Exists(x => x == CategoriaLista.NombreCategoria))
                    {
                        CategoriaFiltro.Add(CategoriaLista.NombreCategoria);

                    }
                }
            }

            CategoriaFiltro = CategoriaFiltro.DistinctBy(x => x).ToList();


            lBoxProductos.ItemsSource = ReturnListaProductoFiltrada(FiltrosCategoria(onc.GetProductoList(), CategoriaFiltro), FiltrosProveedor(onc.GetProductoList(), ProveedorFiltro), FiltrosRegiones(onc.GetProductoList(), RegionFiltro));



        }

        private void cbSelectCategory_Checked(object sender, RoutedEventArgs e)
        {
            CategoriaFiltro.Clear();


            //if (lvCategorias.Items.Cast<Categoria>().Any(x => x.Seleccionado))
            //{

            //    //lBoxProductos.ItemsSource = FiltrosCategoria(onc.GetProductoList(), CategoriaFiltro);
            //}
            //else
            //{
            //    //lBoxProductos.ItemsSource = onc.GetProductoList();

            //}

            foreach (var item in lvCategorias.Items)
            {
                var CategoriaLista = ((Categoria)item);

                if (CategoriaLista.Seleccionado)
                {
                    if (!CategoriaFiltro.Exists(x => x == CategoriaLista.NombreCategoria))
                    {
                        CategoriaFiltro.Add(CategoriaLista.NombreCategoria);

                    }
                }
            }

            CategoriaFiltro = CategoriaFiltro.DistinctBy(x => x).ToList();

            lBoxProductos.ItemsSource = ReturnListaProductoFiltrada(FiltrosCategoria(onc.GetProductoList(), CategoriaFiltro), FiltrosProveedor(onc.GetProductoList(), ProveedorFiltro), FiltrosRegiones(onc.GetProductoList(), RegionFiltro));



        }




        private void cbSelectProviders_Checked(object sender, RoutedEventArgs e)
        {
            ProveedorFiltro.Clear();

            //if (lvProveedores.Items.Cast<Proveedor>().Any(x => x.Seleccionado))
            //{


            //}
            //else
            //{

            //}

            foreach (var item in lvProveedores.Items)
            {
                var ProveedorLista = ((Proveedor)item);

                if (ProveedorLista.Seleccionado)
                {
                    if (!ProveedorFiltro.Exists(x => x == ProveedorLista.Nombre))
                    {
                        ProveedorFiltro.Add(ProveedorLista.Nombre);
                    }
                }
            }

            ProveedorFiltro = ProveedorFiltro.DistinctBy(x => x).ToList();

            lBoxProductos.ItemsSource = ReturnListaProductoFiltrada(FiltrosCategoria(onc.GetProductoList(), CategoriaFiltro), FiltrosProveedor(onc.GetProductoList(), ProveedorFiltro), FiltrosRegiones(onc.GetProductoList(), RegionFiltro));
        }

        private void cbSelectProviders_Unchecked(object sender, RoutedEventArgs e)
        {
            ProveedorFiltro.Clear();

            //if (lvProveedores.Items.Cast<Proveedor>().Any(x => x.Seleccionado))
            //{


            //}
            //else
            //{

            //}

            foreach (var item in lvProveedores.Items)
            {
                var ProveedorLista = ((Proveedor)item);

                if (ProveedorLista.Seleccionado)
                {
                    if (!ProveedorFiltro.Exists(x => x == ProveedorLista.Nombre))
                    {
                        ProveedorFiltro.Add(ProveedorLista.Nombre);
                    }
                }
            }

            ProveedorFiltro = ProveedorFiltro.DistinctBy(x => x).ToList();

            //Aquí va el ItemSource
            lBoxProductos.ItemsSource = ReturnListaProductoFiltrada(FiltrosCategoria(onc.GetProductoList(), CategoriaFiltro), FiltrosProveedor(onc.GetProductoList(), ProveedorFiltro), FiltrosRegiones(onc.GetProductoList(), RegionFiltro));
        }

        private void cbFiltros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
