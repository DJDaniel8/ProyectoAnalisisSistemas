using MultiMarcasAPP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Services.DAO
{
    public class TotalDepositosDAO
    {
        public static SqlConnection miconexion { get; } = new SqlConnection(DBConection.CadenaDeConexionContabilidad);

        public static List<int> GetAños()
        {
            string consulta = @"SELECT YEAR(fecha) FROM DepositosInternos
                                GROUP BY YEAR(fecha)";

            SqlCommand comando = new SqlCommand(consulta, miconexion);
            List<int> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lista.Add(reader.GetInt32(0));
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<TotalDepositosMensual> GetByYear(int año)
        {
            string consulta = @"SELECT MONTH(Fecha), YEAR(Fecha), SUM(Monto)
                                FROM DepositosInternos
                                WHERE YEAR(Fecha) = @año
                                GROUP BY YEAR(Fecha), MONTH(Fecha)";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@año", año);
            List<TotalDepositosMensual> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TotalDepositosMensual total = new();
                        total.Mes = reader.GetInt32(0);
                        total.Año = reader.GetInt32(1);
                        total.Monto = reader.GetDecimal(2);
                        total.FechaLetras = $"{total.Mes}/{total.Año}";
                        lista.Add(total);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }
    }
}
