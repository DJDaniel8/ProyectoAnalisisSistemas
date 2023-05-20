using MultiMarcasAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.ViewModels.CajaViewModels
{
    public class GastoViewModel : ViewModelBase
    {
        public GastoViewModel(Gasto gasto)
        {
            Gasto = gasto;
            _Nombre = gasto.Nombre;
            _Descripcion = gasto.Descripcion;
            _Fecha = gasto.Fecha;
            _Monto = gasto.Monto;
            _EsDeducible = gasto.EsDeducible;
            _EsEfectivo = gasto.EsEfectivo;
        }

        public Gasto Gasto { get; set; }

        private DateTime _Fecha;
        public DateTime Fecha
        {
            get
            {
                return _Fecha;
            }
            set
            {
                _Fecha = value;
                OnPropertyChanged(nameof(Fecha));
                Gasto.Fecha = Fecha;
            }
        }

        private string _Nombre;
        public string Nombre
        {
            get
            {
                return _Nombre;
            }
            set
            {
                _Nombre = value;
                OnPropertyChanged(nameof(Nombre));
                Gasto.Nombre = Nombre;
            }
        }

        private decimal _Monto;
        public decimal Monto
        {
            get
            {
                return _Monto;
            }
            set
            {
                _Monto = value;
                OnPropertyChanged(nameof(Monto));
                Gasto.Monto = Monto;
            }
        }

        private string _Descripcion;
        public string Descripcion
        {
            get
            {
                return _Descripcion;
            }
            set
            {
                _Descripcion = value;
                OnPropertyChanged(nameof(Descripcion));
                Gasto.Descripcion = Descripcion;
            }
        }

        private bool _EsDeducible;
        public bool EsDeducible
        {
            get
            {
                return _EsDeducible;
            }
            set
            {
                _EsDeducible = value;
                OnPropertyChanged(nameof(EsDeducible));
                Gasto.EsDeducible = EsDeducible;
            }
        }

        private bool _EsEfectivo;
        public bool EsEfectivo
        {
            get
            {
                return _EsEfectivo;
            }
            set
            {
                _EsEfectivo = value;
                OnPropertyChanged(nameof(EsEfectivo));
                Gasto.EsEfectivo = EsEfectivo;
            }
        }
    }
}
