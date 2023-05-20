using MultiMarcasAPP.Commands.InformacionIngresosCommands.VerPagosCommands;
using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.PagosViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.InformacionIngresosViewModels
{
    public class InformacionIngresosVerPagosViewModel : ViewModelBase
    {
        public InformacionIngresosVerPagosViewModel(InformacionIngresosViewModel viewModelPadre)
        {
            ViewModelPadre = viewModelPadre;
            RegresarCommand = new RegresarCommand(this);
            _Pagos = new();
        }
        public ICommand RegresarCommand { get; set; }
        public InformacionIngresosViewModel ViewModelPadre { get; set; }

        private Visibility _Visibility = Visibility.Collapsed;
        public Visibility Visibility
        {
            get
            {
                return _Visibility;
            }
            set
            {
                _Visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }

        public ObservableCollection<PagoViewModel> _Pagos;
        public IEnumerable<PagoViewModel> Pagos => _Pagos;

        public void CargarPagos()
        {
            _Pagos.Clear();
            foreach (var item in PagosDAO.GetByInformacionIngresosId(ViewModelPadre.informacionIngresosVerViewModel.SelectedIIV.Id))
            {
                _Pagos.Add(item);
            }
        }
    }
}
