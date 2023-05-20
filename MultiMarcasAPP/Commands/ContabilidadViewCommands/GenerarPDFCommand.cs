using MultiMarcasAPP.ViewModels.ContabilidadViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ContabilidadViewCommands
{
    public class GenerarPDFCommand : CommandBase
    {
        
        private ReporteVentasViewModel _viewModel;
        public GenerarPDFCommand(ReporteVentasViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ReporteVentasViewModel.SelectedAño) ||
                e.PropertyName == nameof(ReporteVentasViewModel.SelectedMes) )
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_viewModel.SelectedAño > 0) &&
                    (_viewModel.SelectedMes > 0) &&
                    base.CanExecute(parameter);
        }
        
        public async override void Execute(object? parameter)
        {

            Task<bool> t = Services.PDF.PDFMaker.CreateReporteVentas(_viewModel.Reportes.ToList(), _viewModel.Vienen, _viewModel.SaldoFinal);

            await t;


            //Task<bool> t2 = Services.PDF.PrintPDF.PrintVenta();

            //await t2;
        }
    }
}
