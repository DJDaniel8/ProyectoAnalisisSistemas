using MultiMarcasAPP.ViewModels.ClientesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ClientesViewCommands
{
    public class CancelarCommand : CommandBase
    {
        private ClientesViewModel _ViewModel;
        public CancelarCommand(ClientesViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            InformacionClienteViewModel vm = (InformacionClienteViewModel)_ViewModel.CurrentViewModel;
            vm.Cliente = new ClienteViewModel(new Models.Cliente());
            vm.TxtBoxReadOnly = true;
            _ViewModel.MainButtonsBarVisibility = true;
            _ViewModel.SecundaryButtonsBarVisibility = false;
            _ViewModel.DataGridEneable = true;
        }
    }
}
