using MultiMarcasAPP.Models;
using MultiMarcasAPP.Services.PDF;
using MultiMarcasAPP.ViewModels.FacturasViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Commands.FacturasViewCommands
{
    public class ImprimirCommand : AsyncCommandBase
    {
        private FacturasViewModel _viewModel;
        public ImprimirCommand(FacturasViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(FacturasViewModel.SelectedVenta))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return (_viewModel.SelectedVenta != null) &&
                    (_viewModel.SelectedVenta.Id > 0) &&
                    (_viewModel.Productos.Count() > 0) &&
                    base.CanExecute(parameter);
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            switch (CargarDatos(1))
            {
                case "80mm":
                    Task tarea1 = Services.PDF.PDFMaker.CreatePDFVenta8mm(_viewModel.Productos.ToList(), _viewModel.SelectedVenta);
                    await tarea1;

                    Task tarea2 = Services.PDF.PrintPDF.PrintVenta();
                    await tarea2;
                    break;
                case "Carta":
                    Task tarea3 = Services.PDF.PDFMaker.CreatePDFVenta(_viewModel.Productos.ToList(), _viewModel.SelectedVenta);
                    await tarea3;

                    Task tarea4 = Services.PDF.PrintPDF.PrintVenta();
                    await tarea4;
                    break;
                default:
                    break;
            }
        }

        private static string CargarDatos(int tipo)
        {
            string nombreImpresora = "";
            if (!Directory.Exists("C:/MultiMarcasApp"))
            {
                Directory.CreateDirectory("C:/MultiMarcasApp");
            }
            if (!Directory.Exists("C:/MultiMarcasApp/Configuraciones"))
            {
                Directory.CreateDirectory("C:/MultiMarcasApp/Configuraciones");
            }

            String line;
            try
            {

                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("C:/MultiMarcasApp/Configuraciones\\Config.txt");
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    if (line.StartsWith(tipo.ToString()))
                    {
                        nombreImpresora = line.Replace($"{tipo}:", "");
                    }
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
            }
            return nombreImpresora;
        }
    }
}
