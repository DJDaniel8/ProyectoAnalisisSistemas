using MultiMarcasAPP.ViewModels;
using MultiMarcasAPP.ViewModels.PersonalViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.PersonalViewCommands
{
    public class CancelarCommand : CommandBase
    {
        private PersonalViewModel _ViewModel;
        public CancelarCommand(PersonalViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            
            _ViewModel.DataGridEneable = true;
            _ViewModel.MainButtonNavigationBarVisibility = true;
            _ViewModel.SecundaryButtonNavigationBarVisiblity = false;
            _ViewModel.PedirInformacionPersonalViewModel.ControlVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.MostrarInformacionPersonalViewModel.ControlVisibility = System.Windows.Visibility.Visible;
            _ViewModel.PedirInformacionPersonalViewModel.PasswordVisibility = System.Windows.Visibility.Collapsed;
        }
    }
}
