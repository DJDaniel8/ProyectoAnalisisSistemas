using MultiMarcasAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.ViewModels.StocksViewModels
{
    public class InformacionStockViewModel : ViewModelBase
    {
        public InformacionStockViewModel(StockViewModel stockViewModel)
        {
            _StockViewModel = stockViewModel; OnPropertyChanged(nameof(StockViewModel));
        }

        public InformacionStockViewModel()
        {
            _StockViewModel = new(new(),new());
        }

        private StockViewModel _StockViewModel;
        public StockViewModel StockViewModel
        {
            get
            {
                return _StockViewModel;
            }
            set
            {
                _StockViewModel = value;
                OnPropertyChanged(nameof(StockViewModel));
            }
        }

        private bool _TxtBoxReadOnly = true;
        public bool TxtBoxReadOnly
        {
            get
            {
                return _TxtBoxReadOnly;
            }
            set
            {
                _TxtBoxReadOnly = value;
                OnPropertyChanged(nameof(TxtBoxReadOnly));
            }
        }

        private Visibility _PriceVisibility = Visibility.Collapsed;
        public Visibility PriceVisibility
        {
            get
            {
                return _PriceVisibility;
            }
            set
            {
                _PriceVisibility = value;
                OnPropertyChanged(nameof(PriceVisibility));
            }
        }

        private Visibility _CantidadVisibility = Visibility.Collapsed;
        public Visibility CantidadVisibility
        {
            get
            {
                return _CantidadVisibility;
            }
            set
            {
                _CantidadVisibility = value;
                OnPropertyChanged(nameof(CantidadVisibility));
            }
        }

        private int _Cantidad;
        public int Cantidad
        {
            get
            {
                return _Cantidad;
            }
            set
            {
                _Cantidad = value;
                OnPropertyChanged(nameof(Cantidad));
            }
        }
    }
}
