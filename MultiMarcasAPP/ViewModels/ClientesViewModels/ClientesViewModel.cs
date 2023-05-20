using MultiMarcasAPP.Commands;
using MultiMarcasAPP.Commands.ClientesViewCommands;
using MultiMarcasAPP.Services.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.ClientesViewModels
{
    public class ClientesViewModel : ViewModelBase
    {

        public ClientesViewModel()
        {
            CloseCommand = new CloseCommand();
            MinimizeCommand = new MinimizeCommand();
            MaximizeCommand = new MaximizeCommand();
            RestoreCommand = new RestoreCommand();
            CrearCommand = new CrearCommand(this);
            GuardarCommand = new GuardarCommand(this);
            EditarCommand = new EditarCommand(this);
            CancelarCommand = new CancelarCommand(this);
            RestoreVisibility = Visibility.Collapsed;
            _Clientes = new();
            CurrentViewModel = new InformacionClienteViewModel(new ClienteViewModel(new Models.Cliente()));
            CargarClientes();
        }

        private ObservableCollection<ClienteViewModel> _Clientes;
        public IEnumerable<ClienteViewModel> Clientes => _Clientes;

        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand CancelarCommand { get; }
        public ICommand RestoreCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand CrearCommand { get; }
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

        private ViewModelBase _CurrentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _CurrentViewModel;
            }
            set
            {
                _CurrentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        private ClienteViewModel _SelectedCliente;
        public ClienteViewModel SelectedCliente
        {
            get
            {
                return _SelectedCliente;
            }
            set
            {
                _SelectedCliente = value;
                if(value != null) ((InformacionClienteViewModel)CurrentViewModel).Cliente = value;
                OnPropertyChanged(nameof(SelectedCliente));
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

        private bool _MainButtonsBarVisibility = true;
        public bool MainButtonsBarVisibility
        {
            get
            {
                return _MainButtonsBarVisibility;
            }
            set
            {
                _MainButtonsBarVisibility = value;
                OnPropertyChanged(nameof(MainButtonsBarVisibility));
            }
        }

        private bool _SecundaryButtonsBarVisibilty = false; 
        public bool SecundaryButtonsBarVisibility
        {
            get
            {
                return _SecundaryButtonsBarVisibilty;
            }
            set
            {
                _SecundaryButtonsBarVisibilty = value;
                OnPropertyChanged(nameof(SecundaryButtonsBarVisibility));
            }
        }

        public void CargarClientes()
        {
            _Clientes.Clear();
            var lista = ClienteDAO.Get();
            foreach (var cliente in lista)
            {
                _Clientes.Add(new ClienteViewModel(cliente));
            }
        }
    }
}
