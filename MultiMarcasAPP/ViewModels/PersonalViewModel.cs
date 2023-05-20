using MultiMarcasAPP.Commands;
using MultiMarcasAPP.Commands.PersonalViewCommands;
using MultiMarcasAPP.ViewModels.PersonalViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels
{
    public class PersonalViewModel : ViewModelBase
    {
        public PersonalViewModel()
        {
            PrivilegiosPersonalViewModel = new(this);
            CloseCommand = new CloseCommand();
            MinimizeCommand = new MinimizeCommand();
            MaximizeCommand = new MaximizeCommand();
            RestoreCommand = new RestoreCommand();
            CrearCommand = new CrearCommand(this);
            GuardarCommand = new GuardarCommand(this);
            EditarCommand = new EditarCommand(this);
            CancelarCommand = new CancelarCommand(this);
            PrivilegiosCommand = new PrivilegiosCommand(this);
            RestoreVisibility = Visibility.Collapsed;
            _trabajadores = new();
            CargarTrabajadores();
        }

        public PrivilegiosPersonalViewModel PrivilegiosPersonalViewModel { get; set; }

        private ObservableCollection<TrabajadorViewModel> _trabajadores;
        public IEnumerable<TrabajadorViewModel> Trabajadores => _trabajadores;

        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand CancelarCommand { get; }
        public ICommand RestoreCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand CrearCommand { get; }
        public ICommand GuardarCommand { get; }
        public ICommand PrivilegiosCommand { get; }


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


        private MostrarInformacionPersonalViewModel _mostrarInformacionPersonalViewModel = new();
        public MostrarInformacionPersonalViewModel MostrarInformacionPersonalViewModel
        {
            get
            {
                return _mostrarInformacionPersonalViewModel;
            }
            set
            {
                _mostrarInformacionPersonalViewModel = value;
                OnPropertyChanged(nameof(MostrarInformacionPersonalViewModel));
            }
        }

        private PedirInformacionPersonalViewModel _pedirInformacionPersonalViewModel = new();
        public PedirInformacionPersonalViewModel PedirInformacionPersonalViewModel
        {
            get
            {
                return _pedirInformacionPersonalViewModel;
            }
            set
            {
                _pedirInformacionPersonalViewModel = value;
                OnPropertyChanged(nameof(PedirInformacionPersonalViewModel));
            }
        }

        private TrabajadorViewModel _SelectedTrabajador;
        public TrabajadorViewModel SelectedTrabajador
        {
            get
            {
                return _SelectedTrabajador;
            }
            set
            {
                _SelectedTrabajador = value;
                OnPropertyChanged(nameof(SelectedTrabajador));
                MostrarInformacionPersonalViewModel.Trabajador = value;
                PedirInformacionPersonalViewModel.Trabajador = value;
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

        private Visibility _PersonalInformacionVisibility = Visibility.Visible;
        public Visibility PersonalInformacionVisibility
        {
            get
            {
                return _PersonalInformacionVisibility;
            }
            set
            {
                _PersonalInformacionVisibility = value;
                OnPropertyChanged(nameof(PersonalInformacionVisibility));
            }
        }

        public void CargarTrabajadores()
        {
            var lista = Services.DAO.TrabajadorDAO.Get();
            _trabajadores.Clear();
            foreach (var item in lista)
            {
                _trabajadores.Add(new TrabajadorViewModel(item));
            }
        }
    }


}
