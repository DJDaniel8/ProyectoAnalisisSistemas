using MultiMarcasAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.ViewModels.StocksViewModels
{
    public class StockViewModel : ViewModelBase
    {
        public StockViewModel(Stock stock, Producto producto)
        {
            Stock = stock;
            Producto = producto;
            PrecioMinimo = Stock.PrecioMinimo;
            PrecioVenta = Stock.PrecioVenta;
        }

        public Stock Stock { get; set; }
        public Producto Producto { get; set; }

        private decimal _PrecioVenta;
        public decimal PrecioVenta
        {
            get
            {
                return _PrecioVenta;
            }
            set
            {
                if(value >= PrecioMinimo) _PrecioVenta = value;
                OnPropertyChanged(nameof(PrecioVenta));
            }
        }

        private decimal _PrecioMinimo;
        public decimal PrecioMinimo
        {
            get
            {
                return _PrecioMinimo;
            }
            set
            {
                if(value > Stock.PrecioCompra) _PrecioMinimo = value;
                OnPropertyChanged(nameof(PrecioMinimo));
            }
        }
    }
}
