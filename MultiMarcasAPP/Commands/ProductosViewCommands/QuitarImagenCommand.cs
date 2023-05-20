using MultiMarcasAPP.ViewModels.ProductosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ProductosViewCommands
{
    public class QuitarImagenCommand : CommandBase
    {
        private ProductoInformacionViewModel _ViewModel;
        public QuitarImagenCommand(ProductoInformacionViewModel viewModel)
        {
            _ViewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _ViewModel.ImagenData = null;
            _ViewModel.Imagen = null;
        }
    }
}
