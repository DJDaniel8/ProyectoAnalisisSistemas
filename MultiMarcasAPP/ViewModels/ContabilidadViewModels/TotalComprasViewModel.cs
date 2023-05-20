using MultiMarcasAPP.Models;
using MultiMarcasAPP.Services.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.ViewModels.ContabilidadViewModels
{
    public class TotalComprasViewModel : ViewModelBase
    {
        public TotalComprasViewModel()
        {
            _Meses = new();
            _ingresos = new();
            Años = TotalComprasDAO.GetAños();
        }

        public ObservableCollection<TotalComprasMensual> _Meses;
        public IEnumerable<TotalComprasMensual> Meses => _Meses;
        public IEnumerable<int> Años { get; set; }


        private ObservableCollection<Ingreso> _ingresos;
        public IEnumerable<Ingreso> Ingresos => _ingresos;
        private TotalComprasMensual _SelectedTotal = new();
        public TotalComprasMensual SelectedTotal
        {
            get
            {
                return _SelectedTotal;
            }
            set
            {
                _SelectedTotal = value;
                OnPropertyChanged(nameof(SelectedTotal));
                CargarIngresos();
            }
        }

        private int _SelectedAño;
        public int SelectedAño
        {
            get
            {
                return _SelectedAño;
            }
            set
            {
                _SelectedAño = value;
                OnPropertyChanged(nameof(SelectedAño));
                CargarTotales();
            }
        }

        public void CargarTotales()
        {
            if(SelectedAño > 0)
            {
                _Meses.Clear();
                foreach (var item in TotalComprasDAO.Get(SelectedAño))
                {
                    _Meses.Add(item);
                }
            }
        }

        public void CargarIngresos()
        {
            
            if(SelectedTotal != null && SelectedTotal.Mes > 0)
            {
                _ingresos.Clear();
                foreach (var item in IngresoDAO.GetByDate(SelectedTotal.Mes, SelectedTotal.Año))
                {
                    _ingresos.Add(item);
                }
            }
        }
    }
}
