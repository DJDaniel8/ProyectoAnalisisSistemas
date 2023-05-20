using MultiMarcasAPP.ViewModels.IngresosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.IngresosViewCommands
{
    public class EnviarCommand : CommandBase
    {
        private BuscarProductoIngresoViewModel _ViewModel;
        public EnviarCommand(BuscarProductoIngresoViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(BuscarProductoIngresoViewModel.SelectedProducto))
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
            IngresosViewModel? ingresosViewModel = ((Window)parameter).Owner.DataContext as IngresosViewModel;
            if(ingresosViewModel != null)
            {
                ingresosViewModel.NuevoIngresoViewModel.AgregarProductoALista(_ViewModel.SelectedProducto.Producto);
            }
        }
    }
}
