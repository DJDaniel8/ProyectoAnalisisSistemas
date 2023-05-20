using Microsoft.Win32;
using MultiMarcasAPP.ViewModels.ProductosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MultiMarcasAPP.Commands.ProductosViewCommands
{
    public class CargarImagenCommand : CommandBase
    {
        private ProductoInformacionViewModel _ViewModel;
        public CargarImagenCommand(ProductoInformacionViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            BitmapImage imagen = new BitmapImage();
            fileDialog.Title = "Seleccione la Imagen del Producto";
            fileDialog.Filter = "Todos(*.*) | *.*| Imagenes | *.jpg; *.gif; *.png; *.bmp";
            if(fileDialog.ShowDialog() == true)
            {
                imagen.BeginInit();
                imagen.UriSource = new Uri(fileDialog.FileName);
                imagen.EndInit();
                _ViewModel.Imagen = imagen;
                System.IO.Stream stream = new System.IO.MemoryStream();
                try
                {
                    if((stream = fileDialog.OpenFile()) != null)
                    {
                        using(stream)
                        {
                            _ViewModel.ImagenData = new byte[stream.Length];
                            stream.Read(_ViewModel.ImagenData, 0, (int)stream.Length);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
