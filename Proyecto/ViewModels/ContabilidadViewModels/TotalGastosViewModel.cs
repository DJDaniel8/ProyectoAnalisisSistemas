using MultiMarcasAPP.Commands.ContabilidadViewCommands.GastosCommands;
using MultiMarcasAPP.Models;
using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.GastosViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.ContabilidadViewModels
{
    public class TotalGastosViewModel : ViewModelBase
    {
        public TotalGastosViewModel()
        {
            _Meses = new();
            _Gastos = new();
            Años = TotalGastosDAO.GetAños();
            GenerarPDFCommand = new GenerarPDFCommand(this);
        }

        public ICommand GenerarPDFCommand { get; }

        public ObservableCollection<TotalGastosMensual> _Meses;
        public IEnumerable<TotalGastosMensual> Meses => _Meses;
        public IEnumerable<int> Años { get; set; }

        private ObservableCollection<GastoViewModel> _Gastos;
        public IEnumerable<GastoViewModel> Gastos => _Gastos;

        private TotalGastosMensual _SelectedTotal = new();
        public TotalGastosMensual SelectedTotal
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
            if (SelectedAño > 0)
            {
                _Meses.Clear();
                var list = TotalGastosDAO.GetNoDeduciblesByDate(SelectedAño);
                var list2 = TotalGastosDAO.GetDeduciblesByDate(SelectedAño);
                if(list.Count == list2.Count)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        _Meses.Add(list[i]);
                        _Meses.Add(list2[i]);
                    }
                }
                else
                {
                    foreach (var item in list)
                    {
                        _Meses.Add(item);
                    }
                    foreach (var item in list2)
                    {
                        _Meses.Add(item);
                    }
                }
            }
        }

        public void CargarIngresos()
        {

            if (SelectedTotal != null && SelectedTotal.Mes > 0)
            {
                _Gastos.Clear();
                if(_SelectedTotal.Deducible)
                {
                    foreach (var item in GastoDAO.GetByMonthAndYearDeducibles(_SelectedTotal.Mes, _SelectedTotal.Año))
                    {
                        _Gastos.Add(item);
                    }
                }
                else
                {
                    foreach (var item in GastoDAO.GetByMonthAndYearNoDeducibles(SelectedTotal.Mes, SelectedTotal.Año))
                    {
                        _Gastos.Add(item);
                    }
                }
            }
        }
    }
}
