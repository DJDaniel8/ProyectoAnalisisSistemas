using MultiMarcasAPP.Commands;
using MultiMarcasAPP.Commands.VentaViewCommands;
using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.ClientesViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.VentaViewModels
{
    public class InformacionVentaViewModel : ViewModelBase
    {
        public InformacionVentaViewModel()
        {
            CloseCommand = new CloseCommand();
            MinimizeCommand = new MinimizeCommand();
            MaximizeCommand = new MaximizeCommand();
            RestoreCommand = new RestoreCommand();
            CFCommand = new CFCommand(this);
            AceptarCommand = new AceptarCommand(this);
            _Clientes = new();
            CargarClientes();
        }

        private ObservableCollection<ClienteViewModel> _Clientes;
        public IEnumerable<ClienteViewModel> Clientes => _Clientes;

        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand RestoreCommand { get; }
        public ICommand CFCommand { get; }
        public ICommand AceptarCommand { get; }

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
                OnPropertyChanged(nameof(SelectedCliente));
            }
        }

        private string _Nombre;
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

        private string _Nit;
        public string Nit
        {
            get
            {
                return _Nit;
            }
            set
            {
                _Nit = value;
                OnPropertyChanged(nameof(Nit));
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
