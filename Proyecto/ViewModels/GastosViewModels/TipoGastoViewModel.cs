using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.ViewModels.GastosViewModels
{
    public class TipoGastoViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Deducible { get; set; }
    }
}
