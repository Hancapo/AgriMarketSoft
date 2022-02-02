using Agri.Connect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using Path = System.IO.Path;

namespace AgriMarketSoft
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class VentanaPrincipal : Window
    {

        ConnectSQL csql = new();
        public VentanaPrincipal()
        {
            InitializeComponent();
            IniciarApp();
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

        }

        private void IniciarApp()
        {
            fMain.Navigate(new PantallaLogin());
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void spTab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) DragMove();
        }

        private void MainAmigo_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (fMain.Content.GetType().Name != "PantallaLogin" )
            {
                string correopath = Path.Combine(Path.GetTempPath(), "3SbFHNhAg68dZFOIdPUz.tmp");
                string correo = File.ReadAllLines(correopath)[0];
                csql.RunSqlNonQuery($"UPDATE Usuario SET sesion = 0 WHERE correo = '{correo}'");
                File.Delete(correopath);
            }
        }
    }
}
