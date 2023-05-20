using MultiMarcasAPP.Commands.PagosCommands.NuevoTipoCommands;
using MultiMarcasAPP.Services.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.PagosViewModels
{
    public class PagosNuevoTipoViewModel : ViewModelBase
    {
        public PagosNuevoTipoViewModel()
        {
            GuardarCommand = new GuardarCommand(this);
            CancelarCommand = new CancelarCommand(this);
            _TiposPagos = new();
            CargarTipos();
        }

        public ObservableCollection<TipoPagoViewModel> _TiposPagos;
        public IEnumerable<TipoPagoViewModel> TiposPagos => _TiposPagos;

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

        private string _Nombre = String.Empty;
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

        private bool _SaleDeCaja;
        public bool SaleDeCaja
        {
            get
            {
                return _SaleDeCaja;
            }
            set
            {
                _SaleDeCaja = value;
                OnPropertyChanged(nameof(SaleDeCaja));
            }
        }

        public void CargarTipos()
        {
            _TiposPagos.Clear();
            foreach (var item in PagosDAO.GetTipos())
            {
                _TiposPagos.Add(item);
            }
        }
    }
}
