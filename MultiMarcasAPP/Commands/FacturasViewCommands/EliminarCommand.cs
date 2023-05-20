using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.FacturasViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.FacturasViewCommands
{
    public class EliminarCommand : CommandBase
    {
        private FacturasViewModel _ViewModel;
        public EliminarCommand(FacturasViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(FacturasViewModel.SelectedVenta))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            bool avanzar = true;
            DateTime fechaHoy = DateTime.Now;
            if (_ViewModel.SelectedVenta != null)
            {
                if (_ViewModel.SelectedVenta.Fecha.AddDays(7) < fechaHoy)
                {
                    avanzar = false;
                }
            }

            return  avanzar &&
                    (_ViewModel.SelectedVenta != null) &&
                    (_ViewModel.SelectedVenta.Id > 0 ) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            var res = MessageBox.Show("Esta Seguro que Desea Eliminar Esa venta?\n " +
                                      "Al Eliminar Esta Venta Los Productos Vendidos se sumaran a su Stock Debido", "Eliminar?", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if(res == MessageBoxResult.Yes)
            {
                VentaDAO.Delete(_ViewModel.SelectedVenta.Id, _ViewModel.Productos.ToList());
            }
            _ViewModel.CargarVentas();
        }
    }
}
