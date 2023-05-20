using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.InyeccionCapitalViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.InyeccionCapitalViewCommands
{
    public class GuardarCommand : CommandBase
    {
        private IngresoYVerViewModel _viewModel;
        public GuardarCommand(IngresoYVerViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(IngresoYVerViewModel.Monto) || e.PropertyName == nameof(IngresoYVerViewModel.Fecha))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_viewModel.Monto > 0) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            InyeccionCapitalDAO.Insert(_viewModel.Monto, _viewModel.Fecha);
            _viewModel.Monto = 0;
            _viewModel.Fecha = DateTime.Now;
            _viewModel.cargarInyecciones();
        }
    }
}
