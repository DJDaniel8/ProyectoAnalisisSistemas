using MultiMarcasAPP.ViewModels.InformacionIngresosViewModels;
using MultiMarcasAPP.Views.InformacionIngresosView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.InformacionIngresosCommands.VerCommands
{
    public class VerPagosCommand : CommandBase
    {
        private InformacionIngresosVerViewModel _viewModel;
        public VerPagosCommand(InformacionIngresosVerViewModel viewModel)
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
                    (_viewModel.SelectedIIV.Id > 0) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _viewModel.Visibility = System.Windows.Visibility.Hidden;
            _viewModel.ViewModelPadre.InformacionIngresosVerPagosViewModel.Visibility = System.Windows.Visibility.Visible;
            _viewModel.ViewModelPadre.InformacionIngresosVerPagosViewModel.CargarPagos();
        }
    }
}
