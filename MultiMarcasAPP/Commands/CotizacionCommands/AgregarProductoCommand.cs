using MultiMarcasAPP.ViewModels.CotizacionViewModels;
using MultiMarcasAPP.ViewModels.VentaViewModels;
using MultiMarcasAPP.Views.CotizacionViews;
using MultiMarcasAPP.Views.VentasViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.CotizacionCommands
{
    public class AgregarProductoCommand : CommandBase
    {
        private CotizacionViewModel _ViewModel;
        public AgregarProductoCommand(CotizacionViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            BuscarProductoCotizacionView nuevaVentana = new();
            nuevaVentana.Owner = (Window)parameter;
            BuscarProductoCotizacionViewModel dt = (BuscarProductoCotizacionViewModel)nuevaVentana.DataContext;
            dt.InformacionStockViewModel.CantidadVisibility = Visibility.Visible;
            nuevaVentana.ShowDialog();
        }
    }
}
