using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.StocksViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.StocksViewCommands
{
    public class GuardarCommand : CommandBase
    {
        private StocksViewModel _ViewModel;
        public GuardarCommand(StocksViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.InformacionStockViewModel.PropertyChanged += InformacionStockViewModel_PropertyChanged;
        }

        private void InformacionStockViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(InformacionStockViewModel.StockViewModel))
            {
                _ViewModel.InformacionStockViewModel.StockViewModel.PropertyChanged += StockViewModel_PropertyChanged;
            }
        }

        private void StockViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(StockViewModel.PrecioMinimo) || e.PropertyName == nameof(StockViewModel.PrecioVenta))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_ViewModel.InformacionStockViewModel.StockViewModel.PrecioMinimo > _ViewModel.InformacionStockViewModel.StockViewModel.Stock.PrecioCompra) &&
                    (_ViewModel.InformacionStockViewModel.StockViewModel.PrecioVenta >= _ViewModel.InformacionStockViewModel.StockViewModel.PrecioMinimo) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            StockDAO.Update(_ViewModel.InformacionStockViewModel.StockViewModel);

            _ViewModel.MainButtonNavigationBarVisibility = true;
            _ViewModel.SecundaryButtonNavigationBarVisiblity = false;
            _ViewModel.DataGridEneable = true;
            _ViewModel.InformacionStockViewModel.PriceVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.InformacionStockViewModel.TxtBoxReadOnly = true;
            _ViewModel.CargarStocks();
        }
    }
}
