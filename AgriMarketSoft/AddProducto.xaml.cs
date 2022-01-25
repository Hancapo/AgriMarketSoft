using Agri.Business;
using Agri.Connect;
using Agri.Core;
using System;
using System.Collections.Generic;
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
using MessageBox = System.Windows.MessageBox;

namespace AgriMarketSoft
{
    /// <summary>
    /// Lógica de interacción para AddProducto.xaml
    /// </summary>
    public partial class AddProducto : Page
    {

        OdioNCapas onc = new();
        ConnectSQL ss = new();
        Business b = new();
        string Modo;

        public AddProducto(string modo)
        {
            Modo = modo;
            InitializeComponent();
            cbCategoria.ItemsSource = b.GetCategoriaList().Select(x=> x.NombreCategoria).ToList();
            CbProveedor.ItemsSource = ss.SqltoDataTable($"SELECT nombreproveedor from proveedor where idproveedor = {Modo}");
        }

        private void ImgUpload_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("XD");
        }

        private void btnBackTo_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ImgUpload_MouseDown(object sender, MouseButtonEventArgs e)
        {

            OpenFileDialog ofd = new();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ImgUpload.Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }



        private void btnInsertProducto_Click_1(object sender, RoutedEventArgs e)
        {
            Producto p = new();

            p.IdProducto = b.CalculateID("idproducto", "producto");
            p.NombreProducto = NombreProducto.Text;
            p.Stock = Convert.ToInt32(StockProducto.Text);
            p.IdCategoria = cbCategoria.SelectedIndex + 1;
            p.Descripcion = tbDecripcion.Text;
            p.Medida = Medida.Text;
            p.Precio = Convert.ToInt32(Precio.Text);
            p.Imagen = (BitmapImage?)ImgUpload.Source;
            p.RutProveedor = CbProveedor.Text;

            if (onc.CreateProducto(p))
            {
                MessageBox.Show("El producto ha sido agregado correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("El producto no ha podido ser ingresa", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }
    }
}
