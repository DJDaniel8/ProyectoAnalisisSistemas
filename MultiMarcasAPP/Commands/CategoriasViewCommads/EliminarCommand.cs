using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.CategoriasViewCommads
{
    public class EliminarCommand : CommandBase
    {
        private CategoriasViewModel _ViewModel;
        public EliminarCommand(CategoriasViewModel viewModel)
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
            return (Models.CurrentEmploye.PrivilegiosTrabajador.EliminarCategoria) &&
                    (_ViewModel.SelectedCategoria != null) &&
                    (_ViewModel.SelectedCategoria.Item1 > 0) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            var res = MessageBox.Show("Esta Seguro que \ndesea eliminar la Categoria","Eliminando",MessageBoxButton.YesNoCancel,MessageBoxImage.Warning);
            if(res == MessageBoxResult.Yes)
            {
                CategoriaDAO.Delete(_ViewModel.SelectedCategoria.Item1);
                _ViewModel.CargarCategorias();
            }
        }
    }
}
