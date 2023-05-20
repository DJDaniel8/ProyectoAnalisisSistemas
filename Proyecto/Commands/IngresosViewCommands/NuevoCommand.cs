using MultiMarcasAPP.ViewModels.IngresosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.IngresosViewCommands
{
    public class NuevoCommand : CommandBase
    {
        private IngresosViewModel _ViewModel;
        public NuevoCommand(IngresosViewModel viewModel)
        {
            _ViewModel = viewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return  (Models.CurrentEmploye.PrivilegiosTrabajador.CrearIngreso) &&
                    base.CanExecute(parameter);
        }
        public override void Execute(object? parameter)
        {
            _ViewModel.MainButtonNavigationBarVisibility = false;
            _ViewModel.SecundaryButtonNavigationBarVisiblity = true;
            _ViewModel.ComboBoxProveedorVisibility = System.Windows.Visibility.Visible;
            _ViewModel.TablasIngresosViewModel.ControlVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.NuevoIngresoViewModel.ControlVisibility = System.Windows.Visibility.Visible;
            _ViewModel.TablaStockExistentesViewModel.ControlVisibility = System.Windows.Visibility.Visible;
            _ViewModel.FiltrarComboBoxVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.SelectedDate = DateTime.Now;
        }
    }
}
