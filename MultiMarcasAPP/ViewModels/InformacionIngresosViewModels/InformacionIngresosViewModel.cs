using MultiMarcasAPP.Commands;
using MultiMarcasAPP.ViewModels.GastosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace MultiMarcasAPP.ViewModels.InformacionIngresosViewModels
{
    public class InformacionIngresosViewModel : ViewModelBase
    {
        public InformacionIngresosViewModel()
        {
            CloseCommand = new CloseCommand();
            MinimizeCommand = new MinimizeCommand();
            MaximizeCommand = new MaximizeCommand();
            RestoreCommand = new RestoreCommand();
            informacionIngresosVerViewModel = new(this);
            InformacionIngresosVerPagosViewModel = new(this);
            InformacionIngresosInsertarPagosViewModel = new(this);
            InformacionIngresosVerProductosViewModel = new(this);
        }

        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand RestoreCommand { get; }
        public ICommand InsertarPagoCommand { get; }

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

        public InformacionIngresosVerPagosViewModel InformacionIngresosVerPagosViewModel { get; set; }
        public InformacionIngresosInsertarPagosViewModel InformacionIngresosInsertarPagosViewModel { get; set; }
        public InformacionIngresosVerViewModel informacionIngresosVerViewModel { get; set; }

        public InformacionIngresosVerProductosViewModel InformacionIngresosVerProductosViewModel { get; set; }
    }
}
