using MultiMarcasAPP.Models;
using MultiMarcasAPP.Services.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.ViewModels.IngresosViewModels
{
    public class TablasIngresosViewModel : ViewModelBase
    {
        public TablasIngresosViewModel()
        {
            _ingresos = new();
            _productosIngrsos = new();
        }

        private ObservableCollection<Ingreso> _ingresos;
        public IEnumerable<Ingreso> Ingresos => _ingresos;

        private ObservableCollection<ProductoIngreso> _productosIngrsos;
        public IEnumerable<ProductoIngreso> ProductosIngrsos => _productosIngrsos;

        private Ingreso _SelectedIngreso;
        public Ingreso SelectedIngreso
        {
            get
            {
                return _SelectedIngreso;
            }
            set
            {
                _SelectedIngreso = value;
                if (value != null) CargarProductosIngreso();
                else _productosIngrsos.Clear();
                OnPropertyChanged(nameof(SelectedIngreso));
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

        public void CargarIngresos(string tipo, DateTime fecha1, DateTime fecha2)
        {
            _ingresos.Clear();
            List<Ingreso> lista;
            if (tipo == "Ultimos 10") lista = IngresoDAO.Get10();
            else if (tipo == "Ultimos 50") lista = IngresoDAO.Get50();
            else lista = IngresoDAO.GetbyDate(fecha1, fecha2);
            foreach (var item in lista)
            {
                _ingresos.Add(item);
            }
        }

        public void CargarProductosIngreso()
        {
            _productosIngrsos.Clear();
            foreach (var item in IngresoDAO.GetProductosIngresos(SelectedIngreso.Id))
            {
                _productosIngrsos.Add(item);
            }
        }
    }
}
