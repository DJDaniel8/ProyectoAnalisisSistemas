﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Models
{
    public class Venta
    {
        public Venta(Cliente cliente, Trabajador trabajador)
        {
            Cliente = cliente;
            Trabajador = trabajador;
            Nombre = cliente.Nombre + " " + cliente.Apellido;
            NombreTrabajador = trabajador.Nombre + " " + trabajador.Apellido;
        }

        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public Trabajador Trabajador { get; set; }
        public DateTime Fecha { get; set; }
        public bool EsAuditorada { get; set; }
        public string Nombre { get; set; }
        public string NombreTrabajador { get; set; }
        public decimal Total { get; set; }
    }
}
