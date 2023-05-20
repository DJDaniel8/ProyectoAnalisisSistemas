using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.InformacionIngresosViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.InformacionIngresosCommands.InsertarCommands
{
    public class GuardarCommand : CommandBase
    {
        private InformacionIngresosInsertarPagosViewModel _viewModel;
        public GuardarCommand(InformacionIngresosInsertarPagosViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(InformacionIngresosInsertarPagosViewModel.EsPagoTotal) ||
                e.PropertyName == nameof(InformacionIngresosInsertarPagosViewModel.Monto) ||
                e.PropertyName == nameof(InformacionIngresosInsertarPagosViewModel.NumeroDeDocumento) ||
                e.PropertyName == nameof(InformacionIngresosInsertarPagosViewModel.SelectedPago) ||
                e.PropertyName == nameof(InformacionIngresosInsertarPagosViewModel.FechaPago) )
            {
                OnCanExecutedChanged();
            }
                
        }

        public override bool CanExecute(object? parameter)
        {
            return  (_viewModel.Monto > 0) &&
                    (_viewModel.SelectedPago.Id > 0) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            var res = MessageBox.Show("Seguro que desea guardar el pago?", "Guardar?", MessageBoxButton.YesNo);
            if(res == MessageBoxResult.Yes)
            {
                if (_viewModel.SelectedPago.SaleCaja)
                {
                    decimal SaldoFinal = ReporteVentaDAO.GetSaldoFinal();
                    if (SaldoFinal < _viewModel.Monto)
                    {
                        NumberFormatInfo nfi = new CultureInfo("es-GT", false).NumberFormat;
                        MessageBox.Show($"El monto del gasto es mayor al efectivo en caja\nEfectivo en Caja: {SaldoFinal.ToString("c", nfi)}", "Efectivo Insuficiente", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                InformacionIngresosDAO.InsertPago(_viewModel);
                InformacionIngresosDAO.InsertPagoInformacionIngresos(PagosDAO.LastInsert().Id, _viewModel.ViewModelPadre.informacionIngresosVerViewModel.SelectedIIV.Id);
                decimal totalIngreso = _viewModel.ViewModelPadre.informacionIngresosVerViewModel.SelectedIIV.TotalIngreso;
                decimal totalPagado = _viewModel.ViewModelPadre.informacionIngresosVerViewModel.SelectedIIV.TotalPagado + _viewModel.Monto;
                if (totalIngreso == totalPagado) InformacionIngresosDAO.UpdateInformacionIngresos(true, _viewModel.ViewModelPadre.informacionIngresosVerViewModel.SelectedIIV.Id);
                _viewModel.Visibility = System.Windows.Visibility.Hidden;
                _viewModel.ViewModelPadre.informacionIngresosVerViewModel.CargarInformacion();
                _viewModel.ViewModelPadre.informacionIngresosVerViewModel.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
