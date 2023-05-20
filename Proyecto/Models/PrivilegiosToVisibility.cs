using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Models
{
    public static class PrivilegiosToVisibility
    {
        public static Visibility Ventas { get; set; }
        public static Visibility CambiarPrecio { get; set; }
        public static Visibility Cotizaciones { get; set; }
        public static Visibility CambiarPrecio2 { get; set; }
        public static Visibility Productos { get; set; }
        public static Visibility CrearProducto { get; set; }
        public static Visibility EditarProducto { get; set; }
        public static Visibility EliminarProducto { get; set; }
        public static Visibility Stocks { get; set; }
        public static Visibility EditarStock { get; set; }
        public static Visibility Facturas { get; set; }
        public static Visibility EliminarFactura { get; set; }
        public static Visibility Personal { get; set; }
        public static Visibility PrivilegiosPersonal { get; set; }
        public static Visibility CrearPersonal { get; set; }
        public static Visibility EditarPersonal { get; set; }
        public static Visibility EliminarPersonal { get; set; }
        public static Visibility Utilidades { get; set; }
        public static Visibility Clientes { get; set; }
        public static Visibility CrearClientes { get; set; }
        public static Visibility EditarClientes { get; set; }
        public static Visibility EliminarClientes { get; set; }
        public static Visibility Categorias { get; set; }
        public static Visibility CrearCategoria { get; set; }
        public static Visibility EditarCategoria { get; set; }
        public static Visibility EliminarCategoria { get; set; }
        public static Visibility Proveedores { get; set; }
        public static Visibility CrearProveedor { get; set; }
        public static Visibility EditarProveedor { get; set; }
        public static Visibility EliminarProveedor { get; set; }
        public static Visibility Ingresos { get; set; }
        public static Visibility CrearIngreso { get; set; }
        public static Visibility EliminarIngreso { get; set; }
        public static Visibility Caja { get; set; }
        public static Visibility ContinuarCaja { get; set; }
        public static Visibility EliminarCaja { get; set; }
        public static Visibility Contabilidad { get; set; }
        public static Visibility Pagos { get; set; }
        public static Visibility Gastos { get; set; }
        public static Visibility InformacionIngresos { get; set; }
        public static Visibility DepositosInternos { get; set; }
        public static Visibility InyeccionCapital { get; set; }

        public static void LoadFromPrivilegios()
        {
            Ventas = CurrentEmploye.PrivilegiosTrabajador.Ventas == true ? Visibility.Visible : Visibility.Collapsed;
             CambiarPrecio = CurrentEmploye.PrivilegiosTrabajador.CambiarPrecio == true ? Visibility.Visible : Visibility.Collapsed;
            Cotizaciones = CurrentEmploye.PrivilegiosTrabajador.Cotizaciones == true ? Visibility.Visible : Visibility.Collapsed;
            CambiarPrecio2 = CurrentEmploye.PrivilegiosTrabajador.CambiarPrecio2 == true ? Visibility.Visible : Visibility.Collapsed;
            Productos = CurrentEmploye.PrivilegiosTrabajador.Productos == true ? Visibility.Visible : Visibility.Collapsed;
            CrearProducto = CurrentEmploye.PrivilegiosTrabajador.CrearProducto == true ? Visibility.Visible : Visibility.Collapsed;
            EditarProducto = CurrentEmploye.PrivilegiosTrabajador.EditarProducto == true ? Visibility.Visible : Visibility.Collapsed;
            EliminarProducto = CurrentEmploye.PrivilegiosTrabajador.EliminarProducto == true ? Visibility.Visible : Visibility.Collapsed;
            Stocks = CurrentEmploye.PrivilegiosTrabajador.Stocks == true ? Visibility.Visible : Visibility.Collapsed;
            EditarStock = CurrentEmploye.PrivilegiosTrabajador.EditarStock == true ? Visibility.Visible : Visibility.Collapsed;
            Facturas = CurrentEmploye.PrivilegiosTrabajador.Facturas == true ? Visibility.Visible : Visibility.Collapsed;
            EliminarFactura = CurrentEmploye.PrivilegiosTrabajador.EliminarFactura == true ? Visibility.Visible : Visibility.Collapsed;
            Personal = CurrentEmploye.PrivilegiosTrabajador.Personal == true ? Visibility.Visible : Visibility.Collapsed;
            PrivilegiosPersonal = CurrentEmploye.PrivilegiosTrabajador.PrivilegiosPersonal == true ? Visibility.Visible : Visibility.Collapsed;
            CrearPersonal = CurrentEmploye.PrivilegiosTrabajador.CrearPersonal == true ? Visibility.Visible : Visibility.Collapsed;
            EliminarPersonal = CurrentEmploye.PrivilegiosTrabajador.EliminarPersonal == true ? Visibility.Visible : Visibility.Collapsed;
            Utilidades = CurrentEmploye.PrivilegiosTrabajador.Utilidades == true ? Visibility.Visible : Visibility.Collapsed;
            Clientes = CurrentEmploye.PrivilegiosTrabajador.Clientes == true ? Visibility.Visible : Visibility.Collapsed;
            CrearClientes = CurrentEmploye.PrivilegiosTrabajador.CrearClientes == true ? Visibility.Visible : Visibility.Collapsed;
            EditarClientes = CurrentEmploye.PrivilegiosTrabajador.EditarClientes == true ? Visibility.Visible : Visibility.Collapsed;
            EliminarClientes = CurrentEmploye.PrivilegiosTrabajador.EliminarClientes == true ? Visibility.Visible : Visibility.Collapsed;
            Categorias = CurrentEmploye.PrivilegiosTrabajador.Categorias == true ? Visibility.Visible : Visibility.Collapsed;
            CrearCategoria = CurrentEmploye.PrivilegiosTrabajador.CrearCategoria == true ? Visibility.Visible : Visibility.Collapsed;
            EditarCategoria = CurrentEmploye.PrivilegiosTrabajador.EditarCategoria == true ? Visibility.Visible : Visibility.Collapsed;
            EliminarCategoria = CurrentEmploye.PrivilegiosTrabajador.EliminarCategoria == true ? Visibility.Visible : Visibility.Collapsed;
            Proveedores = CurrentEmploye.PrivilegiosTrabajador.Proveedores == true ? Visibility.Visible : Visibility.Collapsed;
            CrearProveedor = CurrentEmploye.PrivilegiosTrabajador.CrearProveedor == true ? Visibility.Visible : Visibility.Collapsed;
            EditarProveedor = CurrentEmploye.PrivilegiosTrabajador.EditarProveedor == true ? Visibility.Visible : Visibility.Collapsed;
            EliminarProveedor = CurrentEmploye.PrivilegiosTrabajador.EliminarProveedor == true ? Visibility.Visible : Visibility.Collapsed;
            Ingresos = CurrentEmploye.PrivilegiosTrabajador.Ingresos == true ? Visibility.Visible : Visibility.Collapsed;
            CrearIngreso = CurrentEmploye.PrivilegiosTrabajador.CrearIngreso == true ? Visibility.Visible : Visibility.Collapsed;
            EliminarIngreso = CurrentEmploye.PrivilegiosTrabajador.EliminarIngreso == true ? Visibility.Visible : Visibility.Collapsed;
            Caja = CurrentEmploye.PrivilegiosTrabajador.Caja == true ? Visibility.Visible : Visibility.Collapsed;
            ContinuarCaja = CurrentEmploye.PrivilegiosTrabajador.ContinuarCaja == true ? Visibility.Visible : Visibility.Collapsed;
            EliminarCaja = CurrentEmploye.PrivilegiosTrabajador.EliminarCaja == true ? Visibility.Visible : Visibility.Collapsed;
            Contabilidad = CurrentEmploye.PrivilegiosTrabajador.Contabilidad == true ? Visibility.Visible : Visibility.Collapsed;
            Pagos = CurrentEmploye.PrivilegiosTrabajador.Pagos == true ? Visibility.Visible : Visibility.Collapsed;
            Gastos = CurrentEmploye.PrivilegiosTrabajador.Gastos == true ? Visibility.Visible : Visibility.Collapsed;
            InformacionIngresos = CurrentEmploye.PrivilegiosTrabajador.InformacionIngresos == true ? Visibility.Visible : Visibility.Collapsed;
            DepositosInternos = CurrentEmploye.PrivilegiosTrabajador.DepositosInternos == true ? Visibility.Visible : Visibility.Collapsed;
            InyeccionCapital = CurrentEmploye.PrivilegiosTrabajador.InyeccionCapital == true ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
