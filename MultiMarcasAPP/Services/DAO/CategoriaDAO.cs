using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Services.DAO
{
    public class CategoriaDAO
    {
        public static SqlConnection miconexion { get; } = new SqlConnection(DBConection.CadenaDeConexion);

        public static List<Tuple<int,string>> Get()
        {
            return GetAll();
        }

        public static List<Tuple<int, string>> Get(int id)
        {
            return GetByProductId(id);
        }

        private static List<Tuple<int,string>> GetAll()
        {
            string consulta = "SELECT * FROM Categorias";
            SqlCommand comando = new(consulta, miconexion);
            List<Tuple<int, string>> list = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using(reader)
            {
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        list.Add(new(reader.GetInt32(0), reader.GetString(1)));
                    }
                }
            }
            miconexion.Close();
            return list;
        }

        private static List<Tuple<int, string>> GetByProductId(int id)
        {
            string consulta = @"SELECT c.categoriaId, c.nombre FROM Categorias as c
                                INNER JOIN Categorias_Productos as cp ON c.categoriaId = cp.categoria
                                WHERE cp.productoId = @id";
            SqlCommand comando = new(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", id);
            List<Tuple<int, string>> list = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(new(reader.GetInt32(0), reader.GetString(1)));
                    }
                }
            }
            miconexion.Close();
            return list;
        }

        public static List<Tuple<int, string>> GetWithoutRelashionship(int id)
        {
            string consulta = @"SELECT c.categoriaId as CID, c.nombre as CN FROM (SELECT * FROM categorias_productos WHERE categorias_productos.productoId = @id) as cp
            RIGHT JOIN categorias as c ON c.categoriaId = cp.categoria
            WHERE cp.Id is null";
            SqlCommand comando = new(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", id);
            List<Tuple<int, string>> list = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(new(reader.GetInt32(0), reader.GetString(1)));
                    }
                }
            }
            miconexion.Close();
            return list;
        }

        public static bool Insert(string nombre)
        {
            string consulta = "INSERT INTO Categorias (nombre) VALUES (@nombre)";
            SqlCommand comando = new(consulta, miconexion);
            comando.Parameters.AddWithValue("@nombre", nombre);
            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();

            return true;
        }

        public static bool Update(int id, string nombre)
        {
            string consulta = "UPDATE Categorias SET nombre = @nombre WHERE categoriaId = @id";
            SqlCommand comando = new(consulta, miconexion);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@id", id);
            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();

            return true;
        }

        public static bool Delete(int id)
        {
            string consulta = "DELETE FROM Categorias WHERE categoriaId = @id";
            SqlCommand comando = new(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", id);
            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();

            return true;
        }
    }
}
