using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.ProductosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ProductosViewCommands
{
    public class QuitarCategoriaCommand : CommandBase
    {
        private ProductoInformacionViewModel _ViewModel;
        public QuitarCategoriaCommand(ProductoInformacionViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ProductoInformacionViewModel.SelectedCategoria))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_ViewModel.SelectedCategoria != null && _ViewModel.SelectedCategoria.Id != 0) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            ProductoDAO.DeleteCategory(_ViewModel.SelectedCategoria.Id, _ViewModel.Producto.Producto.Id);

            _ViewModel.CargarCategoriasById(_ViewModel.Producto.Producto.Id);
        }
    }
}
