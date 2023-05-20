using MultiMarcasAPP.Commands;
using MultiMarcasAPP.Commands.UtilidadesViewCommands;
using MultiMarcasAPP.Services.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.UtilidadesViewModels
{
    public class UtilidadesViewModel : ViewModelBase
    {
        public UtilidadesViewModel()
        {
            FiltrarPorListVentas = new List<string>(new string[] { "Ultimas 100", "Hoy", "Fecha" });
            FiltrarPorListProductos = new List<string>(new string[] { "Top 10", "Top 100", "Codigo" });
            OrdenarPorVentas = new List<string>(new string[] { "Mayor a Menor", "Menor a Mayor" });
            CloseCommand = new CloseCommand();
            MinimizeCommand = new MinimizeCommand();
            MaximizeCommand = new MaximizeCommand();
            RestoreCommand = new RestoreCommand();
            VentasCommand = new VentasCommand(this);
            ProductosCommand = new ProductosCommand(this);
            FechaCommand = new FechaCommand(this);
            CurrentList = FiltrarPorListVentas;
            VentaViewModel = new();
            ProductosViewModel = new();
            FechaViewModel = new();
        }

        public List<string> FiltrarPorListVentas { get; }
        public List<string> OrdenarPorVentas { get; }
        public List<string> FiltrarPorListProductos { get; }
        public UtilidadesPorVentaViewModel VentaViewModel { get; set; }
        public UtilidadesPorProductosViewModel ProductosViewModel { get; set; }
        public UtilidadesPorFechaViewModel FechaViewModel { get; set; }

        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand RestoreCommand { get; }
        public ICommand VentasCommand { get; }
        public ICommand ProductosCommand { get; }
        public ICommand FechaCommand { get; }

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


        private string _BuscarPorText = string.Empty;
        public string BuscarPorText
        {
            get
            {
                return _BuscarPorText;
            }
            set
            {
                _BuscarPorText = value;
                if (!string.IsNullOrEmpty(BuscarPorText))
                {
                    int id = ProductoDAO.GetIdByCode(BuscarPorText);
                    if (id > 0) ProductosViewModel.CargarUtilidades(3, id);
                }
                OnPropertyChanged(nameof(BuscarPorText));
            }
        }

        private string _SelectedFiltrarPor = string.Empty;
        public string SelectedFiltrarPor
        {
            get
            {
                return _SelectedFiltrarPor;
            }
            set
            {
                DatePickerVisibility1 = Visibility.Collapsed;
                TxtBoxVisibility = Visibility.Collapsed;
                _SelectedFiltrarPor = value;
                if (value == "Ultimas 100") VentaViewModel.CargarUtilidades(1, null);
                else if (value == "Hoy") VentaViewModel.CargarUtilidades(2, null);
                else if (value == "Fecha")
                {
                    VentaViewModel.CargarUtilidades(3, _SelectedDate);
                    DatePickerVisibility1 = Visibility.Visible;
                }
                else if (value == "Top 10") ProductosViewModel.CargarUtilidades(1, 0);
                else if(value == "Top 100") ProductosViewModel.CargarUtilidades(2,0);
                else if(value == "Codigo")
                {
                    TxtBoxVisibility = Visibility.Visible;
                    if(!string.IsNullOrEmpty(BuscarPorText))
                    {
                        int id = ProductoDAO.GetIdByCode(BuscarPorText);
                        if (id > 0) ProductosViewModel.CargarUtilidades(3, id);
                    }
                }
                OnPropertyChanged(nameof(SelectedFiltrarPor));
            }
        }

        private DateTime _SelectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get
            {
                return _SelectedDate;
            }
            set
            {
                _SelectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
                VentaViewModel.CargarUtilidades(3, _SelectedDate);
                FechaViewModel.CargarUtilidad(value, SelectedDate2);
                FechaViewModel.SelectedUtilidad = new();
            }
        }

        private DateTime _SelectedDate2 = DateTime.Now;
        public DateTime SelectedDate2
        {
            get
            {
                return _SelectedDate2;
            }
            set
            {
                _SelectedDate2 = value;
                OnPropertyChanged(nameof(SelectedDate2));
                FechaViewModel.CargarUtilidad(SelectedDate,value);
                FechaViewModel.SelectedUtilidad = new();
            }
        }

        private List<string> _CurrentList;
        public List<string> CurrentList
        {
            get
            {
                return _CurrentList;
            }
            set
            {
                _CurrentList = value;
                OnPropertyChanged(nameof(CurrentList));
            }
        }

        private Visibility _DatePickerVisibility1 = Visibility.Collapsed;
        public Visibility DatePickerVisibility1
        {
            get
            {
                return _DatePickerVisibility1;
            }
            set
            {
                _DatePickerVisibility1 = value;
                OnPropertyChanged(nameof(DatePickerVisibility1));
            }
        }

        private Visibility _DatePickerVisibility2 = Visibility.Collapsed;
        public Visibility DatePickerVisibility2
        {
            get
            {
                return _DatePickerVisibility2;
            }
            set
            {
                _DatePickerVisibility2 = value;
                OnPropertyChanged(nameof(DatePickerVisibility2));
            }
        }

        private Visibility _TxtBoxVisibility = Visibility.Collapsed;
        public Visibility TxtBoxVisibility
        {
            get
            {
                return _TxtBoxVisibility;
            }
            set
            {
                _TxtBoxVisibility = value;
                OnPropertyChanged(nameof(TxtBoxVisibility));
            }
        }

        private Visibility _ComboBoxFiltrar;
        public Visibility ComboBoxFiltrar
        {
            get
            {
                return _ComboBoxFiltrar;
            }
            set
            {
                _ComboBoxFiltrar = value;
                OnPropertyChanged(nameof(ComboBoxFiltrar));
            }
        }
    }
}
