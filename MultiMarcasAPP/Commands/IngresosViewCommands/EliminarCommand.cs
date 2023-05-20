using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.IngresosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.IngresosViewCommands
{
    public class EliminarCommand : CommandBase
    {
        private IngresosViewModel _ViewModel;
        public EliminarCommand(IngresosViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.TablasIngresosViewModel.PropertyChanged += TablasIngresosViewModel_PropertyChanged;
        }

        private void TablasIngresosViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(TablasIngresosViewModel.SelectedIngreso))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (Models.CurrentEmploye.PrivilegiosTrabajador.EliminarIngreso) &&
                    (_ViewModel.TablasIngresosViewModel.SelectedIngreso != null) &&
                    (_ViewModel.TablasIngresosViewModel.SelectedIngreso.Id > 0) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            if (StockDAO.ValidarDeleteIngreso(_ViewModel.TablasIngresosViewModel.SelectedIngreso.Id))
            {
                var res = MessageBox.Show("Seguro que quiere eliminar El ingreso?\n Eliminar el Ingreso eliminar stock del producto ingresado", "Eliminando", MessageBoxButton.YesNoCancel);
                if(res == MessageBoxResult.Yes) IngresoDAO.Delete(_ViewModel.TablasIngresosViewModel.SelectedIngreso.Id, _ViewModel.TablasIngresosViewModel.ProductosIngrsos.ToList());
            }
            else MessageBox.Show("No se puede eliminar el ingreso\nporque los stock ya no tienen los productos ingresados");
            _ViewModel.TablasIngresosViewModel.CargarIngresos(_ViewModel.SelectedFiltrarPor, _ViewModel.SelectedFiltrar1, _ViewModel.SelectedFiltrar2);
        }
    }
}
