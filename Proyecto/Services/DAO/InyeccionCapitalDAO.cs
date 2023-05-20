using MultiMarcasAPP.ViewModels.InyeccionCapitalViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Services.DAO
{
    internal class InyeccionCapitalDAO
    {
        public static SqlConnection miconexion { get; } = new SqlConnection(DBConection.CadenaDeConexionContabilidad);

        public static bool Insert(decimal Monto, DateTime fecha)
        {
            string consulta = @"INSERT INTO InyeccionesCapital (Monto, Fecha)
                                VALUES (@Monto, @Fecha)";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@Monto", Monto);
            comando.Parameters.AddWithValue("@Fecha", fecha);

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

        public static bool Delete(int Id)
        {
            string consulta = @"DELETE FROM InyeccionesCapital WHERE Id = @Id";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@Id", Id);

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

        public static List<InyeccionViewModel> Get()
        {
            string consulta = "SELECT * FROM InyeccionesCapital";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            List<InyeccionViewModel> lista = new();

            try
            {
                miconexion.Open();
                SqlDataReader reader = comando.ExecuteReader();
                using (reader)
                {
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            InyeccionViewModel i = new();
                            i.Id = reader.GetInt32(0);
                            i.Monto = reader.GetDecimal(1);
                            i.Fecha = reader.GetDateTime(2);
                            lista.Add(i);
                        }
                    }
                }
                miconexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return lista;
        }
    }
}
