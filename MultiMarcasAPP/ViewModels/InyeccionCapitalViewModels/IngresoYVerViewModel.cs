using MultiMarcasAPP.Commands.InyeccionCapitalViewCommands;
using MultiMarcasAPP.Services.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.InyeccionCapitalViewModels
{
    public class IngresoYVerViewModel : ViewModelBase
    {
        public IngresoYVerViewModel()
        {
			_Inyecciones = new();
			CancelarCommand = new CancelarCommand(this);
			GuardarCommand = new GuardarCommand(this);
			EliminarCommand = new EliminarCommand(this);
			cargarInyecciones();
        }

        public ObservableCollection<InyeccionViewModel> _Inyecciones;
        public IEnumerable<InyeccionViewModel> Inyecciones => _Inyecciones;

        public ICommand GuardarCommand { get; }
        public ICommand CancelarCommand { get; }
        public ICommand EliminarCommand { get; }

		private decimal _Monto;
		public decimal Monto
		{
			get
			{
				return _Monto;
			}
			set
			{
				_Monto = value;
				OnPropertyChanged(nameof(Monto));
			}
		}

		private DateTime _Fecha = DateTime.Now;
		public DateTime Fecha
		{
			get
			{
				return _Fecha;
			}
			set
			{
				_Fecha = value;
				OnPropertyChanged(nameof(Fecha));
			}
		}

		private InyeccionViewModel _SelectedInyeccion = new();
		public InyeccionViewModel SelectedInyeccion
		{
			get
			{
				return _SelectedInyeccion;
			}
			set
			{
				_SelectedInyeccion = value;
				OnPropertyChanged(nameof(SelectedInyeccion));
			}
		}

		public void cargarInyecciones()
		{
			_Inyecciones.Clear();
			foreach (var item in InyeccionCapitalDAO.Get())
			{
				_Inyecciones.Add(item);
			}
		}
	}
}
