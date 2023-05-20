using MultiMarcasAPP.ViewModels.DepositosInternosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.UserDataTasks;

namespace MultiMarcasAPP.Commands.DepositosInternosCommands
{
    public class CrearCommand : CommandBase
    {

        private DepositosVerViewModel _viewModel;
        public CrearCommand(DepositosVerViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            _viewModel.MainButtonNavigationBarVisibility = false;
            _viewModel.SecundaryButtonNavigationBarVisiblity = true;
            _viewModel.IsEneable = true;
            _viewModel.IsReadOnly = false;
            _viewModel.SelectedDeposito = new();
        }
    }
}
