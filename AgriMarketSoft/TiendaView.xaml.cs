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
                    lbTienda.Content = "Tienda: Cliente";
                    break;
                case 2:
                    lbTienda.Content = "Tienda: Proveedor";
                    break;
                default:
                    break;
            }
        }
    }
}
