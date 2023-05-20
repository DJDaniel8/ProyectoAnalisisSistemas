using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Services.DAO
{
    public static class DBConection
    {
        public static string CadenaDeConexion { get; } = ConfigurationManager.ConnectionStrings["MULTIMARCASCONNECTION"].ConnectionString;
        public static string CadenaDeConexionContabilidad { get; } = ConfigurationManager.ConnectionStrings["MULTIMARCASCONTABILIDADCONNECTION"].ConnectionString;
    }
}
