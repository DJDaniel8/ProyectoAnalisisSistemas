using MultiMarcasAPP.ViewModels.ProductosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ProductosViewCommands
{
    public class CrearCommand : CommandBase
    {
        private ProductosViewModel _ViewModel;
        public CrearCommand(ProductosViewModel viewModel)
        {
            _ViewModel = viewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return (Models.CurrentEmploye.PrivilegiosTrabajador.CrearProducto) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _ViewModel.MainButtonNavigationBarVisibility = false;
            _ViewModel.SecundaryButtonNavigationBarVisiblity = true;
            _ViewModel.ProductoInformacionViewModel.TxtBoxReadOnly = false;
            _ViewModel.DataGridEneable = false;
            _ViewModel.ProductoInformacionViewModel.EsDerivado = false;
            _ViewModel.ProductoInformacionViewModel.ComboBoxVisibility = System.Windows.Visibility.Visible;
            _ViewModel.ProductoInformacionViewModel.EsDerivadoControlsVisibility = System.Windows.Visibility.Visible;
            _ViewModel.ProductoInformacionViewModel.CargarCategorias();
            _ViewModel.ProductoInformacionViewModel.CargarProveedores();
            _ViewModel.ProductoInformacionViewModel.Producto = new();
        }
    }
}
