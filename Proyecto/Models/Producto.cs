using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool EsDerivado { get; set; }
        public int Multiplicador { get; set; }
        public int PadreId { get; set; }
        public string Categorias { get; set; } = string.Empty;
        public string CodigoPadre { get; set; } = string.Empty;
        public string Proveedores { get; set; } = string.Empty;
        public decimal Stock { get; set; }
    }
}
