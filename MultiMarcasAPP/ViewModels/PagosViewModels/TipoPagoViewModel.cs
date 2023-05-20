using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.ViewModels.PagosViewModels
{
    public class TipoPagoViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool SaleCaja { get; set; }
    }
}
