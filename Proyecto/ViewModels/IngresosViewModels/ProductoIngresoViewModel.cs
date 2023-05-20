using MultiMarcasAPP.Commands.IngresosViewCommands;
using MultiMarcasAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.IngresosViewModels
{
    public class ProductoIngresoViewModel : ViewModelBase
    {
        public ProductoIngresoViewModel(ProductoIngreso productoIngreso, NuevoIngresoViewModel? nuevoIngresoViewModel)
        {
            ProductoIngreso = productoIngreso;
            if(nuevoIngresoViewModel != null)
            {
                QuitarProductoCommand = new QuitarProductoCommand(nuevoIngresoViewModel);
            }
            Producto = ProductoIngreso.Producto;
            Cantidad = ProductoIngreso.Cantidad;
            PrecioCompra = productoIngreso.PrecioCompra;
        }

        public ICommand? QuitarProductoCommand { get; }

        public ProductoIngreso ProductoIngreso { get; set; }

        private int _Id;
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int IngresoId { get; set; }

        public Producto Producto { get; set; }

        private int _Cantidad;
        public int Cantidad
        {
            get
            {
                return _Cantidad;
            }
            set
            {
                if(value >= 0) _Cantidad = value;
                OnPropertyChanged(nameof(Cantidad));
                Total = (Cantidad * PrecioCompra) - Descuento;
            }
        }

        private decimal _PrecioCompra;
        public decimal PrecioCompra
        {
            get
            {
                return _PrecioCompra;
            }
            set
            {
                if(value > 0) _PrecioCompra = value;
                OnPropertyChanged(nameof(PrecioCompra));
                Total = (Cantidad * PrecioCompra) - Descuento;
            }
        }

        private decimal _Descuento;
        public decimal Descuento
        {
            get
            {
                return _Descuento;
            }
            set
            {
                if(value < Total) _Descuento = value;
                OnPropertyChanged(nameof(Descuento));
                Total = (Cantidad * PrecioCompra) - Descuento;
            }
        }

        private decimal _Total;
        public decimal Total
        {
            get
            {
                return _Total;
            }
            set
            {
                _Total = value;
                OnPropertyChanged(nameof(Total));
            }
        }


        private decimal _PrecioVentaMinimo;
        public decimal PrecioVentaMinimo
        {
            get
            {
                return _PrecioVentaMinimo;
            }
            set
            {
                if(Cantidad != 0 && PrecioCompra - (Descuento/Cantidad) < value) _PrecioVentaMinimo = value;
                else if(PrecioCompra < value) _PrecioVentaMinimo = value;
                OnPropertyChanged(nameof(PrecioVentaMinimo));
            }
        }

        private decimal _PrecioVenta;
        public decimal PrecioVenta
        {
            get
            {
                return _PrecioVenta;
            }
            set
            {
                if(PrecioVentaMinimo <= value) _PrecioVenta = value;
                OnPropertyChanged(nameof(PrecioVenta));
            }
        }
    }
}
