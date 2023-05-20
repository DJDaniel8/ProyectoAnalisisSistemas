using MultiMarcasAPP.ViewModels.GastosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.GastosViewCommands.InsertarGastoCommands
{
    public class CancelarCommand : CommandBase
    {
        private GastosInsertarViewModel _viewModel;
        public CancelarCommand(GastosInsertarViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GastosInsertarViewModel.Monto) || e.PropertyName == nameof(GastosInsertarViewModel.Descripcion) ||
                e.PropertyName == nameof(GastosInsertarViewModel.NoDoc) )
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return 
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _viewModel.Monto = 0;
            _viewModel.Descripcion = String.Empty;
            _viewModel.Descripcion = String.Empty;
        }
    }
}
