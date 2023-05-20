using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.ViewModels.PersonalViewModels
{
    public class MostrarInformacionPersonalViewModel : ViewModelBase
    {
        public MostrarInformacionPersonalViewModel()
        {
            _Trabajador = new(new());
        }
        public MostrarInformacionPersonalViewModel(TrabajadorViewModel trabajador)
        {
            _Trabajador = trabajador;
        }

        private TrabajadorViewModel _Trabajador;
        public TrabajadorViewModel Trabajador
        {
            get
            {
                return _Trabajador;
            }
            set
            {
                _Trabajador = value;
                OnPropertyChanged(nameof(Trabajador));
            }
        }

        private Visibility _controlVisibility = Visibility.Visible;
        public Visibility ControlVisibility
        {
            get
            {
                return _controlVisibility;
            }
            set
            {
                _controlVisibility = value;
                OnPropertyChanged(nameof(ControlVisibility));
            }
        }
    }
}
