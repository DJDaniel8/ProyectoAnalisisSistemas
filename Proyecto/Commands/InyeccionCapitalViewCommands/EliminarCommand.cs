using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.InyeccionCapitalViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.InyeccionCapitalViewCommands
{
    public class EliminarCommand : CommandBase
    {
        private IngresoYVerViewModel _viewModel;
        public EliminarCommand(IngresoYVerViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(IngresoYVerViewModel.SelectedInyeccion))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_viewModel.SelectedInyeccion != null) &&
                    (_viewModel.SelectedInyeccion.Id > 0) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            var res = MessageBox.Show("Seguro que Quieres Eliminar?","Eliminando",MessageBoxButton.YesNo,MessageBoxImage.Warning);
            if(res == MessageBoxResult.Yes)
            {
                InyeccionCapitalDAO.Delete(_viewModel.SelectedInyeccion.Id);
                _viewModel.cargarInyecciones();
            }
            
        }
    }
}
