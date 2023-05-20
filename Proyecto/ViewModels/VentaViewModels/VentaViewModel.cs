using MultiMarcasAPP.Commands;
using MultiMarcasAPP.Commands.VentaViewCommands;
using MultiMarcasAPP.ViewModels.StocksViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.VentaViewModels
{
    public class VentaViewModel : ViewModelBase
    {
        public VentaViewModel()
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
            if(e.NewItems!= null && e.NewItems.Count > 0)
            {
                foreach (ProductoVentaViewModel item in e.NewItems)
                {
                    item.PropertyChanged += Item_PropertyChanged;
                }
            }
            if(e.OldItems != null && e.OldItems.Count > 0)
            {
                foreach(ProductoVentaViewModel item in e.OldItems)
                {
                    item.PropertyChanged -= Item_PropertyChanged;
                }
            }
            CalcularTotal();
        }

        private void Item_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ProductoVentaViewModel.Total))
            {
                CalcularTotal();
            }
        }

        public ObservableCollection<ProductoVentaViewModel> _Productos;
        public IEnumerable<ProductoVentaViewModel> Productos => _Productos;

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

        private ProductoVentaViewModel _SelectedProducto;
        public ProductoVentaViewModel SelectedProducto
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

        public void QuitarProductoLista(ProductoVentaViewModel producto)
        {
            _Productos.Remove(producto);
            CalcularTotal();
        }

        public void AgregarProductoLista(StockViewModel stockViewModel, int cantidad)
        {
            bool agregar = true;
            foreach (ProductoVentaViewModel productoVenta in _Productos)
            {
                if (productoVenta.Stock.Id == stockViewModel.Stock.Id || stockViewModel.Stock.Cantidad <= 0)
                {
                    agregar = false;
                }
            }
            if(agregar)
            {
                ProductoVentaViewModel p = new ProductoVentaViewModel(stockViewModel.Producto, stockViewModel.Stock, this);
                if(cantidad > 1)
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
