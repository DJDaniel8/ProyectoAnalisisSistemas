using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.ProveedoresViewModels
{
    public class InformacionBancoViewModel : ViewModelBase
    {
        public InformacionBancoViewModel()
        {
            _Banco = new();
        }
        public InformacionBancoViewModel(ICommand guardarCommand)
        {
            _Banco = new();
            GuardarBancoCommand = guardarCommand;
        }

        public InformacionBancoViewModel(BancoViewModel banco, ICommand guardarCommand)
        {
            _Banco = banco;
            OnPropertyChanged(nameof(Banco));
            GuardarBancoCommand = guardarCommand;
        }

        public ICommand GuardarBancoCommand { get; set; }

        private BancoViewModel _Banco;
        public BancoViewModel Banco
        {
            get
            {
                return _Banco;
            }
            set
            {
                _Banco = value;
                OnPropertyChanged(nameof(Banco));
            }
        }

        private Visibility _InformacionBancoVisibility = Visibility.Collapsed;
        public Visibility InformacionBancoVisibility
        {
            get
            {
                return _InformacionBancoVisibility;
            }
            set
            {
                _InformacionBancoVisibility = value;
                OnPropertyChanged(nameof(InformacionBancoVisibility));
            }
        }
    }
}
