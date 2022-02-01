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
    /// Lógica de interacción para PantallaLogin.xaml
    /// </summary>
    public partial class PantallaLogin : Page
    {

        ConnectSQL csql = new();
        Business bb = new();
        int UserType;
        public PantallaLogin()
        {
            InitializeComponent();
        }

        private void BtnIngresar_Click(object sender, RoutedEventArgs e)
        {
            if (tbCorreo.Text == string.Empty)
            {
                MessageBox.Show("El correo no puede estar vacío, introduzca un correo e inténtelo nuevamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (pbPassword.Password == String.Empty)
            {
                MessageBox.Show("La contraseña no puede estar vacía, introduzca la contraseña e inténtelo nuevamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {


                var LoginT = bb.LoginProcess(tbCorreo.Text, pbPassword.Password).Item1;
                var UserT = bb.LoginProcess(tbCorreo.Text, pbPassword.Password).Item2;

                switch (LoginT)
                {
                    case 0:
                        MessageBox.Show("La cuenta no existe.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case 2:
                        MessageBox.Show("La contraseña es incorrecta.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case 3:
                        Usuario u = new() { Correo = tbCorreo.Text };
                        NavigationService.Navigate(new TiendaView(u));
                        break;
                    default:
                        break;
                }
            }
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

        private void pbPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }

        private void tbCorreo_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }
    }
}
