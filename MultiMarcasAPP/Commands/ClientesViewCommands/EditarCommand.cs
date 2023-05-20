using MultiMarcasAPP.ViewModels.ClientesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ClientesViewCommands
{
    public class EditarCommand : CommandBase
    {
        private ClientesViewModel _ViewModel;
        public EditarCommand(ClientesViewModel ViewModel)
        {
            _ViewModel = ViewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ClientesViewModel.SelectedCliente))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (Models.CurrentEmploye.PrivilegiosTrabajador.EditarClientes) &&
                    (_ViewModel.SelectedCliente != null) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            InformacionClienteViewModel vm = (InformacionClienteViewModel)_ViewModel.CurrentViewModel;
            vm.TxtBoxReadOnly = false;
            _ViewModel.MainButtonsBarVisibility = false;
            _ViewModel.SecundaryButtonsBarVisibility = true;
            _ViewModel.DataGridEneable = false;
        }
    }
}
