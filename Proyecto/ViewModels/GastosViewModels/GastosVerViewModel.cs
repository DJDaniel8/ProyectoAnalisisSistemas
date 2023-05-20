using MultiMarcasAPP.Services.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.ViewModels.GastosViewModels
{
    public class GastosVerViewModel : ViewModelBase
    {
        public GastosVerViewModel()
        {
            _Gastos = new();
            CargarGastos();
        }

        public ObservableCollection<GastoViewModel> _Gastos;
        public IEnumerable<GastoViewModel> Gastos => _Gastos;

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
                CargarGastos();
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
                CargarGastos();
            }
        }



        public void CargarGastos()
        {
            _Gastos.Clear();
            foreach (var item in GastoDAO.GetGastosByDates(Fecha1,Fecha2))
            {
                _Gastos.Add(item);
            }
        }
    }
}
