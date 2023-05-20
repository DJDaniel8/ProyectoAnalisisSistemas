using MultiMarcasAPP.ViewModels.ProveedoresViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ProveedoresViewCommands
{
    public class CancelarCommand : CommandBase
    {
        private ProveedoresViewModel _ViewModel;
        public CancelarCommand(ProveedoresViewModel viewModel)
        {
            _ViewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _ViewModel.MainButtonNavigationBarVisibility = true;
            _ViewModel.SecundaryButtonNavigationBarVisiblity = false;
            _ViewModel.InformacionViewModel.TxtBoxReadOnly = true;
            _ViewModel.DataGridEneable = true;
            _ViewModel.InformacionViewModel.Proveedor = new();
            _ViewModel.InformacionBancoViewModel.InformacionBancoVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.InformacionViewModel.BancosButtosVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.CargarProveedores();
        }
    }
}
