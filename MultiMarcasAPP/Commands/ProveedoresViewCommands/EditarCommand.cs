using MultiMarcasAPP.ViewModels.ProveedoresViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ProveedoresViewCommands
{
    public class EditarCommand : CommandBase
    {
        private ProveedoresViewModel _ViewModel;
        public EditarCommand(ProveedoresViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ProveedoresViewModel.SelectedProveedor))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (Models.CurrentEmploye.PrivilegiosTrabajador.EditarProveedor) &&
                    (_ViewModel.SelectedProveedor != null) &&
                    (_ViewModel.SelectedProveedor.Proveedor.Id > 0 ) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _ViewModel.MainButtonNavigationBarVisibility = false;
            _ViewModel.SecundaryButtonNavigationBarVisiblity = true;
            _ViewModel.InformacionViewModel.TxtBoxReadOnly = false;
            _ViewModel.DataGridEneable = false;
            _ViewModel.InformacionViewModel.BancosButtosVisibility = System.Windows.Visibility.Visible;
            _ViewModel.GuardarCommand.CanExecute(null);
        }
    }
}
