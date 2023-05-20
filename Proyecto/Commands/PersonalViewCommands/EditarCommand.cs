using MultiMarcasAPP.ViewModels;
using MultiMarcasAPP.ViewModels.PersonalViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.PersonalViewCommands
{
    public class EditarCommand : CommandBase
    {
        private PersonalViewModel _ViewModel;

        public EditarCommand(PersonalViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(PersonalViewModel.SelectedTrabajador))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (Models.CurrentEmploye.PrivilegiosTrabajador.EditarPersonal) &&
                    (_ViewModel.SelectedTrabajador != null && _ViewModel.SelectedTrabajador.trabajador.Id != 0) && 
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _ViewModel.DataGridEneable = false;
            _ViewModel.MainButtonNavigationBarVisibility = false;
            _ViewModel.SecundaryButtonNavigationBarVisiblity = true;
            _ViewModel.PedirInformacionPersonalViewModel.PasswordVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.PedirInformacionPersonalViewModel.ControlVisibility = System.Windows.Visibility.Visible;
            _ViewModel.MostrarInformacionPersonalViewModel.ControlVisibility = System.Windows.Visibility.Collapsed;
            
        }
    }
}
