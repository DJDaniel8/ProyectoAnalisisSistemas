using MultiMarcasAPP.ViewModels.StocksViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.StocksViewCommands
{
    public class EditarCommand : CommandBase
    {
        private StocksViewModel _ViewModel;
        public EditarCommand(StocksViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(StocksViewModel.SelectedStock))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (Models.CurrentEmploye.PrivilegiosTrabajador.EditarStock) &&
                    (_ViewModel.SelectedStock != null) &&
                    (_ViewModel.SelectedStock.Stock.Id > 0) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _ViewModel.InformacionStockViewModel.TxtBoxReadOnly = false;
            _ViewModel.SecundaryButtonNavigationBarVisiblity = true;
            _ViewModel.MainButtonNavigationBarVisibility = false;
            _ViewModel.DataGridEneable = false;
            _ViewModel.InformacionStockViewModel.PriceVisibility = System.Windows.Visibility.Visible;

        }
    }
}
