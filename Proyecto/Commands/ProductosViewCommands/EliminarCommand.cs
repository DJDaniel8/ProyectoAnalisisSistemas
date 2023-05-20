using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.ProductosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.ProductosViewCommands
{
    public class EliminarCommand : CommandBase
    {
        private ProductosViewModel _ViewModel;
        public EliminarCommand(ProductosViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ProductosViewModel.SelectedProducto))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (Models.CurrentEmploye.PrivilegiosTrabajador.EliminarProducto) &&
                    (_ViewModel.SelectedProducto != null && _ViewModel.SelectedProducto.Producto.Id > 0) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            var res = MessageBox.Show("Esta Seguro que Desea Eliminar el Producto Seleccionado?", "Eliminando", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if(res == MessageBoxResult.Yes)
            {
                ProductoDAO.DeleteProduct(_ViewModel.SelectedProducto.Producto.Id);
                _ViewModel.CargarProductos();
            }
        }
    }
}
