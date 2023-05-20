using MultiMarcasAPP.Models;
using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.CajaViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.CajaViewCommands
{
    public class ContinuarCommand : CommandBase
    {
        private CajaViewModel _viewModel;
        public ContinuarCommand(CajaViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(CajaViewModel.SelectedVenta))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_viewModel.SelectedVenta != null) &&
                    (_viewModel.SelectedVenta.Id > 0) &&
                    base.CanExecute(parameter);
        }

        public async override void Execute(object? parameter)
        {
            
            foreach (var item in _viewModel.Productos)
            {
                if (!VentaDAO.StockSuficiente(item))
                {
                    MessageBox.Show($"El Stock de un Producto no es Suficiente\n Producto: {item.Producto.Nombre}", "STOCK INSUFICIENTE", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            VentaDAO.Insert(_viewModel.SelectedVenta);
            VentaDAO.InsertProductosVenta(VentaDAO.GetLastInsert(), _viewModel.Productos.ToList(), _viewModel.SelectedVenta.EsAuditorada);

            PreVentaDAO.Delete(_viewModel.SelectedVenta.Id);

            var res = MessageBox.Show("Venta Exitosa\n" +
                "Desea Imprimir la Venta?", "EXITOSA", MessageBoxButton.YesNoCancel);
            if (res == MessageBoxResult.Yes)
            {
                Venta venta = VentaDAO.GetLastOne().First();
                try
                {
                    Task tarea1 = Services.PDF.PDFMaker.CreatePDFVenta8mm(_viewModel.Productos.ToList(), venta);
                    await tarea1;

                    Task tarea2 = Services.PDF.PrintPDF.PrintVenta();
                    await tarea2;
                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\n" + e.HelpLink);
                }

            }

            _viewModel.CargarVentas();
        }
    }
}
