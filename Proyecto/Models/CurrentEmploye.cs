using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Models
{
    public static class CurrentEmploye
    {
        public static Trabajador Trabajador { get; set; } = new();
        public static PrivilegiosTrabajador PrivilegiosTrabajador { get; set; } = new();
    }
}
