using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.ProductosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ProductosViewCommands
{
    public class ConfirmacionAgregarCategoriaCommand : CommandBase
    {
        private ProductoInformacionViewModel _ViewModel;
        public ConfirmacionAgregarCategoriaCommand(ProductoInformacionViewModel viewModel)
        {
            _ViewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            if(_ViewModel.SelectedCategoria == null || _ViewModel.SelectedCategoria.Id == 0)
            {
                _ViewModel.CargarCategoriasById(_ViewModel.Producto.Producto.Id);
                _ViewModel.AddRemoveCategoryButtonsVisibility = System.Windows.Visibility.Visible;
                _ViewModel.ConfirmSaveCategory = System.Windows.Visibility.Collapsed;
            }
            else
            {
                ProductoDAO.InsertCategory(_ViewModel.SelectedCategoria.Id, _ViewModel.Producto.Producto.Id);
                _ViewModel.CargarCategoriasById(_ViewModel.Producto.Producto.Id);
                _ViewModel.AddRemoveCategoryButtonsVisibility = System.Windows.Visibility.Visible;
                _ViewModel.ConfirmSaveCategory = System.Windows.Visibility.Collapsed;
            }
        }
    }
}
