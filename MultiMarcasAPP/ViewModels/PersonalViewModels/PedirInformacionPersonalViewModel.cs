using MultiMarcasAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.ViewModels.PersonalViewModels
{
    public class PedirInformacionPersonalViewModel : ViewModelBase
    {
        public PedirInformacionPersonalViewModel()
        {

        }
        public PedirInformacionPersonalViewModel(Trabajador trabajador)
        {
            _Trabajador = new(trabajador);
        }

        private Visibility _controlVisibility = Visibility.Collapsed;
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

        private string _Password = string.Empty;
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
                OnPropertyChanged(nameof(Password));
            }   
        }

        private Visibility _PasswordVisibility;
        public Visibility PasswordVisibility
        {
            get
            {
                return _PasswordVisibility;
            }
            set
            {
                _PasswordVisibility = value;
                OnPropertyChanged(nameof(PasswordVisibility));
            }
        }
    }
}
