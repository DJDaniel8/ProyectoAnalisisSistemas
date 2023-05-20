using MultiMarcasAPP.ViewModels.ProductosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ProductosViewCommands
{
    public class AgregarCategoriaCommand : CommandBase
    {
        private ProductoInformacionViewModel _ViewModel;
        public AgregarCategoriaCommand(ProductoInformacionViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            _ViewModel.CargarCategoriasByIdSinRelacion(_ViewModel.Producto.Producto.Id);
            _ViewModel.AddRemoveCategoryButtonsVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.ConfirmSaveCategory = System.Windows.Visibility.Visible;
        }
    }
}
