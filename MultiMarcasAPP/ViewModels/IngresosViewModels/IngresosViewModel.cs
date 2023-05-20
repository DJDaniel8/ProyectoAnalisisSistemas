using MultiMarcasAPP.Commands;
using MultiMarcasAPP.Commands.IngresosViewCommands;
using MultiMarcasAPP.Models;
using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.ProveedoresViewModels;
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
    public class IngresosViewModel : ViewModelBase
    {
        public IngresosViewModel()
        {
            TablaStockExistentesViewModel = new(this);
            NuevoIngresoViewModel = new();
            FiltrarPorList = new List<string>(new string[] { "Ultimos 10", "Ultimos 50", "Fecha" });
            CloseCommand = new CloseCommand();
            MinimizeCommand = new MinimizeCommand();
            MaximizeCommand = new MaximizeCommand();
            RestoreCommand = new RestoreCommand();
            NuevoCommand = new NuevoCommand(this);
            AgregarProductoCommand = new AgregarProductoCommand(this);
            GuardarCommand = new GuardarCommand(this);
            CancelarCommand = new CancelarCommand(this);
            //EditarCommand = new EditarCommand(this);
            EliminarCommand = new EliminarCommand(this);
            _Proveedores = new();
            CargarProveedores();
            TablasIngresosViewModel.CargarIngresos(SelectedFiltrarPor, SelectedFiltrar1, SelectedFiltrar2);
            NuevoIngresoViewModel.PropertyChanged += NuevoIngresoViewModel_PropertyChanged;
        }

        private void NuevoIngresoViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(NuevoIngresoViewModel.SelectedProductoIngreso))
            {
                TablaStockExistentesViewModel.CargarStocks(NuevoIngresoViewModel.SelectedProductoIngreso);
            }
        }

        private ObservableCollection<Proveedor> _Proveedores;
        public IEnumerable<Proveedor> Proveedores => _Proveedores;

        public List<string> FiltrarPorList { get; set; }

        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand CancelarCommand { get; }
        public ICommand RestoreCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand NuevoCommand { get; }
        public ICommand GuardarCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand AgregarProductoCommand { get; }

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

        private string _SelectedFiltrarPor = "Ultimos 10";
        public string SelectedFiltrarPor
        {
            get
            {
                return _SelectedFiltrarPor;
            }
            set
            {
                _SelectedFiltrarPor = value;
                if (value == "Fecha") FiltrarPorFechaVisibility = Visibility.Visible;
                else FiltrarPorFechaVisibility = Visibility.Collapsed;
                TablasIngresosViewModel.CargarIngresos(SelectedFiltrarPor, SelectedFiltrar1, SelectedFiltrar2);
                OnPropertyChanged(nameof(SelectedFiltrarPor));
            }
        }

        private Visibility _FiltrarComboBoxVisibility = Visibility.Visible;
        public Visibility FiltrarComboBoxVisibility
        {
            get
            {
                return _FiltrarComboBoxVisibility;
            }
            set
            {
                _FiltrarComboBoxVisibility = value;
                FiltrarPorFechaVisibility = FiltrarPorFechaVisibility;
                OnPropertyChanged(nameof(FiltrarComboBoxVisibility));
            }
        }

        private Visibility _FiltrarPorFechaVisibility = Visibility.Collapsed;
        public Visibility FiltrarPorFechaVisibility
        {
            get
            {
                return _FiltrarPorFechaVisibility;
            }
            set
            {
                if (value == Visibility.Visible && FiltrarComboBoxVisibility == Visibility.Visible) _FiltrarPorFechaVisibility = value;
                else if (value == Visibility.Collapsed) _FiltrarPorFechaVisibility = value;
                else _FiltrarPorFechaVisibility = Visibility.Collapsed;
                OnPropertyChanged(nameof(FiltrarPorFechaVisibility));
            }
        }

        private DateTime _SelectedFiltrar1 = DateTime.Now;
        public DateTime SelectedFiltrar1
        {
            get
            {
                return _SelectedFiltrar1;
            }
            set
            {
                _SelectedFiltrar1 = value;
                TablasIngresosViewModel.CargarIngresos(SelectedFiltrarPor, SelectedFiltrar1, SelectedFiltrar2);
                OnPropertyChanged(nameof(SelectedFiltrar1));
            }
        }

        private DateTime _SelectedFiltrar2 = DateTime.Now;
        public DateTime SelectedFiltrar2
        {
            get
            {
                return _SelectedFiltrar2;
            }
            set
            {
                _SelectedFiltrar2 = value;
                TablasIngresosViewModel.CargarIngresos(SelectedFiltrarPor, SelectedFiltrar1, SelectedFiltrar2);
                OnPropertyChanged(nameof(SelectedFiltrar2));
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

        private Visibility _ComboBoxProveedorVisibility = Visibility.Collapsed;
        public Visibility ComboBoxProveedorVisibility
        {
            get
            {
                return _ComboBoxProveedorVisibility;
            }
            set
            {
                _ComboBoxProveedorVisibility = value;
                OnPropertyChanged(nameof(ComboBoxProveedorVisibility));
            }
        }

        private Proveedor _SelectedProveedor;
        public Proveedor SelectedProveedor
        {
            get
            {
                return _SelectedProveedor;
            }
            set
            {
                _SelectedProveedor = value;
                NuevoIngresoViewModel.LimpiarLista();
                OnPropertyChanged(nameof(SelectedProveedor));
            }
        }

        private bool _IsAuditorado = false;
        public bool EsAuditorado
        {
            get
            {
                return _IsAuditorado;
            }
            set
            {
                _IsAuditorado = value;
                OnPropertyChanged(nameof(EsAuditorado));
            }
        }

        public TablasIngresosViewModel TablasIngresosViewModel { get; set; } = new();
        public NuevoIngresoViewModel NuevoIngresoViewModel { get; set; }

        public TablaStockExistentesViewModel TablaStockExistentesViewModel { get; set; }

        private void CargarProveedores()
        {
            _Proveedores.Clear();
            foreach (var proveedor in ProveedorDAO.Get())
            {
                _Proveedores.Add(proveedor);
            }
        }

        private DateTime _SelectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get
            {
                return _SelectedDate;
            }
            set
            {
                _SelectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }
    }
}
