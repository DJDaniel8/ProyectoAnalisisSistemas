using MultiMarcasAPP.ViewModels.CotizacionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.CotizacionCommands
{
    public class FinalizarCommand : CommandBase
    {
        private CotizacionViewModel _ViewModel;
        public FinalizarCommand(CotizacionViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(CotizacionViewModel.Productos))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_ViewModel.Productos.Count() > 0) &&
                    base.CanExecute(parameter);
        }

        public override async void Execute(object? parameter)
        {
            var res = MessageBox.Show("Cotizacion Exitosa\n" +
                "Desea Imprimir la Cotizacion?", "EXITOSA", MessageBoxButton.YesNoCancel);
            if (res == MessageBoxResult.Yes)
            {
                Task tarea = Services.PDF.PDFMaker.CreatePDFCotizacion(_ViewModel._Productos.ToList(), _ViewModel.Nombre);
                SystemCommands.CloseWindow((Window)parameter);
                await tarea;
                Task tarea2 = Services.PDF.PrintPDF.PrintCotizacion();
                await tarea2;
            }
            else
            {
                SystemCommands.CloseWindow((Window)parameter);
            }
        }
    }
}
