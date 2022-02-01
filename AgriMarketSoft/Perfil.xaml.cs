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

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (lvProductosProveedor.SelectedItem != null)
            {
                var productoLista = (Producto)lvProductosProveedor.SelectedItem;

                Producto pro = new();
                pro.IdProducto = Convert.ToInt32(productoLista.IdProducto);

                if (System.Windows.MessageBox.Show("¿Esta seguro que quiere eliminar el producto :" + productoLista.NombreProducto + "?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (odio.EliminarProducto(pro))
                    {
                        System.Windows.MessageBox.Show("Se ha eliminado el producto");
                     
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("No se ha elimnado nada");
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Seleccione una fila para eliminar");
            }
        }


        
   



    }
}
