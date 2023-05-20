using MultiMarcasAPP.ViewModels.InformacionIngresosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.InformacionIngresosCommands.VerPagosCommands
{
    public class RegresarCommand : CommandBase
    {
        private InformacionIngresosVerPagosViewModel _viewModel;
        public RegresarCommand(InformacionIngresosVerPagosViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.Visibility = System.Windows.Visibility.Hidden;
            _viewModel.ViewModelPadre.informacionIngresosVerViewModel.CargarInformacion();
            _viewModel.ViewModelPadre.informacionIngresosVerViewModel.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
