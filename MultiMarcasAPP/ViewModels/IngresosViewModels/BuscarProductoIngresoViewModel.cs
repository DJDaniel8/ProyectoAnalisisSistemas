using MultiMarcasAPP.Commands;
using MultiMarcasAPP.Commands.IngresosViewCommands;
using MultiMarcasAPP.Commands.ProductosViewCommands;
using MultiMarcasAPP.Models;
using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.ProductosViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MultiMarcasAPP.ViewModels.IngresosViewModels
{
    public class BuscarProductoIngresoViewModel : ViewModelBase
    {
        public BuscarProductoIngresoViewModel()
        {
            CloseCommand = new CloseCommand();
            MinimizeCommand = new MinimizeCommand();
            MaximizeCommand = new MaximizeCommand();
            RestoreCommand = new RestoreCommand();
            EnviarCommand = new EnviarCommand(this);
            Seleccionar1 = new SelectRowCommand(() =>
            {
                SelectedRow = 0;
            });
            Seleccionar2 = new SelectRowCommand(() =>
            {
                SelectedRow = 1;
            });
            Seleccionar3 = new SelectRowCommand(() =>
            {
                SelectedRow = 2;
            });

            Seleccionar4 = new SelectRowCommand(() =>
            {
                SelectedRow = 3;
            });
            Seleccionar5 = new SelectRowCommand(() =>
            {
                SelectedRow = 4;
            });
            Seleccionar6 = new SelectRowCommand(() =>
            {
                SelectedRow = 5;
            });
            _ProductoInformacionViewModel = new(); OnPropertyChanged(nameof(ProductoInformacionViewModel));
            ProductoInformacionViewModel.CargarImagenCommand = new CargarImagenCommand(ProductoInformacionViewModel);
            ProductoInformacionViewModel.QuitarImagenCommand = new QuitarImagenCommand(ProductoInformacionViewModel);
            ProductoInformacionViewModel.AgregarCategoriaCommand = new AgregarCategoriaCommand(ProductoInformacionViewModel);
            ProductoInformacionViewModel.ConfirmarAgregarCategoriaCommand = new ConfirmacionAgregarCategoriaCommand(ProductoInformacionViewModel);
            ProductoInformacionViewModel.QuitarCategoriaCommand = new QuitarCategoriaCommand(ProductoInformacionViewModel);
            ProductoInformacionViewModel.AgregarProveedorCommand = new AgregarProveedorCommand(ProductoInformacionViewModel);
            ProductoInformacionViewModel.ConfirmarAgregarProveedorCommand = new ConfirmarAgragarProveedorCommand(ProductoInformacionViewModel);
            ProductoInformacionViewModel.QuitarProveedorCommand = new QuitarProveedorCommand(ProductoInformacionViewModel);
            _productos = new();
            BuscarPorList = new List<string>(new string[] { "Codigo", "Nombre", "Todo" });
            FiltrarPorList = new List<string>(new string[] { "Categoria", "Proveedor", "Niguno" });
            _FiltrarValorList = new();
            CargarProductos();
        }

        private ObservableCollection<ProductoViewModel> _productos;
        public IEnumerable<ProductoViewModel> Productos => _productos;

        public List<string> BuscarPorList { get; }
        public List<string> FiltrarPorList { get; }

        private ObservableCollection<FiltrarValorViewModel> _FiltrarValorList;
        public IEnumerable<FiltrarValorViewModel> FiltrarValorList => _FiltrarValorList;

        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand RestoreCommand { get; }
        public ICommand EnviarCommand { get; }
        public ICommand Seleccionar1 { get; }
        public ICommand Seleccionar2 { get; }
        public ICommand Seleccionar3 { get; }
        public ICommand Seleccionar4 { get; }
        public ICommand Seleccionar5 { get; }
        public ICommand Seleccionar6 { get; }

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

        private bool _DataGridEneable = true;
        public bool DataGridEneable
        {
            get
            {
                return _DataGridEneable;
            }
            set
            {
                _DataGridEneable = value;
                OnPropertyChanged(nameof(DataGridEneable));
            }
        }

        private bool _MainButtonNavigationBarVisibility = true;
        public bool MainButtonNavigationBarVisibility
        {
            get
            {
                return _MainButtonNavigationBarVisibility;
            }
            set
            {
                _MainButtonNavigationBarVisibility = value;
                OnPropertyChanged(nameof(MainButtonNavigationBarVisibility));
            }
        }

        private bool _SecundaryButtonNavigationBarVisiblity = false;
        public bool SecundaryButtonNavigationBarVisiblity
        {
            get
            {
                return _SecundaryButtonNavigationBarVisiblity;
            }
            set
            {
                _SecundaryButtonNavigationBarVisiblity = value;
                OnPropertyChanged(nameof(SecundaryButtonNavigationBarVisiblity));
            }
        }


        private ProductoInformacionViewModel _ProductoInformacionViewModel;
        public ProductoInformacionViewModel ProductoInformacionViewModel
        {
            get
            {
                return _ProductoInformacionViewModel;
            }
            set
            {
                _ProductoInformacionViewModel = value;
                OnPropertyChanged(nameof(ProductoInformacionViewModel));
            }
        }

        private int _SelectedRow;
        public int SelectedRow
        {
            get
            {
                return _SelectedRow;
            }
            set
            {
                _SelectedRow = value;
                OnPropertyChanged(nameof(SelectedRow));
            }
        }

        private ProductoViewModel _SelectedProducto = new();
        public ProductoViewModel SelectedProducto
        {
            get
            {
                return _SelectedProducto;
            }
            set
            {
                _SelectedProducto = value;
                if (value != null) ProductoInformacionViewModel.Producto = value;
                OnPropertyChanged(nameof(SelectedProducto));
                CargarImagen();
            }
        }

        private string _SelectedBuscarPor = string.Empty;
        public string SelectedBuscarPor
        {
            get
            {
                return _SelectedBuscarPor;
            }
            set
            {
                _SelectedBuscarPor = value;
                CargarProductos();
                OnPropertyChanged(nameof(SelectedBuscarPor));
            }
        }

        private string _BuscarPorText = string.Empty;
        public string BuscarPorText
        {
            get
            {
                return _BuscarPorText;
            }
            set
            {
                _BuscarPorText = value;
                CargarProductos();
                OnPropertyChanged(nameof(BuscarPorText));
            }
        }

        private string _SelectedFiltrarPor = string.Empty;
        public string SelectedFiltrarPor
        {
            get
            {
                return _SelectedFiltrarPor;
            }
            set
            {
                _SelectedFiltrarPor = value;
                OnPropertyChanged(nameof(SelectedFiltrarPor));
                CargarProductos();
                CargarFiltrarValor();
            }
        }

        private FiltrarValorViewModel _SelectedFiltrarValor = new();
        public FiltrarValorViewModel SelectedFiltrarValor
        {
            get
            {
                return _SelectedFiltrarValor;
            }
            set
            {
                _SelectedFiltrarValor = value;
                CargarProductos();
                OnPropertyChanged(nameof(SelectedFiltrarValor));
            }
        }


        public void CargarProductos()
        {
            _productos.Clear();
            List<Producto> lista;
            //SIN FILTRO
            if (SelectedFiltrarPor == "Ninguno" || SelectedFiltrarPor == string.Empty)
            {
                //BUSCARPORTEXT VACIO
                if (BuscarPorText == string.Empty)
                {
                    lista = ProductoDAO.Get();
                }
                //BUSCAR POR CODIGO Y NOMBRE
                else if (SelectedBuscarPor == string.Empty || SelectedBuscarPor == "Todo")
                {
                    lista = ProductoDAO.GetByAllSearch(BuscarPorText);
                }
                //BUSCAR SOLO POR CODIGO
                else if (SelectedBuscarPor == "Codigo")
                {
                    lista = ProductoDAO.GetByCodeSearch(BuscarPorText);
                }
                // BUSCAR SOLO POR NOMBRE
                else if (SelectedBuscarPor == "Nombre")
                {
                    lista = ProductoDAO.GetByNameSearch(BuscarPorText);
                }
                else lista = ProductoDAO.Get();
            }
            //CON FILTRO
            else
            {
                if (SelectedFiltrarValor != null && SelectedFiltrarValor.Item1 > 0)
                {
                    //FILTRAR SIN BUSQUEDA
                    if (BuscarPorText == String.Empty)
                    {
                        lista = ProductoDAO.GetByFilter(SelectedFiltrarPor, SelectedFiltrarValor.Item1);
                    }
                    //BUSCAR POR CODIGO Y NOMBRE
                    else if (SelectedBuscarPor == string.Empty || SelectedBuscarPor == "Todo")
                    {
                        lista = ProductoDAO.GetByFilterAndAllSearch(SelectedFiltrarPor, SelectedFiltrarValor.Item1, BuscarPorText);
                    }
                    //BUSCAR SOLO POR CODIGO
                    else if (SelectedBuscarPor == "Codigo")
                    {
                        lista = ProductoDAO.GetByFilterAndCodeSearch(SelectedFiltrarPor, SelectedFiltrarValor.Item1, BuscarPorText);
                    }
                    // BUSCAR SOLO POR NOMBRE
                    else if (SelectedBuscarPor == "Nombre")
                    {
                        lista = ProductoDAO.GetByFilterAndNameSearch(SelectedFiltrarPor, SelectedFiltrarValor.Item1, BuscarPorText);
                    }
                    else lista = ProductoDAO.Get();
                }
                else
                {
                    //BUSCARPORTEXT VACIO
                    if (BuscarPorText == string.Empty)
                    {
                        lista = ProductoDAO.Get();
                    }
                    //BUSCAR POR CODIGO Y NOMBRE
                    else if (SelectedBuscarPor == string.Empty || SelectedBuscarPor == "Todo")
                    {
                        lista = ProductoDAO.GetByAllSearch(BuscarPorText);
                    }
                    //BUSCAR SOLO POR CODIGO
                    else if (SelectedBuscarPor == "Codigo")
                    {
                        lista = ProductoDAO.GetByCodeSearch(BuscarPorText);
                    }
                    // BUSCAR SOLO POR NOMBRE
                    else if (SelectedBuscarPor == "Nombre")
                    {
                        lista = ProductoDAO.GetByNameSearch(BuscarPorText);
                    }
                    else lista = ProductoDAO.Get();
                }
            }
            foreach (var item in lista)
            {
                _productos.Add(new(item));
            }
        }

        private void CargarImagen()
        {
            Tuple<BitmapImage, byte[]>? tupla = null;
            if (SelectedProducto != null) tupla = ProductoDAO.GetProductImageById(SelectedProducto.Producto.Id);
            if (tupla != null)
            {
                ProductoInformacionViewModel.Imagen = tupla.Item1;
                ProductoInformacionViewModel.ImagenData = tupla.Item2;
            }

            if (tupla == null || SelectedProducto == null)
            {
                ProductoInformacionViewModel.Imagen = null;
                ProductoInformacionViewModel.ImagenData = null;
            }
        }

        private void CargarFiltrarValor()
        {
            _FiltrarValorList.Clear();
            if (SelectedFiltrarPor == "Categoria")
            {
                foreach (var item in CategoriaDAO.Get())
                {
                    _FiltrarValorList.Add(new(item.Item1, item.Item2));
                }
            }
            else if (SelectedFiltrarPor == "Proveedor")
            {
                foreach (var item in ProveedorDAO.Get())
                {
                    _FiltrarValorList.Add(new(item.Id, item.RazonSocial));
                }
            }
        }
    }
}
