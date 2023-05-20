using MultiMarcasAPP.ViewModels.PagosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.PagosCommands.NuevoTipoCommands
{
    public class CancelarCommand : CommandBase
    {
        private PagosNuevoTipoViewModel _viewModel;
        public CancelarCommand(PagosNuevoTipoViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(PagosNuevoTipoViewModel.Nombre) || e.PropertyName == nameof(PagosNuevoTipoViewModel.SaleDeCaja))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (!string.IsNullOrEmpty(_viewModel.Nombre)) ||
                    (_viewModel.SaleDeCaja) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _viewModel.Nombre = string.Empty;
            _viewModel.SaleDeCaja = false;
        }
    }
}
