using MultiMarcasAPP.ViewModels.ProveedoresViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ProveedoresViewCommands
{
    public class AgregarBancoCommand : CommandBase
    {
        private ProveedoresViewModel _ViewModel;
        public AgregarBancoCommand(ProveedoresViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            _ViewModel.InformacionBancoViewModel.Banco = new BancoViewModel();
            _ViewModel.InformacionBancoViewModel.InformacionBancoVisibility = System.Windows.Visibility.Visible;
        }
    }
}
