using Agri.Business;
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
    /// Lógica de interacción para RegistroView.xaml
    /// </summary>
    public partial class RegistroView : Page
    {
        private Business Negocio = new();
        public RegistroView()
        {
            InitializeComponent();
            cbTipoUsuario.ItemsSource = new List<string>() { "Cliente", "Proveedor" }; 
            
        }

        private void btnVolverLogin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidacionRegistroUsuario())
            {
                Usuario usuario = new()
                {
                    Correo = txtEmail.Text,
                    RutUsuario = txtRut.Text,
                    Nombres = txtNombres.Text,
                    Apellidos = txtApellidos.Text,
                    IdTipoUsuario = Convert.ToInt32(cbTipoUsuario.SelectedIndex + 1),
                    Contrasena = txtPassword.Password

             
                };

                if (Negocio.CrearUsuario(usuario))
                {
                    System.Windows.MessageBox.Show("Se registró el usuario correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                else
                {
                    System.Windows.MessageBox.Show("No se ha registrado el usuario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            else
            {
                System.Windows.MessageBox.Show("Por favor, rellene todos los campos", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        //public void LimpiarCamposDeUsuario()
        //{
        //    txtEmail.Text = string.Empty;
        //    txtRut.Text = string.Empty;
        //    txtNombres.Text = string.Empty;
        //    txtApellidos.Text = string.Empty;
        //    cbTipoUsuario.SelectedIndex = 0;
        //    txtPassword.Password = string.Empty;

        //}

        public bool ValidacionRegistroUsuario()
        {
            int valdCount = 0;

            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                valdCount++;
            }

            if (!string.IsNullOrEmpty(txtRut.Text))
            {
                valdCount++;
            }

            if (!string.IsNullOrEmpty(txtNombres.Text))
            {
                valdCount++;
            }

            if (!string.IsNullOrEmpty(txtApellidos.Text))
            {
                valdCount++;
            }



            if (cbTipoUsuario.SelectedItem != null)
            {
                valdCount++;
            }

            if (!string.IsNullOrEmpty(txtPassword.Password))
            {
                valdCount++;
            }

            if (checkBoxTerminos.IsChecked == true)
            {
                valdCount++;
            }
            else
            {
                System.Windows.MessageBox.Show("Debe aceptar los términos y condiciones para crear la cuenta", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if (valdCount == 7)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void txtRut_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<char> CharRut = txtRut.Text.ToCharArray().ToList();

            if (txtRut.Text.Length == 9)
            {
                CharRut.Insert(8, '-');
                txtRut.Text = new string(CharRut.ToArray());
            }



        }
    }
}
