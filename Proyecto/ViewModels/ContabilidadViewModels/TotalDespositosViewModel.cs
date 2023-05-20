using MultiMarcasAPP.Commands.ContabilidadViewCommands.DepositosCommands;
using MultiMarcasAPP.Models;
using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.CajaViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.ContabilidadViewModels
{
    public class TotalDespositosViewModel : ViewModelBase
    {
        public TotalDespositosViewModel()
        {
            _Meses = new();
            _Depositos = new();
            Años = TotalDepositosDAO.GetAños();
            GenerarPDFCommand = new GenerarPDFCommand(this);
        }

        public ICommand GenerarPDFCommand { get; }

        public ObservableCollection<TotalDepositosMensual> _Meses;
        public IEnumerable<TotalDepositosMensual> Meses => _Meses;
        public IEnumerable<int> Años { get; set; }

        private ObservableCollection<DepositoViewModel> _Depositos;
        public IEnumerable<DepositoViewModel> Depositos => _Depositos;

        private TotalDepositosMensual _SelectedTotal = new();
        public TotalDepositosMensual SelectedTotal
        {
            get
            {
                return _SelectedTotal;
            }
            set
            {
                _SelectedTotal = value;
                OnPropertyChanged(nameof(SelectedTotal));
                CargarDepositos();
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
                var list = TotalDepositosDAO.GetByYear(SelectedAño);
                foreach (var item in list)
                {
                    _Meses.Add(item);
                }
            }
        }

        public void CargarDepositos()
        {

            if (SelectedTotal != null && SelectedTotal.Mes > 0)
            {
                _Depositos.Clear();
                foreach (var item in DepositoDAO.GetByYearAndMonth(SelectedAño, SelectedTotal.Mes))
                {
                    _Depositos.Add(item);
                }
            }
        }
    }
}
