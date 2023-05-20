using MultiMarcasAPP.Models;
using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.VentaViewModels;
using MultiMarcasAPP.Views.VentasViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.VentaViewCommands
{
    public class AceptarCommand : CommandBase
    {
        private InformacionVentaViewModel _ViewModel;
        public AceptarCommand(InformacionVentaViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(InformacionVentaViewModel.SelectedCliente))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_ViewModel.SelectedCliente != null) &&
                    (_ViewModel.SelectedCliente.Cliente.Id > 0) &&
                    base.CanExecute(parameter);
        }

        public override async void Execute(object? parameter)
        {
            VentaViewModel ventaViewModel = ((InformacionVentaView)parameter).Owner.DataContext as VentaViewModel;
            foreach (var item in ventaViewModel.Productos)
            {
                if (!VentaDAO.StockSuficiente(item))
                {
                    MessageBox.Show($"El Stock de un Producto no es Suficiente\n Producto: {item.Producto.Nombre}", "STOCK INSUFICIENTE", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            bool auditorado = false;
            foreach (var item in ventaViewModel._Productos)
            {
                if (item.Stock.Auditorado) auditorado = true;
            }

            PreVentaDAO.Insert(_ViewModel.SelectedCliente.Cliente.Id, auditorado);
            PreVentaDAO.InsertProductosPreVenta(PreVentaDAO.GetLastInsert(), ventaViewModel.Productos.ToList(), auditorado);
            Venta venta = PreVentaDAO.GetLastOne().First();

            SystemCommands.CloseWindow((Window)parameter);
            SystemCommands.CloseWindow(((InformacionVentaView)parameter).Owner);
        }
    }
}
