using Agri.Business;
using Agri.Core;
using System.Windows;
using System.Windows.Controls;
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
        public TiendaView(Usuario userlog)
        {
            userType = userlog.IdTipo;
            u = userlog;
            InitializeComponent();
            ModoUsuario();
            if (lbTipo.Content.ToString().Contains("Proveedor"))
            {
                btnAgregarProducto.Visibility = Visibility.Visible;

            }
        }

        
        private void ModoUsuario()
        {
            

            switch (userType)
            {
                case 1:
                    TiendaPageFrame.Navigate(new TiendaList());

                    lbTipo.Content = $"Cliente: {u.nombres}";
                    btnAgregarProducto.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    TiendaPageFrame.Navigate(new TiendaList());
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


        private void CerrarSesion_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de querer cerrar sesión?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                NavigationService.GoBack();
            }
        }
    }
}
