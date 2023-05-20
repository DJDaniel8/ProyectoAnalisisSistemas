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
    public class EliminarCommand : CommandBase
    {
        private CajaViewModel _viewModel;
        public EliminarCommand(CajaViewModel viewModel)
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
        public override void Execute(object? parameter)
        {
            var res = MessageBox.Show("Esta seguro que quiere eliminar\nla preventa?", "Eliminando", MessageBoxButton.YesNoCancel);
            if(res == MessageBoxResult.Yes)
            {
                PreVentaDAO.Delete(_viewModel.SelectedVenta.Id);
                _viewModel.CargarVentas();
            }
            
        }
    }
}
