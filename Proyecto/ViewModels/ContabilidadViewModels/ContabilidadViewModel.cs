using MultiMarcasAPP.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.ContabilidadViewModels
{
    public class ContabilidadViewModel : ViewModelBase
    {
        public ContabilidadViewModel()
        {

            CloseCommand = new CloseCommand();
            MinimizeCommand = new MinimizeCommand();
            MaximizeCommand = new MaximizeCommand();
            RestoreCommand = new RestoreCommand();
            ReporteInventarioViewModel = new ReporteInventarioViewModel();
            TotalComprasViewModel = new();
            TotalGastosViewModel = new();
            ReporteVentasViewModel = new ReporteVentasViewModel();
            totalDespositosViewModel = new();
            PagosProveedoresViewModel = new();
        }

        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand RestoreCommand { get; }

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

        public TotalComprasViewModel TotalComprasViewModel { get; set; }
        public TotalGastosViewModel TotalGastosViewModel { get; set; }
        public ReporteVentasViewModel ReporteVentasViewModel { get; set; }
        public TotalDespositosViewModel totalDespositosViewModel { get; set; }
        public ReporteInventarioViewModel ReporteInventarioViewModel { get; set; }
        public PagosProveedoresViewModel PagosProveedoresViewModel { get; set; }
    }
}
