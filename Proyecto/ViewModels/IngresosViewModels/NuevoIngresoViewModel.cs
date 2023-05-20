using MultiMarcasAPP.Models;
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
    public class NuevoIngresoViewModel : ViewModelBase
    {
        public NuevoIngresoViewModel()
        {
            _ProductosIngresos = new();
            _ProductosIngresos.CollectionChanged += _ProductosIngresos_CollectionChanged;
        }

        private void _ProductosIngresos_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (var item in _ProductosIngresos)
            {
                item.PropertyChanged += Item_PropertyChanged;
            }

            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                Total = 0;
                foreach (var item in _ProductosIngresos)
                {
                    Total += item.Total;
                }
            }
        }

        private void Item_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Total = 0;
            foreach (var item in _ProductosIngresos)
            {
                Total += item.Total;
            }
        }

        public ObservableCollection<ProductoIngresoViewModel> _ProductosIngresos;
        public IEnumerable<ProductoIngresoViewModel> ProductosIngresos => _ProductosIngresos;

        private decimal _Total;
        public decimal Total
        {
            get
            {
                return _Total;
            }
            set
            {
                _Total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        private ProductoIngresoViewModel _SelectedProductoIngreso;
        public ProductoIngresoViewModel SelectedProductoIngreso
        {
            get
            {
                return _SelectedProductoIngreso;
            }
            set
            {
                _SelectedProductoIngreso = value;
                OnPropertyChanged(nameof(SelectedProductoIngreso));
            }
        }

        public void AgregarProductoALista(Producto producto)
        {
            bool agregar = true;
            if (!producto.EsDerivado)
            {
                foreach (var item in _ProductosIngresos)
                {
                    if (item.Producto.Id == producto.Id)
                    {
                        agregar = false;
                        break;
                    }
                }
                if (agregar)
                {
                    _ProductosIngresos.Add(new(new(producto), this));
                }
            }
        }


        public void QuitarProductoDeLista(ProductoIngresoViewModel producto)
        {
            _ProductosIngresos.Remove(producto);
        }

        public void LimpiarLista()
        {
            _ProductosIngresos.Clear();
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

        private string _NumeroDocumento = string.Empty;
        public string NumeroDocumento
        {
            get
            {
                return _NumeroDocumento;
            }
            set
            {
                _NumeroDocumento = value;
                OnPropertyChanged(nameof(NumeroDocumento));
            }
        }

        public void RecibirInformacion(StockViewModel stock)
        {
            SelectedProductoIngreso.PrecioCompra = stock.Stock.PrecioCompra;
            SelectedProductoIngreso.PrecioVentaMinimo = stock.Stock.PrecioMinimo;
            SelectedProductoIngreso.PrecioVenta = stock.Stock.PrecioVenta;
            SelectedProductoIngreso.Descuento = 0;
        }
    }
}
