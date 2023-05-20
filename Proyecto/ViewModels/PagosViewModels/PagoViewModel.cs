using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.ViewModels.PagosViewModels
{
    public class PagoViewModel : ViewModelBase
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

        private DateTime _Fecha;
        public DateTime Fecha
        {
            get
            {
                return _Fecha;
            }
            set
            {
                _Fecha = value;
                OnPropertyChanged(nameof(Fecha));
            }
        }

        private decimal _Monto;
        public decimal Monto
        {
            get
            {
                return _Monto;
            }
            set
            {
                _Monto = value;
                OnPropertyChanged(nameof(Monto));
            }
        }

        private string _NoDoc = string.Empty;
        public string NoDoc
        {
            get
            {
                return _NoDoc;
            }
            set
            {
                _NoDoc = value;
                OnPropertyChanged(nameof(NoDoc));
            }
        }

        private TipoPagoViewModel _TipoPago = new();
        public TipoPagoViewModel TipoPago
        {
            get
            {
                return _TipoPago;
            }
            set
            {
                _TipoPago = value;
                OnPropertyChanged(nameof(TipoPago));
            }
        }
    }
}
