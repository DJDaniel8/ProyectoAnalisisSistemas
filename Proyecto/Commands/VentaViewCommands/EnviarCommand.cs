using MultiMarcasAPP.ViewModels.CotizacionViewModels;
using MultiMarcasAPP.ViewModels.VentaViewModels;
using MultiMarcasAPP.Views.VentasViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.VentaViewCommands
{
    public class EnviarCommand : CommandBase
    {
        private BuscarProductoVentaViewModel _ViewModel;
        public EnviarCommand(BuscarProductoVentaViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(BuscarProductoVentaViewModel.SelectedStock))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_ViewModel.SelectedStock != null) &&
                    (_ViewModel.SelectedStock.Stock.Id > 0) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            
                BuscarProductoVentaView ventanaStocks = (BuscarProductoVentaView)parameter;
                VentaViewModel ventanaVentaViewModel = ventanaStocks.Owner.DataContext as VentaViewModel;
                ventanaVentaViewModel.AgregarProductoLista(_ViewModel.SelectedStock, _ViewModel.InformacionStockViewModel.Cantidad);
                _ViewModel.InformacionStockViewModel.Cantidad = 0;
           
        }
    }
}
