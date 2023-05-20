using MultiMarcasAPP.Commands;
using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.Commands.StocksViewCommands;
using MultiMarcasAPP.ViewModels.ProductosViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.StocksViewModels
{
    public class StocksViewModel : ViewModelBase
    {
        public StocksViewModel()
        {
            CloseCommand = new CloseCommand();
            MinimizeCommand = new MinimizeCommand();
            MaximizeCommand = new MaximizeCommand();
            RestoreCommand = new RestoreCommand();
            EditarCommand = new EditarCommand(this);
            GuardarCommand = new GuardarCommand(this);
            CancelarCommand = new CancelarCommand(this);
            _stocks = new();
            BuscarPorList = new List<string>(new string[] { "Codigo", "Nombre", "Todo" });
            FiltrarPorList = new List<string>(new string[] { "Categoria", "Proveedor", "Niguno" });
            _FiltrarValorList = new();
            CargarStocks();
        }


        private ObservableCollection<StockViewModel> _stocks;
        public IEnumerable<StockViewModel> Stocks => _stocks;

        public List<string> BuscarPorList { get; }
        public List<string> FiltrarPorList { get; }

        private ObservableCollection<FiltrarValorViewModel> _FiltrarValorList;
        public IEnumerable<FiltrarValorViewModel> FiltrarValorList => _FiltrarValorList;

        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand CancelarCommand { get; }
        public ICommand RestoreCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand GuardarCommand { get; }

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

        private StockViewModel _SelectedStock;
        public StockViewModel SelectedStock
        {
            get
            {
                return _SelectedStock;
            }
            set
            {
                _SelectedStock = value;
                if(value != null) InformacionStockViewModel.StockViewModel = value;
                OnPropertyChanged(nameof(SelectedStock));
            }
        }

        public InformacionStockViewModel InformacionStockViewModel { get; set; } = new();

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
                CargarStocks();
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
                CargarStocks();
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
                CargarStocks();
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
                CargarStocks();
                OnPropertyChanged(nameof(SelectedFiltrarValor));
            }
        }

        public void CargarStocks()
        {
            if(MainButtonNavigationBarVisibility)
            {
                _stocks.Clear();
                foreach (var stock in StockDAO.Get())
                {
                    _stocks.Add(stock);
                }

                _stocks.Clear();
                List<StockViewModel> lista;
                //SIN FILTRO
                if (SelectedFiltrarPor == "Ninguno" || SelectedFiltrarPor == string.Empty)
                {
                    //BUSCARPORTEXT VACIO
                    if (BuscarPorText == string.Empty)
                    {
                        lista = StockDAO.Get();
                    }
                    //BUSCAR POR CODIGO Y NOMBRE
                    else if (SelectedBuscarPor == string.Empty || SelectedBuscarPor == "Todo")
                    {
                        lista = StockDAO.GetByAllSearch(BuscarPorText);
                    }
                    //BUSCAR SOLO POR CODIGO
                    else if (SelectedBuscarPor == "Codigo")
                    {
                        lista = StockDAO.GetByCodeSearch(BuscarPorText);
                    }
                    // BUSCAR SOLO POR NOMBRE
                    else if (SelectedBuscarPor == "Nombre")
                    {
                        lista = StockDAO.GetByNameSearch(BuscarPorText);
                    }
                    else lista = StockDAO.Get();
                }
                //CON FILTRO
                else
                {
                    if (SelectedFiltrarValor != null && SelectedFiltrarValor.Item1 > 0)
                    {
                        //FILTRAR SIN BUSQUEDA
                        if (BuscarPorText == String.Empty)
                        {
                            lista = StockDAO.GetByFilter(SelectedFiltrarPor, SelectedFiltrarValor.Item1);
                        }
                        //BUSCAR POR CODIGO Y NOMBRE
                        else if (SelectedBuscarPor == string.Empty || SelectedBuscarPor == "Todo")
                        {
                            lista = StockDAO.GetByFilterAndAllSearch(SelectedFiltrarPor, SelectedFiltrarValor.Item1, BuscarPorText);
                        }
                        //BUSCAR SOLO POR CODIGO
                        else if (SelectedBuscarPor == "Codigo")
                        {
                            lista = StockDAO.GetByFilterAndCodeSearch(SelectedFiltrarPor, SelectedFiltrarValor.Item1, BuscarPorText);
                        }
                        // BUSCAR SOLO POR NOMBRE
                        else if (SelectedBuscarPor == "Nombre")
                        {
                            lista = StockDAO.GetByFilterAndNameSearch(SelectedFiltrarPor, SelectedFiltrarValor.Item1, BuscarPorText);
                        }
                        else lista = StockDAO.Get();
                    }
                    else
                    {
                        //BUSCARPORTEXT VACIO
                        if (BuscarPorText == string.Empty)
                        {
                            lista = StockDAO.Get();
                        }
                        //BUSCAR POR CODIGO Y NOMBRE
                        else if (SelectedBuscarPor == string.Empty || SelectedBuscarPor == "Todo")
                        {
                            lista = StockDAO.GetByAllSearch(BuscarPorText);
                        }
                        //BUSCAR SOLO POR CODIGO
                        else if (SelectedBuscarPor == "Codigo")
                        {
                            lista = StockDAO.GetByCodeSearch(BuscarPorText);
                        }
                        // BUSCAR SOLO POR NOMBRE
                        else if (SelectedBuscarPor == "Nombre")
                        {
                            lista = StockDAO.GetByNameSearch(BuscarPorText);
                        }
                        else lista = StockDAO.Get();
                    }
                }
                foreach (var item in lista)
                {
                    _stocks.Add(item);
                }
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
