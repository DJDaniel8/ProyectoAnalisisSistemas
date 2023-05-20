using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.StocksViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.ViewModels.IngresosViewModels
{
    public class TablaStockExistentesViewModel : ViewModelBase
    {
        public TablaStockExistentesViewModel(IngresosViewModel padre)
        {
            _stocks = new();
            this.padre = padre;
        }

        private IngresosViewModel padre; 

        private ObservableCollection<StockViewModel> _stocks;
        public IEnumerable<StockViewModel> Stocks => _stocks;

        private StockViewModel _SelectedStock = new(new(), new());
        public StockViewModel SelectedStock
        {
            get
            {
                return _SelectedStock;
            }
            set
            {
                _SelectedStock = value;
                OnPropertyChanged(nameof(SelectedStock));
                if (value != null)
                {
                    var res = MessageBox.Show("Usar Stock Como guia?", "Stock Guia", MessageBoxButton.YesNoCancel);
                    if(res == MessageBoxResult.Yes)
                    {
                        MandarInformacion();
                    }
                }
            }
        }
            
        private Visibility _ControlVisibility = Visibility.Collapsed;
        public Visibility ControlVisibility
        {
            get
            {
                return _ControlVisibility;
            }
            set
            {
                _ControlVisibility = value;
                OnPropertyChanged(nameof(ControlVisibility));
            }
        }

        public void CargarStocks(ProductoIngresoViewModel producto)
        {
            _stocks.Clear();
            if(producto != null && producto.Producto.Id > 0)
            {
                foreach (var item in StockDAO.GetByProductId(producto.Producto.Id))
                {
                    _stocks.Add(item);
                }
            }
        }

        public void MandarInformacion()
        {
            padre.NuevoIngresoViewModel.RecibirInformacion(SelectedStock);
        }
    }
}
