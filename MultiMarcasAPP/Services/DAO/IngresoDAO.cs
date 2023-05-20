using MultiMarcasAPP.Models;
using MultiMarcasAPP.ViewModels.IngresosViewModels;
using MultiMarcasAPP.Views.IngresosViews;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Services.DAO
{
    public class IngresoDAO
    {

        public static SqlConnection miconexion { get; } = new SqlConnection(DBConection.CadenaDeConexion);


        public static List<Ingreso> Get10()
        {
            string consulta = @"SELECT TOP(10) *, (SELECT SUM(cantidad*preciocompra) FROM Productos_Ingresos
                                WHERE ingresoId = i.ingresoId) total FROM Ingresos as i
                                INNER JOIN Proveedores as p ON i.proveedorId = p.proveedorId
                                ORDER BY i.fecha DESC";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            miconexion.Open();
            List<Ingreso> lista = new();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Proveedor p = new();
                        p.Id = reader.GetInt32(5);
                        p.RazonSocial = reader.GetString(6);
                        if(!reader.IsDBNull(7)) p.Direccion = reader.GetString(7);
                        if(!reader.IsDBNull(8)) p.Telefono = reader.GetString(8);

                        Ingreso i = new(p);
                        i.Id = reader.GetInt32(0);
                        i.Fecha = reader.GetDateTime(1);
                        i.TrabajadorId = reader.GetInt32(2);
                        i.Auditorado = reader.GetByte(4) == 0?  false : true;
                        i.TotalIngreso = reader.GetDecimal(9);

                        lista.Add(i);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<Ingreso> Get50()
        {
            string consulta = @"SELECT TOP(50) *, (SELECT SUM(cantidad*preciocompra) FROM Productos_Ingresos
                                WHERE ingresoId = i.ingresoId) total FROM Ingresos as i
                                INNER JOIN Proveedores as p ON i.proveedorId = p.proveedorId
                                ORDER BY i.fecha DESC";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            miconexion.Open();
            List<Ingreso> lista = new();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Proveedor p = new();
                        p.Id = reader.GetInt32(5);
                        p.RazonSocial = reader.GetString(6);
                        if (!reader.IsDBNull(7)) p.Direccion = reader.GetString(7);
                        if (!reader.IsDBNull(8)) p.Telefono = reader.GetString(8);

                        Ingreso i = new(p);
                        i.Id = reader.GetInt32(0);
                        i.Fecha = reader.GetDateTime(1);
                        i.TrabajadorId = reader.GetInt32(2);
                        i.Auditorado = reader.GetByte(4) == 0 ? false : true;
                        i.TotalIngreso = reader.GetDecimal(9);

                        lista.Add(i);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<Ingreso> GetbyDate(DateTime fecha1, DateTime fecha2)
        {
            string consulta = @"SELECT *, (SELECT SUM(cantidad*preciocompra) FROM Productos_Ingresos
                                WHERE ingresoId = i.ingresoId) total FROM Ingresos as i
                                INNER JOIN Proveedores as p ON i.proveedorId = p.proveedorId
                                WHERE i.fecha between @fecha1 AND @fecha2
                                ORDER BY i.fecha DESC";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@fecha1", fecha1);
            comando.Parameters.AddWithValue("@fecha2", fecha2);
            miconexion.Open();
            List<Ingreso> lista = new();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Proveedor p = new();
                        p.Id = reader.GetInt32(5);
                        p.RazonSocial = reader.GetString(6);
                        if (!reader.IsDBNull(7)) p.Direccion = reader.GetString(7);
                        if (!reader.IsDBNull(8)) p.Telefono = reader.GetString(8);

                        Ingreso i = new(p);
                        i.Id = reader.GetInt32(0);
                        i.Fecha = reader.GetDateTime(1);
                        i.TrabajadorId = reader.GetInt32(2);
                        i.Auditorado = reader.GetByte(4) == 0 ? false : true;
                        i.TotalIngreso = reader.GetDecimal(9);

                        lista.Add(i);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<Ingreso> GetByDate(int mes, int año)
        {
            string consulta = @"SELECT i.ingresoId, i.fecha, i.trabajadorId, i.proveedorId, i.auditorado,
                                i.proveedorId, p.razonSocial, p.direccion, p.telefono, 
                                (SELECT SUM(cantidad*preciocompra) FROM productos_ingresos WHERE productos_ingresos.ingresoId = i.ingresoId)
                                FROM Ingresos as i
                                INNER JOIN Proveedores as p ON i.proveedorId = p.proveedorId
                                WHERE MONTH(fecha) = @mes AND YEAR(fecha) = @año
                                ORDER BY i.fecha DESC";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@mes", mes);
            comando.Parameters.AddWithValue("@año", año);
            miconexion.Open();
            List<Ingreso> lista = new();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Proveedor p = new();
                        p.Id = reader.GetInt32(5);
                        p.RazonSocial = reader.GetString(6);
                        if (!reader.IsDBNull(7)) p.Direccion = reader.GetString(7);
                        if (!reader.IsDBNull(8)) p.Telefono = reader.GetString(8);

                        Ingreso i = new(p);
                        i.Id = reader.GetInt32(0);
                        i.Fecha = reader.GetDateTime(1);
                        i.TrabajadorId = reader.GetInt32(2);
                        i.Auditorado = reader.GetByte(4) == 0 ? false : true;
                        i.TotalIngreso = reader.GetDecimal(9);
                        lista.Add(i);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<ProductoIngreso> GetProductosIngresos(int id)
        {
            string consulta = @"SELECT * FROM Productos_Ingresos as pin
                                INNER JOIN Productos as p ON pin.productoId = p.productoId
                                WHERE pin.ingresoId = @id";
            SqlCommand comando = new(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", id);
            List<ProductoIngreso> lista = new();
            miconexion.Open();
            using (SqlDataReader reader = comando.ExecuteReader())
            {
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto p = new();
                        p.Id = reader.GetInt32(6);
                        p.Codigo = reader.GetString(7);
                        p.Nombre = reader.GetString(8);
                        ProductoIngreso pi = new(p);
                        pi.Id = reader.GetInt32(0);
                        pi.IngresoId = reader.GetInt32(1);
                        pi.Cantidad = reader.GetInt32(3);
                        pi.PrecioCompra = reader.GetDecimal(4);
                        pi.stockId = reader.GetInt32(5);
                        lista.Add(pi);
                    }
                }
            }

            miconexion.Close();
            return lista;
        }

        public static void Insert(List<ProductoIngresoViewModel> productos, int proveedorId, bool auditorado, object? ventana, DateTime fecha)
        {
            string consulta = @"INSERT INTO Ingresos (fecha, trabajadorId, proveedorId, auditorado) 
                                VALUES (@fecha, @trabajadorId, @proveedorId, @auditorado)";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@fecha", fecha);
            comando.Parameters.AddWithValue("@trabajadorId", CurrentEmploye.Trabajador.Id);
            comando.Parameters.AddWithValue("@proveedorId", proveedorId);
            comando.Parameters.AddWithValue("@auditorado", auditorado);

            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();
            foreach (var item in productos)
            {
                item.PrecioCompra = item.PrecioCompra - (item.Descuento / item.Cantidad);
            }
            InsertProductosIngreso(productos, GetIdLastInsert(), auditorado, ventana);
        }

        public static int GetIdLastInsert()
        {
            string consulta = @"SELECT TOP(1) * FROM Ingresos ORDER BY ingresoId DESC";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            miconexion.Open();
            int id = 0;
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if(reader.HasRows)
                {
                    reader.Read();
                    id =  reader.GetInt32(0);
                }
            }
            miconexion.Close();
            return id;
        }

        public static void InsertProductosIngreso(List<ProductoIngresoViewModel> productos, int IngresoId, bool auditorado, object? ventana)
        {
            string consulta = @"INSERT INTO Productos_Ingresos (ingresoId, productoId, cantidad, precioCompra, stockId)
                                VALUES (@ingresoId, @productoId, @cantidad, @precioCompra, @stockId)";
            miconexion.Open();
            foreach (var item in productos)
            {
                int stockId = StockDAO.Insert(item, ventana, null, auditorado);
                SqlCommand comando = new SqlCommand(consulta, miconexion);
                comando.Parameters.AddWithValue("@ingresoId", IngresoId);
                comando.Parameters.AddWithValue("@productoId", item.Producto.Id);
                comando.Parameters.AddWithValue("@cantidad", item.Cantidad);
                comando.Parameters.AddWithValue("@precioCompra", item.PrecioCompra);
                comando.Parameters.AddWithValue("@stockId", stockId);
                comando.ExecuteNonQuery();
            }
            miconexion.Close();
        }

        public static void Delete(int ingresoId, List<ProductoIngreso> lista)
        {
            
            foreach (var item in lista)
            {
                StockDAO.UpdateStockWhenDeleteIngreso(item.Cantidad, item.stockId);
                StockDAO.UpdateStocksHijosWhenDeleteIngreso(item.stockId);
            }

            string consulta = @"DELETE FROM Ingresos WHERE ingresoId = @id";
            miconexion.Open();
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", ingresoId);
            comando.ExecuteNonQuery();
            miconexion.Close();

            //Eliminamos Informacion de InformacionIngresos 
            InformacionIngresosDAO.Delete(ingresoId);
        }
    }
}
