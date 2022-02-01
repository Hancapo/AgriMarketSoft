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
    /// Lógica de interacción para AddProductNew.xaml
    /// </summary>
    public partial class AddProductNew : Page
    {
        OdioNCapas onc = new();
        ConnectSQL ss = new();
        Business b = new();
        string RutProv;
        string ImgFileName;
        public AddProductNew(string rutproveedor)
        {
            RutProv = rutproveedor;
            InitializeComponent();
            cbCategoria.ItemsSource = b.GetCategoriaList().Select(x => x.NombreCategoria).ToList();
            CbProveedor.ItemsSource = new List<string>() { ss.RunSqlExecuteScalar($"SELECT nombreproveedor from proveedor where rutproveedor = '{RutProv}'").ToString() };
            CbProveedor.SelectedIndex = 0;
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
                ImgFileName = ofd.FileName;
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
            p.RutProveedor = RutProv;
            try
            {
                p.Imagen = (BitmapImage?)ImgUpload.Source;

            }
            catch
            {
                p.Imagen = null;
            }
            try
            {
                p.ImagenByte = b.ImagePathToBytes(ImgFileName);

            }
            catch
            {

                p.ImagenByte = null;
            }

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
