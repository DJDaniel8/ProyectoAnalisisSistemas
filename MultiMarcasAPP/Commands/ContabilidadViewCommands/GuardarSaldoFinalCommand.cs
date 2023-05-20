using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.ContabilidadViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.ContabilidadViewCommands
{
    public class GuardarSaldoFinalCommand : CommandBase
    {
        
        private ReporteVentasViewModel _viewModel;
        public GuardarSaldoFinalCommand(ReporteVentasViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ReporteVentasViewModel.SelectedMes) ||
                e.PropertyName == nameof(ReporteVentasViewModel.Vienen) ||
                e.PropertyName == nameof(ReporteVentasViewModel.SaldoFinal))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_viewModel.SelectedMes > 0) &&
                    (_viewModel.SaldoFinal > 0) &&
                    base.CanExecute(parameter);
        }

        
        public override void Execute(object? parameter)
        {
            
            if(ReporteVentaDAO.ExisteSaldoFinalMesAnterior(_viewModel.SelectedAño, _viewModel.SelectedMes))
            {
                MessageBox.Show("No puedes guardar el saldo final porque ya existe uno existente",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                DateTime fechaHoy = DateTime.Now;
                int año = fechaHoy.Year;
                int mes = fechaHoy.Month;
                int dia = fechaHoy.Day;
                if(año >= _viewModel.SelectedAño)
                {
                    if(mes > _viewModel.SelectedMes)
                    {
                        if(mes-1 == _viewModel.SelectedMes)
                        {
                            if(dia > 7)
                            {
                                ReporteVentaDAO.InsertSaldoFinal(_viewModel.SelectedAño, _viewModel.SelectedMes, _viewModel.SaldoFinal);
                                MessageBox.Show("Saldo Guardado Perfectamente");
                                return;
                            }
                            else
                            {
                                MessageBox.Show("No podemos guardar el saldo ya que deben de haber pasado minimo 7 dias del siguiente mes del saldo para guardarlo",
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                        else
                        {
                            ReporteVentaDAO.InsertSaldoFinal(_viewModel.SelectedAño, _viewModel.SelectedMes, _viewModel.SaldoFinal);
                            MessageBox.Show("Saldo Guardado Perfectamente");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No podemos guardar el saldo ya que el mes en curso es el mismo o anterior al saldo que quiere guardar",
                            "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("No podemos guardar el Saldo ya que el año en curso es anterior al saldo que quiere guardar",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            
        }
    }
}
