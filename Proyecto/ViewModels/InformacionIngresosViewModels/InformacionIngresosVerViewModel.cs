using MultiMarcasAPP.Commands.InformacionIngresosCommands.VerCommands;
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
    public class InformacionIngresosVerViewModel : ViewModelBase
    {
        public InformacionIngresosVerViewModel(InformacionIngresosViewModel viewModelPadre)
        {
            ViewModelPadre = viewModelPadre;
            _InformacionIngresos = new();
            string[] filtrar = { "Pendientes", "Mes Actual", "Fecha" };
            FiltrarPor = filtrar.ToList<string>();
            Años = InformacionIngresosDAO.GetAños();
            CargarInformacion();
            InsertarPagoCommand = new InsertarPagoCommand(this);
            VerPagosCommand = new VerPagosCommand(this);
            VerProductosCommand = new VerProductosCommand(this);
        }

        public InformacionIngresosViewModel ViewModelPadre { get; set; }

        public ICommand InsertarPagoCommand { get; set; }
        public ICommand VerPagosCommand { get; set; }
        public ICommand VerProductosCommand { get; set; }

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

        private ObservableCollection<InformacionIngresoViewModel> _InformacionIngresos;
        public IEnumerable<InformacionIngresoViewModel> InformacionIngresos => _InformacionIngresos;
        public List<string> FiltrarPor { get; set; }
        public IEnumerable<int> Años { get; set; }
        public IEnumerable<int>? Meses { get; set; }


        private InformacionIngresoViewModel _SelectedIIV = new();
        public InformacionIngresoViewModel SelectedIIV
        {
            get
            {
                return _SelectedIIV;
            }
            set
            {
                _SelectedIIV = value;
                OnPropertyChanged(nameof(SelectedIIV));
            }
        }

        private string _SelectedFiltrar = string.Empty;
        public string SelectedFiltrar
        {
            get
            {
                return _SelectedFiltrar;
            }
            set
            {
                _SelectedFiltrar = value;
                if (value == "Pendientes")
                {
                    FechaVisibility = Visibility.Collapsed;
                    CargarInformacion();
                }
                else if (value == "Mes Actual")
                {
                    FechaVisibility = Visibility.Collapsed;
                    CargarInformacionByFecha(DateTime.Now.Year, DateTime.Now.Month);
                }
                else if (value == "Fecha")
                {
                    FechaVisibility = Visibility.Visible;
                }
                OnPropertyChanged(nameof(SelectedFiltrar));
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
                if (value > 0)
                {
                    Meses = InformacionIngresosDAO.GetMeses(value);
                    OnPropertyChanged(nameof(Meses));
                }
                OnPropertyChanged(nameof(SelectedAño));
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
                if(value > 0)
                {
                    CargarInformacionByFecha(SelectedAño, SelectedMes);
                }
                OnPropertyChanged(nameof(SelectedMes));
            }
        }

        private Visibility _FechaVisibility = Visibility.Collapsed;
        public Visibility FechaVisibility
        {
            get
            {
                return _FechaVisibility;
            }
            set
            {
                _FechaVisibility = value;
                OnPropertyChanged(nameof(FechaVisibility));
            }
        }

        public void CargarInformacion()
        {
            _InformacionIngresos.Clear();
            foreach (var item in InformacionIngresosDAO.GetPendientes())
            {
                _InformacionIngresos.Add(item);
            }
        }

        public void CargarInformacionByFecha(int año, int mes)
        {
            _InformacionIngresos.Clear();
            foreach (var item in InformacionIngresosDAO.GetByFecha(año,mes))
            {
                _InformacionIngresos.Add(item);
            }
        }
    }
}
