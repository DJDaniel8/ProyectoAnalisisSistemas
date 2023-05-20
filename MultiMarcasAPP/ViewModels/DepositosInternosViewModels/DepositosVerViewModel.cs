using MultiMarcasAPP.Commands.DepositosInternosCommands;
using MultiMarcasAPP.Models;
using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.CajaViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.DepositosInternosViewModels
{
    public class DepositosVerViewModel : ViewModelBase
    {
        public DepositosVerViewModel()
        {
            _Depositos = new();
            Años = DepositoDAO.GetAños();
            CrearCommand = new CrearCommand(this);
            CancelarCommand = new CancelarCommand(this);
            GuardarCommand = new GuardarCommand(this);
            EliminarCommand = new EliminarCommand(this);
        }

        public ICommand GuardarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand CrearCommand { get; set; }
        public ICommand CancelarCommand { get; set; }

        public IEnumerable<int> Años { get; set; }
        public IEnumerable<int>? Meses { get; set; }

        private ObservableCollection<DepositoViewModel> _Depositos;
        public IEnumerable<DepositoViewModel> Depositos => _Depositos;

        private DepositoViewModel _SelectedDesposito;
        public DepositoViewModel SelectedDeposito
        {
            get
            {
                return _SelectedDesposito;
            }
            set
            {
                _SelectedDesposito = value;
                OnPropertyChanged(nameof(SelectedDeposito));
                if(value != null)
                {
                    value.PropertyChanged += Value_PropertyChanged;
                }
            }
        }

        private void Value_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(SelectedDeposito));
        }

        private int _año;
        public int SelectedAño
        {
            get
            {
                return _año;
            }
            set
            {
                _año = value;
                OnPropertyChanged(nameof(SelectedAño));
                if (value > 0)
                {
                    Meses = DepositoDAO.GetMeses(SelectedAño);
                    OnPropertyChanged(nameof(Meses));
                }
            }
        }

        private int _SelectedMes;
        public int SelectedMes
        {
            get
            {
                return _SelectedMes;
            }
            set
            {
                _SelectedMes = value;
                if (value > 0)
                {
                    CargarDepositos();
                }
                OnPropertyChanged(nameof(SelectedMes));
            }
        }

        private bool _IsReadOnly = true;
        public bool IsReadOnly
        {
            get
            {
                return _IsReadOnly;
            }
            set
            {
                _IsReadOnly = value;
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

        private bool _IsEneable = false;
        public bool IsEneable
        {
            get
            {
                return _IsEneable;
            }
            set
            {
                _IsEneable = value;
                OnPropertyChanged(nameof(IsEneable));
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

        public void CargarDepositos()
        {
            _Depositos.Clear();
            foreach (var item in DepositoDAO.GetByYearAndMonth(SelectedAño, SelectedMes))
            {
                _Depositos.Add(item);
            }
        }
    }
}
