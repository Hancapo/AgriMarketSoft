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
    /// Lógica de interacción para Perfil.xaml
    /// </summary>
    public partial class Perfil : Page
    {
        private Usuario user;

        private OdioNCapas odio = new();
        public Perfil(Usuario usuario)
        {
            InitializeComponent();
            user = usuario;
            lvProductosProveedor.ItemsSource = odio.ListarProductosFromProveedor(usuario.Correo);
        }

        private void lvProductosProveedor_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
