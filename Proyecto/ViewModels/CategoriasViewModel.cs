using MultiMarcasAPP.Commands;
using MultiMarcasAPP.Commands.CategoriasViewCommads;
using MultiMarcasAPP.Services.DAO;
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
    public class CategoriasViewModel : ViewModelBase
    {
        public CategoriasViewModel()
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
            _Categorias = new();
            CargarCategorias();

        }

        private ObservableCollection<Tuple<int,string>> _Categorias;
        public IEnumerable<Tuple<int,string>> Categorias => _Categorias;

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

        private Visibility _DataGridVisibility; 
        public Visibility DataGridVisibility
        {
            get
            {
                return _DataGridVisibility;
            }
            set
            {
                _DataGridVisibility = value;
                OnPropertyChanged(nameof(DataGridVisibility));
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

        private Tuple<int,string> _SelectedCategoria;
        public Tuple<int,string> SelectedCategoria
        {
            get
            {
                return _SelectedCategoria;
            }
            set
            {
                _SelectedCategoria = value;
                OnPropertyChanged(nameof(SelectedCategoria));
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

        public bool IsEdit { get; set; } = false;

        public void CargarCategorias()
        {
            _Categorias.Clear();
            foreach (var item in CategoriaDAO.Get())
            {
                _Categorias.Add(item);
            }
        }
    }
}
