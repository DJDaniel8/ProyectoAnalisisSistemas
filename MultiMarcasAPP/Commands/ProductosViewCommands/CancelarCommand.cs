using MultiMarcasAPP.ViewModels.ProductosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.ProductosViewCommands
{
    public class CancelarCommand : CommandBase
    {
        private ProductosViewModel _ViewModel;
        public CancelarCommand(ProductosViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            _ViewModel.MainButtonNavigationBarVisibility = true;
            _ViewModel.SecundaryButtonNavigationBarVisiblity = false;
            _ViewModel.ProductoInformacionViewModel.TxtBoxReadOnly = true;
            _ViewModel.ProductoInformacionViewModel.TxtBoxVisibility = Visibility.Visible;
            _ViewModel.ProductoInformacionViewModel.ComboBoxVisibility = Visibility.Collapsed;
            _ViewModel.ProductoInformacionViewModel.AddRemoveCategoryButtonsVisibility = Visibility.Collapsed;
            _ViewModel.ProductoInformacionViewModel.AddRemoveProviderButtonsVisibility = Visibility.Collapsed;
            _ViewModel.ProductoInformacionViewModel.ConfirmSaveCategory = Visibility.Collapsed;
            _ViewModel.ProductoInformacionViewModel.ConfirmSaveProvider = Visibility.Collapsed;
            _ViewModel.ProductoInformacionViewModel.EsDerivadoControlsVisibility = Visibility.Collapsed;
            _ViewModel.ProductoInformacionViewModel.Producto = new();
            _ViewModel.DataGridEneable = true;
            _ViewModel.ProductoInformacionViewModel.ImagenData = null;
            _ViewModel.ProductoInformacionViewModel.Imagen = null;
            _ViewModel.CargarProductos();
        }
    }
}
