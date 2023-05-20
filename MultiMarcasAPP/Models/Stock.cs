using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PrecioMinimo { get; set; }
        public int ProductoId { get; set; }
        public int StockIdPadre { get; set; }
        public bool Auditorado { get; set; }

    }
}
