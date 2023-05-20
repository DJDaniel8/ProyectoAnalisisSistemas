using MultiMarcasAPP.Services.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.ViewModels.UtilidadesViewModels
{
    public class UtilidadesPorFechaViewModel : ViewModelBase
    {
        public UtilidadesPorFechaViewModel()
        {
            _Utilidades = new();
        }

        private ObservableCollection<UtilidadFecha> _Utilidades;
        public IEnumerable<UtilidadFecha> Utilidades => _Utilidades;

        private UtilidadFecha _SelectedUtilidad;
        public UtilidadFecha SelectedUtilidad
        {
            get
            {
                return _SelectedUtilidad;
            }
            set
            {
                _SelectedUtilidad = value;
                OnPropertyChanged(nameof(SelectedUtilidad));
            }
        }

        private Visibility _ControlVisibility = Visibility.Collapsed;
        public Visibility ControlVisibility
        {
            get
            {
                return _ControlVisibility;
            }
            set
            {
                _ControlVisibility = value;
                OnPropertyChanged(nameof(ControlVisibility));
            }
        }

        public void CargarUtilidad(DateTime inicio, DateTime Fin)
        {
            _Utilidades.Clear();
            _Utilidades.Add(UtilidadesDAO.GetUtilidadPorFecha(inicio,Fin));
        }
    }

    public struct UtilidadFecha
    {
        public string Fecha { get; set; }
        public decimal Total { get; set; }
        public decimal Utilidad { get; set; }
    }
}
