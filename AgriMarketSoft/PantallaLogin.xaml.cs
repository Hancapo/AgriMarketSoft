using Agri.Connect;
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
    /// Lógica de interacción para PantallaLogin.xaml
    /// </summary>
    public partial class PantallaLogin : Page
    {

        ConnectSQL csql = new();
        public PantallaLogin()
        {
            InitializeComponent();
        }

        private void BtnIngresar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            if (csql.CheckDatabase())
            {
                MessageBox.Show("La conexión ha sido exitosa.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No se ha podido conectar con la base de datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void pbPassword_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void tbCorreo_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
