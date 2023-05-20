using MultiMarcasAPP.ViewModels.DepositosInternosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.DepositosInternosCommands
{
    public class CancelarCommand : CommandBase
    {
        private DepositosVerViewModel _viewModel;
        public CancelarCommand(DepositosVerViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            _viewModel.MainButtonNavigationBarVisibility = true;
            _viewModel.SecundaryButtonNavigationBarVisiblity = false;
            _viewModel.IsEneable = false;
            _viewModel.IsReadOnly = true;
            _viewModel.SelectedDeposito = new();
        }
    }
}
