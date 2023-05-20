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
    public class MostrarInformacionProveedoresViewModel : ViewModelBase
    {
        public MostrarInformacionProveedoresViewModel(ICommand crearCommand, ICommand eliminarBancoCommand)
        {
            _Proveedor = new();
            CrearBancoCommand = crearCommand;
            EliminarBancoCommand = eliminarBancoCommand;
            _Bancos = new();
        }

        public MostrarInformacionProveedoresViewModel(ProveedorViewModel proveedor, ICommand crearCommand, ICommand eliminarBancoCommand)
        {
            _Proveedor = proveedor;
            OnPropertyChanged(nameof(Proveedor));
            EliminarBancoCommand = eliminarBancoCommand;
            CrearBancoCommand = crearCommand;
            _Bancos = new();
        }

        public ObservableCollection<BancoViewModel> _Bancos;
        public IEnumerable<BancoViewModel> Bancos => _Bancos;

        public ICommand CrearBancoCommand { get; }
        public ICommand EliminarBancoCommand { get; }

        private ProveedorViewModel _Proveedor;
        public ProveedorViewModel Proveedor
        {
            get
            {
                return _Proveedor;
            }
            set
            {
                _Proveedor = value;
                OnPropertyChanged(nameof(Proveedor));
            }
        }

        private Visibility _BancosButtosVisibility = Visibility.Collapsed;
        public Visibility BancosButtosVisibility
        {   
            get
            {
                return _BancosButtosVisibility;
            }
            set
            {
                _BancosButtosVisibility = value;
                OnPropertyChanged(nameof(BancosButtosVisibility));
            }
        }

        private bool _TxtBoxReadOnly = true;
        public bool TxtBoxReadOnly
        {
            get
            {
                return _TxtBoxReadOnly;
            }
            set
            {
                _TxtBoxReadOnly = value;
                OnPropertyChanged(nameof(TxtBoxReadOnly));
            }
        }

        private BancoViewModel _SelectedBanco;
        public BancoViewModel SelectedBanco
        {
            get
            {
                return _SelectedBanco;
            }
            set
            {
                _SelectedBanco = value;
                OnPropertyChanged(nameof(SelectedBanco));
            }
        }

        public void CargarBancosProveedor(int idProveedor)
        {
            _Bancos.Clear();

            foreach (var item in BancoDAO.GetByIdProvider(idProveedor))
            {
                _Bancos.Add(new(item));
            }
        }
    }
}
