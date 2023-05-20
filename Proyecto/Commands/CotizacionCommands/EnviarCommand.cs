using MultiMarcasAPP.ViewModels.CotizacionViewModels;
using MultiMarcasAPP.ViewModels.VentaViewModels;
using MultiMarcasAPP.Views.CotizacionViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.CotizacionCommands
{
    public class EnviarCommand : CommandBase
    {
        private BuscarProductoCotizacionViewModel _ViewModel;
        public EnviarCommand(BuscarProductoCotizacionViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BuscarProductoCotizacionViewModel.SelectedStock))
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

                BuscarProductoCotizacionView ventanaStocks = (BuscarProductoCotizacionView)parameter;
                CotizacionViewModel ventanaCotizacionViewModel = ventanaStocks.Owner.DataContext as CotizacionViewModel;
                ventanaCotizacionViewModel.AgregarProductoLista(_ViewModel.SelectedStock, _ViewModel.InformacionStockViewModel.Cantidad);
                _ViewModel.InformacionStockViewModel.Cantidad = 0;
            
        }
    }
}
