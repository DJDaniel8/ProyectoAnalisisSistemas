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
    public class EliminarBancoCommand : CommandBase
    {
        private ProveedoresViewModel _ViewModel;

        public EliminarBancoCommand(ProveedoresViewModel viewModel)
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
            if(e.PropertyName == nameof(MostrarInformacionProveedoresViewModel.SelectedBanco))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_ViewModel.InformacionViewModel.SelectedBanco != null) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            var res = MessageBox.Show("Seguro que Desea Eliminar El siguiente Banco?", "Eliminar", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if(res == MessageBoxResult.Yes)
            {
                BancoDAO.Delete(_ViewModel.InformacionViewModel.SelectedBanco.Banco);
            }

            _ViewModel.InformacionViewModel.CargarBancosProveedor(_ViewModel.SelectedProveedor.Proveedor.Id);
        }
    }
}
