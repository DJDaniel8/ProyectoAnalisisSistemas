using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.ProveedoresViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ProveedoresViewCommands
{
    public class GuardarBancoCommand : CommandBase
    {
        private ProveedoresViewModel _ViewModel;
        private InformacionBancoViewModel _ViewModelBanco;
        public GuardarBancoCommand(ProveedoresViewModel viewModel, InformacionBancoViewModel viewModelBanco)
        {
            _ViewModel = viewModel;
            _ViewModelBanco = viewModelBanco;
            _ViewModelBanco.PropertyChanged += _ViewModelBanco_PropertyChanged;
        }

        private void _ViewModelBanco_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _ViewModelBanco.Banco.PropertyChanged += Banco_PropertyChanged;
        }

        private void Banco_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return (!String.IsNullOrEmpty(_ViewModelBanco.Banco.NombreBanco)) &&
                    (!String.IsNullOrEmpty(_ViewModelBanco.Banco.NombreCuenta)) &&
                    (!String.IsNullOrEmpty(_ViewModelBanco.Banco.NumeroCuenta)) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            BancoDAO.Insert(_ViewModelBanco.Banco.Banco, _ViewModel.SelectedProveedor.Proveedor.Id);

            _ViewModel.InformacionBancoViewModel.InformacionBancoVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.InformacionViewModel.CargarBancosProveedor(_ViewModel.SelectedProveedor.Proveedor.Id);
        }
    }
}
