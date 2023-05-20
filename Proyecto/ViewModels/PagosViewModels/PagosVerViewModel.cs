using MultiMarcasAPP.Services.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.ViewModels.PagosViewModels
{
    public class PagosVerViewModel : ViewModelBase
    {
        public PagosVerViewModel()
        {
            _Pagos = new();
            CargarPagos();
        }

        public ObservableCollection<PagoViewModel> _Pagos;
        public IEnumerable<PagoViewModel> Pagos => _Pagos;

        private bool _MainButtonNavigationBarVisibility = true;
        public bool MainButtonNavigationBarVisibility
        {
            get
            {
                return _MainButtonNavigationBarVisibility;
            }
            set
            {
                _MainButtonNavigationBarVisibility = value;
                OnPropertyChanged(nameof(MainButtonNavigationBarVisibility));
            }
        }

        private bool _SecundaryButtonNavigationBarVisiblity = false;
        public bool SecundaryButtonNavigationBarVisiblity
        {
            get
            {
                return _SecundaryButtonNavigationBarVisiblity;
            }
            set
            {
                _SecundaryButtonNavigationBarVisiblity = value;
                OnPropertyChanged(nameof(SecundaryButtonNavigationBarVisiblity));
            }
        }

        private DateTime _Fecha1 = DateTime.Now;
        public DateTime Fecha1
        {
            get
            {
                return _Fecha1;
            }
            set
            {
                _Fecha1 = value;
                OnPropertyChanged(nameof(Fecha1));
                CargarPagos();
            }
        }

        private DateTime _Fecha2 = DateTime.Now;
        public DateTime Fecha2
        {
            get
            {
                return _Fecha2;
            }
            set
            {
                _Fecha2 = value;
                OnPropertyChanged(nameof(Fecha2));
                CargarPagos();
            }
        }



        public void CargarPagos()
        {
            _Pagos.Clear();
            foreach (var item in PagosDAO.GetByDate(Fecha1, Fecha2))
            {
                _Pagos.Add(item);
            }
        }
    }
}
