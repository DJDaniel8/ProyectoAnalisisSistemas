using MultiMarcasAPP.ViewModels.IngresosViewModels;
using MultiMarcasAPP.Views.IngresosViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.IngresosViewCommands
{
    public class AgregarProductoCommand : CommandBase
    {
        private IngresosViewModel _ViewModel;
        public AgregarProductoCommand(IngresosViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(IngresosViewModel.SelectedProveedor))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_ViewModel.SelectedProveedor != null) &&
                    (_ViewModel.SelectedProveedor.Id > 0) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            BuscarProductosIngresoView nuevaVentana = new BuscarProductosIngresoView();
            if (parameter != null) nuevaVentana.Owner = (IngresosView)parameter;
            BuscarProductoIngresoViewModel? ViewModel = nuevaVentana.DataContext as BuscarProductoIngresoViewModel;
            if (ViewModel != null)
            {
                ViewModel.SelectedFiltrarPor = "Proveedor";
                ViewModel.SelectedFiltrarValor = new(_ViewModel.SelectedProveedor.Id, "");
            }
            nuevaVentana.Show();
        }
    }
}
