using MultiMarcasAPP.ViewModels.UtilidadesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.UtilidadesViewCommands
{
    public class FechaCommand : CommandBase
    {
        private UtilidadesViewModel _ViewModel;
        public FechaCommand(UtilidadesViewModel viewModel)
        {
            _ViewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _ViewModel.VentaViewModel.ControlVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.ProductosViewModel.ControlVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.FechaViewModel.ControlVisibility = System.Windows.Visibility.Visible;
            _ViewModel.DatePickerVisibility1 = System.Windows.Visibility.Visible;
            _ViewModel.DatePickerVisibility2 = System.Windows.Visibility.Visible;
            _ViewModel.ComboBoxFiltrar = System.Windows.Visibility.Collapsed;
        }
    }
}
