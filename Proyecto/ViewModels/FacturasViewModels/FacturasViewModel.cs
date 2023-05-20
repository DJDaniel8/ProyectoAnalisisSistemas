using MultiMarcasAPP.Commands;
using MultiMarcasAPP.Commands.FacturasViewCommands;
using MultiMarcasAPP.Models;
using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.ProductosViewModels;
using MultiMarcasAPP.ViewModels.VentaViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.FacturasViewModels
{
    public class FacturasViewModel : ViewModelBase
    {
        public FacturasViewModel()
        {
            CloseCommand = new CloseCommand();
            MinimizeCommand = new MinimizeCommand();
            MaximizeCommand = new MaximizeCommand();
            RestoreCommand = new RestoreCommand();
            Eliminar = new EliminarCommand(this);
            Imprimir = new ImprimirCommand(this);
            FiltrarPorList = new List<string>(new string[] {"Ultimas 100", "Hoy" , "Fecha" });
            _Productos = new();
            _Ventas = new();
            CargarVentas();
        }

        public ObservableCollection<ProductoVentaViewModel> _Productos;
        public IEnumerable<ProductoVentaViewModel> Productos => _Productos;

        public ObservableCollection<Venta> _Ventas;
        public IEnumerable<Venta> Ventas => _Ventas;

        public List<string> FiltrarPorList { get; }


        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand RestoreCommand { get; }
        public ICommand Eliminar { get; }
        public ICommand Imprimir { get; }

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

        private Venta _SelectedVenta;
        public Venta SelectedVenta
        {
            get
            {
                return _SelectedVenta;
            }
            set
            {
                _SelectedVenta = value;
                if (value != null && value.Id > 0) CargarProductosVenta();
                OnPropertyChanged(nameof(SelectedVenta));
            }
        }

        private Visibility _DatePickerVisibility = Visibility.Collapsed;
        public Visibility DatePickerVisibility
        {
            get
            {
                return _DatePickerVisibility;
            }
            set
            {
                _DatePickerVisibility = value;
                OnPropertyChanged(nameof(DatePickerVisibility));
            }
        }

        public void CargarVentas()
        {
            _Ventas.Clear();
            List<Venta> lista;
            if(String.IsNullOrEmpty(BuscarPorText))
            {
                if (SelectedFiltrarPor == "Ultimas 100") lista = VentaDAO.GetLast100();
                else if (SelectedFiltrarPor == "Hoy") lista = VentaDAO.GetToday();
                else if (SelectedFiltrarPor == "Fecha") lista = VentaDAO.GetByDate(SelectedDate);
                else lista = VentaDAO.GetLast100();
            }
            else
            {
                if (SelectedFiltrarPor == "Ultimas 100") lista = VentaDAO.GetLast100AndSearch(BuscarPorText);
                else if (SelectedFiltrarPor == "Hoy") lista = VentaDAO.GetTodayAndSearch(BuscarPorText);
                else if (SelectedFiltrarPor == "Fecha") lista = VentaDAO.GetByDateAndSearch(SelectedDate,BuscarPorText);
                else lista = VentaDAO.GetLast100AndSearch(BuscarPorText);
            }

            foreach (var item in lista)
            {
                _Ventas.Add(item);
            }
        }

        public void CargarProductosVenta()
        {
            _Productos.Clear();
            foreach (var item in VentaDAO.GetProductosVenta(SelectedVenta.Id))
            {
                _Productos.Add(item);
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
                CargarVentas();
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
                _SelectedFiltrarPor = value;
                if (value == "Fecha") DatePickerVisibility = Visibility.Visible;
                else DatePickerVisibility = Visibility.Collapsed;
                OnPropertyChanged(nameof(SelectedFiltrarPor));
                CargarVentas();
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
                CargarVentas();
            }
        }
    }
}
