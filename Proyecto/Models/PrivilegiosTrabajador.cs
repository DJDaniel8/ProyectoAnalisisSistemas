using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Models
{
    public class PrivilegiosTrabajador
    {
        public bool Ventas { get; set; }
        public bool CambiarPrecio { get; set; }
        public bool Cotizaciones { get; set; }
        public bool CambiarPrecio2 { get; set; }
        public bool Productos { get; set; }
        public bool CrearProducto { get; set; }
        public bool EditarProducto { get; set; }
        public bool EliminarProducto { get; set; }
        public bool Stocks { get; set; }
        public bool EditarStock { get; set; }
        public bool Facturas { get; set; }
        public bool EliminarFactura { get; set; }
        public bool Personal { get; set; }
        public bool PrivilegiosPersonal { get; set; }
        public bool CrearPersonal { get; set; }
        public bool EditarPersonal { get; set; }
        public bool EliminarPersonal { get; set; }
        public bool Utilidades { get; set; }
        public bool Clientes { get; set; }
        public bool CrearClientes { get; set; }
        public bool EditarClientes { get; set; }
        public bool EliminarClientes { get; set; }
        public bool Categorias { get; set; }
        public bool CrearCategoria { get; set; }
        public bool EditarCategoria { get; set; }
        public bool EliminarCategoria { get; set; }
        public bool Proveedores { get; set; }
        public bool CrearProveedor { get; set; }
        public bool EditarProveedor { get; set; }
        public bool EliminarProveedor { get; set; }
        public bool Ingresos { get; set; }
        public bool CrearIngreso { get; set; }
        public bool EliminarIngreso { get; set; }
        public bool Caja { get; set; }
        public bool ContinuarCaja { get; set; }
        public bool EliminarCaja { get; set; }
        public bool Contabilidad { get; set; }
        public bool Pagos { get; set; }
        public bool Gastos { get; set; }
        public bool InformacionIngresos { get; set; }
        public bool DepositosInternos { get; set; }
        public bool InyeccionCapital { get; set; }
    }
}
