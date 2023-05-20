using MultiMarcasAPP.Models;
using MultiMarcasAPP.ViewModels.PagosViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiMarcasAPP.Services.DAO
{
    public class PagosDAO
    {
        public static SqlConnection miconexion { get; } = new SqlConnection(DBConection.CadenaDeConexionContabilidad);

        public static bool InsertarNuevoTipo(string nombre, bool saleDeCaja)
        {
            string consulta = @"INSERT INTO TiposPago (Nombre, SaleCaja)
                                VALUES (@nombre, @saleCaja) ";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@saleCaja", saleDeCaja);
            miconexion.Open();
            try
            {
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            
            miconexion.Close();
            return true;
        }

        public static List<TipoPagoViewModel> GetTipos()
        {
            string consulta = @"SELECT * FROM TiposPago";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            List<TipoPagoViewModel> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TipoPagoViewModel Tipo = new();
                        Tipo.Id = reader.GetInt32(0);
                        Tipo.Nombre = reader.GetString(1);
                        Tipo.SaleCaja = reader.GetByte(2) == 0? false : true;
                        lista.Add(Tipo);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static bool InsertPago(PagoViewModel pago)
        {
            string consulta = @"INSERT INTO Pagos (Fecha,Monto,NoDoc,TipoPagoId)
                                VALUES (@Fecha,@Monto,@NoDoc,@TipoPagoId)";
            SqlCommand command = new SqlCommand(consulta, miconexion);
            command.Parameters.AddWithValue("@Fecha", pago.Fecha);
            command.Parameters.AddWithValue("@Monto", pago.Monto);
            command.Parameters.AddWithValue("@NoDoc", String.IsNullOrEmpty(pago.NoDoc)? DBNull.Value : pago.NoDoc);
            command.Parameters.AddWithValue("@TipoPagoId", pago.TipoPago.Id);

            
            try
            {
                miconexion.Open();
                command.ExecuteNonQuery();
                miconexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            
            

            return true;
        }

        public static PagoViewModel LastInsert()
        {
            string consulta = @"SELECT TOP(1) * FROM Pagos as p 
                                INNER JOIN TiposPago as tp on p.TipoPagoId = tp.Id
                                ORDER BY p.Id DESC";
            SqlCommand comando = new SqlCommand(consulta,miconexion);

            PagoViewModel pago = new();
            
            try
            {
                miconexion.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        pago.Id = reader.GetInt32(0);
                        pago.Fecha = reader.GetDateTime(1);
                        pago.Monto = reader.GetDecimal(2);
                        pago.NoDoc = reader.GetString(3);
                        pago.TipoPago.Id = reader.GetInt32(4);
                        pago.TipoPago.Nombre = reader.GetString(6);
                        pago.TipoPago.SaleCaja = reader.GetByte(7) == 0 ? false : true;
                    }
                }
                miconexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return pago;
        }

        public static List<PagoViewModel> GetByDate(DateTime fecha1, DateTime fecha2)
        {
            string consulta = @"SELECT * FROM Pagos as p
                                INNER JOIN TiposPago as tp ON p.TipoPagoId = tp.Id
                                WHERE p.Fecha BETWEEN @fecha1 AND @fecha2";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@fecha1", fecha1);
            comando.Parameters.AddWithValue("@fecha2", fecha2);
            List<PagoViewModel> lista = new();

            try
            {
                miconexion.Open();
                SqlDataReader reader = comando.ExecuteReader();
                using(reader)
                {
                    if (reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            PagoViewModel Pago = new PagoViewModel();
                            Pago.Id = reader.GetInt32(0);
                            Pago.Fecha = reader.GetDateTime(1);
                            Pago.Monto = reader.GetDecimal(2);
                            if(!reader.IsDBNull(3)) Pago.NoDoc = reader.GetString(3);
                            Pago.TipoPago.Id = reader.GetInt32(5);
                            Pago.TipoPago.Nombre = reader.GetString(6);
                            Pago.TipoPago.SaleCaja = reader.GetByte(7) == 0 ? false : true;
                            lista.Add(Pago);
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

        public static List<TotalPagosMensual> GetTotales(int año)
        {
            string consulta = @"SELECT YEAR(Fecha), MONTH(FECHA), SUM(p.Monto), tp.SaleCaja  FROM Pagos p
                                INNER JOIN InformacionIngresos_Pagos iip ON p.Id = iip.PagoId
                                INNER JOIN InformacionIngresos ii ON iip.InformacionIngresoId = ii.Id
                                INNER JOIN TiposPago tp ON p.TipoPagoId = tp.Id
                                WHERE YEAR(Fecha) = @año
                                GROUP BY YEAR(Fecha) , MONTH(FECHA), tp.SaleCaja";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@año", año);
            List<TotalPagosMensual> lista = new();

            try
            {
                miconexion.Open();
                SqlDataReader reader = comando.ExecuteReader();
                using (reader)
                {
                    if(reader.HasRows)
                    {
                        while(reader.Read()) 
                        {
                            TotalPagosMensual tpm = new();
                            tpm.Año = reader.GetInt32(0);
                            tpm.Mes = reader.GetInt32(1);
                            tpm.Total = reader.GetDecimal(2);
                            tpm.FechaLetras = $"{tpm.Mes}/{tpm.Año}";
                            tpm.SaleDeCaja = reader.GetByte(3) == 1? true : false;
                            lista.Add(tpm);
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

        public static List<PagoProveedor> GetPagosByYearAndMonth(int año, int mes, bool saleCaja)
        {
            string consulta = @"SELECT p.Fecha, ii.NoDoc, p.NoDoc, tp.Nombre, p.Monto, ii.IngresoId
                                FROM Pagos p
                                INNER JOIN InformacionIngresos_Pagos iip ON p.Id = iip.PagoId
                                INNER JOIN InformacionIngresos ii ON iip.InformacionIngresoId = ii.Id
                                INNER JOIN TiposPago tp ON p.TipoPagoId = tp.Id
                                WHERE YEAR(p.Fecha) = @año AND MONTH(p.Fecha) = @mes AND tp.SaleCaja = @saleCaja";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@año", año);
            comando.Parameters.AddWithValue("@mes", mes);
            comando.Parameters.AddWithValue("@saleCaja", saleCaja == true ? 1 : 0);
            List<PagoProveedor> lista = new();

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
                            PagoProveedor pp = new();
                            pp.Fecha = reader.GetDateTime(0);
                            pp.NoDocFactura = reader.GetString(1);
                            pp.NoDocPago = reader.GetString(2);
                            pp.TipoPago = reader.GetString(3);
                            pp.Monto = reader.GetDecimal(4);
                            pp.proveedor = new();
                            pp.proveedor.Id = reader.GetInt32(5);
                            lista.Add(pp);
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

        public static List<PagoViewModel> GetByInformacionIngresosId(int id)
        {
            string consulta = @"SELECT * FROM Pagos as p
                                INNER JOIN TiposPago as tp ON p.TipoPagoId = tp.Id
                                INNER JOIN InformacionIngresos_Pagos as iip ON p.Id = iip.PagoId
                                WHERE iip.InformacionIngresoId = @id";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", id);
            List<PagoViewModel> lista = new();

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
                            PagoViewModel Pago = new PagoViewModel();
                            Pago.Id = reader.GetInt32(0);
                            Pago.Fecha = reader.GetDateTime(1);
                            Pago.Monto = reader.GetDecimal(2);
                            if (!reader.IsDBNull(3)) Pago.NoDoc = reader.GetString(3);
                            Pago.TipoPago.Id = reader.GetInt32(5);
                            Pago.TipoPago.Nombre = reader.GetString(6);
                            Pago.TipoPago.SaleCaja = reader.GetByte(7) == 0 ? false : true;
                            lista.Add(Pago);
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

        public static List<int> GetAños()
        {
            string consulta = @"SELECT YEAR(fecha) FROM Pagos
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
    }
}
