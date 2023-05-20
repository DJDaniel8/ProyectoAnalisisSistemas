using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Models
{
    public class Deposito
    {
        public int Id { get; set; }
        public string NoBoleta { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public bool Interno { get; set; }
        public Proveedor Proveedor { get; set; } = new();
        public Banco Banco { get; set; } = new();
        public DateTime Fecha { get; set; }

    }
}
