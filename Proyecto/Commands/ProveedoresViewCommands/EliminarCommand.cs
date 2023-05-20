using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.ProveedoresViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.ProveedoresViewCommands
{
    public class EliminarCommand : CommandBase
    {
        private ProveedoresViewModel _ViewModel;
        public EliminarCommand(ProveedoresViewModel ViewModel)
        {
            _ViewModel = ViewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ProveedoresViewModel.SelectedProveedor))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (Models.CurrentEmploye.PrivilegiosTrabajador.EliminarProveedor) &&
                    (_ViewModel.SelectedProveedor != null) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            var resp = MessageBox.Show("Esta Seguro que Desea Eliminar el Proveedor Seleccionado?", "Eliminar?", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if(resp == MessageBoxResult.Yes)
            {
                ProveedorDAO.Delete(_ViewModel.SelectedProveedor.Proveedor);
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
