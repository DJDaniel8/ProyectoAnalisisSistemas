using MultiMarcasAPP.Commands.CajaViewCommands;
using MultiMarcasAPP.Commands;
using MultiMarcasAPP.Models;
using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.VentaViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using iText.Kernel.Pdf;
using System.IO;
using MultiMarcasAPP.Commands.ConfiguracionesCommands;
using MultiMarcasAPP.ViewModels.ClientesViewModels;
using System.Printing;

namespace MultiMarcasAPP.ViewModels.ConfiguracionesViewModels
{
    public class ConfiguracionesViewModel : ViewModelBase
    {
        public ConfiguracionesViewModel()
        {
            _Impresoras = new();
            CloseCommand = new CloseCommand();
            MinimizeCommand = new MinimizeCommand();
            MaximizeCommand = new MaximizeCommand();
            RestoreCommand = new RestoreCommand();
            GuardarCommand = new GuardarCommand(this);
            LocalPrintServer printServer = new LocalPrintServer();
            PrintQueueCollection listaImpresoras = printServer.GetPrintQueues();
            foreach (var item in listaImpresoras)
            {
                _Impresoras.Add(item.Name);
            }
            CargarDatos();
        }

        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand RestoreCommand { get; }
        public ICommand GuardarCommand { get; }

        private ObservableCollection<string> _Impresoras;
        public IEnumerable<string> Impresoras => _Impresoras;

        public IEnumerable<string> TipoDocumentos { get; set; } = new string[] { "Carta", "80mm" };

        private Visibility _maximizeVisibility = Visibility.Visible;
        public Visibility MaximizeVisibility
        {
            get { return _maximizeVisibility; }
            set
            {
                _maximizeVisibility = value;
                OnPropertyChanged(nameof(MaximizeVisibility));
            }
        }

        private Visibility _restoreVisibility = Visibility.Collapsed;

        public Visibility RestoreVisibility
        {
            get { return _restoreVisibility; }
            set
            {
                _restoreVisibility = value;
                OnPropertyChanged(nameof(RestoreVisibility));
            }
        }

        private WindowState _windowState;

        public WindowState StateOfWindow
        {
            get { return _windowState; }
            set
            {
                _windowState = value;
                OnPropertyChanged(nameof(StateOfWindow));
                if (WindowState.Maximized == _windowState)
                {
                    MaximizeVisibility = Visibility.Collapsed;
                    RestoreVisibility = Visibility.Visible;
                }
                else if (WindowState.Normal == _windowState)
                {
                    MaximizeVisibility = Visibility.Visible;
                    RestoreVisibility = Visibility.Collapsed;
                }
            }
        }

        private string _TipoImpresionVenta;
        public string TipoImpresionVenta
        {
            get
            {
                return _TipoImpresionVenta;
            }
            set
            {
                _TipoImpresionVenta = value;
                OnPropertyChanged(nameof(TipoImpresionVenta));
            }
        }

        private string _TipoImpresionCotizacion;
        public string TipoImpresionCotizacion
        {
            get
            {
                return _TipoImpresionCotizacion;
            }
            set
            {
                _TipoImpresionCotizacion = value;
                OnPropertyChanged(nameof(TipoImpresionCotizacion));
            }
        }

        private string _ImpresoraVenta;
        public string ImpresoraVenta
        {
            get
            {
                return _ImpresoraVenta;
            }
            set
            {
                _ImpresoraVenta = value;
                OnPropertyChanged(nameof(ImpresoraVenta));
            }
        }

        private string _ImpresoraCotizacion;
        public string ImpresoraCotizacion
        {
            get
            {
                return _ImpresoraCotizacion;
            }
            set
            {
                _ImpresoraCotizacion = value;
                OnPropertyChanged(nameof(ImpresoraCotizacion));
            }
        }

        public void CargarDatos()
        {
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
                    if(line.StartsWith("1"))
                    {
                        TipoImpresionVenta = line.Replace("1:", "");
                    }
                    else if(line.StartsWith("2"))
                    {
                        TipoImpresionCotizacion = line.Replace("2:", "");
                    }
                    else if(line.StartsWith("3"))
                    {
                        ImpresoraVenta = line.Replace("3:", "");
                    }
                    else if(line.StartsWith("4"))
                    {
                        ImpresoraCotizacion = line.Replace("4:", "");
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
        }

        public void GuardarDatos()
        {
            if (!Directory.Exists("C:/MultiMarcasApp"))
            {
                Directory.CreateDirectory("C:/MultiMarcasApp");
            }
            if (!Directory.Exists("C:/MultiMarcasApp/Configuraciones"))
            {
                Directory.CreateDirectory("C:/MultiMarcasApp/Configuraciones");
            }

            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = File.CreateText("C:/MultiMarcasApp/Configuraciones\\Config.txt");

                //Write a line of text
                sw.WriteLine($"1:{TipoImpresionVenta}");
                sw.WriteLine($"2:{TipoImpresionCotizacion}");
                sw.WriteLine($"3:{ImpresoraVenta}");
                sw.WriteLine($"4:{ImpresoraCotizacion}");
                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
            }
        }
    }
}
