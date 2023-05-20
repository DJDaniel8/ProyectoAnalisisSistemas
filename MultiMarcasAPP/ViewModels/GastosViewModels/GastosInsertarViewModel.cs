using MultiMarcasAPP.Commands.GastosViewCommands.InsertarGastoCommands;
using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.PagosViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.GastosViewModels
{
    public class GastosInsertarViewModel : ViewModelBase
    {
        public GastosInsertarViewModel()
        {
            GuardarCommand = new GuardarCommand(this);
            CancelarCommand = new CancelarCommand(this);
            _TiposGastos = new();
            _TiposPagos = new();
            CargarTipos();
            CargarTiposPagos();
        }

        public ObservableCollection<TipoPagoViewModel> _TiposPagos;
        public IEnumerable<TipoPagoViewModel> TiposPagos => _TiposPagos;

        public ObservableCollection<TipoGastoViewModel> _TiposGastos;
        public IEnumerable<TipoGastoViewModel> TiposGastos => _TiposGastos;

        public ICommand CancelarCommand { get; }
        public ICommand GuardarCommand { get; }

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

        private DateTime _Fecha = DateTime.Now;
        public DateTime Fecha
        {
            get
            {
                return _Fecha;
            }
            set
            {
                _Fecha = value;
                OnPropertyChanged(nameof(Fecha));
            }
        }

        private decimal _Monto;
        public decimal Monto
        {
            get
            {
                return _Monto;
            }
            set
            {
                _Monto = value;
                OnPropertyChanged(nameof(Monto));
            }
        }

        private string _Descripcion = string.Empty;
        public string Descripcion
        {
            get
            {
                return _Descripcion;
            }
            set
            {
                _Descripcion = value;
                OnPropertyChanged(nameof(Descripcion));
            }
        }

        private string _NoDoc = string.Empty;
        public string NoDoc
        {
            get
            {
                return _NoDoc;
            }
            set
            {
                _NoDoc = value;
                OnPropertyChanged(nameof(NoDoc));
            }
        }

        private TipoGastoViewModel _SelectedGasto = new();
        public TipoGastoViewModel SelectedGasto
        {
            get
            {
                return _SelectedGasto;
            }
            set
            {
                _SelectedGasto = value;
                OnPropertyChanged(nameof(SelectedGasto));
            }
        }

        private TipoPagoViewModel _SelectedPago = new();
        public TipoPagoViewModel SelectedPago
        {
            get
            {
                return _SelectedPago;
            }
            set
            {
                _SelectedPago = value;
                OnPropertyChanged(nameof(SelectedPago));
            }
        }

        public void CargarTipos()
        {
            _TiposGastos.Clear();
            foreach (var item in GastoDAO.GetTipos())
            {
                _TiposGastos.Add(item);
            }
        }

        public void CargarTiposPagos()
        {
            _TiposPagos.Clear();
            foreach (var item in PagosDAO.GetTipos())
            {
                _TiposPagos.Add(item);
            }
        }
    }
}
