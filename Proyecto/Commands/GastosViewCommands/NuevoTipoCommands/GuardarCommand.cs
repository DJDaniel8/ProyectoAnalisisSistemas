using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.GastosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.GastosViewCommands.NuevoTipoCommands
{
    public class GuardarCommand : CommandBase
    {
        private GastosNuevoTipoViewModel _viewModel;

        public GuardarCommand(GastosNuevoTipoViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GastosNuevoTipoViewModel.Nombre))
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

            var res = MessageBox.Show($"Desea Insertar el siguiente tipo de Gasto?\n{_viewModel.Nombre} Deducible {_viewModel.Deducible}",
                            "Insertar?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                GastoDAO.InsertarNuevoTipo(_viewModel.Nombre, _viewModel.Deducible);
            }
            _viewModel.Nombre = String.Empty;
            _viewModel.Deducible = false;
            _viewModel.CargarTipos();
        }
    }
}
