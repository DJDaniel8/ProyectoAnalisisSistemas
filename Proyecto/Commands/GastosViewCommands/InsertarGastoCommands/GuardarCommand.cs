using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.GastosViewModels;
using MultiMarcasAPP.ViewModels.PagosViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.GastosViewCommands.InsertarGastoCommands
{
    internal class GuardarCommand : CommandBase
    {
        private GastosInsertarViewModel _viewModel;

        public GuardarCommand(GastosInsertarViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GastosInsertarViewModel.Fecha) ||
                e.PropertyName == nameof(GastosInsertarViewModel.Monto) ||
                e.PropertyName == nameof(GastosInsertarViewModel.Descripcion) ||
                e.PropertyName == nameof(GastosInsertarViewModel.NoDoc) ||
                e.PropertyName == nameof(GastosInsertarViewModel.SelectedGasto) ||
                e.PropertyName == nameof(GastosInsertarViewModel.SelectedPago) )
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return  (_viewModel.Monto > 0) &&
                    (_viewModel.SelectedGasto != null && _viewModel.SelectedGasto.Id > 0) &&
                    (_viewModel.SelectedPago != null && _viewModel.SelectedPago.Id > 0) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {

            var res = MessageBox.Show($"Desea Insertar el siguiente Gasto?\n{_viewModel.SelectedGasto.Nombre} \n{_viewModel.SelectedPago.Nombre}\n{_viewModel.Monto}\n{_viewModel.NoDoc}\n{_viewModel.Fecha}",
                            "Insertar?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                if(_viewModel.SelectedPago.SaleCaja)
                {
                    decimal SaldoFinal = ReporteVentaDAO.GetSaldoFinal();
                    if(SaldoFinal < _viewModel.Monto)
                    {
                        NumberFormatInfo nfi = new CultureInfo("es-GT", false).NumberFormat;
                        MessageBox.Show($"El monto del gasto es mayor al efectivo en caja\nEfectivo en Caja: {SaldoFinal.ToString("c", nfi)}", "Efectivo Insuficiente", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                PagoViewModel pago = new();
                pago.Fecha = _viewModel.Fecha;
                pago.Monto = _viewModel.Monto;
                pago.NoDoc = _viewModel.NoDoc;
                pago.TipoPago.Id = _viewModel.SelectedPago.Id;
                PagosDAO.InsertPago(pago);
                GastoViewModel gasto = new();
                gasto.Descripcion = _viewModel.Descripcion;
                gasto.Pago = PagosDAO.LastInsert();
                gasto.TipoGasto = _viewModel.SelectedGasto;
                GastoDAO.InsertGasto(gasto);
            }
            _viewModel.Fecha = DateTime.Now;
            _viewModel.Monto = 0;
            _viewModel.NoDoc = String.Empty;
            _viewModel.Descripcion = String.Empty;

        }
    }
}
