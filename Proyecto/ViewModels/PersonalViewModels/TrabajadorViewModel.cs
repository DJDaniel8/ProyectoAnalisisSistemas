using MultiMarcasAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.ViewModels.PersonalViewModels
{
    public class TrabajadorViewModel : ViewModelBase
    {
        public TrabajadorViewModel(Trabajador trabajador)
        {
            this.trabajador = trabajador;
            Nombre = trabajador.Nombre;
            Apellido = trabajador.Apellido;
            Sexo = trabajador.Sexo;
            Puesto = trabajador.Puesto;
            Direccion = trabajador.Direccion;
            Telefono = trabajador.Telefono;
            Email = trabajador.Email;
            Usuario = trabajador.Usuario;
            Sueldo = trabajador.Sueldo;
        }

        public Trabajador trabajador { get; set; }

        public PrivilegiosTrabajador Privilegios { get; set; } = new();

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
                trabajador.Nombre = value;
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
                trabajador.Apellido = value;
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
                trabajador.Sexo = value;
                OnPropertyChanged(nameof(Sexo));
            }
        }

        private string _Puesto = string.Empty;
        public string Puesto
        {
            get
            {
                return _Puesto;
            }
            set
            {
                _Puesto = value;
                trabajador.Puesto = value;
                OnPropertyChanged(nameof(Puesto));
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
                trabajador.Direccion = value;
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
                trabajador.Telefono = value;
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
                trabajador.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _Usuario = string.Empty;
        public string Usuario
        {
            get
            {
                return _Usuario;
            }
            set
            {
                _Usuario = value;
                trabajador.Usuario = value;
                OnPropertyChanged(nameof(Usuario));
            }
        }

        private decimal _Sueldo;
        public decimal Sueldo
        {
            get
            {
                return _Sueldo;
            }
            set
            {
                _Sueldo = value;
                trabajador.Sueldo = value;
                OnPropertyChanged(nameof(Sueldo));
            }
        }
    }
}
