using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.DepositosInternosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.DepositosInternosCommands
{
    public class EliminarCommand : CommandBase
    {
        private DepositosVerViewModel _viewModel;
        public EliminarCommand(DepositosVerViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DepositosVerViewModel.SelectedDeposito))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_viewModel.SelectedDeposito != null) &&
                    (_viewModel.SelectedDeposito.Id > 0) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            var res = MessageBox.Show("Se eliminara el Deposito seleccionado", "Eliminar?",MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(res == MessageBoxResult.Yes)
            {
                DepositoDAO.Delete(_viewModel.SelectedDeposito.Id);
                _viewModel.MainButtonNavigationBarVisibility = true;
                _viewModel.SecundaryButtonNavigationBarVisiblity = false;
                _viewModel.IsEneable = false;
                _viewModel.IsReadOnly = true;
                _viewModel.SelectedDeposito = new();
                _viewModel.CargarDepositos();
            }
            
        }
    }
}
