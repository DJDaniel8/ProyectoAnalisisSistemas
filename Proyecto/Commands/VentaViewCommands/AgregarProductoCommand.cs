using MultiMarcasAPP.ViewModels.VentaViewModels;
using MultiMarcasAPP.Views.VentasViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.VentaViewCommands
{
    public class AgregarProductoCommand : CommandBase
    {
        private VentaViewModel _ViewModel;
        public AgregarProductoCommand(VentaViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            BuscarProductoVentaView nuevaVentana = new();
            nuevaVentana.Owner = (Window)parameter;
            BuscarProductoVentaViewModel dt = (BuscarProductoVentaViewModel)nuevaVentana.DataContext;
            dt.InformacionStockViewModel.CantidadVisibility = Visibility.Visible;
            nuevaVentana.ShowDialog();
        }
    }
}
