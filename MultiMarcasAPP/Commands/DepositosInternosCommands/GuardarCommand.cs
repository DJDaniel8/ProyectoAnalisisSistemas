using MultiMarcasAPP.Models;
using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.DepositosInternosViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MultiMarcasAPP.Commands.DepositosInternosCommands
{
    public class GuardarCommand : CommandBase
    {
        private DepositosVerViewModel _viewModel;
        public GuardarCommand(DepositosVerViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(DepositosVerViewModel.SelectedDeposito))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return  (_viewModel.SelectedDeposito != null) &&
                    (!string.IsNullOrEmpty(_viewModel.SelectedDeposito.NoDoc)) &&
                    (_viewModel.SelectedDeposito.Monto > 0) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            decimal SaldoFinal = ReporteVentaDAO.GetSaldoFinal();

            if(SaldoFinal >= _viewModel.SelectedDeposito.Monto)
            {
                DepositoDAO.Insert(_viewModel.SelectedDeposito);
                _viewModel.MainButtonNavigationBarVisibility = true;
                _viewModel.SecundaryButtonNavigationBarVisiblity = false;
                _viewModel.IsEneable = false;
                _viewModel.IsReadOnly = true;
                _viewModel.SelectedDeposito = new();
            }
            else
            {
                NumberFormatInfo nfi = new CultureInfo("es-GT", false).NumberFormat;
                MessageBox.Show($"El monto del deposito es mayor al efectivo en caja\nEfectivo en Caja: {SaldoFinal.ToString("c",nfi)}","Efectivo Insuficiente",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            
        }
    }
}
