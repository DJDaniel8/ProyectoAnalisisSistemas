using MultiMarcasAPP.Commands.PersonalViewCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.PersonalViewModels
{
    public class PrivilegiosPersonalViewModel : ViewModelBase
    {
        public PrivilegiosPersonalViewModel(PersonalViewModel viewModelPadre)
        {
            ViewModelPadre = viewModelPadre;
            GuardarCommand = new GuardarPrivilegiosCommand(this);
            CancelarCommand = new CancelarPrivilegiosCommand(this);
        }

        public ICommand GuardarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }

        public PersonalViewModel ViewModelPadre { get; set; }

        private TrabajadorViewModel _Trabajador = new(new());
        public TrabajadorViewModel Trabajador
        {
            get
            {
                return _Trabajador;
            }
            set
            {
                _Trabajador = value;
                CargarPrivilegios();
                OnPropertyChanged(nameof(Trabajador));
            }
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

        #region Propiedades de Privilegios



        
        private bool _Ventas;
        public bool Ventas
        {
            get
            {
                return _Ventas;
            }
            set
            {
                _Ventas = value;
                if (value == false) CambiarPrecio = false;
                OnPropertyChanged(nameof(Ventas));
            }
        }

        private bool _CambiarPrecio;
        public bool CambiarPrecio
        {
            get
            {
                return _CambiarPrecio;
            }
            set
            {
                _CambiarPrecio = value;
                if (value == true) Ventas = true;
                OnPropertyChanged(nameof(CambiarPrecio));
            }
        }

        private bool _Cotizaciones;
        public bool Cotizaciones
        {
            get
            {
                return _Cotizaciones;
            }
            set
            {
                _Cotizaciones = value;
                if (value == false) CambiarPrecio2 = false;
                OnPropertyChanged(nameof(Cotizaciones));
            }
        }

        private bool _CambiarPrecio2;
        public bool CambiarPrecio2
        {
            get
            {
                return _CambiarPrecio2;
            }
            set
            {
                _CambiarPrecio2 = value;
                if (value == true) Cotizaciones = true;
                OnPropertyChanged(nameof(CambiarPrecio2));
            }
        }

        private bool _Productos;
        public bool Productos
        {
            get
            {
                return _Productos;
            }
            set
            {
                _Productos = value;
                if(value == false)
                {
                    CrearProducto = false;
                    EditarProducto = false;
                    EliminarProducto = false;
                }
                OnPropertyChanged(nameof(Productos));
            }
        }

        private bool _CrearProducto;
        public bool CrearProducto
        {
            get
            {
                return _CrearProducto;
            }
            set
            {
                _CrearProducto = value;
                if(value == true)
                {
                    Productos = true;
                }
                OnPropertyChanged(nameof(CrearProducto));
            }
        }

        private bool _EditarProducto;
        public bool EditarProducto
        {
            get
            {
                return _EditarProducto;
            }
            set
            {
                _EditarProducto = value;
                if (value == true)
                {
                    Productos = true;
                }
                OnPropertyChanged(nameof(EditarProducto));
            }
        }

        private bool _EliminarProducto;
        public bool EliminarProducto
        {
            get
            {
                return _EliminarProducto;
            }
            set
            {
                _EliminarProducto = value;
                if (value == true)
                {
                    Productos = true;
                }
                OnPropertyChanged(nameof(EliminarProducto));
            }
        }


        private bool _Stocks;
        public bool Stocks
        {
            get
            {
                return _Stocks;
            }
            set
            {
                _Stocks = value;
                if (value == false) EditarStock = false;
                OnPropertyChanged(nameof(Stocks));
            }
        }

        private bool _EditarStock;
        public bool EditarStock
        {
            get
            {
                return _EditarStock;
            }
            set
            {
                _EditarStock = value;
                if (value == true) Stocks = true;
                OnPropertyChanged(nameof(EditarStock));
            }
        }

        private bool _Facturas;
        public bool Facturas
        {
            get
            {
                return _Facturas;
            }
            set
            {
                _Facturas = value;
                if (value == false) EliminarFactura = false;
                OnPropertyChanged(nameof(Facturas));
            }
        }

        private bool _EliminarFactura;
        public bool EliminarFactura
        {
            get
            {
                return _EliminarFactura;
            }
            set
            {
                _EliminarFactura = value;
                if (value == true) Facturas = true;
                OnPropertyChanged(nameof(EliminarFactura));
            }
        }

        private bool _Personal;
        public bool Personal
        {
            get
            {
                return _Personal;
            }
            set
            {
                _Personal = value;
                if(value == false)
                {
                    PrivilegiosPersonal = false;
                    EditarPersonal = false;
                    CrearPersonal = false;
                    EliminarPersonal = false;
                }
                OnPropertyChanged(nameof(Personal));
            }
        }

        private bool _PrivilegiosPersonal;
        public bool PrivilegiosPersonal
        {
            get
            {
                return _PrivilegiosPersonal;
            }
            set
            {
                _PrivilegiosPersonal = value;
                if (value == true) Personal = true;
                OnPropertyChanged(nameof(PrivilegiosPersonal));
            }
        }

        private bool _CrearPersonal;
        public bool CrearPersonal
        {
            get
            {
                return _CrearPersonal;
            }
            set
            {
                _CrearPersonal = value;
                if (value == true) Personal = true;
                OnPropertyChanged(nameof(CrearPersonal));
            }
        }

        private bool _EditarPersonal;
        public bool EditarPersonal
        {
            get
            {
                return _EditarPersonal;
            }
            set
            {
                _EditarPersonal = value;
                if (value == true) Personal = true;
                OnPropertyChanged(nameof(EditarPersonal));
            }
        }

        private bool _EliminarPersonal;
        public bool EliminarPersonal
        {
            get
            {
                return _EliminarPersonal;
            }
            set
            {
                _EliminarPersonal = value;
                if (value == true) Personal = true;
                OnPropertyChanged(nameof(EliminarPersonal));
            }
        }

        private bool _Utilidades;
        public bool Utilidades
        {
            get
            {
                return _Utilidades;
            }
            set
            {
                _Utilidades = value;
                OnPropertyChanged(nameof(Utilidades));
            }
        }

        private bool _Clientes;
        public bool Clientes
        {
            get
            {
                return _Clientes;
            }
            set
            {
                _Clientes = value;
                if (value == false)
                {
                    CrearCliente = false;
                    EditarCliente = false;
                    EliminarCliente = false;
                }
                OnPropertyChanged(nameof(Clientes));
            }
        }

        private bool _CrearCliente;
        public bool CrearCliente
        {
            get
            {
                return _CrearCliente;
            }
            set
            {
                _CrearCliente = value;
                if (value == true) Clientes = true;
                OnPropertyChanged(nameof(CrearCliente));
            }
        }

        private bool _EditarCliente;
        public bool EditarCliente
        {
            get
            {
                return _EditarCliente;
            }
            set
            {
                _EditarCliente = value;
                if (value == true) Clientes = true;
                OnPropertyChanged(nameof(EditarCliente));
            }
        }

        private bool _EliminarCliente;
        public bool EliminarCliente
        {
            get
            {
                return _EliminarCliente;
            }
            set
            {
                _EliminarCliente = value;
                if (value == true) Clientes = true;
                OnPropertyChanged(nameof(EliminarCliente));
            }
        }

        private bool _Categorias;
        public bool Categorias
        {
            get
            {
                return _Categorias;
            }
            set
            {
                _Categorias = value;
                if(value == false)
                {
                    CrearCategoria = false;
                    EditarCategoria = false;
                    EliminarCategoria = false;
                }
                OnPropertyChanged(nameof(Categorias));
            }
        }

        private bool _CrearCategoria;
        public bool CrearCategoria
        {
            get
            {
                return _CrearCategoria;
            }
            set
            {
                _CrearCategoria = value;
                if (value == true) Categorias = true;
                OnPropertyChanged(nameof(CrearCategoria));
            }
        }

        private bool _EditarCategoria;
        public bool EditarCategoria
        {
            get
            {
                return _EditarCategoria;
            }
            set
            {
                _EditarCategoria = value;
                if (value == true) Categorias = true;
                OnPropertyChanged(nameof(EditarCategoria));
            }
        }

        private bool _EliminarCategoria;
        public bool EliminarCategoria
        {
            get
            {
                return _EliminarCategoria;
            }
            set
            {
                _EliminarCategoria = value;
                if (value == true) Categorias = true;
                OnPropertyChanged(nameof(EliminarCategoria));
            }
        }

        private bool _Proveedores;
        public bool Proveedores
        {
            get
            {
                return _Proveedores;
            }
            set
            {
                _Proveedores = value;
                if(value == false)
                {
                    CrearProveedor = false;
                    EditarProveedor = false;
                    EliminarProveedor = false;
                }
                OnPropertyChanged(nameof(Proveedores));
            }
        }

        private bool _CrearProveedor;
        public bool CrearProveedor
        {
            get
            {
                return _CrearProveedor;
            }
            set
            {
                _CrearProveedor = value;
                if (value == true) Proveedores = true;
                OnPropertyChanged(nameof(CrearProveedor));
            }
        }

        private bool _EditarProveedor;
        public bool EditarProveedor
        {
            get
            {
                return _EditarProveedor;
            }
            set
            {
                _EditarProveedor = value;
                if (value == true) Proveedores = true;
                OnPropertyChanged(nameof(EditarProveedor));
            }
        }

        private bool _EliminarProveedor;
        public bool EliminarProveedor
        {
            get
            {
                return _EliminarProveedor;
            }
            set
            {
                _EliminarProveedor = value;
                if (value == true) Proveedores = true;
                OnPropertyChanged(nameof(EliminarProveedor));
            }
        }

        private bool _Ingresos;
        public bool Ingresos
        {
            get
            {
                return _Ingresos;
            }
            set
            {
                _Ingresos = value;
                if(value == false)
                {
                    CrearIngreso = false;
                    EliminarIngreso = false;
                }
                OnPropertyChanged(nameof(Ingresos));
            }
        }

        private bool _CrearIngreso;
        public bool CrearIngreso
        {
            get
            {
                return _CrearIngreso;
            }
            set
            {
                _CrearIngreso = value;
                if (value == true) Ingresos = true;
                OnPropertyChanged(nameof(CrearIngreso));
            }
        }

        private bool _EliminarIngreso;
        public bool EliminarIngreso
        {
            get
            {
                return _EliminarIngreso;
            }
            set
            {
                _EliminarIngreso = value;
                if (value == true) Ingresos = true;
                OnPropertyChanged(nameof(EliminarIngreso));
            }
        }

        private bool _Caja;
        public bool Caja
        {
            get
            {
                return _Caja;
            }
            set
            {
                _Caja = value;
                if(value == false)
                {
                    ContinuarCaja = false;
                    EliminarCaja = false;
                }
                OnPropertyChanged(nameof(Caja));
            }
        }

        private bool _ContinuarCaja;
        public bool ContinuarCaja
        {
            get
            {
                return _ContinuarCaja;
            }
            set
            {
                _ContinuarCaja = value;
                if (value == true) Caja = true;
                OnPropertyChanged(nameof(ContinuarCaja));
            }
        }

        private bool _EliminarCaja;
        public bool EliminarCaja
        {
            get
            {
                return _EliminarCaja;
            }
            set
            {
                _EliminarCaja = value;
                if (value == true) Caja = true;
                OnPropertyChanged(nameof(EliminarCaja));
            }
        }

        private bool _Contabilidad;
        public bool Contabilidad
        {
            get
            {
                return _Contabilidad;
            }
            set
            {
                _Contabilidad = value;
                OnPropertyChanged(nameof(Contabilidad));
            }
        }

        private bool _Pagos;
        public bool Pagos
        {
            get
            {
                return _Pagos;
            }
            set
            {
                _Pagos = value;
                OnPropertyChanged(nameof(Pagos));
            }
        }

        private bool _Gastos;
        public bool Gastos
        {
            get
            {
                return _Gastos;
            }
            set
            {
                _Gastos = value;
                OnPropertyChanged(nameof(Gastos));
            }
        }

        private bool _InformacionIngresos;
        public bool InformacionIngresos
        {
            get
            {
                return _InformacionIngresos;
            }
            set
            {
                _InformacionIngresos = value;
                OnPropertyChanged(nameof(InformacionIngresos));
            }
        }

        private bool _DepositosInternos;
        public bool DepositosInternos
        {
            get
            {
                return _DepositosInternos;
            }
            set
            {
                _DepositosInternos = value;
                OnPropertyChanged(nameof(DepositosInternos));
            }
        }

        private bool _InyeccionCapital;
        public bool InyeccionCapital
        {
            get
            {
                return _InyeccionCapital;
            }
            set
            {
                _InyeccionCapital = value;
                OnPropertyChanged(nameof(InyeccionCapital));
            }
        }

        #endregion


        public void CargarPrivilegios()
        {
            Ventas = Trabajador.Privilegios.Ventas;
            CambiarPrecio = Trabajador.Privilegios.CambiarPrecio;
            Cotizaciones = Trabajador.Privilegios.Cotizaciones;
            CambiarPrecio2 = Trabajador.Privilegios.CambiarPrecio2;
            Productos = Trabajador.Privilegios.Productos;
            CrearProducto = Trabajador.Privilegios.CrearProducto;
            EditarProducto = Trabajador.Privilegios.EditarProducto;
            EliminarProducto = Trabajador.Privilegios.EliminarProducto;
            Stocks = Trabajador.Privilegios.Stocks;
            EditarStock = Trabajador.Privilegios.EditarStock;
            Facturas = Trabajador.Privilegios.Facturas;
            EliminarFactura = Trabajador.Privilegios.EliminarFactura;
            Personal = Trabajador.Privilegios.Personal;
            PrivilegiosPersonal = Trabajador.Privilegios.PrivilegiosPersonal;
            CrearPersonal = Trabajador.Privilegios.CrearPersonal;
            EditarPersonal = Trabajador.Privilegios.EditarPersonal;
            EliminarPersonal = Trabajador.Privilegios.EliminarPersonal;
            Utilidades = Trabajador.Privilegios.Utilidades;
            Clientes = Trabajador.Privilegios.Clientes;
            CrearCliente = Trabajador.Privilegios.CrearClientes;
            EditarCliente = Trabajador.Privilegios.EditarClientes;
            EliminarCliente = Trabajador.Privilegios.EliminarClientes;
            Categorias = Trabajador.Privilegios.Categorias;
            CrearCategoria = Trabajador.Privilegios.CrearCategoria;
            EditarCategoria = Trabajador.Privilegios.EditarCategoria;
            EliminarCategoria = Trabajador.Privilegios.EliminarCategoria;
            Proveedores = Trabajador.Privilegios.Proveedores;
            CrearProveedor = Trabajador.Privilegios.CrearProveedor;
            EditarProveedor = Trabajador.Privilegios.EditarProveedor;
            EliminarProveedor = Trabajador.Privilegios.EliminarProveedor;
            Ingresos = Trabajador.Privilegios.Ingresos;
            CrearIngreso = Trabajador.Privilegios.CrearIngreso;
            EliminarIngreso = Trabajador.Privilegios.EliminarIngreso;
            Caja = Trabajador.Privilegios.Caja;
            ContinuarCaja = Trabajador.Privilegios.ContinuarCaja;
            EliminarCaja = Trabajador.Privilegios.EliminarCaja;
            Contabilidad = Trabajador.Privilegios.Contabilidad;
            Pagos = Trabajador.Privilegios.Pagos;
            Gastos = Trabajador.Privilegios.Gastos;
            InformacionIngresos = Trabajador.Privilegios.InformacionIngresos;
            DepositosInternos = Trabajador.Privilegios.DepositosInternos;
            InyeccionCapital = Trabajador.Privilegios.InyeccionCapital;
        }

    }
}
