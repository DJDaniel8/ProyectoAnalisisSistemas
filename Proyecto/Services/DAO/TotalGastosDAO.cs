using MultiMarcasAPP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Services.DAO
{
    public class TotalGastosDAO
    {
        public static SqlConnection miconexion { get; } = new SqlConnection(DBConection.CadenaDeConexionContabilidad);

        public static List<int> GetAños()
        {
            string consulta = @"SELECT YEAR(P.Fecha) 
                                FROM Gastos as g
                                INNER JOIN Pagos as p ON g.PagoId = p.Id
                                GROUP BY YEAR(p.Fecha)";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            List<int> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        lista.Add(reader.GetInt32(0));
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<TotalGastosMensual> GetDeduciblesByDate(int año)
        {
            string consulta = @"SELECT MONTH(p.Fecha), YEAR(p.Fecha), SUM(p.Monto)
                                FROM Gastos AS g
                                INNER JOIN Pagos as p ON g.PagoId = p.Id
                                INNER JOIN CategoriaGastos as cg ON g.CategoriaGastoId = cg.Id
                                WHERE YEAR(p.Fecha) = @año AND cg.Deducible = 1
                                GROUP BY YEAR(p.Fecha), MONTH(p.Fecha)";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@año", año);
            List<TotalGastosMensual> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        TotalGastosMensual total = new();
                        total.Mes = reader.GetInt32(0);
                        total.Año = reader.GetInt32(1);
                        total.Total = reader.GetDecimal(2);
                        total.FechaLetras = $"{total.Mes}/{total.Año} Deducible";
                        total.Deducible = true;
                        lista.Add(total);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<TotalGastosMensual> GetNoDeduciblesByDate(int año)
        {
            string consulta = @"SELECT MONTH(p.Fecha), YEAR(p.Fecha), SUM(p.Monto)
                                FROM Gastos AS g
                                INNER JOIN Pagos as p ON g.PagoId = p.Id
                                INNER JOIN CategoriaGastos as cg ON g.CategoriaGastoId = cg.Id
                                WHERE YEAR(p.Fecha) = @año AND cg.Deducible = 0
                                GROUP BY YEAR(p.Fecha), MONTH(p.Fecha)";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@año", año);
            List<TotalGastosMensual> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TotalGastosMensual total = new();
                        total.Mes = reader.GetInt32(0);
                        total.Año = reader.GetInt32(1);
                        total.Total = reader.GetDecimal(2);
                        total.FechaLetras = $"{total.Mes}/{total.Año} No Deducible";
                        total.Deducible = false;
                        lista.Add(total);
                    }
                }
            }

            miconexion.Close();
            return lista;
        }
    }
}
