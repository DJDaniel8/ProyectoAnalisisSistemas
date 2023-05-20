using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Models
{
    public class PagoProveedor
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string NoDocFactura { get; set; }
        public string NoDocPago { get; set; }
        public string TipoPago { get; set; }
        public decimal Monto { get; set; }
        public Proveedor proveedor { get; set; }
    }
}
