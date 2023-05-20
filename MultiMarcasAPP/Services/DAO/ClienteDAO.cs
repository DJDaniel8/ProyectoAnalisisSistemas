using MultiMarcasAPP.Models;
using MultiMarcasAPP.ViewModels.ClientesViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Services.DAO
{
    internal class ClienteDAO
    {
        public static SqlConnection miconexion { get; } = new SqlConnection(DBConection.CadenaDeConexion);

        public static List<Cliente> Get()
        {
            return GetAll();
        }

        private static List<Cliente> GetAll()
        {
            string consulta = "SELECT * FROM Clientes";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            List<Cliente> lista = new();
            if(reader.HasRows)
            {
                while (reader.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.Id = reader.GetInt32(0);
                    cliente.Nombre = reader.GetString(1);
                    cliente.Apellido = reader.GetString(2);
                    cliente.Sexo = reader.GetString(3);
                    cliente.Nit = reader.GetString(4);
                    if(!reader.IsDBNull(5)) cliente.Direccion = reader.GetString(5);
                    if (!reader.IsDBNull(6))  cliente.Telefono = reader.GetString(6);
                    if (!reader.IsDBNull(7))  cliente.Email = reader.GetString(7);
                    lista.Add(cliente);
                }
            }
            miconexion.Close();
            return lista;
        }

        public static void Insert(ClienteViewModel cliente)
        {
            string consulta = @"INSERT INTO Clientes (nombre, apellido, sexo, nit, direccion, telefono, email)
                                VALUES (@nombre, @apellido, @sexo, @nit, @direccion, @telefono, @email)";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@nombre", cliente.Cliente.Nombre);
            comando.Parameters.AddWithValue("@apellido", cliente.Cliente.Apellido);
            comando.Parameters.AddWithValue("@sexo", cliente.Cliente.Sexo);
            comando.Parameters.AddWithValue("@nit", cliente.Cliente.Nit);
            if(!String.IsNullOrEmpty(cliente.Cliente.Direccion)) comando.Parameters.AddWithValue("@direccion", cliente.Cliente.Direccion);
            else comando.Parameters.AddWithValue("@direccion", DBNull.Value);
            if (!String.IsNullOrEmpty(cliente.Cliente.Telefono)) comando.Parameters.AddWithValue("@telefono", cliente.Cliente.Telefono);
            else comando.Parameters.AddWithValue("@telefono", DBNull.Value);
            if (!String.IsNullOrEmpty(cliente.Cliente.Email)) comando.Parameters.AddWithValue("@email", cliente.Cliente.Email);
            else comando.Parameters.AddWithValue("@email", DBNull.Value);

            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();
        }

        public static void Update(ClienteViewModel cliente)
        {
            string consulta = @"UPDATE Clientes SET nombre = @nombre, apellido = @apellido, sexo = @sexo, nit = @nit, direccion = @direccion, 
                                telefono = @telefono, email = @email WHERE clienteId = @id";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@nombre", cliente.Cliente.Nombre);
            comando.Parameters.AddWithValue("@apellido", cliente.Cliente.Apellido);
            comando.Parameters.AddWithValue("@sexo", cliente.Cliente.Sexo);
            comando.Parameters.AddWithValue("@nit", cliente.Cliente.Nit);
            if (!String.IsNullOrEmpty(cliente.Cliente.Direccion)) comando.Parameters.AddWithValue("@direccion", cliente.Cliente.Direccion);
            else comando.Parameters.AddWithValue("@direccion", DBNull.Value);
            if (!String.IsNullOrEmpty(cliente.Cliente.Telefono)) comando.Parameters.AddWithValue("@telefono", cliente.Cliente.Telefono);
            else comando.Parameters.AddWithValue("@telefono", DBNull.Value);
            if (!String.IsNullOrEmpty(cliente.Cliente.Email)) comando.Parameters.AddWithValue("@email", cliente.Cliente.Email);
            else comando.Parameters.AddWithValue("@email", DBNull.Value);
            comando.Parameters.AddWithValue("@id", cliente.Cliente.Id);

            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();
        }
    }
}
