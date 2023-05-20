using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Models
{
    public class Banco
    {
        public int BancoId { get; set; }
        public int ProveedorId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string NombreCuenta { get; set; } = string.Empty;
        public string NumeroCuenta { get; set; } = string.Empty;
    }
}
