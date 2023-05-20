using MultiMarcasAPP.ViewModels.ProveedoresViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ProveedoresViewCommands
{
    public class CrearCommand : CommandBase
    {
        private ProveedoresViewModel _ViewModel;

        public CrearCommand(ProveedoresViewModel viewModel)
        {
            _ViewModel = viewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return (Models.CurrentEmploye.PrivilegiosTrabajador.CrearProveedor) &&
                    base.CanExecute(parameter);
        }
        public override void Execute(object? parameter)
        {
            _ViewModel.MainButtonNavigationBarVisibility = false;
            _ViewModel.SecundaryButtonNavigationBarVisiblity = true;
            _ViewModel.InformacionViewModel.TxtBoxReadOnly = false;
            _ViewModel.DataGridEneable = false;
            _ViewModel.InformacionViewModel.Proveedor = new();
            _ViewModel.SelectedProveedor = new();
        }
    }
}
