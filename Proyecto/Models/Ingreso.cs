using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Models
{
    public class Ingreso
    {
        public Ingreso(Proveedor proveedor)
        {
            Proveedor = proveedor;
        }


        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int TrabajadorId { get; set; }
        public Proveedor Proveedor { get; set; }
        public bool Auditorado { get; set; }
        public decimal TotalIngreso { get; set; }
    }
}
