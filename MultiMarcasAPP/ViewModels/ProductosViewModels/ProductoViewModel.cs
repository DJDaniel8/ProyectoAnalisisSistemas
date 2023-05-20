using MultiMarcasAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.ViewModels.ProductosViewModels
{
    public class ProductoViewModel : ViewModelBase
    {
        public ProductoViewModel()
        {
            Producto = new();
        }

        public ProductoViewModel(Producto producto)
        {
            Producto = producto;
            _Codigo = producto.Codigo;
            _Nombre = producto.Nombre;
            _Categoria = producto.Categorias;
            _Proveedor = producto.Proveedores;
            _Descripcion = producto.Descripcion;
            _Multiplicador = producto.Multiplicador;
            _Padre = producto.CodigoPadre;
            _Stock = producto.Stock;
        }

        public Producto Producto { get; set; }

        private string _Codigo = string.Empty;
        public string Codigo
        {
            get
            {
                return _Codigo;
            }
            set
            {
                _Codigo = value;
                this.Producto.Codigo = value;
                OnPropertyChanged(nameof(Codigo));
            }
        }

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
                this.Producto.Nombre = value;
                OnPropertyChanged(nameof(Nombre));
            }
        }

        private string _Categoria = string.Empty;
        public string Categoria
        {
            get
            {
                return _Categoria;
            }
            set
            {
                _Categoria = value;
                OnPropertyChanged(nameof(Categoria));
            }
        }

        private string _Proveedor = string.Empty;
        public string Proveedor
        {
            get
            {
                return _Proveedor;
            }
            set
            {
                _Proveedor = value;
                OnPropertyChanged(nameof(Proveedor));
            }
        }

        private string _Padre = string.Empty;
        public string Padre
        {
            get
            {
                return _Padre;
            }
            set
            {
                _Padre = value;
                OnPropertyChanged(nameof(Padre));
            }
        }

        private string _Descripcion = string.Empty;
        public string Descripcion
        {
            get
            {
                return _Descripcion;
            }
            set
            {
                _Descripcion = value;
                this.Producto.Descripcion = value;
                OnPropertyChanged(nameof(Descripcion));
            }
        }

        private int _Multiplicador;
        public int Multiplicador
        {
            get
            {
                return _Multiplicador;
            }
            set
            {
                _Multiplicador = value;
                Producto.Multiplicador = value;
                OnPropertyChanged(nameof(Multiplicador));
            }
        }

        private decimal _Stock;
        public decimal Stock
        {
            get
            {
                return _Stock;
            }
            set
            {
                _Stock = value;
                OnPropertyChanged(nameof(Stock));
            }
        }
    }
}
