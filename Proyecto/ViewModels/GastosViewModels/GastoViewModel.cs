using MultiMarcasAPP.ViewModels.PagosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.ViewModels.GastosViewModels
{
    public class GastoViewModel : ViewModelBase
    {
        private int _Id;
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _Descripcion = string.Empty;
        public string Descripcion
        {
            get
            {
                return _Descripcion;
            }
            set
            {
                _Descripcion = value;
                OnPropertyChanged(nameof(Descripcion));
            }
        }

        private PagoViewModel _Pago = new();
        public PagoViewModel Pago
        {
            get
            {
                return _Pago;
            }
            set
            {
                _Pago = value;
                OnPropertyChanged(nameof(Pago));
            }
        }

        private TipoGastoViewModel _TipoGasto = new();
        public TipoGastoViewModel TipoGasto
        {
            get
            {
                return _TipoGasto;
            }
            set
            {
                _TipoGasto = value;
                OnPropertyChanged(nameof(TipoGasto));
            }
        }
    }
}
