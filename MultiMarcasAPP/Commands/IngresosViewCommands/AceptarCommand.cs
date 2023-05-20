using MultiMarcasAPP.ViewModels.IngresosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.IngresosViewCommands
{
    public class AceptarCommand : CommandBase
    {
        IngresoProductoDerivadosViewModel ViewModel;

        public AceptarCommand(IngresoProductoDerivadosViewModel viewModel)
        {
            ViewModel = viewModel;
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Producto))
            {
                ViewModel.Producto.PropertyChanged += ProductoBase_PropertyChanged;
            }
        }

        private void ProductoBase_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Producto.PrecioVenta) || e.PropertyName == nameof(ViewModel.Producto.PrecioVentaMinimo))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (ViewModel.Producto?.PrecioVentaMinimo > 0) &&
                   (ViewModel.Producto.PrecioVenta > 0) &&
                   (ViewModel.Producto.PrecioVenta >= ViewModel.Producto.PrecioVentaMinimo) &&
                   base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            if (parameter != null) SystemCommands.CloseWindow((Window)parameter);
        }
    }
}
