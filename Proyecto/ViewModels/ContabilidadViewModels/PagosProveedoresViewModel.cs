using MultiMarcasAPP.Commands.ContabilidadViewCommands.PagosProveedoresCommands;
using MultiMarcasAPP.Models;
using MultiMarcasAPP.Services.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.ContabilidadViewModels
{
    public class PagosProveedoresViewModel : ViewModelBase
    {
        public PagosProveedoresViewModel()
        {
            _Meses = new();
            _Pagos = new();
            Años = PagosDAO.GetAños();
            GenerarPDFCommand = new GenerarPDFCommand(this);
        }

        public ICommand GenerarPDFCommand { get; }

        public ObservableCollection<TotalPagosMensual> _Meses;
        public IEnumerable<TotalPagosMensual> Meses => _Meses;
        public IEnumerable<int> Años { get; set; }

        private ObservableCollection<PagoProveedor> _Pagos;
        public IEnumerable<PagoProveedor> Pagos => _Pagos;

        private TotalPagosMensual _SelectedTotal = new();
        public TotalPagosMensual SelectedTotal
        {
            get
            {
                return _SelectedTotal;
            }
            set
            {
                _SelectedTotal = value;
                OnPropertyChanged(nameof(SelectedTotal));
                CargarPagos();
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
            if (SelectedAño > 0)
            {
                _Meses.Clear();
                var list = PagosDAO.GetTotales(SelectedAño);
                foreach (var item in list)
                {
                    _Meses.Add(item);
                }
            }
        }

        public void CargarPagos()
        {

            if (SelectedTotal != null && SelectedTotal.Mes > 0)
            {
                _Pagos.Clear();
                foreach (var item in PagosDAO.GetPagosByYearAndMonth(SelectedTotal.Año,SelectedTotal.Mes, SelectedTotal.SaleDeCaja))
                {
                    item.proveedor = ProveedorDAO.GetByIngresoId(item.proveedor.Id);
                    _Pagos.Add(item);
                }
            }
        }
    }
}
