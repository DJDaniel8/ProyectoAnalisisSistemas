using MultiMarcasAPP.ViewModels;
using MultiMarcasAPP.ViewModels.PersonalViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.PersonalViewCommands
{
    public class CrearCommand : CommandBase
    {
        private PersonalViewModel _viewModel;
        public CrearCommand(PersonalViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return (Models.CurrentEmploye.PrivilegiosTrabajador.CrearPersonal) &&
                    base.CanExecute(parameter);
        }
        public override void Execute(object? parameter)
        {
            _viewModel.PedirInformacionPersonalViewModel.Trabajador = new(new());
            _viewModel.PedirInformacionPersonalViewModel.ControlVisibility = System.Windows.Visibility.Visible;
            _viewModel.MostrarInformacionPersonalViewModel.ControlVisibility = System.Windows.Visibility.Collapsed;
            _viewModel.DataGridEneable = false;
            _viewModel.MainButtonNavigationBarVisibility = false;
            _viewModel.SecundaryButtonNavigationBarVisiblity = true;
        }
    }
}
