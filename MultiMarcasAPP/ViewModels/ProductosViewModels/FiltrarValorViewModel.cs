using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.ViewModels.ProductosViewModels
{
    public class FiltrarValorViewModel : ViewModelBase
    {
        public FiltrarValorViewModel()
        {

        }

        public FiltrarValorViewModel(int item1, string item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public int Item1 { get; set; }
        public string Item2 { get; set; } = string.Empty;
    }
}
