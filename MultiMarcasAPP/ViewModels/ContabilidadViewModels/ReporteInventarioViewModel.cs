using MultiMarcasAPP.Commands.ContabilidadViewCommands.ReporteInventarioCommands;
using MultiMarcasAPP.Models;
using MultiMarcasAPP.Services.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.ContabilidadViewModels
{
    public class ReporteInventarioViewModel : ViewModelBase
    {
        public ReporteInventarioViewModel()
        {
			GenerarPDFCommand = new GenerarPDFCommand(this);
        }
        public ICommand GenerarPDFCommand { get; }

		private DateTime _Fecha1 = DateTime.Now; 
		public DateTime Fecha1
		{
			get
			{
				return _Fecha1;
			}
			set
			{
				_Fecha1 = value;
				OnPropertyChanged(nameof(Fecha1));
			}
		}

		
	}
}
