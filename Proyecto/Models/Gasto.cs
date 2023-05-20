using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Models
{
    public class Gasto
    {
        public int ID { get; set; }
        public DateTime Fecha { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public bool EsDeducible { get; set; }
        public bool EsEfectivo { get; set; }
    }
}
