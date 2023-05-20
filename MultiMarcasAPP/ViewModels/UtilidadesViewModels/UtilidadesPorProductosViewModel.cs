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
    public class UtilidadesPorProductosViewModel : ViewModelBase
    {
        public UtilidadesPorProductosViewModel()
        {
            _Utilidades = new();
        }

        private ObservableCollection<UtilidadProducto> _Utilidades;
        public IEnumerable<UtilidadProducto> Utilidades => _Utilidades;

        private UtilidadProducto _SelectedUtilidad;
        public UtilidadProducto SelectedUtilidad
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

        public void CargarUtilidades(int tipo, int id)
        {
            _Utilidades.Clear();
            List<UtilidadProducto> lista;
            if (tipo == 1) lista = UtilidadesDAO.GetUtilidadesPorProductosTop10();
            else if (tipo == 2) lista = UtilidadesDAO.GetUtilidadesPorProductosTop100();
            else if (tipo == 3) lista = UtilidadesDAO.GetUtilidadesPorProductosTopByCode(id);
            else lista = UtilidadesDAO.GetUtilidadesPorProductosTop10();

            foreach (var item in lista)
            {
                _Utilidades.Add(item);
            }
        }

    }
    public struct UtilidadProducto
    {
        public Producto producto { get; set; }
        public decimal Total { get; set; }
        public decimal Utilidad { get; set; }
    }
}
