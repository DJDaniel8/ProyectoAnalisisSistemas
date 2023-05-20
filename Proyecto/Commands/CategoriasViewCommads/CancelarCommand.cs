using MultiMarcasAPP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.CategoriasViewCommads
{
    public class CancelarCommand : CommandBase
    {
        private CategoriasViewModel _ViewModel;
        public CancelarCommand(CategoriasViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            _ViewModel.DataGridVisibility = System.Windows.Visibility.Visible;
            _ViewModel.MainButtonNavigationBarVisibility = true;
            _ViewModel.SecundaryButtonNavigationBarVisiblity = false;
        }
    }
}
