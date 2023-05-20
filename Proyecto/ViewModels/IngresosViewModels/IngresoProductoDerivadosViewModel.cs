using MultiMarcasAPP.Commands.IngresosViewCommands;
using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.StocksViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.IngresosViewModels
{
    public class IngresoProductoDerivadosViewModel : ViewModelBase
    {
        public IngresoProductoDerivadosViewModel()
        {
            _stocks = new();
            AceptarCommand = new AceptarCommand(this);
        }

        public ICommand AceptarCommand { get; }


        private ProductoIngresoViewModel? _Producto = new(new(new()), new());
        public ProductoIngresoViewModel? Producto
        {
            get { return _Producto; }
            set
            {
                _Producto = value;
                OnPropertyChanged(nameof(Producto));
            }
        }

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
                    if (res == MessageBoxResult.Yes)
                    {
                        Producto.PrecioVentaMinimo = SelectedStock.Stock.PrecioMinimo;
                        Producto.PrecioVenta = SelectedStock.Stock.PrecioVenta;
                    }
                }
            }
        }

        public void CargarStocksExistentes()
        {
            _stocks.Clear();
            foreach (var stock in StockDAO.GetByProductId(Producto.Producto.Id))
            {
                _stocks.Add(stock);
            }
        }
    }
}
