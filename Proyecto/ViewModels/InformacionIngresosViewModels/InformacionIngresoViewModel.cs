using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.ViewModels.InformacionIngresosViewModels
{
    public class InformacionIngresoViewModel : ViewModelBase
    {
        public int Id { get; set; }

        public string NumeroDocumento { get; set; } = string.Empty;

        public decimal TotalIngreso { get; set; }

        public int IngresoId { get; set; }

        public bool Pagado { get; set; } = false;

        public DateTime Fecha { get; set; }

        public string Proveedor { get; set; } = string.Empty;

        public decimal TotalPagado { get; set; } 
    }
}
