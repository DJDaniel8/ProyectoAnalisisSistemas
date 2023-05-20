using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.ProductosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ProductosViewCommands
{
    public class QuitarProveedorCommand : CommandBase
    {
        private ProductoInformacionViewModel _ViewModel;
        public QuitarProveedorCommand(ProductoInformacionViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ProductoInformacionViewModel.SelectedProveedor))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_ViewModel.SelectedProveedor != null && _ViewModel.SelectedProveedor.Id != 0) &&
                    (_ViewModel.HowManyProviders() > 1) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            ProductoDAO.DeleteProvider(_ViewModel.SelectedProveedor.Id, _ViewModel.Producto.Producto.Id);
            _ViewModel.CargarProveedoresById(_ViewModel.Producto.Producto.Id);
        }
    }
}
