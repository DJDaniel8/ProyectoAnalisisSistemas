using MultiMarcasAPP.Models;
using MultiMarcasAPP.ViewModels.IngresosViewModels;
using MultiMarcasAPP.ViewModels.StocksViewModels;
using MultiMarcasAPP.Views.IngresosViews;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Services.DAO
{
    internal class StockDAO
    {
        public static SqlConnection miconexion { get; } = new SqlConnection(DBConection.CadenaDeConexion);

        public static List<StockViewModel> Get()
        {
            string consulta = @"SELECT p.productoId, p.codigo, p.nombre, p.derivado, p.multiplicador, s.*,
                                (SELECT STRING_AGG(razonSocial, ', ') FROM Proveedores INNER JOIN Proveedores_Productos as pp ON pp.proveedorId = Proveedores.proveedorId
		                                                                WHERE pp.productoId = p.productoId) 
										                                FROM Stocks as s
                                INNER JOIN Productos as p ON s.productoId = p.productoId
                                WHERE s.stock > 0
                                ORDER BY codigo, precio_venta_sugerido desc";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            List<StockViewModel> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Producto p = new();
                        Stock s = new();
                        p.Id = reader.GetInt32(0);
                        p.Codigo = reader.GetString(1);
                        p.Nombre = reader.GetString(2);
                        p.EsDerivado = reader.GetByte(3) == 0? false: true;
                        p.Multiplicador = reader.GetInt32(4);

                        s.Id = reader.GetInt32(5);
                        s.Cantidad = reader.GetDecimal(6);
                        s.PrecioCompra = reader.GetDecimal(7);
                        s.PrecioVenta = reader.GetDecimal(8);
                        s.PrecioMinimo = reader.GetDecimal(9);
                        s.ProductoId = reader.GetInt32(10);
                        s.StockIdPadre = reader.IsDBNull(11)? 0: reader.GetInt32(11);
                        s.Auditorado = reader.GetByte(12) == 0? false: true;

                        p.Proveedores = reader.GetString(13);

                        StockViewModel sv = new(s,p);
                        lista.Add(sv);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<StockViewModel> GetByAllSearch(string searchValue)
        {
            string consulta = @"SELECT p.productoId, p.codigo, p.nombre, p.derivado, p.multiplicador, s.*,
                                (SELECT STRING_AGG(razonSocial, ', ') FROM Proveedores INNER JOIN Proveedores_Productos as pp ON pp.proveedorId = Proveedores.proveedorId
                                WHERE pp.productoId = p.productoId) 
                                FROM Stocks as s
                                INNER JOIN Productos as p ON s.productoId = p.productoId " +
                                $" WHERE s.stock > 0 AND (p.Codigo LIKE '%{searchValue}%' OR p.Nombre LIKE '%{searchValue}%') " +
                                $" ORDER BY codigo, precio_venta_sugerido desc";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            List<StockViewModel> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto p = new();
                        Stock s = new();
                        p.Id = reader.GetInt32(0);
                        p.Codigo = reader.GetString(1);
                        p.Nombre = reader.GetString(2);
                        p.EsDerivado = reader.GetByte(3) == 0 ? false : true;
                        p.Multiplicador = reader.GetInt32(4);

                        s.Id = reader.GetInt32(5);
                        s.Cantidad = reader.GetDecimal(6);
                        s.PrecioCompra = reader.GetDecimal(7);
                        s.PrecioVenta = reader.GetDecimal(8);
                        s.PrecioMinimo = reader.GetDecimal(9);
                        s.ProductoId = reader.GetInt32(10);
                        s.StockIdPadre = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
                        s.Auditorado = reader.GetByte(12) == 0 ? false : true;

                        p.Proveedores = reader.GetString(13);

                        StockViewModel sv = new(s, p);
                        lista.Add(sv);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<StockViewModel> GetByCodeSearch(string searchValue)
        {
            string consulta = @"SELECT p.productoId, p.codigo, p.nombre, p.derivado, p.multiplicador, s.*,
                                (SELECT STRING_AGG(razonSocial, ', ') FROM Proveedores INNER JOIN Proveedores_Productos as pp ON pp.proveedorId = Proveedores.proveedorId
                                WHERE pp.productoId = p.productoId) 
                                FROM Stocks as s
                                INNER JOIN Productos as p ON s.productoId = p.productoId " +
                                $" WHERE s.stock > 0 AND (p.Codigo LIKE '%{searchValue}%') " +
                                $" ORDER BY codigo, precio_venta_sugerido desc ";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            List<StockViewModel> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto p = new();
                        Stock s = new();
                        p.Id = reader.GetInt32(0);
                        p.Codigo = reader.GetString(1);
                        p.Nombre = reader.GetString(2);
                        p.EsDerivado = reader.GetByte(3) == 0 ? false : true;
                        p.Multiplicador = reader.GetInt32(4);

                        s.Id = reader.GetInt32(5);
                        s.Cantidad = reader.GetDecimal(6);
                        s.PrecioCompra = reader.GetDecimal(7);
                        s.PrecioVenta = reader.GetDecimal(8);
                        s.PrecioMinimo = reader.GetDecimal(9);
                        s.ProductoId = reader.GetInt32(10);
                        s.StockIdPadre = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
                        s.Auditorado = reader.GetByte(12) == 0 ? false : true;

                        p.Proveedores = reader.GetString(13);

                        StockViewModel sv = new(s, p);
                        lista.Add(sv);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<StockViewModel> GetByNameSearch(string searchValue)
        {
            string consulta = @"SELECT p.productoId, p.codigo, p.nombre, p.derivado, p.multiplicador, s.*,
                                (SELECT STRING_AGG(razonSocial, ', ') FROM Proveedores INNER JOIN Proveedores_Productos as pp ON pp.proveedorId = Proveedores.proveedorId
                                WHERE pp.productoId = p.productoId) 
                                FROM Stocks as s
                                INNER JOIN Productos as p ON s.productoId = p.productoId " +
                                $" WHERE s.stock > 0 AND (p.Nombre LIKE '%{searchValue}%') " +
                                $" ORDER BY codigo, precio_venta_sugerido desc";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            List<StockViewModel> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto p = new();
                        Stock s = new();
                        p.Id = reader.GetInt32(0);
                        p.Codigo = reader.GetString(1);
                        p.Nombre = reader.GetString(2);
                        p.EsDerivado = reader.GetByte(3) == 0 ? false : true;
                        p.Multiplicador = reader.GetInt32(4);

                        s.Id = reader.GetInt32(5);
                        s.Cantidad = reader.GetDecimal(6);
                        s.PrecioCompra = reader.GetDecimal(7);
                        s.PrecioVenta = reader.GetDecimal(8);
                        s.PrecioMinimo = reader.GetDecimal(9);
                        s.ProductoId = reader.GetInt32(10);
                        s.StockIdPadre = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
                        s.Auditorado = reader.GetByte(12) == 0 ? false : true;

                        p.Proveedores = reader.GetString(13);

                        StockViewModel sv = new(s, p);
                        lista.Add(sv);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<StockViewModel> GetByFilter(string filterType, int filterValue)
        {
            string consulta = @"SELECT p.productoId, p.codigo, p.nombre, p.derivado, p.multiplicador, s.*,
                                (SELECT STRING_AGG(razonSocial, ', ') FROM Proveedores INNER JOIN Proveedores_Productos as pp ON pp.proveedorId = Proveedores.proveedorId
                                WHERE pp.productoId = p.productoId) 
                                FROM Stocks as s
                                INNER JOIN Productos as p ON s.productoId = p.productoId ";
            if (filterType == "Categoria") consulta = consulta + @"LEFT JOIN Categorias_Productos as cp ON p.productoId = cp.productoId 
                                                                   WHERE s.stock > 0 AND cp.categoria = @id 
                                                                    ORDER BY codigo, precio_venta_sugerido desc";
            else if (filterType == "Proveedor") consulta = consulta + @"LEFT JOIN Proveedores_Productos as cp ON p.productoId = cp.productoId 
                                                                        WHERE cp.proveedorId = @id
                                                                        ORDER BY codigo, precio_venta_sugerido desc ";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", filterValue);
            List<StockViewModel> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto p = new();
                        Stock s = new();
                        p.Id = reader.GetInt32(0);
                        p.Codigo = reader.GetString(1);
                        p.Nombre = reader.GetString(2);
                        p.EsDerivado = reader.GetByte(3) == 0 ? false : true;
                        p.Multiplicador = reader.GetInt32(4);

                        s.Id = reader.GetInt32(5);
                        s.Cantidad = reader.GetDecimal(6);
                        s.PrecioCompra = reader.GetDecimal(7);
                        s.PrecioVenta = reader.GetDecimal(8);
                        s.PrecioMinimo = reader.GetDecimal(9);
                        s.ProductoId = reader.GetInt32(10);
                        s.StockIdPadre = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
                        s.Auditorado = reader.GetByte(12) == 0 ? false : true;

                        p.Proveedores = reader.GetString(13);

                        StockViewModel sv = new(s, p);
                        lista.Add(sv);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<StockViewModel> GetByFilterAndAllSearch(string filterType, int filterValue, string searchValue)
        {
            string consulta = @"SELECT p.productoId, p.codigo, p.nombre, p.derivado, p.multiplicador, s.*,
                                (SELECT STRING_AGG(razonSocial, ', ') FROM Proveedores INNER JOIN Proveedores_Productos as pp ON pp.proveedorId = Proveedores.proveedorId
                                WHERE pp.productoId = p.productoId) 
                                FROM Stocks as s
                                INNER JOIN Productos as p ON s.productoId = p.productoId ";
            if (filterType == "Categoria") consulta = consulta + @"LEFT JOIN Categorias_Productos as cp ON p.productoId = cp.productoId " +
                                                                   $"WHERE s.stock > 0 AND cp.categoria = @id AND (p.Codigo LIKE '%{searchValue}%' OR p.Nombre LIKE '%{searchValue}%') " +
                                                                   $" ORDER BY codigo, precio_venta_sugerido desc";
            else if (filterType == "Proveedor") consulta = consulta + @"LEFT JOIN Proveedores_Productos as cp ON p.productoId = cp.productoId "+ 
                                                                       $" WHERE s.stock > 0 AND cp.proveedorId = @id AND (p.Codigo LIKE '%{searchValue}%' OR p.Nombre LIKE '%{searchValue}%')" +
                                                                       $" ORDER BY codigo, precio_venta_sugerido desc"; ;
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", filterValue);
            List<StockViewModel> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto p = new();
                        Stock s = new();
                        p.Id = reader.GetInt32(0);
                        p.Codigo = reader.GetString(1);
                        p.Nombre = reader.GetString(2);
                        p.EsDerivado = reader.GetByte(3) == 0 ? false : true;
                        p.Multiplicador = reader.GetInt32(4);

                        s.Id = reader.GetInt32(5);
                        s.Cantidad = reader.GetDecimal(6);
                        s.PrecioCompra = reader.GetDecimal(7);
                        s.PrecioVenta = reader.GetDecimal(8);
                        s.PrecioMinimo = reader.GetDecimal(9);
                        s.ProductoId = reader.GetInt32(10);
                        s.StockIdPadre = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
                        s.Auditorado = reader.GetByte(12) == 0 ? false : true;

                        p.Proveedores = reader.GetString(13);

                        StockViewModel sv = new(s, p);
                        lista.Add(sv);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<StockViewModel> GetByFilterAndCodeSearch(string filterType, int filterValue, string searchValue)
        {
            string consulta = @"SELECT p.productoId, p.codigo, p.nombre, p.derivado, p.multiplicador, s.*,
                                (SELECT STRING_AGG(razonSocial, ', ') FROM Proveedores INNER JOIN Proveedores_Productos as pp ON pp.proveedorId = Proveedores.proveedorId
                                WHERE pp.productoId = p.productoId) 
                                FROM Stocks as s
                                INNER JOIN Productos as p ON s.productoId = p.productoId ";
            if (filterType == "Categoria") consulta = consulta + @"LEFT JOIN Categorias_Productos as cp ON p.productoId = cp.productoId " +
                                                                   $"WHERE s.stock > 0 AND cp.categoria = @id AND (p.Codigo LIKE '%{searchValue}%' )" +
                                                                   $" ORDER BY codigo, precio_venta_sugerido desc";
            else if (filterType == "Proveedor") consulta = consulta + @"LEFT JOIN Proveedores_Productos as cp ON p.productoId = cp.productoId " +
                                                                       $" WHERE s.stock > 0 AND cp.proveedorId = @id AND (p.Codigo LIKE '%{searchValue}%')" +
                                                                       $" ORDER BY codigo, precio_venta_sugerido desc"; ;
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", filterValue);
            List<StockViewModel> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto p = new();
                        Stock s = new();
                        p.Id = reader.GetInt32(0);
                        p.Codigo = reader.GetString(1);
                        p.Nombre = reader.GetString(2);
                        p.EsDerivado = reader.GetByte(3) == 0 ? false : true;
                        p.Multiplicador = reader.GetInt32(4);

                        s.Id = reader.GetInt32(5);
                        s.Cantidad = reader.GetDecimal(6);
                        s.PrecioCompra = reader.GetDecimal(7);
                        s.PrecioVenta = reader.GetDecimal(8);
                        s.PrecioMinimo = reader.GetDecimal(9);
                        s.ProductoId = reader.GetInt32(10);
                        s.StockIdPadre = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
                        s.Auditorado = reader.GetByte(12) == 0 ? false : true;

                        p.Proveedores = reader.GetString(13);

                        StockViewModel sv = new(s, p);
                        lista.Add(sv);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<StockViewModel> GetByFilterAndNameSearch(string filterType, int filterValue, string searchValue)
        { 
            string consulta = @"SELECT p.productoId, p.codigo, p.nombre, p.derivado, p.multiplicador, s.*,
                                (SELECT STRING_AGG(razonSocial, ', ') FROM Proveedores INNER JOIN Proveedores_Productos as pp ON pp.proveedorId = Proveedores.proveedorId
                                WHERE pp.productoId = p.productoId) 
                                FROM Stocks as s
                                INNER JOIN Productos as p ON s.productoId = p.productoId ";
            if (filterType == "Categoria") consulta = consulta + @"LEFT JOIN Categorias_Productos as cp ON p.productoId = cp.productoId " +
                                                                   $"WHERE s.stock > 0 AND cp.categoria = @id AND ( p.Nombre LIKE '%{searchValue}%')" +
                                                                   $" ORDER BY codigo, precio_venta_sugerido desc";
            else if (filterType == "Proveedor") consulta = consulta + @"LEFT JOIN Proveedores_Productos as cp ON p.productoId = cp.productoId " +
                                                                       $" WHERE s.stock > 0 AND cp.proveedorId = @id AND ( p.Nombre LIKE '%{searchValue}%') " +
                                                                       $" ORDER BY codigo, precio_venta_sugerido desc"; ;
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", filterValue);
            List<StockViewModel> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto p = new();
                        Stock s = new();
                        p.Id = reader.GetInt32(0);
                        p.Codigo = reader.GetString(1);
                        p.Nombre = reader.GetString(2);
                        p.EsDerivado = reader.GetByte(3) == 0 ? false : true;
                        p.Multiplicador = reader.GetInt32(4);

                        s.Id = reader.GetInt32(5);
                        s.Cantidad = reader.GetDecimal(6);
                        s.PrecioCompra = reader.GetDecimal(7);
                        s.PrecioVenta = reader.GetDecimal(8);
                        s.PrecioMinimo = reader.GetDecimal(9);
                        s.ProductoId = reader.GetInt32(10);
                        s.StockIdPadre = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
                        s.Auditorado = reader.GetByte(12) == 0 ? false : true;

                        p.Proveedores = reader.GetString(13);

                        StockViewModel sv = new(s, p);
                        lista.Add(sv);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<StockViewModel> GetByProductId(int id)
        {
            string consulta = @"SELECT p.productoId, p.codigo, p.nombre, p.derivado, p.multiplicador, s.*,
                                (SELECT STRING_AGG(razonSocial, ', ') FROM Proveedores INNER JOIN Proveedores_Productos as pp ON pp.proveedorId = Proveedores.proveedorId
                                WHERE pp.productoId = p.productoId) 
                                FROM Stocks as s
                                INNER JOIN Productos as p ON s.productoId = p.productoId
	                            WHERE p.productoId = @id 
                                ORDER BY codigo, stock desc, precio_venta_sugerido desc";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", id);
            List<StockViewModel> lista = new();
            if(miconexion.State == System.Data.ConnectionState.Closed) miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto p = new();
                        Stock s = new();
                        p.Id = reader.GetInt32(0);
                        p.Codigo = reader.GetString(1);
                        p.Nombre = reader.GetString(2);
                        p.EsDerivado = reader.GetByte(3) == 0 ? false : true;
                        p.Multiplicador = reader.GetInt32(4);

                        s.Id = reader.GetInt32(5);
                        s.Cantidad = reader.GetDecimal(6);
                        s.PrecioCompra = reader.GetDecimal(7);
                        s.PrecioVenta = reader.GetDecimal(8);
                        s.PrecioMinimo = reader.GetDecimal(9);
                        s.ProductoId = reader.GetInt32(10);
                        s.StockIdPadre = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
                        s.Auditorado = reader.GetByte(12) == 0 ? false : true;

                        p.Proveedores = reader.GetString(13);

                        StockViewModel sv = new(s, p);
                        lista.Add(sv);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static int Insert(ProductoIngresoViewModel item, object? window, int? stockPadreId, bool auditorado)
        {
            if (miconexion.State == System.Data.ConnectionState.Closed) miconexion.Open();

            string consulta = @"SELECT * FROM Stocks WHERE productoId = @productoId AND precio_compra = @precioCompra AND
                            precio_minimo = @precioMinimo AND precio_venta_sugerido = @precioVenta  AND auditorado = @auditorado";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@productoId", item.Producto.Id);
            comando.Parameters.AddWithValue("@precioCompra", item.PrecioCompra);
            comando.Parameters.AddWithValue("@precioMinimo", item.PrecioVentaMinimo);
            comando.Parameters.AddWithValue("@precioVenta", item.PrecioVenta);
            comando.Parameters.AddWithValue("@auditorado", auditorado);
            SqlDataReader reader = comando.ExecuteReader();
            int stockId = 0;
            decimal cantidad = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    stockId = reader.GetInt32(0);
                    cantidad = reader.GetDecimal(1);
                }

                string consulta2 = @"UPDATE Stocks SET stock = @stock WHERE stockId = @stockId";
                SqlCommand comando2 = new SqlCommand(consulta2, miconexion);
                comando2.Parameters.AddWithValue("@stock", cantidad + item.Cantidad);
                comando2.Parameters.AddWithValue("@stockId", stockId);
                reader.Close();
                comando2.ExecuteNonQuery();
                if (ProductoDAO.EsProductoPadre(item.Producto.Id))
                {
                    InsertAProductoHijo(item, window, stockId, auditorado);
                }

                if (miconexion.State == System.Data.ConnectionState.Open) miconexion.Close();
                return stockId;
            }
            else
            {
                reader.Close();
                string consulta3 = @"INSERT INTO stocks (stock,precio_compra,precio_venta_sugerido,precio_minimo,productoId,stockIdPadre, auditorado)
                                        VALUES (@stock,@precioCompra,@precioVenta,@precioMinimo,@productoId,@stockIdPadre, @auditorado)";

                SqlCommand comando3 = new SqlCommand(consulta3, miconexion);
                comando3.Parameters.AddWithValue("@stock", item.Cantidad);
                comando3.Parameters.AddWithValue("@precioCompra", item.PrecioCompra);
                comando3.Parameters.AddWithValue("@precioVenta", item.PrecioVenta);
                comando3.Parameters.AddWithValue("@precioMinimo", item.PrecioVentaMinimo);
                comando3.Parameters.AddWithValue("@productoId", item.Producto.Id);
                comando3.Parameters.AddWithValue("@auditorado", auditorado);
                if (stockPadreId != null && stockPadreId > 0) comando3.Parameters.AddWithValue("@stockIdPadre", stockPadreId);
                else comando3.Parameters.AddWithValue("@stockIdPadre", DBNull.Value);
                comando3.ExecuteNonQuery();
                string consulta2 = @"SELECT * FROM stocks WHERE productoId = @productoId AND precio_compra = @precioCompra AND
                            precio_minimo = @precioMinimo AND precio_venta_sugerido = @precioVenta AND auditorado = @auditorado";
                SqlCommand comando4 = new SqlCommand(consulta2, miconexion);
                comando4.Parameters.AddWithValue("@productoId", item.Producto.Id);
                comando4.Parameters.AddWithValue("@precioCompra", item.PrecioCompra);
                comando4.Parameters.AddWithValue("@precioMinimo", item.PrecioVentaMinimo);
                comando4.Parameters.AddWithValue("@precioVenta", item.PrecioVenta);
                comando4.Parameters.AddWithValue("@auditorado", auditorado);
                SqlDataReader reader2 = comando4.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        stockId = reader2.GetInt32(0);
                    }
                    reader2.Close();
                }
                if (ProductoDAO.EsProductoPadre(item.Producto.Id))
                {
                    InsertAProductoHijo(item, window, stockId, auditorado);
                }
            }
            
            if (miconexion.State == System.Data.ConnectionState.Open) miconexion.Close();


            return stockId;
        }

        public static void InsertAProductoHijo(ProductoIngresoViewModel productoPadre, object? window, int? stockPadreId, bool auditorado)
        {
            foreach (var item in ProductoDAO.GetProductosHijos(productoPadre.Producto.Id))
            {
                IngresoProductosDerivadosView ventana = new();
                ventana.Owner = window as IngresosView;
                IngresoProductoDerivadosViewModel ViewModel = ventana.DataContext as IngresoProductoDerivadosViewModel;
                item.PrecioCompra = productoPadre.PrecioCompra / item.Producto.Multiplicador;
                item.Cantidad = productoPadre.Cantidad * item.Producto.Multiplicador;
                ProductoIngresoViewModel pr = new ProductoIngresoViewModel(item, null);
                ViewModel.Producto = pr;
                ViewModel.CargarStocksExistentes();
                ventana.ShowDialog();
                List<ProductoIngresoViewModel> lista = new();
                lista.Add(pr);
                foreach (var item2 in lista)
                {
                    Insert(item2, null, stockPadreId, auditorado);
                }
            }
        }

        public static void Update(StockViewModel stock)
        {
            string consulta = @"UPDATE Stocks SET precio_minimo = @precioMinimo, precio_venta_sugerido = @precioVenta
                                WHERE stockId = @id";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@precioMinimo", stock.PrecioMinimo);
            comando.Parameters.AddWithValue("@precioVenta", stock.PrecioVenta);
            comando.Parameters.AddWithValue("@id", stock.Stock.Id);

            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();
        }

        public static void UpdateStockWhenDeleteIngreso(int cantidad, int stockId)
        {
            miconexion.Open();
            decimal stock = 0;
            string consulta = "SELECT * FROM Stocks WHERE stockId = @id";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", stockId);
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        stock = reader.GetDecimal(1);
                    }
                }
            }
            stock = stock - cantidad;
            string consulta2 = @"UPDATE Stocks SET stock = @stock WHERE stockId = @stockId";
            SqlCommand comando2 = new(consulta2, miconexion);
            comando2.Parameters.AddWithValue("@stockId", stockId);
            comando2.Parameters.AddWithValue("@stock", stock);
            comando2.ExecuteNonQuery();

            miconexion.Close();
        }

        public static void UpdateStocksHijosWhenDeleteIngreso(int stockPadreId)
        {
            miconexion.Open();
            string consulta = @"SELECT * FROM Stocks 
                                INNER JOIN Productos on Productos.productoId = Stocks.productoId
                                WHERE stockIdPadre = @idPadre";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@idPadre", stockPadreId);

            List<Tuple<int,int>> idHijos = new();
            
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Tuple<int, int> t = new(reader.GetInt32(0), reader.GetInt32(14));
                        idHijos.Add(t);
                    }
                }
            }

            foreach (var item in idHijos)
            {
                string consulta2 = "UPDATE Stocks SET stock = (SELECT stock FROM Stocks WHERE stockId = @stockIdPadre) * @stock WHERE stockId = @stockId";
                SqlCommand comando2 = new(consulta2, miconexion);
                comando2.Parameters.AddWithValue("@stockIdPadre", stockPadreId);
                comando2.Parameters.AddWithValue("@stockId", item.Item1);
                comando2.Parameters.AddWithValue("@stock", item.Item2);
                comando2.ExecuteNonQuery();
            }
            miconexion.Close();
        }

        public static bool ValidarDeleteIngreso(int ingreso)
        {
            miconexion.Open();
            string consulta = @"SELECT * FROM Productos_Ingresos
                                WHERE ingresoId = @ingreso";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@ingreso", ingreso);

            SqlDataReader reader = comando.ExecuteReader();
            List<Tuple<int, int>> lista = new();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Tuple<int, int> t = new(reader.GetInt32(3), reader.GetInt32(5));
                        lista.Add(t);
                    }
                }
            }
            bool continuar = true;
            foreach (var item in lista)
            {
                string consulta2 = @"SELECT * FROM Stocks WHERE stockId = @id";
                SqlCommand comando2 = new SqlCommand(consulta2, miconexion);
                comando2.Parameters.AddWithValue("@id", item.Item2);
                SqlDataReader reader2 = comando2.ExecuteReader();
                using (reader2)
                {
                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            if (reader2.GetDecimal(1) < item.Item1) continuar = false;
                        }
                    }
                }
            }
            miconexion.Close();
            return continuar;
        }
    }
}
