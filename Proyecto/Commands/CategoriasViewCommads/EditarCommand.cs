using MultiMarcasAPP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.CategoriasViewCommads
{
    public class EditarCommand : CommandBase
    {
        private CategoriasViewModel _ViewModel;

        public EditarCommand(CategoriasViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(CategoriasViewModel.SelectedCategoria))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (Models.CurrentEmploye.PrivilegiosTrabajador.EditarCategoria) &&
                    (_ViewModel.SelectedCategoria != null) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _ViewModel.Nombre = _ViewModel.SelectedCategoria.Item2;
            _ViewModel.DataGridVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.MainButtonNavigationBarVisibility = false;
            _ViewModel.SecundaryButtonNavigationBarVisiblity = true;
            _ViewModel.IsEdit = true;
        }
    }
}
