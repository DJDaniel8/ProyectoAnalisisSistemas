using MultiMarcasAPP.Commands;
using MultiMarcasAPP.Commands.CajaViewCommands;
using MultiMarcasAPP.Models;
using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.VentaViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.CajaViewModels
{
    public class CajaViewModel : ViewModelBase
    {
        public CajaViewModel()
        {
            _Productos = new();
            _Ventas = new();
            ContinuarCommand = new ContinuarCommand(this);
            EliminarCommand = new EliminarCommand(this);
            CloseCommand = new CloseCommand();
            MinimizeCommand = new MinimizeCommand();
            MaximizeCommand = new MaximizeCommand();
            RestoreCommand = new RestoreCommand();
            Seleccionar1 = new SelectRowCommand(() =>
            {
                SelectedVentaRow = 0;
            });
            Seleccionar2 = new SelectRowCommand(() =>
            {
                SelectedVentaRow = 1;
            });
            Seleccionar3 = new SelectRowCommand(() =>
            {
                SelectedVentaRow = 2;
            });

            Seleccionar4 = new SelectRowCommand(() =>
            {
                SelectedVentaRow = 3;
            });
            Seleccionar5 = new SelectRowCommand(() =>
            {
                SelectedVentaRow = 4;
            });
            Seleccionar6 = new SelectRowCommand(() =>
            {
                SelectedVentaRow = 5;
            });
            CargarVentas();
        }

        public ObservableCollection<ProductoVentaViewModel> _Productos;
        public IEnumerable<ProductoVentaViewModel> Productos => _Productos;

        public ObservableCollection<Venta> _Ventas;
        public IEnumerable<Venta> Ventas => _Ventas;

        public ICommand ContinuarCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand RestoreCommand { get; }
        public ICommand Seleccionar1 { get; }
        public ICommand Seleccionar2 { get; }
        public ICommand Seleccionar3 { get; }
        public ICommand Seleccionar4 { get; }
        public ICommand Seleccionar5 { get; }
        public ICommand Seleccionar6 { get; }

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

        private int _SelectedVentaRow;
        public int SelectedVentaRow
        {
            get
            {
                return _SelectedVentaRow;
            }
            set
            {
                _SelectedVentaRow = value;
                OnPropertyChanged(nameof(SelectedVentaRow));
            }
        }

        public void CargarVentas()
        {
            _Ventas.Clear();
            List<Venta> lista;
            lista = PreVentaDAO.GetAll();
            foreach (var item in lista)
            {
                _Ventas.Add(item);
            }
        }

        public void CargarProductosVenta()
        {
            _Productos.Clear();
            foreach (var item in PreVentaDAO.GetProductosVenta(SelectedVenta.Id))
            {
                _Productos.Add(item);
            }
        }

        
    }
}
