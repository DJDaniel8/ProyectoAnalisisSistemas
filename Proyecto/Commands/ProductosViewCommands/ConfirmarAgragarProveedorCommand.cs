using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.ProductosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ProductosViewCommands
{
    public class ConfirmarAgragarProveedorCommand : CommandBase
    {
        private ProductoInformacionViewModel _ViewModel;
        public ConfirmarAgragarProveedorCommand(ProductoInformacionViewModel ViewModel)
        {
            _ViewModel = ViewModel;
        }

        public override void Execute(object? parameter)
        {
            if(_ViewModel.SelectedProveedor == null || _ViewModel.SelectedProveedor.Id == 0)
            {
                _ViewModel.CargarProveedoresById(_ViewModel.Producto.Producto.Id);
                _ViewModel.AddRemoveProviderButtonsVisibility = System.Windows.Visibility.Visible;
                _ViewModel.ConfirmSaveProvider = System.Windows.Visibility.Collapsed;
            }
            else
            {
                ProductoDAO.InsertProvider(_ViewModel.SelectedProveedor.Id, _ViewModel.Producto.Producto.Id);
                _ViewModel.AddRemoveProviderButtonsVisibility = System.Windows.Visibility.Visible;
                _ViewModel.ConfirmSaveProvider = System.Windows.Visibility.Collapsed;
                _ViewModel.CargarProveedoresById(_ViewModel.Producto.Producto.Id);
            }
        }
    }
}
