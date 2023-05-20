using MultiMarcasAPP.ViewModels.ConfiguracionesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ConfiguracionesCommands
{
    public class GuardarCommand : CommandBase
    {
        private ConfiguracionesViewModel _viewModel;
        public GuardarCommand(ConfiguracionesViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            _viewModel.GuardarDatos();
        }
    }
}
