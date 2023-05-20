using MultiMarcasAPP.ViewModels.InformacionIngresosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.InformacionIngresosCommands.VerCommands
{
    public class InsertarPagoCommand : CommandBase
    {
        InformacionIngresosVerViewModel _viewModel;
        public InsertarPagoCommand(InformacionIngresosVerViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(InformacionIngresosVerViewModel.SelectedIIV))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_viewModel.SelectedIIV != null) &&
                    (_viewModel.SelectedIIV.Id> 0) &&
                    (!_viewModel.SelectedIIV.Pagado) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _viewModel.Visibility = System.Windows.Visibility.Collapsed;
            _viewModel.ViewModelPadre.InformacionIngresosInsertarPagosViewModel.Visibility = System.Windows.Visibility.Visible;
            _viewModel.ViewModelPadre.InformacionIngresosVerProductosViewModel.Visibility = System.Windows.Visibility.Collapsed;
            _viewModel.ViewModelPadre.InformacionIngresosInsertarPagosViewModel.Monto = _viewModel.SelectedIIV.TotalIngreso-_viewModel.SelectedIIV.TotalPagado;
        }
    }
}
