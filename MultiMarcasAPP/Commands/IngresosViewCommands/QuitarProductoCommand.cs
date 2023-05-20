using MultiMarcasAPP.ViewModels.IngresosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.IngresosViewCommands
{
    public class QuitarProductoCommand : CommandBase
    {
        private NuevoIngresoViewModel _ViewModel;
        public QuitarProductoCommand(NuevoIngresoViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(NuevoIngresoViewModel.SelectedProductoIngreso))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_ViewModel.SelectedProductoIngreso != null && _ViewModel.SelectedProductoIngreso.Producto.Id > 0) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _ViewModel.QuitarProductoDeLista(_ViewModel.SelectedProductoIngreso);
        }
    }
}
