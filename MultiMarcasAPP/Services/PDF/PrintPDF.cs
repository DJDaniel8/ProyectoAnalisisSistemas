using iText.Kernel.Events;
using MultiMarcasAPP.Models;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Services.PDF
{
    public class PrintPDF
    {
        public static Task<bool> PrintVenta()
        {
            return (Task.Factory.StartNew(() =>
            {
                PdfDocument doc = new PdfDocument();
                string path = "";
                string tipo = CargarDatos(1);
                if (tipo == "80mm")
                {
                    path = $"C:/MultiMarcasApp/Venta/Venta.pdf";
                }
                else if(tipo == "Carta")
                {
                    path = $"C:/MultiMarcasApp/Venta/Venta.pdf";
                }
                doc.LoadFromFile(path);
                doc.PrintSettings.PrinterName = CargarDatos(3);
                doc.CustomScaling = 100;
                doc.PageSettings.Width = doc.Pages[0].ActualSize.Width;
                doc.PageSettings.Height = doc.Pages[0].ActualSize.Height;
                MessageBox.Show(doc.PageSettings.Size.ToString());
                doc.Print();
                return true;
            }));
        }

        public static Task<bool> PrintCotizacion()
        {
            return (Task.Factory.StartNew(() =>
            {
                PdfDocument doc = new PdfDocument();
                string path = "";
                string tipo = CargarDatos(2);
                if (tipo == "80mm")
                {
                    path = $"C:/MultiMarcasApp/Cotizacion/Cotizacion.pdf";
                }
                else if (tipo == "Carta")
                {
                    path = $"C:/MultiMarcasApp/Cotizacion/Cotizacion.pdf";
                }
                doc.LoadFromFile(path);
                doc.PrintSettings.PrinterName = CargarDatos(4);
                doc.Print();
                return true;
            }));
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
