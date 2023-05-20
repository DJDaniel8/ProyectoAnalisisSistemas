using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Models
{
    public class ReporteInventarioProducto
    {
        public int StockId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal PrecioCompra { get; set; }
        public int Stock { get; set; }
        public decimal Total { get; set; }
    }
}
