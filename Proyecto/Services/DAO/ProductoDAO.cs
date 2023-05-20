using MultiMarcasAPP.Models;
using MultiMarcasAPP.ViewModels.IngresosViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MultiMarcasAPP.Services.DAO
{
    public static class ProductoDAO
    {
        public static SqlConnection miconexion { get; } = new SqlConnection(DBConection.CadenaDeConexion);

        public static List<Producto> Get()
        {
            return GetAll();
        }

        public static List<Producto> TransformData(SqlCommand comando)
        {
            List<Producto> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto producto = new Producto();
                        producto.Id = reader.GetInt32(0);
                        producto.Codigo = reader.GetString(1);
                        producto.Nombre = reader.GetString(2);
                        if (!reader.IsDBNull(3)) producto.Descripcion = reader.GetString(3);
                        if (!reader.IsDBNull(4))
                        {
                            if (reader.GetByte(4) == 0) producto.EsDerivado = false;
                            else producto.EsDerivado = true;
                        }
                        if (!reader.IsDBNull(5)) producto.Multiplicador = reader.GetInt32(5);
                        if (!reader.IsDBNull(6)) producto.PadreId = reader.GetInt32(6);
                        if (!reader.IsDBNull(7)) producto.Categorias = reader.GetString(7);
                        producto.Proveedores = reader.GetString(8);
                        if (!reader.IsDBNull(9)) producto.CodigoPadre = reader.GetString(9);
                        if (!reader.IsDBNull(10)) producto.Stock = reader.GetDecimal(10);
                        lista.Add(producto);
                        
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        private static List<Producto> GetAll()
        {
            string consulta = @"SELECT p.productoId, p.codigo, p.nombre, p.descripcion, p.derivado, p.multiplicador, p.padreId,
                                (SELECT STRING_AGG(nombre, ', ') FROM Categorias INNER JOIN Categorias_Productos as cp ON cp.categoria = Categorias.categoriaId
		                                WHERE cp.productoId = p.productoId),
                                (SELECT STRING_AGG(razonSocial, ', ') FROM Proveedores INNER JOIN Proveedores_Productos as pp ON pp.proveedorId = Proveedores.proveedorId
		                                WHERE pp.productoId = p.productoId),
                                (SELECT codigo FROM productos WHERE productos.productoId = p.padreId),
                                (SELECT SUM(stock) FROM stocks WHERE productoId = p.productoId)

                                FROM productos as p";
            SqlCommand comando = new SqlCommand(consulta, miconexion);

            return TransformData(comando);
        }

        public static Producto GetById(int id)
        {
            string consulta = @"SELECT p.productoId, p.codigo, p.nombre, p.descripcion, p.derivado, p.multiplicador, p.padreId,
                                (SELECT STRING_AGG(nombre, ', ') FROM Categorias INNER JOIN Categorias_Productos as cp ON cp.categoria = Categorias.categoriaId
		                                WHERE cp.productoId = p.productoId),
                                (SELECT STRING_AGG(razonSocial, ', ') FROM Proveedores INNER JOIN Proveedores_Productos as pp ON pp.proveedorId = Proveedores.proveedorId
		                                WHERE pp.productoId = p.productoId),
                                (SELECT codigo FROM productos WHERE productos.productoId = p.padreId),
                                (SELECT SUM(stock) FROM stocks WHERE productoId = p.productoId)
                                FROM productos as p
                                WHERE p.productoId = @id";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", id);
            Producto p = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto producto = new Producto();
                        producto.Id = reader.GetInt32(0);
                        producto.Codigo = reader.GetString(1);
                        producto.Nombre = reader.GetString(2);
                        if (!reader.IsDBNull(3)) producto.Descripcion = reader.GetString(3);
                        if (!reader.IsDBNull(4))
                        {
                            if (reader.GetByte(4) == 0) producto.EsDerivado = false;
                            else producto.EsDerivado = true;
                        }
                        if (!reader.IsDBNull(5)) producto.Multiplicador = reader.GetInt32(5);
                        if (!reader.IsDBNull(6)) producto.PadreId = reader.GetInt32(6);
                        if (!reader.IsDBNull(7)) producto.Categorias = reader.GetString(7);
                        producto.Proveedores = reader.GetString(8);
                        if (!reader.IsDBNull(9)) producto.CodigoPadre = reader.GetString(9);
                        p = producto;
                    }
                }
            }
            miconexion.Close();
            return p;
        }

        public static List<Producto> GetByAllSearch(string searchValue)
        {
            string consulta = @"SELECT p.productoId, p.codigo, p.nombre, p.descripcion, p.derivado, p.multiplicador, p.padreId,
                                (SELECT STRING_AGG(nombre, ', ') FROM Categorias INNER JOIN Categorias_Productos as cp ON cp.categoria = Categorias.categoriaId
		                                WHERE cp.productoId = p.productoId),
                                (SELECT STRING_AGG(razonSocial, ', ') FROM Proveedores INNER JOIN Proveedores_Productos as pp ON pp.proveedorId = Proveedores.proveedorId
		                                WHERE pp.productoId = p.productoId),
                                (SELECT codigo FROM productos WHERE productos.productoId = p.padreId),
                                (SELECT SUM(stock) FROM stocks WHERE productoId = p.productoId)
                                FROM productos as p " + 
                              $"WHERE p.Codigo LIKE '%{searchValue}%' OR p.Nombre LIKE '%{searchValue}%'";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            

            return TransformData(comando);
        }

        public static List<Producto> GetByCodeSearch(string searchValue)
        {
            string consulta = @"SELECT p.productoId, p.codigo, p.nombre, p.descripcion, p.derivado, p.multiplicador, p.padreId,
                                (SELECT STRING_AGG(nombre, ', ') FROM Categorias INNER JOIN Categorias_Productos as cp ON cp.categoria = Categorias.categoriaId
		                                WHERE cp.productoId = p.productoId),
                                (SELECT STRING_AGG(razonSocial, ', ') FROM Proveedores INNER JOIN Proveedores_Productos as pp ON pp.proveedorId = Proveedores.proveedorId
		                                WHERE pp.productoId = p.productoId),
                                (SELECT codigo FROM productos WHERE productos.productoId = p.padreId),
                                (SELECT SUM(stock) FROM stocks WHERE productoId = p.productoId)
                                FROM productos as p " +
                              $"WHERE p.Codigo LIKE '%{searchValue}%'";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            

            return TransformData(comando);
        }

        public static List<Producto> GetByNameSearch(string searchValue)
        {
            string consulta = @"SELECT p.productoId, p.codigo, p.nombre, p.descripcion, p.derivado, p.multiplicador, p.padreId,
                                (SELECT STRING_AGG(nombre, ', ') FROM Categorias INNER JOIN Categorias_Productos as cp ON cp.categoria = Categorias.categoriaId
		                                WHERE cp.productoId = p.productoId),
                                (SELECT STRING_AGG(razonSocial, ', ') FROM Proveedores INNER JOIN Proveedores_Productos as pp ON pp.proveedorId = Proveedores.proveedorId
		                                WHERE pp.productoId = p.productoId),
                                (SELECT codigo FROM productos WHERE productos.productoId = p.padreId),
                                (SELECT SUM(stock) FROM stocks WHERE productoId = p.productoId)
                                FROM productos as p " +
                              $"WHERE p.nombre LIKE '%{searchValue}%'";
            SqlCommand comando = new SqlCommand(consulta, miconexion);

            return TransformData(comando); ;
        }

        public static List<Producto> GetByFilter(string filterType, int filterValue)
        {
            string consulta = @"SELECT p.productoId, p.codigo, p.nombre, p.descripcion, p.derivado, p.multiplicador, p.padreId,
                                (SELECT STRING_AGG(nombre, ', ') FROM Categorias INNER JOIN Categorias_Productos as cp ON cp.categoria = Categorias.categoriaId
		                                WHERE cp.productoId = p.productoId),
                                (SELECT STRING_AGG(razonSocial, ', ') FROM Proveedores INNER JOIN Proveedores_Productos as pp ON pp.proveedorId = Proveedores.proveedorId
		                                WHERE pp.productoId = p.productoId),
                                (SELECT codigo FROM productos WHERE productos.productoId = p.padreId),
                                (SELECT SUM(stock) FROM stocks WHERE productoId = p.productoId)
                                FROM productos as p ";
            if (filterType == "Categoria") consulta = consulta + "LEFT JOIN Categorias_Productos as cp ON p.productoId = cp.productoId " +
                                                                  $"WHERE cp.categoria = @id";
            else if (filterType == "Proveedor") consulta = consulta + "LEFT JOIN Proveedores_Productos as cp ON p.productoId = cp.productoId " +
                                                                     $"WHERE cp.proveedorId = @id ";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", filterValue);

            return TransformData(comando);
        }

        public static List<Producto> GetByFilterAndAllSearch(string filterType, int filterValue, string searchValue)
        {
            string consulta = @"SELECT p.productoId, p.codigo, p.nombre, p.descripcion, p.derivado, p.multiplicador, p.padreId,
                                (SELECT STRING_AGG(nombre, ', ') FROM Categorias INNER JOIN Categorias_Productos as cp ON cp.categoria = Categorias.categoriaId
		                                WHERE cp.productoId = p.productoId),
                                (SELECT STRING_AGG(razonSocial, ', ') FROM Proveedores INNER JOIN Proveedores_Productos as pp ON pp.proveedorId = Proveedores.proveedorId
		                                WHERE pp.productoId = p.productoId),
                                (SELECT codigo FROM productos WHERE productos.productoId = p.padreId),
                                (SELECT SUM(stock) FROM stocks WHERE productoId = p.productoId)
                                FROM productos as p ";
            if (filterType == "Categoria") consulta = consulta + "LEFT JOIN Categorias_Productos as cp ON p.productoId = cp.productoId " +
                                                                  $"WHERE cp.categoria = @id AND (p.codigo LIKE '%{searchValue}%' OR p.nombre LIKE '%{searchValue}%')";
            else if (filterType == "Proveedor") consulta = consulta + "LEFT JOIN Proveedores_Productos as cp ON p.productoId = cp.productoId " +
                                                                     $"WHERE cp.proveedorId = @id AND (p.codigo LIKE '%{searchValue}%' OR p.nombre LIKE '%{searchValue}%')";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", filterValue);

            return TransformData(comando);
        }

        public static List<Producto> GetByFilterAndCodeSearch(string filterType, int filterValue, string searchValue)
        {
            string consulta = @"SELECT p.productoId, p.codigo, p.nombre, p.descripcion, p.derivado, p.multiplicador, p.padreId,
                                (SELECT STRING_AGG(nombre, ', ') FROM Categorias INNER JOIN Categorias_Productos as cp ON cp.categoria = Categorias.categoriaId
		                                WHERE cp.productoId = p.productoId),
                                (SELECT STRING_AGG(razonSocial, ', ') FROM Proveedores INNER JOIN Proveedores_Productos as pp ON pp.proveedorId = Proveedores.proveedorId
		                                WHERE pp.productoId = p.productoId),
                                (SELECT codigo FROM productos WHERE productos.productoId = p.padreId),
                                (SELECT SUM(stock) FROM stocks WHERE productoId = p.productoId)
                                FROM productos as p ";
            if (filterType == "Categoria") consulta = consulta + "LEFT JOIN Categorias_Productos as cp ON p.productoId = cp.productoId " +
                                                                  $"WHERE cp.categoria = @id AND (p.codigo LIKE '%{searchValue}%')";
            else if (filterType == "Proveedor") consulta = consulta + "LEFT JOIN Proveedores_Productos as cp ON p.productoId = cp.productoId " +
                                                                     $"WHERE cp.proveedorId = @id AND (p.codigo LIKE '%{searchValue}%' )";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", filterValue);

            return TransformData(comando);
        }

        public static List<Producto> GetByFilterAndNameSearch(string filterType, int filterValue, string searchValue)
        {
            string consulta = @"SELECT p.productoId, p.codigo, p.nombre, p.descripcion, p.derivado, p.multiplicador, p.padreId,
                                (SELECT STRING_AGG(nombre, ', ') FROM Categorias INNER JOIN Categorias_Productos as cp ON cp.categoria = Categorias.categoriaId
		                                WHERE cp.productoId = p.productoId),
                                (SELECT STRING_AGG(razonSocial, ', ') FROM Proveedores INNER JOIN Proveedores_Productos as pp ON pp.proveedorId = Proveedores.proveedorId
		                                WHERE pp.productoId = p.productoId),
                                (SELECT codigo FROM productos WHERE productos.productoId = p.padreId),
                                (SELECT SUM(stock) FROM stocks WHERE productoId = p.productoId)
                                FROM productos as p ";
            if (filterType == "Categoria") consulta = consulta + "LEFT JOIN Categorias_Productos as cp ON p.productoId = cp.productoId " +
                                                                  $"WHERE cp.categoria = @id AND (p.nombre LIKE '%{searchValue}%')";
            else if (filterType == "Proveedor") consulta = consulta + "LEFT JOIN Proveedores_Productos as cp ON p.productoId = cp.productoId " +
                                                                     $"WHERE cp.proveedorId = @id AND (p.nombre LIKE '%{searchValue}%' )";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", filterValue);

            return TransformData(comando);
        }
        public static bool Insert(Producto producto, byte[]? imageData, int proveedorId)
        {
            try
            {
                string consulta;
                if (imageData != null)
                {
                    consulta = @"INSERT INTO productos (codigo, nombre, descripcion, imagen, derivado, multiplicador, padreId)
                                VALUES (@codigo, @nombre, @descripcion, @imagen, @derivado, @multiplicador, @padreId)";
                }
                else
                {
                    consulta = @"INSERT INTO productos (codigo, nombre, descripcion, derivado, multiplicador, padreId)
                                VALUES (@codigo, @nombre, @descripcion, @derivado, @multiplicador, @padreId)";
                }
                SqlCommand comando = new(consulta, miconexion);
                comando.Parameters.AddWithValue("@codigo", producto.Codigo);

                comando.Parameters.AddWithValue("@nombre", producto.Nombre);

                if (!String.IsNullOrEmpty(producto.Descripcion)) comando.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                else comando.Parameters.AddWithValue("@descripcion", DBNull.Value);

                if (imageData != null) comando.Parameters.AddWithValue("@imagen", imageData);

                comando.Parameters.AddWithValue("@derivado", producto.EsDerivado);

                if (producto.EsDerivado)
                {
                    comando.Parameters.AddWithValue("@multiplicador", producto.Multiplicador);
                    comando.Parameters.AddWithValue("@padreId", producto.PadreId);
                }
                else
                {
                    comando.Parameters.AddWithValue("@multiplicador", 1);
                    comando.Parameters.AddWithValue("@padreId", DBNull.Value);
                }

                miconexion.Open();
                comando.ExecuteNonQuery();
                miconexion.Close();

                string consulta2 = "INSERT INTO Proveedores_Productos (productoId, proveedorId) VALUES (@productoId, @proveedorId)";
                SqlCommand comando2 = new(consulta2, miconexion);
                comando2.Parameters.AddWithValue("@productoId", GetIdLastInsert());
                comando2.Parameters.AddWithValue("@proveedorId", proveedorId);

                miconexion.Open();
                comando2.ExecuteNonQuery();
                miconexion.Close();

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                miconexion.Close();
                return false;
            }
        }

        public static bool Update(Producto producto, byte[]? imageData)
        {
            if (imageData != null)
            {
                string consulta = @"UPDATE Productos SET codigo = @codigo, nombre = @nombre, descripcion = @descripcion, imagen = @imagen
                                WHERE productoId = @id";
                SqlCommand comando = new(consulta, miconexion);
                comando.Parameters.AddWithValue("@codigo", producto.Codigo);
                comando.Parameters.AddWithValue("@nombre", producto.Nombre);
                if (!String.IsNullOrEmpty(producto.Descripcion)) comando.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                else comando.Parameters.AddWithValue("@descripcion", DBNull.Value);
                comando.Parameters.AddWithValue("@imagen", imageData);
                comando.Parameters.AddWithValue("@id", producto.Id);
                miconexion.Open(); comando.ExecuteNonQuery(); miconexion.Close();
            }
            else
            {
                string consulta = @"UPDATE Productos SET codigo = @codigo, nombre = @nombre, descripcion = @descripcion, imagen = NULL
                                WHERE productoId = @id";
                SqlCommand comando = new(consulta, miconexion);
                comando.Parameters.AddWithValue("@codigo", producto.Codigo);
                comando.Parameters.AddWithValue("@nombre", producto.Nombre);
                if (!String.IsNullOrEmpty(producto.Descripcion)) comando.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                else comando.Parameters.AddWithValue("@descripcion", DBNull.Value);
                comando.Parameters.AddWithValue("@id", producto.Id);
                miconexion.Open(); comando.ExecuteNonQuery(); miconexion.Close();
            }

            return true;
        }

        public static int GetIdByCode(string codigo)
        {
            string consulta = "SELECT * FROM productos WHERE codigo = @codigo";
            SqlCommand comando = new(consulta, miconexion);
            comando.Parameters.AddWithValue("@codigo", codigo);

            miconexion.Open();
            int res;
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if(reader.HasRows)
                {
                    reader.Read();
                    res =  reader.GetInt32(0);
                }
                else res = 0;
            }
            miconexion.Close();
            return res;
        }

        public static void InsertCategory(int CategoriaId, int ProductoId)
        {
            string consulta = @"INSERT INTO Categorias_Productos (productoId, categoria) VALUES (@productoId, @categoriaId)";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@productoId", ProductoId);
            comando.Parameters.AddWithValue("@categoriaId", CategoriaId);

            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();
        }

        public static void DeleteCategory(int categoriaId, int productoId)
        {
            string consulta = @"DELETE FROM Categorias_Productos WHERE categoria = @categoriaId AND productoId = @productoId";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@categoriaId", categoriaId);
            comando.Parameters.AddWithValue("@productoId", productoId);

            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();
        }

        public static void InsertProvider(int proveedorId, int productoId)
        {
            string consulta = @"INSERT INTO Proveedores_Productos (proveedorId, productoId) 
                                VALUES (@proveedorId, @productoId) ";
            SqlCommand comando = new(consulta, miconexion);
            comando.Parameters.AddWithValue("@proveedorId", proveedorId);
            comando.Parameters.AddWithValue("@productoId", productoId);

            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();
        }

        public static void DeleteProvider(int proveedorId, int productoId)
        {
            string consulta = @"DELETE FROM Proveedores_Productos WHERE proveedorId = @proveedorId AND productoId = @productoId";
            SqlCommand comando = new(consulta, miconexion);
            comando.Parameters.AddWithValue("@proveedorId", proveedorId);
            comando.Parameters.AddWithValue("@productoId", productoId);

            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();
        }

        public static int GetIdLastInsert()
        {
            string consulta = "SELECT TOP 1 * FROM productos ORDER BY productoId desc";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            int res = 0;
            using (reader)
            {
                if(reader.HasRows)
                {
                    reader.Read();
                    res = reader.GetInt32(0);
                };
            }

            miconexion.Close();
            return res;
        }

        public static void DeleteProduct(int productoId)
        {
            string consulta = "DELETE FROM Productos WHERE productoId = @id";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", productoId);

            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();
        }

        public static Tuple<BitmapImage, byte[]>? GetProductImageById(int id)
        {
            string consulta = "SELECT imagen FROM Productos WHERE productoId = @id";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", id);

            miconexion.Open();
            BitmapImage mapadeBits = new BitmapImage();

            SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            Tuple<BitmapImage, byte[]>? tupla = null;

            if (dataSet.Tables[0].Rows.Count == 1)
            {
                if (DBNull.Value != dataSet.Tables[0].Rows[0][0])
                {
                    try
                    {
                        mapadeBits.BeginInit();
                        Byte[] data = new Byte[0];
                        data = (Byte[])(dataSet.Tables[0].Rows[0][0]);
                        MemoryStream mem = new MemoryStream(data);
                        mapadeBits.StreamSource = mem;
                        mapadeBits.EndInit();

                        byte[] bits = new byte[mem.Length];
                        mem.Read(bits, 0, (int)mem.Length);

                        tupla = new(mapadeBits, bits);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    
                }
            }

            miconexion.Close();

            return tupla;
        }

        public static bool EsProductoPadre(int id)
        {
            string consulta = "SELECT * FROM Productos WHERE padreId = @id";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", id);
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();

            if (reader.HasRows)
            {
                miconexion.Close();
                return true;
            }
            else
            {
                miconexion.Close();
                return false;
            }
        }

        public static List<ProductoIngreso> GetProductosHijos(int idPadre)
        {
            string consulta = "SELECT * FROM productos WHERE padreId = @id";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", idPadre);
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            List<ProductoIngreso> lista = new();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    bool resultado;
                    if (reader.GetByte(5) == 1) resultado = true;
                    else resultado = false;
                    Producto po = new Producto()
                    {
                        Id = reader.GetInt32(0),
                        Codigo = reader.GetString(1),
                        Nombre = reader.GetString(2),
                        EsDerivado = resultado,
                        Multiplicador = reader.GetInt32(6),
                        PadreId = reader.GetInt32(7)
                    };
                    ProductoIngreso producto = new ProductoIngreso(po);
                    lista.Add(producto);
                }
            }
            miconexion.Close();

            return lista;
        }
    }
}
