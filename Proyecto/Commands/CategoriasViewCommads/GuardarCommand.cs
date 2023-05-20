using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.CategoriasViewCommads
{
    public class GuardarCommand : CommandBase
    {
        private CategoriasViewModel _ViewModel;

        public GuardarCommand(CategoriasViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(CategoriasViewModel.Nombre))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (!String.IsNullOrEmpty(_ViewModel.Nombre)) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            if (_ViewModel.IsEdit) CategoriaDAO.Update(_ViewModel.SelectedCategoria.Item1,_ViewModel.Nombre);
            else CategoriaDAO.Insert(_ViewModel.Nombre);

            _ViewModel.DataGridVisibility = System.Windows.Visibility.Visible;
            _ViewModel.MainButtonNavigationBarVisibility = true;
            _ViewModel.SecundaryButtonNavigationBarVisiblity = false;
            _ViewModel.CargarCategorias();
        }
    }
}
