using MultiMarcasAPP.Models;
using MultiMarcasAPP.ViewModels.GastosViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.ApplicationModel.CommunicationBlocking;

namespace MultiMarcasAPP.Services.DAO
{
    public class GastoDAO
    {
        public static SqlConnection miconexion { get; } = new SqlConnection(DBConection.CadenaDeConexionContabilidad);

        public static bool InsertarNuevoTipo(string nombre, bool deducible)
        {
            string consulta = @"INSERT INTO CategoriaGastos (Nombre, Deducible)
                                VALUES (@nombre, @deducible) ";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@deducible", deducible);
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

        public static List<TipoGastoViewModel> GetTipos()
        {
            string consulta = @"SELECT * FROM CategoriaGastos";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            List<TipoGastoViewModel> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TipoGastoViewModel Tipo = new();
                        Tipo.Id = reader.GetInt32(0);
                        Tipo.Nombre = reader.GetString(1);
                        Tipo.Deducible = reader.GetByte(2) == 0 ? false : true;
                        lista.Add(Tipo);
                    }
                }
            }
            miconexion.Close();
            return lista;
        }

        public static bool InsertGasto(GastoViewModel gasto)
        {
            string consulta = @"INSERT INTO Gastos (Descripcion,PagoId,Fecha,CategoriaGastoId)
                                VALUES (@Descripcion,@PagoId,@Fecha,@CategoriaGastoId)";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@Descripcion", gasto.Descripcion);
            comando.Parameters.AddWithValue("@PagoId", gasto.Pago.Id);
            comando.Parameters.AddWithValue("@CategoriaGastoId", gasto.TipoGasto.Id);
            comando.Parameters.AddWithValue("@Fecha", gasto.Pago.Fecha);

            try
            {
                miconexion.Open();
                comando.ExecuteNonQuery();
                miconexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            return true;
        }

        public static List<GastoViewModel> GetGastosByDates(DateTime fecha1, DateTime fecha2)
        {
            string consulta = @"select * from Gastos as g
                                INNER JOIN CategoriaGastos as cg ON g.categoriaGastoId = cg.Id
                                INNER JOIN Pagos as p on g.PagoId = p.Id
                                INNER JOIN TiposPago as tp on p.TipoPagoId = tp.Id
                                WHERE p.Fecha BETWEEN @fecha1 AND @fecha2";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@fecha1", fecha1);
            comando.Parameters.AddWithValue("@fecha2", fecha2);
            List<GastoViewModel> lista = new List<GastoViewModel>();
            try
            {
                miconexion.Open();
                
                SqlDataReader reader = comando.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        GastoViewModel gasto = new GastoViewModel();
                        gasto.Id = reader.GetInt32(0);
                        gasto.Descripcion = reader.GetString(1);
                        gasto.Pago.Id = reader.GetInt32(2);
                        gasto.TipoGasto.Id = reader.GetInt32(4);
                        gasto.TipoGasto.Nombre = reader.GetString(6);
                        gasto.TipoGasto.Deducible = reader.GetByte(7) == 0? false: true;
                        gasto.Pago.Fecha = reader.GetDateTime(9);
                        gasto.Pago.Monto = reader.GetDecimal(10);
                        gasto.Pago.NoDoc = reader.IsDBNull(11) == true? String.Empty : reader.GetString(11);
                        gasto.Pago.TipoPago.Id = reader.GetInt32(12);
                        gasto.Pago.TipoPago.Nombre = reader.GetString(14);
                        gasto.Pago.TipoPago.SaleCaja = reader.GetByte(15) == 0? false : true;

                        lista.Add(gasto);
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

        public static List<GastoViewModel> GetByMonthAndYearDeducibles(int mes, int año)
        {
            string consulta = @"select * from Gastos as g
                                INNER JOIN CategoriaGastos as cg ON g.categoriaGastoId = cg.Id
                                INNER JOIN Pagos as p on g.PagoId = p.Id
                                INNER JOIN TiposPago as tp on p.TipoPagoId = tp.Id
                                WHERE MONTH(p.Fecha) = @mes AND YEAR(p.Fecha) = @año AND cg.Deducible = 1";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@mes", mes);
            comando.Parameters.AddWithValue("@año", año);
            List<GastoViewModel> lista = new List<GastoViewModel>();
            try
            {
                miconexion.Open();

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        GastoViewModel gasto = new GastoViewModel();
                        gasto.Id = reader.GetInt32(0);
                        gasto.Descripcion = reader.GetString(1);
                        gasto.Pago.Id = reader.GetInt32(2);
                        gasto.TipoGasto.Id = reader.GetInt32(4);
                        gasto.TipoGasto.Nombre = reader.GetString(6);
                        gasto.TipoGasto.Deducible = reader.GetByte(7) == 0 ? false : true;
                        gasto.Pago.Fecha = reader.GetDateTime(9);
                        gasto.Pago.Monto = reader.GetDecimal(10);
                        gasto.Pago.NoDoc = reader.IsDBNull(11) == true ? String.Empty : reader.GetString(11);
                        gasto.Pago.TipoPago.Id = reader.GetInt32(12);
                        gasto.Pago.TipoPago.Nombre = reader.GetString(14);
                        gasto.Pago.TipoPago.SaleCaja = reader.GetByte(15) == 0 ? false : true;

                        lista.Add(gasto);
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

        public static List<GastoViewModel> GetByMonthAndYearNoDeducibles(int mes, int año)
        {
            string consulta = @"select * from Gastos as g
                                INNER JOIN CategoriaGastos as cg ON g.categoriaGastoId = cg.Id
                                INNER JOIN Pagos as p on g.PagoId = p.Id
                                INNER JOIN TiposPago as tp on p.TipoPagoId = tp.Id
                                WHERE MONTH(p.Fecha) = @mes AND YEAR(p.Fecha) = @año AND cg.Deducible = 0";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@mes", mes);
            comando.Parameters.AddWithValue("@año", año);
            List<GastoViewModel> lista = new List<GastoViewModel>();
            try
            {
                miconexion.Open();

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        GastoViewModel gasto = new GastoViewModel();
                        gasto.Id = reader.GetInt32(0);
                        gasto.Descripcion = reader.GetString(1);
                        gasto.Pago.Id = reader.GetInt32(2);
                        gasto.TipoGasto.Id = reader.GetInt32(4);
                        gasto.TipoGasto.Nombre = reader.GetString(6);
                        gasto.TipoGasto.Deducible = reader.GetByte(7) == 0 ? false : true;
                        gasto.Pago.Fecha = reader.GetDateTime(9);
                        gasto.Pago.Monto = reader.GetDecimal(10);
                        gasto.Pago.NoDoc = reader.IsDBNull(11) == true ? String.Empty : reader.GetString(11);
                        gasto.Pago.TipoPago.Id = reader.GetInt32(12);
                        gasto.Pago.TipoPago.Nombre = reader.GetString(14);
                        gasto.Pago.TipoPago.SaleCaja = reader.GetByte(15) == 0 ? false : true;

                        lista.Add(gasto);
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
