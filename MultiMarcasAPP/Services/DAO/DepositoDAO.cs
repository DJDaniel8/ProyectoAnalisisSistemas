using MultiMarcasAPP.Models;
using MultiMarcasAPP.ViewModels.CajaViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Services.DAO
{
    public class DepositoDAO
    {
        public static SqlConnection miconexion { get; } = new SqlConnection(DBConection.CadenaDeConexionContabilidad);

        public static bool Insert(DepositoViewModel deposito)
        {
            string consulta = @"INSERT INTO DepositosInternos (NoDoc, Fecha, Monto)
                                VALUES (@NoDoc,@Fecha,@Monto)";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@NoDoc", deposito.NoDoc);
            comando.Parameters.AddWithValue("@Fecha", deposito.Fecha);
            comando.Parameters.AddWithValue("@Monto", deposito.Monto);
            try
            {
                miconexion.Open();
                comando.ExecuteNonQuery();
                miconexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            return true;
        }

        public static List<DepositoViewModel> GetByYearAndMonth(int año, int mes)
        {
            string consulta = @"SELECT d.Fecha, d.NoDoc, d.Monto, d.Id
                                FROM DepositosInternos as d
                                WHERE YEAR(Fecha) = @año AND MONTH(Fecha) = @mes";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@año", año);
            comando.Parameters.AddWithValue("@mes", mes);
            List<DepositoViewModel> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DepositoViewModel d = new();
                        d.Fecha = reader.GetDateTime(0);
                        d.NoDoc = reader.GetString(1);
                        d.Monto = reader.GetDecimal(2);
                        d.Id = reader.GetInt32(3);

                        lista.Add(d);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static bool Delete(int Id)
        {
            string consulta = @"DELETE FROM DepositosInternos WHERE Id = @id";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", Id);
            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();
            return true;
        }

        public static List<int> GetAños()
        {
            string consulta = @"SELECT YEAR(Fecha) FROM DepositosInternos
                              GROUP BY YEAR(Fecha)";

            SqlCommand comando = new SqlCommand(consulta, miconexion);
            miconexion.Open();
            List<int> lista = new();
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

        public static List<int> GetMeses(int año)
        {
            string consulta = @"SELECT MONTH(Fecha) 
                                FROM DepositosInternos
                                WHERE YEAR(Fecha) = @año
                                GROUP BY MONTH(Fecha)";

            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@año", año);
            miconexion.Open();
            List<int> lista = new();
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
    }
}
