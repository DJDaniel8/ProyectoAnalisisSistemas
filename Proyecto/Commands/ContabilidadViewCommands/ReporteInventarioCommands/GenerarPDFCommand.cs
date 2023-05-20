using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.ContabilidadViewModels;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ContabilidadViewCommands.ReporteInventarioCommands
{
    public class GenerarPDFCommand : CommandBase
    {
        private ReporteInventarioViewModel _viewModel;
        public GenerarPDFCommand(ReporteInventarioViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ReporteInventarioViewModel.Fecha1))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_viewModel.Fecha1 != null) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            Services.PDF.PDFMaker.CreateReporteInventario(ReporteInventarioDAO.GetInfo(_viewModel.Fecha1.AddDays(1)), _viewModel.Fecha1);
        }
    }
}
