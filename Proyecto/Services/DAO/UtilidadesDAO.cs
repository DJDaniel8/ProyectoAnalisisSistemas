using MultiMarcasAPP.Models;
using MultiMarcasAPP.ViewModels.UtilidadesViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Services.DAO
{
    public class UtilidadesDAO
    {
        public static SqlConnection miconexion { get; } = new SqlConnection(DBConection.CadenaDeConexion);

        public static List<Tuple<Venta, decimal>> GetUtilidadesPorVenta()
        {
            List<Venta> ventas = new List<Venta>();
            List<Tuple<Venta,decimal>> lista = new List<Tuple<Venta,decimal>>();
            ventas = VentaDAO.GetLast100();
            string consulta = @"SELECT SUM((pv.precio_venta - s.precio_compra)*pv.cantidad) FROM Productos_Ventas as pv
                                INNER JOIN Stocks as s ON pv.stockId = s.stockId
                                WHERE pv.ventaId = @id";
            miconexion.Open();
            foreach (var item in ventas)
            {
                SqlCommand comando = new(consulta, miconexion);
                comando.Parameters.AddWithValue("@id", item.Id);
                SqlDataReader reader = comando.ExecuteReader();
                using(reader)
                {
                    reader.Read();
                    lista.Add(new(item, reader.GetDecimal(0)));
                }
            }
            miconexion.Close();

            return lista;
        }

        public static List<Tuple<Venta, decimal>> GetUtilidadesPorVentaToday()
        {
            List<Venta> ventas = new List<Venta>();
            List<Tuple<Venta, decimal>> lista = new List<Tuple<Venta, decimal>>();
            ventas = VentaDAO.GetToday();
            string consulta = @"SELECT SUM((pv.precio_venta - s.precio_compra)*pv.cantidad) FROM Productos_Ventas as pv
                                INNER JOIN Stocks as s ON pv.stockId = s.stockId
                                WHERE pv.ventaId = @id";
            miconexion.Open();
            foreach (var item in ventas)
            {
                SqlCommand comando = new(consulta, miconexion);
                comando.Parameters.AddWithValue("@id", item.Id);
                SqlDataReader reader = comando.ExecuteReader();
                using (reader)
                {
                    reader.Read();
                    lista.Add(new(item, reader.GetDecimal(0)));
                }
            }
            miconexion.Close();

            return lista;
        }

        public static List<Tuple<Venta, decimal>> GetUtilidadesPorVentaDate(DateTime date)
        {
            List<Venta> ventas = new List<Venta>();
            List<Tuple<Venta, decimal>> lista = new List<Tuple<Venta, decimal>>();
            ventas = VentaDAO.GetByDate(date);
            string consulta = @"SELECT SUM((pv.precio_venta - s.precio_compra)*pv.cantidad) FROM Productos_Ventas as pv
                                INNER JOIN Stocks as s ON pv.stockId = s.stockId
                                WHERE pv.ventaId = @id";
            miconexion.Open();
            foreach (var item in ventas)
            {
                SqlCommand comando = new(consulta, miconexion);
                comando.Parameters.AddWithValue("@id", item.Id);
                SqlDataReader reader = comando.ExecuteReader();
                using (reader)
                {
                    reader.Read();
                    lista.Add(new(item, reader.GetDecimal(0)));
                }
            }
            miconexion.Close();

            return lista;
        }

        public static List<UtilidadProducto> GetUtilidadesPorProductosTop10()
        {
            string consulta = @"SELECT TOP(10) s.productoId, 
                                SUM(pv.precio_venta*pv.cantidad) as Total,
                                SUM((pv.precio_venta-s.precio_compra)*pv.cantidad) as Utilidad
                                FROM Ventas AS v
                                INNER JOIN Productos_Ventas AS pv ON v.ventaId = pv.ventaId
                                INNER JOIN Stocks as s ON pv.stockId = s.stockId
                                GROUP BY s.productoId
                                ORDER BY Utilidad Desc";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            List<UtilidadProducto> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using(reader)
            {
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto p = ProductoDAO.GetById(reader.GetInt32(0));
                        UtilidadProducto utilidad = new()
                        {
                            producto = p,
                            Total = reader.GetDecimal(1),
                            Utilidad = reader.GetDecimal(2)
                        };
                        lista.Add(utilidad);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<UtilidadProducto> GetUtilidadesPorProductosTop100()
        {
            string consulta = @"SELECT TOP(100) s.productoId, 
                                SUM(pv.precio_venta*pv.cantidad) as Total,
                                SUM((pv.precio_venta-s.precio_compra)*pv.cantidad) as Utilidad
                                FROM Ventas AS v
                                INNER JOIN Productos_Ventas AS pv ON v.ventaId = pv.ventaId
                                INNER JOIN Stocks as s ON pv.stockId = s.stockId
                                GROUP BY s.productoId
                                ORDER BY Utilidad Desc";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            List<UtilidadProducto> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto p = ProductoDAO.GetById(reader.GetInt32(0));
                        UtilidadProducto utilidad = new()
                        {
                            producto = p,
                            Total = reader.GetDecimal(1),
                            Utilidad = reader.GetDecimal(2)
                        };
                        lista.Add(utilidad);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<UtilidadProducto> GetUtilidadesPorProductosTopByCode(int id)
        {
            string consulta = @"SELECT TOP(10) s.productoId, 
                                SUM(pv.precio_venta*pv.cantidad) as Total,
                                SUM((pv.precio_venta-s.precio_compra)*pv.cantidad) as Utilidad
                                FROM Ventas AS v
                                INNER JOIN Productos_Ventas AS pv ON v.ventaId = pv.ventaId
                                INNER JOIN Stocks as s ON pv.stockId = s.stockId
                                WHERE s.productoId = @id
                                GROUP BY s.productoId
                                ORDER BY Utilidad Desc";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", id);
            List<UtilidadProducto> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto p = ProductoDAO.GetById(reader.GetInt32(0));
                        UtilidadProducto utilidad = new()
                        {
                            producto = p,
                            Total = reader.GetDecimal(1),
                            Utilidad = reader.GetDecimal(2)
                        };
                        lista.Add(utilidad);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static UtilidadFecha GetUtilidadPorFecha(DateTime inicio, DateTime fin)
        {
            string consulta = @"SELECT SUM(pv.precio_venta*cantidad),SUM((pv.precio_venta-s.precio_compra)*pv.cantidad) FROM ventas as v
                                INNER JOIN Productos_Ventas as pv ON v.ventaId = pv.ventaId
                                INNER JOIN Stocks as s ON pv.stockId = s.stockId
                                WHERE v.fecha BETWEEN @inicio AND @fin";
            SqlCommand comando = new(consulta, miconexion);
            comando.Parameters.AddWithValue("@inicio", inicio);
            comando.Parameters.AddWithValue("@fin", fin.AddDays(1));
            UtilidadFecha utilidad = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if(reader.HasRows)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            utilidad.Fecha = inicio.ToShortDateString() + " a " + fin.ToShortDateString();
                            if(!reader.IsDBNull(0)) utilidad.Total = reader.GetDecimal(0);
                            if (!reader.IsDBNull(1)) utilidad.Utilidad = reader.GetDecimal(1);
                        }
                    }
                }
            }
            miconexion.Close();

            return utilidad;
        }
    }
}
