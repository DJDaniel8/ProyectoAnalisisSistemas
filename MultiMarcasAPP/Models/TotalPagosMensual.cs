﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Models
{
    public class TotalPagosMensual
    {
        public int Mes { get; set; }
        public int Año { get; set; }
        public decimal Total { get; set; }
        public string FechaLetras { get; set; } = string.Empty;
        public bool SaleDeCaja { get; set; }
    }
}
