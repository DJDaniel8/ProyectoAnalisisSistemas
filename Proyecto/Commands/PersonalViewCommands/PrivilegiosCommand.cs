using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.PersonalViewCommands
{
    public class PrivilegiosCommand : CommandBase
    {
        private PersonalViewModel _viewModel;
        public PrivilegiosCommand(PersonalViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(PersonalViewModel.SelectedTrabajador))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (Models.CurrentEmploye.PrivilegiosTrabajador.PrivilegiosPersonal) &&
                    (_viewModel.SelectedTrabajador != null) &&
                    (_viewModel.SelectedTrabajador.trabajador.Id > 0) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _viewModel.PrivilegiosPersonalViewModel.ControlVisibility = System.Windows.Visibility.Visible;
            _viewModel.PersonalInformacionVisibility = System.Windows.Visibility.Collapsed;
            _viewModel.PrivilegiosPersonalViewModel.Trabajador = _viewModel.SelectedTrabajador;
            _viewModel.PrivilegiosPersonalViewModel.Trabajador.Privilegios = TrabajadorDAO.GetPrivilegios(_viewModel.SelectedTrabajador.trabajador.Id);
            _viewModel.PrivilegiosPersonalViewModel.CargarPrivilegios();
        }
    }
}
