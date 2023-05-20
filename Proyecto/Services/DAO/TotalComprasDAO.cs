using MultiMarcasAPP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Services.DAO
{
    public class TotalComprasDAO 
    {
        public static SqlConnection miconexion { get; } = new SqlConnection(DBConection.CadenaDeConexion);

        public static List<TotalComprasMensual> Get(int año)
        {
            string consulta = @"SELECT MONTH(fecha), YEAR(fecha), SUM(pri.cantidad*pri.preciocompra)
                                FROM ingresos
                                INNER JOIN productos_ingresos as pri ON ingresos.ingresoId = pri.ingresoId
                                WHERE YEAR(fecha) = @año
                                group by YEAR(fecha), MONTH(fecha)";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@año", año);
            miconexion.Open();
            List<TotalComprasMensual> lista = new();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        TotalComprasMensual totalcompras = new TotalComprasMensual();
                        totalcompras.Mes = reader.GetInt32(0);
                        totalcompras.Año = reader.GetInt32(1);
                        totalcompras.Total = reader.GetDecimal(2);
                        totalcompras.Fecha = $"{totalcompras.Mes}/{totalcompras.Año}";
                        lista.Add(totalcompras);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<int> GetAños()
        {
            string consulta = @"SELECT YEAR(fecha) FROM ingresos
                              GROUP BY YEAR(fecha)";

            SqlCommand comando = new SqlCommand(consulta, miconexion);
            miconexion.Open();
            List<int> lista = new();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
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
    }
}
