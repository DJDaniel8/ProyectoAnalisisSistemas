using MultiMarcasAPP.ViewModels.ContabilidadViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ContabilidadViewCommands.DepositosCommands
{
    public class GenerarPDFCommand : CommandBase
    {
        private TotalDespositosViewModel _viewModel;

        public GenerarPDFCommand(TotalDespositosViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TotalDespositosViewModel.SelectedTotal))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_viewModel.SelectedTotal != null) &&
                    (_viewModel.SelectedTotal.Monto > 0) &&
                    base.CanExecute(parameter);
        }

        public async override void Execute(object? parameter)
        {

            Task<bool> t = Services.PDF.PDFMaker.CreateReporteDepositos(_viewModel.Depositos.ToList());

            await t;


            //Task<bool> t2 = Services.PDF.PrintPDF.PrintVenta();

            //await t2;
        }
    }
}
