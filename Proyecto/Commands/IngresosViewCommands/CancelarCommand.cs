using MultiMarcasAPP.ViewModels.IngresosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.IngresosViewCommands
{
    public class CancelarCommand : CommandBase
    {
        private IngresosViewModel _ViewModel;
        public CancelarCommand(IngresosViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            _ViewModel.NuevoIngresoViewModel.ControlVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.TablasIngresosViewModel.ControlVisibility = System.Windows.Visibility.Visible;
            _ViewModel.ComboBoxProveedorVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.TablaStockExistentesViewModel.ControlVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.MainButtonNavigationBarVisibility = true;
            _ViewModel.SecundaryButtonNavigationBarVisiblity = false;
            _ViewModel.NuevoIngresoViewModel.LimpiarLista();
            _ViewModel.FiltrarComboBoxVisibility = System.Windows.Visibility.Visible;
            _ViewModel.TablasIngresosViewModel.CargarIngresos(_ViewModel.SelectedFiltrarPor, _ViewModel.SelectedFiltrar1, _ViewModel.SelectedFiltrar2);
        }
    }
}
