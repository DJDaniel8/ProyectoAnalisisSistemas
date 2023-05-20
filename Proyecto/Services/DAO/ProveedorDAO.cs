using MultiMarcasAPP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Services.DAO
{
    public static class ProveedorDAO 
    {
        public static SqlConnection miconexion { get; } = new SqlConnection(DBConection.CadenaDeConexion);

        public static List<Proveedor> Get()
        {
            return GetAll();
        }

        public static List<Proveedor> Get(int id)
        {
            return GetByProductId(id);
        }

        private static List<Proveedor> GetAll()
        {
            string consulta = @"SELECT * FROM Proveedores ORDER BY razonSocial ASC";
            SqlCommand comando = new(consulta,miconexion);
            List<Proveedor> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using(reader)
            {
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Proveedor proveedor = new Proveedor();
                        proveedor.Id = reader.GetInt32(0);
                        proveedor.RazonSocial = reader.GetString(1);
                        if(!reader.IsDBNull(2)) proveedor.Direccion = reader.GetString(2);
                        if (!reader.IsDBNull(3)) proveedor.Telefono = reader.GetString(3);
                        lista.Add(proveedor);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<Proveedor> GetByProductId(int id)
        {
            string consulta = @"SELECT * FROM Proveedores as p 
                                INNER JOIN Proveedores_Productos as pp ON p.proveedorId = pp.proveedorId
                                WHERE pp.productoId = @id";
            SqlCommand comando = new(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", id);
            List<Proveedor> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Proveedor proveedor = new Proveedor();
                        proveedor.Id = reader.GetInt32(0);
                        proveedor.RazonSocial = reader.GetString(1);
                        if (!reader.IsDBNull(2)) proveedor.Direccion = reader.GetString(2);
                        if (!reader.IsDBNull(3)) proveedor.Telefono = reader.GetString(3);
                        lista.Add(proveedor);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<Proveedor> GetWithoutRelashionship(int id)
        {
            string consulta = @"SELECT c.* FROM (SELECT * FROM Proveedores_Productos WHERE Proveedores_Productos.productoId = @id) as cp
                                RIGHT JOIN Proveedores as c ON c.proveedorId = cp.proveedorId
                                WHERE cp.Id is null";
            SqlCommand comando = new(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", id);
            List<Proveedor> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Proveedor proveedor = new Proveedor();
                        proveedor.Id = reader.GetInt32(0);
                        proveedor.RazonSocial = reader.GetString(1);
                        if (!reader.IsDBNull(2)) proveedor.Direccion = reader.GetString(2);
                        if (!reader.IsDBNull(3)) proveedor.Telefono = reader.GetString(3);
                        lista.Add(proveedor);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static Proveedor GetByIngresoId(int idIngreso)
        {
            string consulta = @"SELECT * FROM Proveedores p
                                INNER JOIN Ingresos i ON p.proveedorId = i.proveedorId
                                WHERE i.ingresoId = @idIngreso";
            SqlCommand comando = new(consulta, miconexion);
            comando.Parameters.AddWithValue("@idIngreso", idIngreso);
            Proveedor p = new();

            try
            {
                miconexion.Open();
                SqlDataReader reader = comando.ExecuteReader();
                using (reader)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            Proveedor proveedor = new Proveedor();
                            proveedor.Id = reader.GetInt32(0);
                            proveedor.RazonSocial = reader.GetString(1);
                            if (!reader.IsDBNull(2)) proveedor.Direccion = reader.GetString(2);
                            if (!reader.IsDBNull(3)) proveedor.Telefono = reader.GetString(3);
                            p = proveedor;
                        }
                    }
                }
                miconexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return p;
        }

        public static bool Insert(Proveedor proveedor)
        {
            string consulta = @"INSERT INTO Proveedores (razonSocial, direccion, telefono)
                                VALUES (@razonSocial, @direccion, @telefono) ";
            SqlCommand comando = new(consulta,miconexion);
            comando.Parameters.AddWithValue("@razonSocial", proveedor.RazonSocial);
            if (String.IsNullOrEmpty(proveedor.Direccion)) comando.Parameters.AddWithValue("direccion", DBNull.Value);
            else comando.Parameters.AddWithValue("@direccion", proveedor.Direccion);
            if (String.IsNullOrEmpty(proveedor.Telefono)) comando.Parameters.AddWithValue("telefono", DBNull.Value);
            else comando.Parameters.AddWithValue("@telefono", proveedor.Telefono);

            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();

            return true;
        }

        public static bool Update(Proveedor proveedor)
        {
            string consulta = @"UPDATE Proveedores SET razonSocial = @razonSocial, direccion = @direccion, telefono = @telefono WHERE proveedorId = @id";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@razonSocial", proveedor.RazonSocial);
            comando.Parameters.AddWithValue("@direccion", proveedor.Direccion);
            comando.Parameters.AddWithValue("@telefono", proveedor.Telefono);
            comando.Parameters.AddWithValue("@id", proveedor.Id);

            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();

            return true;
        }

        public static bool Delete(Proveedor proveedor)
        {
            string consulta = @"DELETE FROM Proveedores WHERE proveedorId = @id";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", proveedor.Id);
            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();

            return true;
        }
    }
}
