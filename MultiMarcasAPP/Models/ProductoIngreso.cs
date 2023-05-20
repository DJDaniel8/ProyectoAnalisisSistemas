using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Models
{
    public class ProductoIngreso
    {
        public ProductoIngreso(Producto producto)
        {
            Producto = producto;
        }

        public int Id { get; set; }
        public int IngresoId { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVentaMinimo { get; set; }
        public decimal PrecioVenta { get; set; }
        public int stockId { get; set; }
    }
}
