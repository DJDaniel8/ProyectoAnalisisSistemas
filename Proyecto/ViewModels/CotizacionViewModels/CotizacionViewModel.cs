using MultiMarcasAPP.Commands;
using MultiMarcasAPP.Commands.CotizacionCommands;
using MultiMarcasAPP.ViewModels.StocksViewModels;
using MultiMarcasAPP.ViewModels.VentaViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.CotizacionViewModels
{
    public class CotizacionViewModel :ViewModelBase
    {
        public CotizacionViewModel()
        {
            _Productos = new();
            CloseCommand = new CloseCommand();
            MinimizeCommand = new MinimizeCommand();
            MaximizeCommand = new MaximizeCommand();
            RestoreCommand = new RestoreCommand();
            AgregarProductoCommand = new AgregarProductoCommand(this);
            FinalizarCommand = new FinalizarCommand(this);
            PrecioMinimoCommand = new PrecioMinimoCommand(this);
            _Productos.CollectionChanged += _Productos_CollectionChanged;
        }

        private void _Productos_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                foreach (ProductoCotizacionViewModel item in e.NewItems)
                {
                    item.PropertyChanged += Item_PropertyChanged;
                }
            }
            if (e.OldItems != null && e.OldItems.Count > 0)
            {
                foreach (ProductoCotizacionViewModel item in e.OldItems)
                {
                    item.PropertyChanged -= Item_PropertyChanged;
                }
            }
            CalcularTotal();
            OnPropertyChanged(nameof(Productos));
        }

        private void Item_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ProductoCotizacionViewModel.Total))
            {
                CalcularTotal();
            }
        }

        public ObservableCollection<ProductoCotizacionViewModel> _Productos;
        public IEnumerable<ProductoCotizacionViewModel> Productos => _Productos;

        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand RestoreCommand { get; }
        public ICommand AgregarProductoCommand { get; }
        public ICommand FinalizarCommand { get; }
        public ICommand PrecioMinimoCommand { get; }

        private Visibility _maximizeVisibility = Visibility.Visible;

        public Visibility MaximizeVisibility
        {
            get { return _maximizeVisibility; }
            set
            {
                _maximizeVisibility = value;
                OnPropertyChanged(nameof(MaximizeVisibility));
            }
        }

        private Visibility _restoreVisibility = Visibility.Collapsed;

        public Visibility RestoreVisibility
        {
            get { return _restoreVisibility; }
            set
            {
                _restoreVisibility = value;
                OnPropertyChanged(nameof(RestoreVisibility));
            }
        }

        private WindowState _windowState;

        public WindowState StateOfWindow
        {
            get { return _windowState; }
            set
            {
                _windowState = value;
                OnPropertyChanged(nameof(StateOfWindow));
                if (WindowState.Maximized == _windowState)
                {
                    MaximizeVisibility = Visibility.Collapsed;
                    RestoreVisibility = Visibility.Visible;
                }
                else if (WindowState.Normal == _windowState)
                {
                    MaximizeVisibility = Visibility.Visible;
                    RestoreVisibility = Visibility.Collapsed;
                }
            }
        }

        private ProductoCotizacionViewModel _SelectedProducto;
        public ProductoCotizacionViewModel SelectedProducto
        {
            get
            {
                return _SelectedProducto;
            }
            set
            {
                _SelectedProducto = value;
                OnPropertyChanged(nameof(SelectedProducto));
            }
        }

        private string _Nombre;
        public string Nombre
        {
            get
            {
                return _Nombre;
            }
            set
            {
                _Nombre = value;
                OnPropertyChanged(nameof(Nombre));
            }
        }

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

        public void QuitarProductoLista(ProductoCotizacionViewModel producto)
        {
            _Productos.Remove(producto);
            CalcularTotal();
        }

        public void AgregarProductoLista(StockViewModel stockViewModel, int cantidad)
        {
            bool agregar = true;
            foreach (ProductoCotizacionViewModel productoVenta in _Productos)
            {
                if (productoVenta.Stock.Id == stockViewModel.Stock.Id || stockViewModel.Stock.Cantidad <= 0)
                {
                    agregar = false;
                }
            }
            if (agregar)
            {
                ProductoCotizacionViewModel p = new ProductoCotizacionViewModel(stockViewModel.Producto, stockViewModel.Stock, this);
                if (cantidad > 1)
                {
                    p.Cantidad = cantidad;
                }
                _Productos.Add(p);
                CalcularTotal();
            }
        }

        private void CalcularTotal()
        {
            Total = 0;
            foreach (var item in _Productos)
            {
                Total += item.Total;
            }
        }
    }
}
