using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.ViewModels.ClientesViewModels
{
    public class InformacionClienteViewModel : ViewModelBase
    {
        public InformacionClienteViewModel(ClienteViewModel cliente)
        {
            _Cliente = cliente;
            _Cliente.PropertyChanged += (sender,e) => OnPropertyChanged(nameof(Cliente));
        }

        private ClienteViewModel _Cliente;
        public ClienteViewModel Cliente
        {
            get
            {
                return _Cliente;
            }
            set
            {
                _Cliente = value;
                OnPropertyChanged(nameof(Cliente));
                _Cliente.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Cliente));
            }
        }

        private bool _TxtBoxReadOnly = true;   
        public bool TxtBoxReadOnly
        {
            get
            {
                return _TxtBoxReadOnly;
            }
            set
            {
                _TxtBoxReadOnly = value;
                OnPropertyChanged(nameof(TxtBoxReadOnly));
            }
        }


    }
}
