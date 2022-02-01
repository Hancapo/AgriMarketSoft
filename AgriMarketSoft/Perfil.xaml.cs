using Agri.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MessageBox = System.Windows.MessageBox;

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
            lvProductosProveedor.ItemsSource = odio.ListarProductosFromProveedor(user.Correo);
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

                if (MessageBox.Show($"¿Está seguro de querer eliminar el producto {productoLista.NombreProducto}?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (odio.EliminarProducto(pro))
                    {
                        MessageBox.Show($"Se ha eliminado el producto {productoLista.NombreProducto}", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        lvProductosProveedor.ItemsSource = odio.ListarProductosFromProveedor(user.Correo);


                    }
                    else
                    {
                        MessageBox.Show("No Se ha podido eliminar el producto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show($"Selecciona una fila para eliminar.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
