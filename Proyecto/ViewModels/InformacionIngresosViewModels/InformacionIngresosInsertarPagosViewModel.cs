using MultiMarcasAPP.Commands.InformacionIngresosCommands.InsertarCommands;
using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.GastosViewModels;
using MultiMarcasAPP.ViewModels.PagosViewModels;
using Org.BouncyCastle.Crypto.Engines;
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
    public class InformacionIngresosInsertarPagosViewModel : ViewModelBase
    {
        public InformacionIngresosInsertarPagosViewModel(InformacionIngresosViewModel viewModelPadre)
        {
			_TiposPagos = new();
            ViewModelPadre = viewModelPadre;
			RegresarCommand = new RegresarCommand(this);
			GuardarCommand = new GuardarCommand(this);
			CargarTiposPagos();
        }

        public ObservableCollection<TipoPagoViewModel> _TiposPagos;
        public IEnumerable<TipoPagoViewModel> TiposPagos => _TiposPagos;


        public InformacionIngresosViewModel ViewModelPadre { get; set; }

        private Visibility _Visibility = Visibility.Collapsed;
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

        public ICommand RegresarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }

        private bool _EsPagoTotal = true;
		public bool EsPagoTotal
		{
			get
			{
				return _EsPagoTotal;
			}
			set
			{
				if (value == true)
				{ 
					Monto = ViewModelPadre.informacionIngresosVerViewModel.SelectedIIV.TotalIngreso - ViewModelPadre.informacionIngresosVerViewModel.SelectedIIV.TotalPagado;
					IsReadOnlyMonto = true;
				}
				else
				{
					Monto = 0;
					IsReadOnlyMonto = false;
				}

                _EsPagoTotal = value;
				OnPropertyChanged(nameof(EsPagoTotal));
			}
		}

        private TipoPagoViewModel _SelectedPago = new();
        public TipoPagoViewModel SelectedPago
        {
            get
            {
                return _SelectedPago;
            }
            set
            {
                _SelectedPago = value;
                OnPropertyChanged(nameof(SelectedPago));
            }
        }

        private decimal _Monto;
		public decimal Monto
		{
			get
			{
				return _Monto;
			}
			set
			{
				if (ViewModelPadre.informacionIngresosVerViewModel.SelectedIIV != null && value < ViewModelPadre.informacionIngresosVerViewModel.SelectedIIV.TotalIngreso - ViewModelPadre.informacionIngresosVerViewModel.SelectedIIV.TotalPagado)
				{
					_Monto = value;
				}
				else if (value == ViewModelPadre.informacionIngresosVerViewModel.SelectedIIV?.TotalIngreso - ViewModelPadre.informacionIngresosVerViewModel.SelectedIIV?.TotalPagado)
				{
					_Monto = value;
					_EsPagoTotal = true;
					IsReadOnlyMonto = true;
					OnPropertyChanged(nameof(EsPagoTotal));
                }
				OnPropertyChanged(nameof(Monto));
			}
		}

		private bool _IsReadOnlyMonto = true;
		public bool IsReadOnlyMonto
		{
			get
			{
				return _IsReadOnlyMonto;
			}
			set
			{
				_IsReadOnlyMonto = value;
				OnPropertyChanged(nameof(IsReadOnlyMonto));
			}
		}

		private string _NumeroDeDocumento = string.Empty;
		public string NumeroDeDocumento
		{
			get
			{
				return _NumeroDeDocumento;
			}
			set
			{
				_NumeroDeDocumento = value;
				OnPropertyChanged(nameof(NumeroDeDocumento));
			}
		}

		private DateTime _FechaPago = DateTime.Now;
		public DateTime FechaPago
		{
			get
			{
				return _FechaPago;
			}
			set
			{
				_FechaPago = value;
				OnPropertyChanged(nameof(FechaPago));
			}
		}

        public void CargarTiposPagos()
        {
            _TiposPagos.Clear();
            foreach (var item in PagosDAO.GetTipos())
            {
                _TiposPagos.Add(item);
            }
        }
    }
}
