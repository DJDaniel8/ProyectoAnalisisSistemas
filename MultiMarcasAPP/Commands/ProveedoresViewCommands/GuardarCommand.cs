using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.ProveedoresViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ProveedoresViewCommands
{
    public class GuardarCommand : CommandBase
    {
        private ProveedoresViewModel _ViewModel;

        public GuardarCommand(ProveedoresViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ProveedoresViewModel.InformacionViewModel))
            {
                _ViewModel.InformacionViewModel.PropertyChanged += InformacionViewModel_PropertyChanged;
            }
        }

        private void InformacionViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(MostrarInformacionProveedoresViewModel.Proveedor))
            {
                _ViewModel.InformacionViewModel.Proveedor.PropertyChanged += Proveedor_PropertyChanged;
            }
        }

        private void Proveedor_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return (!String.IsNullOrEmpty(_ViewModel.InformacionViewModel.Proveedor.Nombre)) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            if(_ViewModel.SelectedProveedor.Proveedor.Id != 0)
            {
                ProveedorDAO.Update(_ViewModel.InformacionViewModel.Proveedor.Proveedor);
            }
            else
            {
                ProveedorDAO.Insert(_ViewModel.InformacionViewModel.Proveedor.Proveedor);
            }

            _ViewModel.MainButtonNavigationBarVisibility = true;
            _ViewModel.SecundaryButtonNavigationBarVisiblity = false;
            _ViewModel.InformacionViewModel.TxtBoxReadOnly = true;
            _ViewModel.DataGridEneable = true;
            _ViewModel.InformacionViewModel.Proveedor = new();
            _ViewModel.InformacionBancoViewModel.InformacionBancoVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.InformacionViewModel.BancosButtosVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.CargarProveedores();
        }
    }
}
