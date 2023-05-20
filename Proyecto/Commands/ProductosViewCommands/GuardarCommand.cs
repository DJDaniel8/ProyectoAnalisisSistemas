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
    public class GuardarCommand : CommandBase
    {
        private ProductosViewModel _ViewModel;
        public GuardarCommand(ProductosViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ProductosViewModel.ProductoInformacionViewModel))
            {
                _ViewModel.ProductoInformacionViewModel.PropertyChanged += ProductoInformacionViewModel_PropertyChanged;
            }
        }

        private void ProductoInformacionViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ProductoInformacionViewModel.Producto))
            {
                _ViewModel.ProductoInformacionViewModel.Producto.PropertyChanged += Producto_PropertyChanged;
            }
            else if(e.PropertyName == nameof(ProductoInformacionViewModel.EsDerivado) ||
                    e.PropertyName == nameof(ProductoInformacionViewModel.SelectedProveedor) ||
                    e.PropertyName == nameof(ProductoInformacionViewModel.CodigoPadre))
            {
                OnCanExecutedChanged();
            }
        }

        private void Producto_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            bool DerivadoValido = true;
            if(_ViewModel.ProductoInformacionViewModel.EsDerivado)
            {
                if (_ViewModel.ProductoInformacionViewModel.Producto.Producto.Multiplicador == 0 ||
                    _ViewModel.ProductoInformacionViewModel.Producto.Producto.Multiplicador < 0) DerivadoValido = false;
                if (String.IsNullOrEmpty(_ViewModel.ProductoInformacionViewModel.CodigoPadre)) DerivadoValido = false;
            }

            return (!String.IsNullOrEmpty(_ViewModel.ProductoInformacionViewModel.Producto.Producto.Codigo)) &&
                    (!String.IsNullOrEmpty(_ViewModel.ProductoInformacionViewModel.Producto.Producto.Nombre)) &&
                    (_ViewModel.ProductoInformacionViewModel.SelectedProveedor != null || _ViewModel.ProductoInformacionViewModel.Producto.Producto.Id > 0) &&
                    DerivadoValido &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            if (_ViewModel.ProductoInformacionViewModel.Producto.Producto.Id == 0)
            {
                if (_ViewModel.ProductoInformacionViewModel.EsDerivado)
                {
                    int res = ProductoDAO.GetIdByCode(_ViewModel.ProductoInformacionViewModel.CodigoPadre);
                    if (res == 0)
                    {
                        MessageBox.Show("Error El Codigo del Producto Padre\n El Codigo no considio con ninguno");
                        return;
                    }
                    else _ViewModel.ProductoInformacionViewModel.Producto.Producto.PadreId = res;
                }

                ProductoDAO.Insert(_ViewModel.ProductoInformacionViewModel.Producto.Producto, _ViewModel.ProductoInformacionViewModel.ImagenData, _ViewModel.ProductoInformacionViewModel.SelectedProveedor.Id);
                if (_ViewModel.ProductoInformacionViewModel.SelectedCategoria != null && _ViewModel.ProductoInformacionViewModel.SelectedCategoria.Id != 0)
                {
                    ProductoDAO.InsertCategory(_ViewModel.ProductoInformacionViewModel.SelectedCategoria.Id, ProductoDAO.GetIdLastInsert());
                }
            }
            else
            {
                ProductoDAO.Update(_ViewModel.ProductoInformacionViewModel.Producto.Producto, _ViewModel.ProductoInformacionViewModel.ImagenData);
            }

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
