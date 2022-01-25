using Agri.Business;
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
    /// Lógica de interacción para TiendaView.xaml
    /// </summary>
    public partial class TiendaView : Page
    {
        Business b = new();
        int userType = -1;
        public TiendaView(int UserType)
        {
            userType = UserType;
            InitializeComponent();
            ModoUsuario();
        }

        
        private void ModoUsuario()
        {
            

            switch (userType)
            {
                case 1:
                    TiendaPageFrame.Navigate(new TiendaList());

                    lbTipo.Content = "Cliente";
                    btnAgregarProducto.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    TiendaPageFrame.Navigate(new TiendaList());

                    lbTipo.Content = "Proveedor";

                    break;
                default:
                    break;
            }
        }
    }
}
