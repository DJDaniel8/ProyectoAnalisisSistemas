using MultiMarcasAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.ViewModels.ProveedoresViewModels
{
    public class ProveedorViewModel : ViewModelBase
    {
        public ProveedorViewModel()
        {
            Proveedor = new();
        }

        public ProveedorViewModel(Proveedor proveedor)
        {
            Proveedor = proveedor;
            _Nombre = Proveedor.RazonSocial;
            _Direccion = Proveedor.Direccion;
            _Telefono = Proveedor.Telefono;
        }

        public Proveedor Proveedor { get; set; }

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
                Proveedor.RazonSocial = value;
                OnPropertyChanged(nameof(Nombre));
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
                Proveedor.Direccion = value;
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
                Proveedor.Telefono = value;
                OnPropertyChanged(nameof(Telefono));
            }
        }
    }
}
