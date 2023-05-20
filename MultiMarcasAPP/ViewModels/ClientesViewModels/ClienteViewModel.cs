using MultiMarcasAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.ViewModels.ClientesViewModels
{
    public class ClienteViewModel : ViewModelBase
    {
        public ClienteViewModel(Cliente cliente)
        {
            this.Cliente = cliente;
            Nombre = cliente.Nombre;
            Apellido = cliente.Apellido;
            Sexo = cliente.Sexo;
            Nit = cliente.Nit;
            Direccion = cliente.Direccion;
            Telefono = cliente.Telefono;
            Email = cliente.Email;
        }

        public Cliente Cliente { get; set; }

        private string _Nombre = string.Empty;
        public string Nombre
        {
            get
            {
                return _Nombre;
            }
            set
            {
                _Nombre = value;
                Cliente.Nombre = value;
                OnPropertyChanged(nameof(Nombre));
            }
        }

        private string _Apellido = string.Empty;
        public string Apellido
        {
            get
            {
                return _Apellido;
            }
            set
            {
                _Apellido = value;
                Cliente.Apellido = value;
                OnPropertyChanged(nameof(Apellido));
            }
        }

        private string _Sexo = string.Empty;
        public string Sexo
        {
            get
            {
                return _Sexo;
            }
            set
            {
                _Sexo = value;
                Cliente.Sexo = value;
                OnPropertyChanged(nameof(Sexo));
            }
        }

        private string _Nit = string.Empty;
        public string Nit
        {
            get
            {
                return _Nit;
            }
            set
            {
                _Nit = value;
                Cliente.Nit = value;
                OnPropertyChanged(nameof(Nit));
            }
        }

        private string _Direccion = string.Empty;
        public string Direccion
        {
            get
            {
                return _Direccion;
            }
            set
            {
                _Direccion = value;
                Cliente.Direccion = value;
                OnPropertyChanged(nameof(Direccion));
            }
        }

        private string _Telefono = string.Empty;
        public string Telefono
        {
            get
            {
                return _Telefono;
            }
            set
            {
                _Telefono = value;
                Cliente.Telefono = value;
                OnPropertyChanged(nameof(Telefono));
            }
        }

        private string _Email = string.Empty;
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
                Cliente.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
    }
}
