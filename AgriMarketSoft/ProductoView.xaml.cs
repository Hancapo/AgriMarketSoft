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

namespace AgriMarketSoft
{
    /// <summary>
    /// Lógica de interacción para ProductoView.xaml
    /// </summary>
    public partial class ProductoView : Page
    {
        private Producto pcausa = new();
        public ProductoView(Producto p)
        { 
            pcausa = p;
            InitializeComponent();
            SetValuesFromProducto(pcausa);
        }


        public void SetValuesFromProducto(Producto p)
        {
            tbNombre.Content = p.NombreProducto;
            tbPrecio.Content = p.Precio;
            ImagenProductoView.Source = p.Imagen;
            MapaProveedor.Address = $"https://www.google.cl/maps/@{p.LatitudP},{p.LongitudP},16.5z";
            TBoxsDecripcion.Text = p.Descripcion;
            tBoxContacto.Text = p.ContactoProveedor;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
