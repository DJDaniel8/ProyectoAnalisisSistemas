using MultiMarcasAPP.Commands.InformacionIngresosCommands.VerProductosCommands;
using MultiMarcasAPP.Models;
using MultiMarcasAPP.Services.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.InformacionIngresosViewModels
{
    public class InformacionIngresosVerProductosViewModel : ViewModelBase
    {
        public InformacionIngresosVerProductosViewModel(InformacionIngresosViewModel viewModelPadre)
        {
            ViewModelPadre = viewModelPadre;
            _productosIngrsos = new();
            RegresarCommand = new RegresarCommand(this);
        }

        public ICommand RegresarCommand { get; set; }

        public InformacionIngresosViewModel ViewModelPadre { get; set; }

        private ObservableCollection<ProductoIngreso> _productosIngrsos;
        public IEnumerable<ProductoIngreso> ProductosIngrsos => _productosIngrsos;


        private Visibility _Visibility;
        public Visibility Visibility
        {
            get
            {
                return _Visibility;
            }
            set
            {
                _Visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }

        public void CargarProductosIngreso()
        {
            _productosIngrsos.Clear();
            foreach (var item in IngresoDAO.GetProductosIngresos(ViewModelPadre.informacionIngresosVerViewModel.SelectedIIV.IngresoId))
            {
                _productosIngrsos.Add(item);
            }
        }
    }
}
