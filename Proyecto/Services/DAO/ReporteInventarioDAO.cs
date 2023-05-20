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
    public class ReporteInventarioDAO
    {
        public static SqlConnection miconexion { get; } = new SqlConnection(DBConection.CadenaDeConexion);

        public static List<ReporteInventarioProducto> GetInfo(DateTime fecha)
        {
            List<ReporteInventarioProducto> lista1 = GetCurrentStock();
            List<ReporteInventarioProducto> lista2 = GetSoldStock(fecha);
            List<ReporteInventarioProducto> lista3 = GetReceivedStock(fecha);

            foreach (var item in lista2)
            {
                ReporteInventarioProducto p = lista1.FirstOrDefault(x => x.StockId == item.StockId);
                if(p != null)
                {
                    p.Stock += item.Stock;
                    p.Total = p.PrecioCompra * p.Stock;
                }
                else
                {
                    lista1.Add(item);
                }
            }

            foreach (var item in lista3)
            {
                ReporteInventarioProducto p = lista1.FirstOrDefault(x => x.StockId == item.StockId);
                if (p != null)
                {
                    p.Stock -= item.Stock;
                    p.Total = p.PrecioCompra * p.Stock;
                }
            }

            return lista1;
        }

        public static List<ReporteInventarioProducto> GetCurrentStock()
        {
            string consulta = @"SELECT P.nombre, S.stock, S.precio_compra, S.stockId
                                FROM Stocks S
                                INNER JOIN Productos P ON S.productoId = P.productoId
                                WHERE S.stock > 0 AND (S.stockIdPadre is NULL  OR S.stockIdPadre = 0)";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            List<ReporteInventarioProducto> lista = new();
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
                            ReporteInventarioProducto producto = new ReporteInventarioProducto();
                            producto.Descripcion = reader.GetString(0);
                            producto.Stock = int.Parse(reader.GetDecimal(1).ToString("f0"));
                            producto.PrecioCompra = reader.GetDecimal(2);
                            producto.StockId = reader.GetInt32(3);
                            producto.Total = producto.Stock * producto.PrecioCompra;
                            lista.Add(producto);
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

        public static List<ReporteInventarioProducto> GetSoldStock(DateTime fecha)
        {
            string consulta = @"SELECT P.nombre, SUM(PV.cantidad), S.precio_compra, S.stockId 
                                FROM Ventas V
                                INNER JOIN Productos_Ventas PV ON V.ventaId = PV.ventaId
                                INNER JOIN Stocks S ON PV.stockId = S.stockId
                                INNER JOIN Productos P ON S.productoId = P.productoId
                                WHERE V.fecha >= @fecha
                                GROUP BY P.nombre, S.precio_compra, S.stockId";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@fecha", fecha);
            List<ReporteInventarioProducto> lista = new();
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
                            ReporteInventarioProducto producto = new ReporteInventarioProducto();
                            producto.Descripcion = reader.GetString(0);
                            producto.Stock = int.Parse(reader.GetInt32(1).ToString("f0"));
                            producto.PrecioCompra = reader.GetDecimal(2);
                            producto.StockId = reader.GetInt32(3);
                            producto.Total = producto.Stock * producto.PrecioCompra;
                            lista.Add(producto);
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

        public static List<ReporteInventarioProducto> GetReceivedStock(DateTime fecha)
        {
            string consulta = @"SELECT P.nombre, SUM(PRI.cantidad), S.precio_compra, S.stockId 
                                FROM Ingresos I
                                INNER JOIN Productos_Ingresos PRI ON I.ingresoId = PRI.ingresoId
                                INNER JOIN Stocks S ON PRI.stockId = S.stockId
                                INNER JOIN Productos P ON S.productoId = P.productoId
                                WHERE I.fecha >= @fecha
                                GROUP BY P.nombre, S.precio_compra, S.stockId";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@fecha", fecha);
            List<ReporteInventarioProducto> lista = new();
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
                            ReporteInventarioProducto producto = new ReporteInventarioProducto();
                            producto.Descripcion = reader.GetString(0);
                            producto.Stock = int.Parse(reader.GetInt32(1).ToString("f0"));
                            producto.PrecioCompra = reader.GetDecimal(2);
                            producto.StockId = reader.GetInt32(3);
                            producto.Total = producto.Stock * producto.PrecioCompra;
                            lista.Add(producto);
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
