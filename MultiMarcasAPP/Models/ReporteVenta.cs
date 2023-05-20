using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Models
{
    public class ReporteVenta
    {
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int TipoClase { get; set; }
    }
}
