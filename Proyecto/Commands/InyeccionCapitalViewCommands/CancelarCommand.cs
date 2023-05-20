using MultiMarcasAPP.ViewModels.InyeccionCapitalViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.InyeccionCapitalViewCommands
{
    public class CancelarCommand : CommandBase
    {
        private IngresoYVerViewModel _viewModel;
        public CancelarCommand(IngresoYVerViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            _viewModel.Monto = 0;
            _viewModel.Fecha = DateTime.Now;
        }
    }
}
