using MultiMarcasAPP.ViewModels.GastosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.GastosViewCommands.NuevoTipoCommands
{
    public class CancelarCommand : CommandBase
    {
        private GastosNuevoTipoViewModel _viewModel;
        public CancelarCommand(GastosNuevoTipoViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GastosNuevoTipoViewModel.Nombre) || e.PropertyName == nameof(GastosNuevoTipoViewModel.Deducible))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (!string.IsNullOrEmpty(_viewModel.Nombre)) ||
                    (_viewModel.Deducible) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _viewModel.Nombre = string.Empty;
            _viewModel.Deducible = false;
        }
    }
}
