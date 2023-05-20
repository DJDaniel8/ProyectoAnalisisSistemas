using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels;
using MultiMarcasAPP.ViewModels.ClientesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.ClientesViewCommands
{
    public class GuardarCommand : CommandBase
    {
        private ClientesViewModel _ViewModel;

        public GuardarCommand(ClientesViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ClientesViewModel.CurrentViewModel))
            {
                _ViewModel.CurrentViewModel.PropertyChanged += CurrentViewModel_PropertyChanged;
            }
        }

        private void CurrentViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(InformacionClienteViewModel.Cliente))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            ClienteViewModel cliente = ((InformacionClienteViewModel)_ViewModel.CurrentViewModel).Cliente;
            return (cliente != null) &&
                    (!String.IsNullOrEmpty(cliente.Nombre)) &&
                    (!String.IsNullOrEmpty(cliente.Apellido)) &&
                    (!String.IsNullOrEmpty(cliente.Sexo)) &&
                    (!String.IsNullOrEmpty(cliente.Nit)) &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            ClienteViewModel cliente = ((InformacionClienteViewModel)_ViewModel.CurrentViewModel).Cliente;
            if (cliente.Cliente.Id == 0)
            {
                ClienteDAO.Insert(cliente);
            }
            else ClienteDAO.Update(cliente);

            _ViewModel.MainButtonsBarVisibility = true;
            _ViewModel.SecundaryButtonsBarVisibility = false;
            _ViewModel.DataGridEneable = true;
            ((InformacionClienteViewModel)_ViewModel.CurrentViewModel).TxtBoxReadOnly = true;
            ((InformacionClienteViewModel)_ViewModel.CurrentViewModel).Cliente = new ClienteViewModel(new Models.Cliente());
            _ViewModel.CargarClientes();
        }
    }
}
