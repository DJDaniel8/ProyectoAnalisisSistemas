﻿using MultiMarcasAPP.Commands.CotizacionCommands;
using MultiMarcasAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MultiMarcasAPP.ViewModels.CotizacionViewModels
{
    public class ProductoCotizacionViewModel : ViewModelBase
    {
        public ProductoCotizacionViewModel(Producto producto, Stock stock, CotizacionViewModel? viewModel)
        {
            Producto = producto;
            Stock = stock;
            if (viewModel != null) QuitarProductoCommand = new QuitarProductoCommand(viewModel);
            _PrecioVenta = Stock.PrecioVenta; OnPropertyChanged(nameof(PrecioVenta));
            Cantidad = 1;
        }

        public ICommand? QuitarProductoCommand { get; }

        public Producto Producto { get; set; }
        public Stock Stock { get; set; }


        private decimal _PrecioVenta;
        public decimal PrecioVenta
        {
            get
            {
                return _PrecioVenta;
            }
            set
            {
                if (CurrentEmploye.Trabajador.Puesto == "Master" || CurrentEmploye.Trabajador.IsAllowChangePrice)
                {
                    if (value >= Stock.PrecioMinimo)
                    {
                        _PrecioVenta = value;
                        Total = _Cantidad * PrecioVenta;
                    }
                }
                OnPropertyChanged(nameof(PrecioVenta));
            }
        }

        private int _Cantidad;
        public int Cantidad
        {
            get
            {
                return _Cantidad;
            }
            set
            {
                if (value <= Stock.Cantidad && value >= 0)
                {
                    _Cantidad = value;
                    Total = _Cantidad * PrecioVenta;
                }
                OnPropertyChanged(nameof(Cantidad));
            }
        }

        private decimal _Total;
        public decimal Total
        {
            get
            {
                return _Total;
            }
            set
            {
                _Total = value;
                OnPropertyChanged(nameof(Total));
            }
        }
    }
}
