using MultiMarcasAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.ViewModels.ProveedoresViewModels
{
    public class BancoViewModel : ViewModelBase
    {
        public BancoViewModel()
        {
            Banco = new();
        }

        public BancoViewModel(Banco banco)
        {
            Banco = banco;
            NombreBanco = Banco.Nombre;
            NombreCuenta = Banco.NombreCuenta;
            NumeroCuenta = Banco.NumeroCuenta;
        }

        public Banco Banco { get; set; }

        private string _NombreBanco = string.Empty; 
        public string NombreBanco
        {
            get
            {
                return _NombreBanco;
            }
            set
            {
                _NombreBanco = value;
                Banco.Nombre = value;
                OnPropertyChanged(nameof(NombreBanco));
            }
        }

        private string _NombreCuenta = string.Empty;
        public string NombreCuenta
        {
            get
            {
                return _NombreCuenta;
            }
            set
            {
                _NombreCuenta = value;
                Banco.NombreCuenta = value;
                OnPropertyChanged(nameof(NombreCuenta));
            }
        }

        private string _NumeroCuenta = string.Empty;
        public string NumeroCuenta
        {
            get
            {
                return _NumeroCuenta;
            }
            set
            {
                _NumeroCuenta = value;
                Banco.NumeroCuenta = value;
                OnPropertyChanged(nameof(NumeroCuenta));
            }
        }
    }
}
