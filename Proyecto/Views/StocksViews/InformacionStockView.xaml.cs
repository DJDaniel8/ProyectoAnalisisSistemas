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

namespace MultiMarcasAPP.Views.StocksViews
{
    /// <summary>
    /// Lógica de interacción para InformacionStockView.xaml
    /// </summary>
    public partial class InformacionStockView : UserControl
    {
        public InformacionStockView()
        {
            InitializeComponent();
        }

        private void TxtBoxCantidad_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtBoxCantidad.Select(TxtBoxCantidad.Text.Length, 0);
        }
    }
}
