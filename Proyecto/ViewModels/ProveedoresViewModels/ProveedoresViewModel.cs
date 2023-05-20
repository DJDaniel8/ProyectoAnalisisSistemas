using MultiMarcasAPP.Commands;
using MultiMarcasAPP.Commands.ProveedoresViewCommands;
using MultiMarcasAPP.Services.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.ProveedoresViewModels
{
    public class ProveedoresViewModel : ViewModelBase
    {
        public ProveedoresViewModel()
        {
            CloseCommand = new CloseCommand();
            MinimizeCommand = new MinimizeCommand();
            MaximizeCommand = new MaximizeCommand();
            RestoreCommand = new RestoreCommand();
            CrearCommand = new CrearCommand(this);
            GuardarCommand = new GuardarCommand(this);
            EditarCommand = new EditarCommand(this);
            CancelarCommand = new CancelarCommand(this);
            EliminarCommand = new EliminarCommand(this);
            RestoreVisibility = Visibility.Collapsed;
            _proveedores = new();
            InformacionViewModel = new(new AgregarBancoCommand(this), new EliminarBancoCommand(this));
            InformacionBancoViewModel = new();
            InformacionBancoViewModel.GuardarBancoCommand = new GuardarBancoCommand(this, InformacionBancoViewModel);
            CargarProveedores();
        }


        private ObservableCollection<ProveedorViewModel> _proveedores;
        public IEnumerable<ProveedorViewModel> Proveedores => _proveedores;

        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand CancelarCommand { get; }
        public ICommand RestoreCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand CrearCommand { get; }
        public ICommand GuardarCommand { get; }
        public ICommand EliminarCommand { get; }


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

        private Visibility _restoreVisibility;

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

        private MostrarInformacionProveedoresViewModel _InformacionViewModel;
        public MostrarInformacionProveedoresViewModel InformacionViewModel
        {
            get
            {
                return _InformacionViewModel;
            }
            set
            {
                _InformacionViewModel = value;
                OnPropertyChanged(nameof(InformacionViewModel));
            }
        }

        private InformacionBancoViewModel _InformacionBancoViewModel;
        public InformacionBancoViewModel InformacionBancoViewModel
        {
            get
            {
                return _InformacionBancoViewModel;
            }
            set
            {
                _InformacionBancoViewModel = value;
                OnPropertyChanged(nameof(InformacionBancoViewModel));

            }
        }

        private ProveedorViewModel _SelectedProveedor;
        public ProveedorViewModel SelectedProveedor
        {
            get
            {
                return _SelectedProveedor;
            }
            set
            {
                _SelectedProveedor = value;
                if (value != null)
                {
                    InformacionViewModel.Proveedor = value;
                    InformacionViewModel.CargarBancosProveedor(value.Proveedor.Id);
                }
                else InformacionViewModel._Bancos.Clear();
                OnPropertyChanged(nameof(SelectedProveedor));
            }
        }


        public void CargarProveedores()
        {
            _proveedores.Clear();
            foreach (var item in ProveedorDAO.Get())
            {
                _proveedores.Add(new(item));
            }
        }


    }
}
