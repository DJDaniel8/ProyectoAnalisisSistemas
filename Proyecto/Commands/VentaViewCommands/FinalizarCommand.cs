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
    public class FinalizarCommand : CommandBase
    {
        private VentaViewModel _ViewModel;
        public FinalizarCommand(VentaViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel._Productos.CollectionChanged += _Productos_CollectionChanged;
        }

        private void _Productos_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return (_ViewModel._Productos.Count > 0) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            InformacionVentaView nuevaVenta = new();
            nuevaVenta.Owner = (Window)parameter;
            nuevaVenta.ShowDialog();
        }
    }
}
