using MultiMarcasAPP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.CategoriasViewCommads
{
    public class CrearCommand : CommandBase
    {
        private CategoriasViewModel _ViewModel;

        public CrearCommand(CategoriasViewModel viewModel)
        {
            _ViewModel = viewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return (Models.CurrentEmploye.PrivilegiosTrabajador.CrearCategoria) &&
                    base.CanExecute(parameter);
        }
        public override void Execute(object? parameter)
        {
            _ViewModel.Nombre = String.Empty;
            _ViewModel.DataGridVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.MainButtonNavigationBarVisibility = false;
            _ViewModel.SecundaryButtonNavigationBarVisiblity = true;
            _ViewModel.IsEdit = false;
        }
    }
}
