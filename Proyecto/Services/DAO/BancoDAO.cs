using MultiMarcasAPP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Services.DAO
{
    public static class BancoDAO
    {
        public static SqlConnection miconexion { get; } = new SqlConnection(DBConection.CadenaDeConexion);

        public static bool Insert(Banco banco, int proveedorId)
        {
            string consulta = @"INSERT INTO Bancos (proveedorId, nombre, cuenta, nombre_cuenta) VALUES (@proveedorId, @nombre, @cuenta, @nombre_cuenta)";
            SqlCommand comando = new SqlCommand(consulta,miconexion);
            comando.Parameters.AddWithValue("@proveedorId", proveedorId);
            comando.Parameters.AddWithValue("@nombre", banco.Nombre);
            comando.Parameters.AddWithValue("@cuenta", banco.NumeroCuenta);
            comando.Parameters.AddWithValue("@nombre_cuenta", banco.NombreCuenta);

            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();
            return true;
        }

        public static List<Banco> GetByIdProvider(int id)
        {
            string consulta = "SELECT * FROM Bancos WHERE proveedorId = @id";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", id);
            List<Banco> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Banco banco = new();
                        banco.BancoId = reader.GetInt32(0);
                        banco.ProveedorId = reader.GetInt32(1);
                        banco.Nombre = reader.GetString(2);
                        banco.NumeroCuenta = reader.GetString(3);
                        banco.NombreCuenta = reader.GetString(4);
                        lista.Add(banco);
                    }
                }
            }
            miconexion.Close();
            return lista;
            
        }

        public static bool Delete(Banco banco)
        {
            string consulta = "DELETE FROM Bancos WHERE bancoId = @id";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", banco.BancoId);
            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();

            return true;
        }
    }
}
