using MultiMarcasAPP.ViewModels.ProductosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ProductosViewCommands
{
    public class AgregarProveedorCommand : CommandBase
    {
        private ProductoInformacionViewModel _ViewModel;
        public AgregarProveedorCommand(ProductoInformacionViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            _ViewModel.CargarProveedoresByIdSinRelacion(_ViewModel.Producto.Producto.Id);
            _ViewModel.AddRemoveProviderButtonsVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.ConfirmSaveProvider = System.Windows.Visibility.Visible;
        }
    }
}
