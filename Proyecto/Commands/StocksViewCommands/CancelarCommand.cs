using MultiMarcasAPP.ViewModels.StocksViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.StocksViewCommands
{
    public class CancelarCommand : CommandBase
    {
        private StocksViewModel _ViewModel;
        public CancelarCommand(StocksViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            _ViewModel.MainButtonNavigationBarVisibility = true;
            _ViewModel.SecundaryButtonNavigationBarVisiblity = false;
            _ViewModel.DataGridEneable = true;
            _ViewModel.InformacionStockViewModel.PriceVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.InformacionStockViewModel.TxtBoxReadOnly = true;
            _ViewModel.CargarStocks();
        }
    }
}
