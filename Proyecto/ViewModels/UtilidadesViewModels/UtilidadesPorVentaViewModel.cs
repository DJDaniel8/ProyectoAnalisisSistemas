using MultiMarcasAPP.Models;
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
    public class UtilidadesPorVentaViewModel : ViewModelBase
    {
        public UtilidadesPorVentaViewModel()
        {
            _Utilidades = new();
            CargarUtilidades(1,null);
        }

        private ObservableCollection<Tuple<Venta,decimal>> _Utilidades;
        public IEnumerable<Tuple<Venta, decimal>> Utilidades => _Utilidades;

        private Tuple<Venta, decimal> _SelectedUtilidad;    
        public Tuple<Venta, decimal> SelectedUtilidad
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

        private Visibility _ControlVisibility = Visibility.Visible;
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

        public void CargarUtilidades(int tipo, DateTime? date)
        {
            _Utilidades.Clear();
            List<Tuple<Venta, decimal>> lista;
            if (tipo == 1) lista = UtilidadesDAO.GetUtilidadesPorVenta();
            else if (tipo == 2) lista = UtilidadesDAO.GetUtilidadesPorVentaToday();
            else if (tipo == 3) lista = UtilidadesDAO.GetUtilidadesPorVentaDate((DateTime)date);
            else lista = UtilidadesDAO.GetUtilidadesPorVenta();
            foreach (var item in lista)
            {
                _Utilidades.Add(item);
            }
        }
    }
}
