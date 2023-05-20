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
    public class CFCommand : CommandBase
    {
        private InformacionVentaViewModel _ViewModel;
        public CFCommand(InformacionVentaViewModel viewModel)
        {
            _ViewModel = viewModel;
        }


        public override void Execute(object? parameter)
        {
            VentaViewModel ventaViewModel = ((InformacionVentaView)parameter).Owner.DataContext as VentaViewModel;
            foreach (var item in ventaViewModel.Productos)
            {
                if(!VentaDAO.StockSuficiente(item))
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

            if(String.IsNullOrEmpty(_ViewModel.Nombre))
            {
                PreVentaDAO.Insert("CONSUMIDOR FINAL", "C/F", auditorado);
                PreVentaDAO.InsertProductosPreVenta(PreVentaDAO.GetLastInsert(),ventaViewModel.Productos.ToList(), auditorado);
            }
            else
            {
                if(String.IsNullOrEmpty(_ViewModel.Nit))
                {
                    PreVentaDAO.Insert(_ViewModel.Nombre, "C/F", auditorado);
                    PreVentaDAO.InsertProductosPreVenta(PreVentaDAO.GetLastInsert(), ventaViewModel.Productos.ToList(), auditorado);
                }
                else
                {
                    PreVentaDAO.Insert(_ViewModel.Nombre, _ViewModel.Nit, auditorado);
                    PreVentaDAO.InsertProductosPreVenta(PreVentaDAO.GetLastInsert(), ventaViewModel.Productos.ToList(), auditorado);
                }
            }

            SystemCommands.CloseWindow((Window)parameter);
            SystemCommands.CloseWindow( ((Window)parameter).Owner );

        }
    }
}
