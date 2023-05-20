using MultiMarcasAPP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMarcasAPP.Services.DAO
{
    public class ReporteVentaDAO
    {
        public static SqlConnection miconexion { get; } = new SqlConnection(DBConection.CadenaDeConexion);
        public static SqlConnection miconexionContabilidad { get; } = new SqlConnection(DBConection.CadenaDeConexionContabilidad);

        public static List<ReporteVentaVentas> GetVentas(int año, int mes)
        {
            string consulta = @"SELECT CONVERT(DATE,fecha), SUM(pv.cantidad*pv.precio_venta) FROM ventas AS v
                                INNER JOIN productos_ventas AS pv ON v.ventaId = pv.ventaId
                                INNER JOIN stocks AS s ON pv.stockId = s.stockId
                                WHERE YEAR(fecha) = @año AND MONTH(fecha) = @mes
                                GROUP BY CONVERT(DATE,fecha)";
            CultureInfo arSA = new CultureInfo("es-GT");
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@año", año);
            comando.Parameters.AddWithValue("@mes", mes);
            List<ReporteVentaVentas> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ReporteVentaVentas rvv = new();
                        rvv.Fecha = reader.GetDateTime(0);
                        rvv.Monto = reader.GetDecimal(1);
                        rvv.Descripcion = $"Total ventas de la fecha {rvv.Fecha.Date.ToString("d", arSA)}";
                        rvv.TipoClase = 1;
                        lista.Add(rvv);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<ReporteVentaGasto> GetGastos(int año, int mes)
        {
            string consulta = @"SELECT CONVERT(DATE,p.Fecha), SUM(p.Monto) 
                                FROM Gastos as g
                                INNER JOIN Pagos as p ON g.PagoId = p.Id
                                INNER JOIN TiposPago as tp ON p.TipoPagoId = tp.Id
                                WHERE YEAR(p.Fecha) = @año AND MONTH(p.Fecha) = @mes AND tp.SaleCaja = 1
                                GROUP BY CONVERT(DATE,p.Fecha)";
            CultureInfo arSA = new CultureInfo("es-GT");
            SqlCommand comando = new SqlCommand(consulta, miconexionContabilidad);
            comando.Parameters.AddWithValue("@año", año);
            comando.Parameters.AddWithValue("@mes", mes);
            List<ReporteVentaGasto> lista = new();
            miconexionContabilidad.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        ReporteVentaGasto rvg = new();
                        rvg.Fecha = reader.GetDateTime(0);
                        rvg.Monto = reader.GetDecimal(1);
                        rvg.Descripcion = $"Total gastos de la fecha {rvg.Fecha.Date.ToString("d", arSA)}";
                        rvg.TipoClase = 2;
                        lista.Add(rvg);
                    }
                }
            }
            miconexionContabilidad.Close();
            return lista;
        }

        public static List<ReporteVentaDeposito> GetDepositos(int año, int mes)
        {
            string consulta = @"SELECT CONVERT(DATE,fecha), SUM(monto)
                                FROM DepositosInternos
                                WHERE YEAR(Fecha) = @año AND MONTH(Fecha) = @mes
                                GROUP BY CONVERT(DATE,fecha)";
            CultureInfo arSA = new CultureInfo("es-GT");
            SqlCommand comando = new SqlCommand(consulta, miconexionContabilidad);
            comando.Parameters.AddWithValue("@año", año);
            comando.Parameters.AddWithValue("@mes", mes);
            List<ReporteVentaDeposito> lista = new();
            miconexionContabilidad.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ReporteVentaDeposito rvg = new();
                        rvg.Fecha = reader.GetDateTime(0);
                        rvg.Monto = reader.GetDecimal(1);
                        rvg.Descripcion = $"Total Depositos de la fecha {rvg.Fecha.Date.ToString("d", arSA)}";
                        rvg.TipoClase = 3;
                        lista.Add(rvg);
                    }
                }
            }
            miconexionContabilidad.Close();
            return lista;
        }

        public static List<ReporteVentasPagos> GetPagos(int año, int mes)
        {
            string consulta = @"SELECT CONVERT(DATE,p.Fecha), SUM(p.Monto)
                                FROM Pagos AS p 
                                INNER JOIN TiposPago as tp ON p.TipoPagoId = tp.Id
                                INNER JOIN InformacionIngresos_Pagos as iip ON p.Id = iip.PagoId
                                WHERE tp.SaleCaja = 1 AND YEAR(Fecha) = @año AND MONTH(Fecha) = @mes
                                GROUP BY CONVERT(DATE,p.Fecha)";
            CultureInfo arSA = new CultureInfo("es-GT");
            SqlCommand comando = new SqlCommand(consulta, miconexionContabilidad);
            comando.Parameters.AddWithValue("@año", año);
            comando.Parameters.AddWithValue("@mes", mes);
            List<ReporteVentasPagos> lista = new();
            miconexionContabilidad.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ReporteVentasPagos rvg = new();
                        rvg.Fecha = reader.GetDateTime(0);
                        rvg.Monto = reader.GetDecimal(1);
                        rvg.Descripcion = $"Total Pagos a Proveedores de la fecha {rvg.Fecha.Date.ToString("d", arSA)}";
                        rvg.TipoClase = 4;
                        lista.Add(rvg);
                    }
                }
            }
            miconexionContabilidad.Close();
            return lista;
        }

        public static List<ReporteVentaInyeccionCapital> GetInyecciones(int año, int mes)
        {
            string consulta = @"SELECT CONVERT(DATE,Fecha), SUM(Monto)
                                FROM InyeccionesCapital
                                WHERE YEAR(Fecha) = 2022 AND MONTH(Fecha) = 9
                                GROUP BY CONVERT(DATE,Fecha)";
            CultureInfo arSA = new CultureInfo("es-GT");
            SqlCommand comando = new SqlCommand(consulta, miconexionContabilidad);
            comando.Parameters.AddWithValue("@año", año);
            comando.Parameters.AddWithValue("@mes", mes);
            List<ReporteVentaInyeccionCapital> lista = new();
            miconexionContabilidad.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ReporteVentaInyeccionCapital rvg = new();
                        rvg.Fecha = reader.GetDateTime(0);
                        rvg.Monto = reader.GetDecimal(1);
                        rvg.Descripcion = $"Total Inyeccion de Capital de la fecha {rvg.Fecha.Date.ToString("d", arSA)}";
                        rvg.TipoClase = 5;
                        lista.Add(rvg);
                    }
                }
            }
            miconexionContabilidad.Close();
            return lista;
        }

        public static List<int> GetAños()
        {
            string consulta = @"SELECT YEAR(fecha) FROM ventas
                              GROUP BY YEAR(fecha)";

            SqlCommand comando = new SqlCommand(consulta, miconexion);
            miconexion.Open();
            List<int> lista = new();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lista.Add(reader.GetInt32(0));
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static List<int> GetMeses(int año)
        {
            string consulta = @"SELECT MONTH(fecha) 
                                FROM ventas
                                WHERE YEAR(fecha) = @año
                                GROUP BY MONTH(fecha)";

            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@año", año);
            miconexion.Open();
            List<int> lista = new();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lista.Add(reader.GetInt32(0));
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static void InsertSaldoFinal(int año, int mes, decimal saldoFinal)
        {
            string consulta = @"INSERT INTO SaldosFinales (Año, Mes, SaldoFinal)
                                VALUES (@año, @mes, @saldoFinal)";
            SqlCommand comando = new SqlCommand(consulta, miconexionContabilidad);
            comando.Parameters.AddWithValue("@año", año);
            comando.Parameters.AddWithValue("@mes", mes);
            comando.Parameters.AddWithValue("@saldoFinal", saldoFinal);

            miconexionContabilidad.Open();
            comando.ExecuteNonQuery();
            miconexionContabilidad.Close();
        }

        public static decimal GetSaldoFinalMesAnterior(int año, int mes)
        {
            string fecha = $"1/{mes}/{año}";
            string consulta = @"SELECT SaldoFinal
                                FROM SaldosFinales
                                WHERE Año = YEAR(DATEADD(MONTH, -1, CONVERT(DATE, @fecha,103))) AND
                                Mes = MONTH(DATEADD(MONTH, -1, CONVERT(DATE, @fecha,103)))";
            SqlCommand comando = new SqlCommand(consulta, miconexionContabilidad);
            comando.Parameters.AddWithValue("@fecha", fecha);
            decimal saldofinal = 0;
            miconexionContabilidad.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        saldofinal = reader.GetDecimal(0);
                    }
                }
            }
            miconexionContabilidad.Close();
            return saldofinal;
        }

        public static bool ExisteSaldoFinalMesAnterior(int año, int mes)
        {
            string consulta = @"SELECT * FROM SaldosFinales
                                WHERE Año = @año AND Mes = @mes";
            SqlCommand comando = new SqlCommand(consulta, miconexionContabilidad);
            comando.Parameters.AddWithValue("@año", año);
            comando.Parameters.AddWithValue("@mes", mes);
            bool respuesta = false;
            miconexionContabilidad.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                respuesta = reader.HasRows;
            }
            miconexionContabilidad.Close();
            return respuesta;
        }

        public static decimal GetSaldoFinal()
        {
            List<ReporteVenta> _Reportes = new();
            int SelectedAño = DateTime.Now.Year;
            int SelectedMes = DateTime.Now.Month;
            foreach (var item in GetVentas(SelectedAño, SelectedMes))
            {
                _Reportes.Add(item);
            }

            foreach (var item in GetGastos(SelectedAño, SelectedMes))
            {
                _Reportes.Add(item);
            }

            foreach (var item in GetDepositos(SelectedAño, SelectedMes))
            {
                _Reportes.Add(item);
            }

            foreach (var item in GetPagos(SelectedAño, SelectedMes))
            {
                _Reportes.Add(item);
            }

            foreach (var item in GetInyecciones(SelectedAño, SelectedMes))
            {
                _Reportes.Add(item);
            }
            _Reportes = (from rv in _Reportes
                         orderby rv.Fecha ascending
                         select rv).ToList();

            decimal Vienen = GetSaldoFinalMesAnterior(SelectedAño, SelectedMes);

            decimal saldo = Vienen;
            foreach (var item in _Reportes)
            {
                if (item is ReporteVentaVentas)
                {
                    saldo = saldo + item.Monto;
                    item.Saldo = saldo;
                }
                else if (item is ReporteVentaInyeccionCapital)
                {
                    saldo = saldo + item.Monto;
                    item.Saldo = saldo;
                }
                else
                {
                    saldo = saldo - item.Monto;
                    item.Saldo = saldo;
                }
            }
            decimal SaldoFinal = saldo;
            return SaldoFinal;
        }
    }
}
