using MultiMarcasAPP.Models;
using MultiMarcasAPP.ViewModels.VentaViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Services.DAO
{
    public class VentaDAO
    {
        public static SqlConnection miconexion { get; } = new SqlConnection(DBConection.CadenaDeConexion);

        public static List<Venta> GetBySearch(string searchValue)
        {
            string consulta = @"SELECT *, 
                                (SELECT SUM(cantidad*precio_venta) FROM Productos_Ventas WHERE ventaId = v.ventaId)
                                FROM Ventas as v
                                LEFT JOIN Clientes as c ON v.clienteId = c.clienteId
                                INNER JOIN trabajadores as t ON v.trabajadorId = t.trabajadorId " +
                               $"WHERE c.Nombre LIKE '%{searchValue}%' OR c.apellido LIKE '%{searchValue}%' OR v.nombre LIKE '%{searchValue}%' OR t.Nombre LIKE '%{searchValue}%' or t.Apellido LIKE '%{searchValue}%' " +
                              "  ORDER BY v.ventaId Desc";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            

            return TransformarData(comando);
        }

        public static List<Venta> GetLast100()
        {
            string consulta = @"SELECT TOP(100) *, 
                                (SELECT SUM(cantidad*precio_venta) FROM Productos_Ventas WHERE ventaId = v.ventaId)
                                FROM Ventas as v
                                LEFT JOIN Clientes as c ON v.clienteId = c.clienteId
                                INNER JOIN trabajadores as t ON v.trabajadorId = t.trabajadorId

                                ORDER BY v.ventaId Desc";
            SqlCommand comando = new SqlCommand(consulta, miconexion);


            return TransformarData(comando);
        }

        public static List<Venta> GetLastOne()
        {
            string consulta = @"SELECT TOP(1) *, 
                                (SELECT SUM(cantidad*precio_venta) FROM Productos_Ventas WHERE ventaId = v.ventaId)
                                FROM Ventas as v
                                LEFT JOIN Clientes as c ON v.clienteId = c.clienteId
                                INNER JOIN trabajadores as t ON v.trabajadorId = t.trabajadorId

                                ORDER BY v.ventaId Desc";
            SqlCommand comando = new SqlCommand(consulta, miconexion);


            return TransformarData(comando);
        }

        public static List<Venta> GetLast100AndSearch(string searchValue)
        {
            string consulta = @"SELECT TOP(100) *, 
                                (SELECT SUM(cantidad*precio_venta) FROM Productos_Ventas WHERE ventaId = v.ventaId)
                                FROM Ventas as v
                                LEFT JOIN Clientes as c ON v.clienteId = c.clienteId
                                INNER JOIN trabajadores as t ON v.trabajadorId = t.trabajadorId " +
                                $"WHERE c.Nombre LIKE '%{searchValue}%' OR c.apellido LIKE '%{searchValue}%' OR v.nombre LIKE '%{searchValue}%' OR t.Nombre LIKE '%{searchValue}%' or t.Apellido LIKE '%{searchValue}%' " +
                              "  ORDER BY v.ventaId Desc";
            SqlCommand comando = new SqlCommand(consulta, miconexion);


            return TransformarData(comando);
        }

        public static List<Venta> GetToday()
        {
            string consulta = @"SELECT  *, 
                                (SELECT SUM(cantidad*precio_venta) FROM Productos_Ventas WHERE ventaId = v.ventaId)
                                FROM Ventas as v
                                LEFT JOIN Clientes as c ON v.clienteId = c.clienteId
                                INNER JOIN trabajadores as t ON v.trabajadorId = t.trabajadorId
                                WHERE CONVERT(Date,GETDATE(),1) = CONVERT(DATE,v.fecha,1)
                                ORDER BY v.ventaId Desc";
            SqlCommand comando = new SqlCommand(consulta, miconexion);


            return TransformarData(comando);
        }

        public static List<Venta> GetTodayAndSearch(string searchValue)
        {
            string consulta = @"SELECT  *, 
                                (SELECT SUM(cantidad*precio_venta) FROM Productos_Ventas WHERE ventaId = v.ventaId)
                                FROM Ventas as v
                                LEFT JOIN Clientes as c ON v.clienteId = c.clienteId
                                INNER JOIN trabajadores as t ON v.trabajadorId = t.trabajadorId
                                WHERE CONVERT(Date,GETDATE(),1) = CONVERT(DATE,v.fecha,1) " +
                                $" AND (c.Nombre LIKE '%{searchValue}%' OR c.apellido LIKE '%{searchValue}%' OR v.nombre LIKE '%{searchValue}%' OR t.Nombre LIKE '%{searchValue}%' or t.Apellido LIKE '%{searchValue}%') " +
                                " ORDER BY v.ventaId Desc";
            SqlCommand comando = new SqlCommand(consulta, miconexion);


            return TransformarData(comando);
        }

        public static List<Venta> GetByDate(DateTime fecha)
        {
            string consulta = @"SELECT  *, 
                                (SELECT SUM(cantidad*precio_venta) FROM Productos_Ventas WHERE ventaId = v.ventaId)
                                FROM Ventas as v
                                LEFT JOIN Clientes as c ON v.clienteId = c.clienteId
                                INNER JOIN trabajadores as t ON v.trabajadorId = t.trabajadorId
                                WHERE @fecha = CONVERT(DATE,v.fecha,1)
                                ORDER BY v.ventaId Desc";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@fecha", fecha.Date);


            return TransformarData(comando);
        }

        public static List<Venta> GetByDateAndSearch(DateTime fecha, string searchValue)
        {
            string consulta = @"SELECT  *, 
                                (SELECT SUM(cantidad*precio_venta) FROM Productos_Ventas WHERE ventaId = v.ventaId)
                                FROM Ventas as v
                                LEFT JOIN Clientes as c ON v.clienteId = c.clienteId
                                INNER JOIN trabajadores as t ON v.trabajadorId = t.trabajadorId
                                WHERE @fecha = CONVERT(DATE,v.fecha,1) " +
                                $"AND (c.Nombre LIKE '%{searchValue}%' OR c.apellido LIKE '%{searchValue}%' OR v.nombre LIKE '%{searchValue}%' OR t.Nombre LIKE '%{searchValue}%' or t.Apellido LIKE '%{searchValue}%') " +
                               " ORDER BY v.ventaId Desc";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@fecha", fecha.Date);


            return TransformarData(comando);
        }

        private static List<Venta> TransformarData(SqlCommand comando)
        {
            List<Venta> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente();
                        if (reader.IsDBNull(1))
                        {
                            cliente.Nombre = reader.GetString(5);
                            cliente.Nit = reader.GetString(6);
                        }
                        else
                        {
                            cliente.Id = reader.GetInt32(7);
                            cliente.Nombre = reader.GetString(8);
                            cliente.Apellido = reader.GetString(9);
                            cliente.Sexo = reader.GetString(10);
                            cliente.Nit = reader.GetString(11);
                            if (!reader.IsDBNull(12)) cliente.Direccion = reader.GetString(12);
                            if (!reader.IsDBNull(13)) cliente.Telefono = reader.GetString(13);
                        }

                        Trabajador trabajador = new Trabajador();
                        trabajador.Id = reader.GetInt32(15);
                        trabajador.Nombre = reader.GetString(16);
                        trabajador.Apellido = reader.GetString(17);
                        trabajador.Sexo = reader.GetString(18);
                        trabajador.Puesto = reader.GetString(19);
                        trabajador.Usuario = reader.GetString(20);
                        if (!reader.IsDBNull(22)) trabajador.Direccion = reader.GetString(22);
                        if (!reader.IsDBNull(23)) trabajador.Telefono = reader.GetString(23);

                        Venta venta = new Venta(cliente, trabajador);
                        venta.Id = reader.GetInt32(0);
                        venta.Fecha = reader.GetDateTime(3);
                        if (reader.GetByte(4) == 0) venta.EsAuditorada = false;
                        else venta.EsAuditorada = true;
                        venta.Total = reader.GetDecimal(26);

                        lista.Add(venta);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }


        public static List<ProductoVentaViewModel> GetProductosVenta(int ventaId)
        {
            string consulta = @"SELECT * FROM Productos_Ventas as pv
                                INNER JOIN Stocks as s ON pv.stockId = s.stockId
                                INNER JOIN Productos as p ON s.productoId = p.productoId
                                WHERE pv.ventaId = @id";

            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", ventaId);
            List<ProductoVentaViewModel> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Stock stock = new Stock();
                        stock.Id = reader.GetInt32(5);
                        stock.Cantidad = reader.GetDecimal(6);
                        stock.PrecioCompra = reader.GetDecimal(7);
                        stock.PrecioVenta = reader.GetDecimal(8);
                        stock.PrecioMinimo = reader.GetDecimal(9);
                        stock.ProductoId = reader.GetInt32(10);
                        if(!reader.IsDBNull(11)) stock.StockIdPadre = reader.GetInt32(11);
                        if (reader.GetByte(12) == 0) stock.Auditorado = false;
                        else stock.Auditorado = true;

                        Producto producto = new();
                        producto.Id = reader.GetInt32(13);
                        producto.Codigo = reader.GetString(14);
                        producto.Nombre = reader.GetString(15);
                        if(reader.GetByte(18) == 0) producto.EsDerivado = false;
                        else producto.EsDerivado = true;
                        producto.Multiplicador = reader.GetInt32(19);
                        if(!reader.IsDBNull(20)) producto.PadreId = reader.GetInt32(20);

                        ProductoVentaViewModel productoVentaViewModel = new(producto, stock, null);
                        productoVentaViewModel.SetCantidad(reader.GetInt32(3));
                        productoVentaViewModel.PrecioVenta = reader.GetDecimal(4);

                        lista.Add(productoVentaViewModel);
                    }

                }
            }
            miconexion.Close();
            return lista;
        }

        public static void Insert(int clienteId, bool auditorada)
        {
            string consulta = @"INSERT INTO Ventas (clienteId, trabajadorId, fecha, Auditorada)
                                VALUES (@clienteId, @trabajadorId, @fecha, @Auditorada)";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@clienteId", clienteId);
            comando.Parameters.AddWithValue("@trabajadorId", CurrentEmploye.Trabajador.Id);
            comando.Parameters.AddWithValue("@fecha", DateTime.Now);
            comando.Parameters.AddWithValue("@Auditorada", auditorada);
            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();
        }

        public static void Insert(Venta venta)
        {
            string consulta = @"INSERT INTO Ventas (clienteId, trabajadorId, fecha, auditorada, nombre, nit) 
                                VALUES (@clienteId, @trabajadorId, @fecha, @auditorada, @nombre, @nit)";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            if (venta.Cliente.Id > 0) comando.Parameters.AddWithValue("@clienteId", venta.Cliente.Id);
            else comando.Parameters.AddWithValue("@clienteId", DBNull.Value);
            comando.Parameters.AddWithValue("@trabajadorId", venta.Trabajador.Id);
            comando.Parameters.AddWithValue("@fecha", venta.Fecha);
            comando.Parameters.AddWithValue("@auditorada", venta.EsAuditorada);
            if (venta.Cliente.Id == 0) comando.Parameters.AddWithValue("@nombre", venta.Nombre);
            else comando.Parameters.AddWithValue("@nombre", DBNull.Value);
            if (venta.Cliente.Id == 0) comando.Parameters.AddWithValue("@nit", venta.Cliente.Nit);
            else comando.Parameters.AddWithValue("@nit", DBNull.Value);
            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();
        }

        public static void Insert(string nombre, string nit, bool auditorada)
        {
            string consulta = @"INSERT INTO Ventas (trabajadorId, fecha, auditorada, nombre, nit)
                                VALUES (@trabajadorId, @fecha, @auditorada, @nombre, @nit)";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@trabajadorId", CurrentEmploye.Trabajador.Id);
            comando.Parameters.AddWithValue("@fecha", DateTime.Now);
            comando.Parameters.AddWithValue("@auditorada", auditorada);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@nit", nit);

            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();
        }

        public static int GetLastInsert()
        {
            string consulta = @"SELECT TOP(1) * FROM Ventas ORDER BY ventaId DESC";
            SqlCommand comando = new(consulta, miconexion);
            int res = 0;
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        res = reader.GetInt32(0);
                    }
                }
            }
            miconexion.Close();
            return res;
        }

        public static void InsertProductosVenta(int ventaId, List<ProductoVentaViewModel> lista, bool auditorado)
        {
            string consulta = @"INSERT INTO Productos_Ventas (ventaId, stockId, cantidad, precio_venta)
                                VALUES (@ventaId, @stockId, @cantidad, @precio_venta)";
            miconexion.Open();
            foreach (var item in lista)
            {
                SqlCommand comando = new SqlCommand(consulta, miconexion);
                comando.Parameters.AddWithValue("@ventaId", ventaId);
                comando.Parameters.AddWithValue("@stockId", item.Stock.Id);
                comando.Parameters.AddWithValue("@cantidad", item.Cantidad);
                comando.Parameters.AddWithValue("@precio_venta", item.PrecioVenta);
                comando.ExecuteNonQuery();
                RestarStock(item);
            }
            miconexion.Close();
        }

        private static void RestarStock(ProductoVentaViewModel producto)
        {
            if (producto.Stock.StockIdPadre == 0)
            {
                string consulta = @"UPDATE stocks SET stock = ( (SELECT stock FROM stocks WHERE stockId = @stockId) - @cantidad) WHERE stockId = @stockId";
                SqlCommand comando = new SqlCommand(consulta, miconexion);
                comando.Parameters.AddWithValue("@stockId", producto.Stock.Id);
                comando.Parameters.AddWithValue("@cantidad", producto.Cantidad);
                comando.ExecuteNonQuery();
                ActualizarStocksHijos(producto.Stock.Id);
            }
            else
            {
                string consulta = @"UPDATE stocks SET 
                                    stock = ((SELECT stock FROM stocks WHERE stockId = @stockId) - (CAST(@cantidad AS MONEY) / (SELECT multiplicador FROM productos WHERE productoId = (SELECT productoId FROM stocks WHERE stockId = @stockIdHijo))))
                                    WHERE stockId = @stockId";
                SqlCommand comando = new SqlCommand(consulta, miconexion);
                comando.Parameters.AddWithValue("@stockId", producto.Stock.StockIdPadre);
                comando.Parameters.AddWithValue("@cantidad", producto.Cantidad);
                comando.Parameters.AddWithValue("@stockIdHijo", producto.Stock.Id);
                comando.ExecuteNonQuery();
                ActualizarStocksHijos(producto.Stock.StockIdPadre);
            }
        }

        private static void SumarStock(ProductoVentaViewModel producto)
        {
            if (producto.Stock.StockIdPadre == 0)
            {
                string consulta = @"UPDATE stocks SET stock = ( (SELECT stock FROM stocks WHERE stockId = @stockId) + @cantidad) WHERE stockId = @stockId";
                SqlCommand comando = new SqlCommand(consulta, miconexion);
                comando.Parameters.AddWithValue("@stockId", producto.Stock.Id);
                comando.Parameters.AddWithValue("@cantidad", producto.Cantidad);
                comando.ExecuteNonQuery();
                ActualizarStocksHijos(producto.Stock.Id);
            }
            else
            {
                string consulta = @"UPDATE stocks SET 
                                    stock = ((SELECT stock FROM stocks WHERE stockId = @stockId) + (CAST(@cantidad AS MONEY) / (SELECT multiplicador FROM productos WHERE productoId = (SELECT productoId FROM stocks WHERE stockId = @stockIdHijo))))
                                    WHERE stockId = @stockId";
                SqlCommand comando = new SqlCommand(consulta, miconexion);
                comando.Parameters.AddWithValue("@stockId", producto.Stock.StockIdPadre);
                comando.Parameters.AddWithValue("@cantidad", producto.Cantidad);
                comando.Parameters.AddWithValue("@stockIdHijo", producto.Stock.Id);
                comando.ExecuteNonQuery();
                ActualizarStocksHijos(producto.Stock.StockIdPadre);
            }
        }

        private static void ActualizarStocksHijos(int stockIdPadre)
        {
            bool continuar = true;
            string consulta = "SELECT * from stocks WHERE stockIdPadre = @stockIdPadre";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@stockIdPadre", stockIdPadre);
            SqlDataReader reader = comando.ExecuteReader();
            List<Tuple<int, int>> lista = new();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lista.Add(new Tuple<int, int>(reader.GetInt32(0), reader.GetInt32(5)));
                    }
                }
                else continuar = false;
            }

            if (continuar)
            {
                foreach (var item in lista)
                {
                    string consulta2 = @"UPDATE stocks SET stock = (SELECT stock FROM stocks WHERE stockId = @stockIdPadre) * (SELECT multiplicador FROM productos WHERE productoId = @productoId)
                                    WHERE stockId = @stockId";
                    SqlCommand comando2 = new SqlCommand(consulta2, miconexion);
                    comando2.Parameters.AddWithValue("@stockIdPadre", stockIdPadre);
                    comando2.Parameters.AddWithValue("@productoId", item.Item2);
                    comando2.Parameters.AddWithValue("@stockId", item.Item1);
                    comando2.ExecuteNonQuery();
                }
            }

        }

        public static bool StockSuficiente(ProductoVentaViewModel producto)
        {
            string consulta = @"SELECT * FROM Stocks WHERE stockId = @id";
            SqlCommand comando = new(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", producto.Stock.Id);
            decimal stockActual = 0;
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        stockActual = reader.GetDecimal(1);
                    }
                }
            }
            miconexion.Close();
            if (stockActual >= producto.Cantidad) return true;
            else return false;
        }

        public static void Delete(int ventaId, List<ProductoVentaViewModel> productos)
        {
            string consulta = "DELETE FROM Ventas WHERE ventaId = @id";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", ventaId);

            miconexion.Open();
            comando.ExecuteNonQuery();
            foreach (var item in productos)
            {
                SumarStock(item);
            }
            miconexion.Close();
        }


    }


}
