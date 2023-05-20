using MultiMarcasAPP.ViewModels.ProductosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ProductosViewCommands
{
    public class EditarCommand : CommandBase
    {
        private ProductosViewModel _ViewModel;
        public EditarCommand(ProductosViewModel viewModel)
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
            return (Models.CurrentEmploye.PrivilegiosTrabajador.EditarProducto) &&
                    (_ViewModel.SelectedProducto != null && _ViewModel.SelectedProducto.Producto.Id != 0) &&
                    base.CanExecute(parameter);
        }
        public override void Execute(object? parameter)
        {
            _ViewModel.MainButtonNavigationBarVisibility = false;
            _ViewModel.SecundaryButtonNavigationBarVisiblity = true;
            _ViewModel.ProductoInformacionViewModel.TxtBoxReadOnly = false;
            _ViewModel.DataGridEneable = false;
            _ViewModel.ProductoInformacionViewModel.ComboBoxVisibility = System.Windows.Visibility.Visible;
            _ViewModel.ProductoInformacionViewModel.TxtBoxVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.ProductoInformacionViewModel.CargarCategoriasById(_ViewModel.SelectedProducto.Producto.Id);
            _ViewModel.ProductoInformacionViewModel.CargarProveedoresById(_ViewModel.SelectedProducto.Producto.Id);
            _ViewModel.ProductoInformacionViewModel.AddRemoveCategoryButtonsVisibility = System.Windows.Visibility.Visible;
            _ViewModel.ProductoInformacionViewModel.AddRemoveProviderButtonsVisibility = System.Windows.Visibility.Visible;
        }
    }
}
