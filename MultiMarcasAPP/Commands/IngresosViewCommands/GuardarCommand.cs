using MultiMarcasAPP.Services.DAO;
using MultiMarcasAPP.ViewModels.IngresosViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Commands.IngresosViewCommands
{
    public class GuardarCommand : CommandBase
    {
        private IngresosViewModel _ViewModel;
        public GuardarCommand(IngresosViewModel viewModel)
        {
            _ViewModel = viewModel;
            _ViewModel.NuevoIngresoViewModel._ProductosIngresos.CollectionChanged += _ProductosIngresos_CollectionChanged;
        }

        private void _ProductosIngresos_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnCanExecutedChanged();
            if(e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                {
                    item.PropertyChanged -= Item_PropertyChanged;
                }
            }
            if(e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                {
                    item.PropertyChanged += Item_PropertyChanged;
                }
            }
        }

        private void Item_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            bool continuar = true;
            if (_ViewModel.NuevoIngresoViewModel._ProductosIngresos.Count == 0) continuar = false;

            if (continuar)
            {
                foreach (var item in _ViewModel.NuevoIngresoViewModel._ProductosIngresos)
                {
                    continuar = item.Cantidad > 0 ? continuar : false;
                    continuar = item.PrecioCompra > 0 ? continuar : false;
                    if(item.Cantidad != 0) continuar = item.PrecioVentaMinimo > item.PrecioCompra - (item.Descuento / item.Cantidad) ? continuar : false;
                    continuar = item.PrecioVenta >= item.PrecioVentaMinimo ? continuar : false;
                }
            }

            return continuar &&
                    base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            
            IngresoDAO.Insert(_ViewModel.NuevoIngresoViewModel.ProductosIngresos.ToList<ProductoIngresoViewModel>(), _ViewModel.SelectedProveedor.Id, _ViewModel.EsAuditorado, parameter, _ViewModel.SelectedDate);
            decimal total = 0;
            foreach (var item in _ViewModel.NuevoIngresoViewModel.ProductosIngresos)
            {
                total = total + item.Total;
            }
            InformacionIngresosDAO.Insert(_ViewModel.NuevoIngresoViewModel.NumeroDocumento, total, IngresoDAO.GetIdLastInsert());
            _ViewModel.NuevoIngresoViewModel.ControlVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.TablasIngresosViewModel.ControlVisibility = System.Windows.Visibility.Visible;
            _ViewModel.ComboBoxProveedorVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.TablaStockExistentesViewModel.ControlVisibility = System.Windows.Visibility.Collapsed;
            _ViewModel.FiltrarComboBoxVisibility = System.Windows.Visibility.Visible;
            _ViewModel.MainButtonNavigationBarVisibility = true;
            _ViewModel.SecundaryButtonNavigationBarVisiblity = false;
            _ViewModel.NuevoIngresoViewModel.NumeroDocumento = String.Empty;
            _ViewModel.NuevoIngresoViewModel.LimpiarLista();
            _ViewModel.TablasIngresosViewModel.CargarIngresos(_ViewModel.SelectedFiltrarPor, _ViewModel.SelectedFiltrar1, _ViewModel.SelectedFiltrar2);
        }
    }
}
