using MultiMarcasAPP.ViewModels.CotizacionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.CotizacionCommands
{
    internal class PrecioMinimoCommand : CommandBase
    {
        private CotizacionViewModel _ViewModel;
        public PrecioMinimoCommand(CotizacionViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CotizacionViewModel.SelectedProducto))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_ViewModel.SelectedProducto != null) &&
                    (_ViewModel.SelectedProducto.Producto.Id > 0) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _ViewModel.SelectedProducto.PrecioVenta = _ViewModel.SelectedProducto.Stock.PrecioMinimo;
        }
    }
}
