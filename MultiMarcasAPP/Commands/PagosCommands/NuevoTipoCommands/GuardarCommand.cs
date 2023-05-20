using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.PagosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.PagosCommands.NuevoTipoCommands
{
    public class GuardarCommand : CommandBase
    {
        private PagosNuevoTipoViewModel _viewModel;

        public GuardarCommand(PagosNuevoTipoViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(PagosNuevoTipoViewModel.Nombre))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (!string.IsNullOrEmpty(_viewModel.Nombre)) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {

            var res = MessageBox.Show($"Desea Insertar el siguiente tipo de Pago?\n{_viewModel.Nombre} Sale de Caja {_viewModel.SaleDeCaja}",
                            "Insertar?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                PagosDAO.InsertarNuevoTipo(_viewModel.Nombre, _viewModel.SaleDeCaja);
            }
            _viewModel.Nombre = String.Empty;
            _viewModel.SaleDeCaja = false;
            _viewModel.CargarTipos();
        }
    }
}
