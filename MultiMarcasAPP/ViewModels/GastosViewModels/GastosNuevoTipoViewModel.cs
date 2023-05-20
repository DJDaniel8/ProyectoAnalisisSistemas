using MultiMarcasAPP.Commands.GastosViewCommands.NuevoTipoCommands;
using MultiMarcasAPP.Services.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.GastosViewModels
{
    public class GastosNuevoTipoViewModel : ViewModelBase
    {
        public GastosNuevoTipoViewModel()
        {
            GuardarCommand = new GuardarCommand(this);
            CancelarCommand = new CancelarCommand(this);
            _TiposGastos = new();
            CargarTipos();
        }

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

        private bool _Deducible;
        public bool Deducible
        {
            get
            {
                return _Deducible;
            }
            set
            {
                _Deducible = value;
                OnPropertyChanged(nameof(Deducible));
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
    }
}
