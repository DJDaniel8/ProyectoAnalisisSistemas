using MultiMarcasAPP.Models;
using MultiMarcasAPP.Services.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MultiMarcasAPP.Commands.ContabilidadViewCommands;

namespace MultiMarcasAPP.ViewModels.ContabilidadViewModels
{
    public class ReporteVentasViewModel : ViewModelBase
    {
        public ReporteVentasViewModel()
        {
            Años = ReporteVentaDAO.GetAños();
            _Reportes = new();
            GuardarSaldoFinalCommand = new GuardarSaldoFinalCommand(this);
            GenerarPDFCommand = new GenerarPDFCommand(this);
        }

        public ICommand GuardarSaldoFinalCommand { get; }
        public ICommand GenerarPDFCommand { get; }

        public IEnumerable<int> Años { get; set; }
        public IEnumerable<int>? Meses { get; set; }

        private List<ReporteVenta> _Reportes;
        public IEnumerable<ReporteVenta> Reportes => _Reportes;


        private decimal _Vienen;
        public decimal Vienen
        {
            get
            {
                return _Vienen;
            }
            set
            {
                _Vienen = value;
                OnPropertyChanged(nameof(Vienen));
            }
        }

        private decimal _SaldoFinal;
        public decimal SaldoFinal
        {
            get
            {
                return _SaldoFinal;
            }
            set
            {
                _SaldoFinal = value;
                OnPropertyChanged(nameof(SaldoFinal));
            }
        }

        

        private int _año;
        public int SelectedAño
        {
            get
            {
                return _año;
            }
            set
            {
                _año = value;
                OnPropertyChanged(nameof(SelectedAño));
                if (value > 0)
                {
                    Meses = ReporteVentaDAO.GetMeses(SelectedAño);
                    OnPropertyChanged(nameof(Meses));
                }
            }
        }

        private int _SelectedMes;
        public int SelectedMes
        {
            get
            {
                return _SelectedMes;
            }
            set
            {
                _SelectedMes = value;
                if (value > 0)
                {
                    Vienen = ReporteVentaDAO.GetSaldoFinalMesAnterior(SelectedAño, SelectedMes);
                    CargarReportes();
                }
                OnPropertyChanged(nameof(SelectedMes));
            }
        }

        public void CargarReportes()
        {
            _Reportes.Clear();
            foreach (var item in ReporteVentaDAO.GetVentas(SelectedAño, SelectedMes))
            {
                _Reportes.Add(item);
            }

            foreach (var item in ReporteVentaDAO.GetGastos(SelectedAño, SelectedMes))
            {
                _Reportes.Add(item);
            }

            foreach (var item in ReporteVentaDAO.GetDepositos(SelectedAño, SelectedMes))
            {
                _Reportes.Add(item);
            }

            foreach (var item in ReporteVentaDAO.GetPagos(SelectedAño,SelectedMes))
            {
                _Reportes.Add(item);
            }

            foreach (var item in ReporteVentaDAO.GetInyecciones(SelectedAño,SelectedMes))
            {
                _Reportes.Add(item);
            }

            _Reportes = (from rv in _Reportes
                         orderby rv.Fecha ascending
                         select rv).ToList();

            decimal saldo = Vienen;
            foreach (var item in _Reportes)
            {
                if (item is ReporteVentaVentas)
                {
                    saldo = saldo + item.Monto;
                    item.Saldo = saldo;
                }
                else if(item is ReporteVentaInyeccionCapital)
                {
                    saldo = saldo + item.Monto;
                    item.Saldo = saldo;
                }
                else
                {
                    saldo = saldo - item.Monto;
                    item.Saldo = saldo;
                }
            }
            SaldoFinal = saldo;
            OnPropertyChanged(nameof(Reportes));
        }
    }
}
