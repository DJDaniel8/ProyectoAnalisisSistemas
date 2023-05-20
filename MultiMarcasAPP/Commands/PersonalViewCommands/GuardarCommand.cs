using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels;
using MultiMarcasAPP.ViewModels.PersonalViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.PersonalViewCommands
{
    public class GuardarCommand : CommandBase
    {
        private PersonalViewModel _ViewModel;
        public GuardarCommand(PersonalViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.PropertyChanged += _ViewModel_PropertyChanged;
            _ViewModel.PedirInformacionPersonalViewModel.PropertyChanged += PedirInformacionPersonalViewModel_PropertyChanged;
        }

        private void PedirInformacionPersonalViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(PedirInformacionPersonalViewModel.Trabajador))
            {
                if (_ViewModel.PedirInformacionPersonalViewModel.Trabajador != null)
                {
                    _ViewModel.PedirInformacionPersonalViewModel.Trabajador.PropertyChanged += Trabajador_PropertyChanged;
                }
            }
            else if(e.PropertyName == nameof(PedirInformacionPersonalViewModel.Password))
            {
                OnCanExecutedChanged();
            }
        }

        private void Trabajador_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }

        private void _ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if( e.PropertyName == nameof(PersonalViewModel.SelectedTrabajador))
            {
                if (_ViewModel.SelectedTrabajador != null)
                {
                    _ViewModel.SelectedTrabajador.PropertyChanged += SelectedTrabajador_PropertyChanged;
                }
            }
        }

        private void SelectedTrabajador_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            if (_ViewModel.SecundaryButtonNavigationBarVisiblity)
            {
                PedirInformacionPersonalViewModel vm = (PedirInformacionPersonalViewModel)_ViewModel.PedirInformacionPersonalViewModel;
                if(vm.Trabajador.trabajador.Id == 0)
                {
                    return !String.IsNullOrEmpty(vm.Trabajador.Nombre) &&
                            !String.IsNullOrEmpty(vm.Trabajador.Apellido) &&
                            !String.IsNullOrEmpty(vm.Trabajador.Sexo) &&
                            !String.IsNullOrEmpty(vm.Trabajador.Puesto) &&
                            !String.IsNullOrEmpty(vm.Trabajador.Usuario) &&
                            !String.IsNullOrEmpty(vm.Password);
                }
                else
                {
                    return !String.IsNullOrEmpty(vm.Trabajador.Nombre) &&
                            !String.IsNullOrEmpty(vm.Trabajador.Apellido) &&
                            !String.IsNullOrEmpty(vm.Trabajador.Sexo) &&
                            !String.IsNullOrEmpty(vm.Trabajador.Puesto) &&
                            !String.IsNullOrEmpty(vm.Trabajador.Usuario);
                }
            }
            else return false;
            
        }
        public override void Execute(object? parameter)
        {
            PedirInformacionPersonalViewModel vm = (PedirInformacionPersonalViewModel)_ViewModel.PedirInformacionPersonalViewModel;
            if (vm.Trabajador.trabajador.Id == 0)
            {
                TrabajadorDAO.Insert(vm.Trabajador.trabajador, vm.Password);
            }
            else
            {
                TrabajadorDAO.Update(vm.Trabajador.trabajador);
            }

            
            _ViewModel.DataGridEneable = true;
            _ViewModel.MainButtonNavigationBarVisibility = true;
            _ViewModel.SecundaryButtonNavigationBarVisiblity = false;
            _ViewModel.CargarTrabajadores();
            _ViewModel.PedirInformacionPersonalViewModel.ControlVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.MostrarInformacionPersonalViewModel.ControlVisibility = System.Windows.Visibility.Visible;
            _ViewModel.PedirInformacionPersonalViewModel.PasswordVisibility = System.Windows.Visibility.Collapsed;
        }
    }
}
