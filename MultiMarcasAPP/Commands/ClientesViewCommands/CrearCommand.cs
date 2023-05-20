using MultiMarcasAPP.ViewModels.ClientesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ClientesViewCommands
{
    public class CrearCommand : CommandBase
    {
        private ClientesViewModel _ViewModel;

        public CrearCommand(ClientesViewModel viewModel)
        {
            _ViewModel = viewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return (Models.CurrentEmploye.PrivilegiosTrabajador.CrearClientes) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            InformacionClienteViewModel vm = (InformacionClienteViewModel)_ViewModel.CurrentViewModel;
            vm.Cliente = new ClienteViewModel(new Models.Cliente());
            vm.TxtBoxReadOnly = false;
            _ViewModel.MainButtonsBarVisibility = false;
            _ViewModel.SecundaryButtonsBarVisibility = true;
            _ViewModel.DataGridEneable = false;
        }
    }
}
