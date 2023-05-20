using MultiMarcasAPP.ViewModels.InformacionIngresosViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.ApplicationModel.UserDataTasks;

namespace MultiMarcasAPP.Services.DAO
{
    public class InformacionIngresosDAO
    {
        public static SqlConnection miconexion { get; } = new SqlConnection(DBConection.CadenaDeConexionContabilidad);
        public static SqlConnection miconexionBD { get; } = new SqlConnection(DBConection.CadenaDeConexion);

        public static bool Insert(string noDocumento, decimal total, int ingresoId)
        {
            string consulta = @"INSERT INTO InformacionIngresos (NoDoc, TotalIngreso, IngresoId, Pagado)
                                VALUES (@NoDoc, @TotalIngreso, @IngresoId, @Pagado)";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@NoDoc", noDocumento);
            comando.Parameters.AddWithValue("@TotalIngreso", total);
            comando.Parameters.AddWithValue("@IngresoId", ingresoId);
            comando.Parameters.AddWithValue("@Pagado", 0);

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

        public static List<InformacionIngresoViewModel> GetPendientes()
        {
            string consulta = @"SELECT ii.*,
                                i.fecha, p.razonSocial,
                                (SELECT SUM(Pagos.Monto) FROM Pagos 
                                INNER JOIN InformacionIngresos_Pagos ON Pagos.Id = InformacionIngresos_Pagos.PagoId
                                WHERE InformacionIngresos_Pagos.InformacionIngresoId = ii.Id)
                                FROM InformacionIngresos as ii
                                INNER JOIN [MultiMarcasDB].[dbo].[Ingresos] as i ON ii.IngresoId = i.ingresoId
                                INNER JOIN [MultiMarcasDB].[dbo].[Proveedores] as p ON i.proveedorId = p.proveedorId
                                WHERE ii.Pagado = 0";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            List<InformacionIngresoViewModel> lista = new();
            try
            {
                miconexion.Open();
                SqlDataReader reader = comando.ExecuteReader();
                using(reader)
                {
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            InformacionIngresoViewModel IIV = new();
                            IIV.Id = reader.GetInt32(0);
                            if(!reader.IsDBNull(1)) IIV.NumeroDocumento = reader.GetString(1);
                            IIV.TotalIngreso = reader.GetDecimal(2);
                            IIV.IngresoId = reader.GetInt32(3);
                            IIV.Pagado = reader.GetByte(4) == 0 ? false : true;
                            IIV.Fecha = reader.GetDateTime(5);
                            IIV.Proveedor = reader.GetString(6);
                            IIV.TotalPagado = reader.IsDBNull(7) == true ? 0 : reader.GetDecimal(7);
                            lista.Add(IIV);
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

        public static List<InformacionIngresoViewModel> GetByFecha(int año, int mes)
        {
            string consulta = @"SELECT ii.*,
                                i.fecha, p.razonSocial,
                                (SELECT SUM(Pagos.Monto) FROM Pagos 
                                INNER JOIN InformacionIngresos_Pagos ON Pagos.Id = InformacionIngresos_Pagos.PagoId
                                WHERE InformacionIngresos_Pagos.InformacionIngresoId = ii.Id)
                                FROM InformacionIngresos as ii
                                INNER JOIN [MultiMarcasDB].[dbo].[Ingresos] as i ON ii.IngresoId = i.ingresoId
                                INNER JOIN [MultiMarcasDB].[dbo].[Proveedores] as p ON i.proveedorId = p.proveedorId
                                WHERE YEAR(i.fecha) = @año AND MONTH(i.fecha) = @mes";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@año", año);
            comando.Parameters.AddWithValue("@mes", mes);
            List<InformacionIngresoViewModel> lista = new();
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
                            InformacionIngresoViewModel IIV = new();
                            IIV.Id = reader.GetInt32(0);
                            if (!reader.IsDBNull(1)) IIV.NumeroDocumento = reader.GetString(1);
                            IIV.TotalIngreso = reader.GetDecimal(2);
                            IIV.IngresoId = reader.GetInt32(3);
                            IIV.Pagado = reader.GetByte(4) == 0 ? false : true;
                            IIV.Fecha = reader.GetDateTime(5);
                            IIV.Proveedor = reader.GetString(6);
                            IIV.TotalPagado = reader.IsDBNull(7) == true ? 0 : reader.GetDecimal(7);
                            lista.Add(IIV);
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

        public static bool InsertPago(InformacionIngresosInsertarPagosViewModel info)
        {
            string consulta = @"INSERT INTO Pagos (Fecha, Monto, NoDoc, TipoPagoId)
                                VALUES (@Fecha, @Monto, @NoDoc, @TipoPagoId)";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@Fecha", info.FechaPago);
            comando.Parameters.AddWithValue("@Monto", info.Monto);
            comando.Parameters.AddWithValue("@NoDoc", info.NumeroDeDocumento);
            comando.Parameters.AddWithValue("@TipoPagoId", info.SelectedPago.Id);

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

        public static bool InsertPagoInformacionIngresos(int pagoId, int InformacionIngresoId)
        {
            string consulta = @"INSERT INTO InformacionIngresos_Pagos (InformacionIngresoId, PagoId)
                                VALUES (@Id1, @Id2)";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@Id1", InformacionIngresoId);
            comando.Parameters.AddWithValue("@Id2", pagoId);

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

        public static bool UpdateInformacionIngresos(bool value, int InformacionIngresoId)
        {
            string consulta = @"UPDATE InformacionIngresos SET Pagado = @value WHERE Id = @Id";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@value", value);
            comando.Parameters.AddWithValue("@Id", InformacionIngresoId);

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

        public static bool Delete(int ingresoId)
        {
            string consulta = @"DELETE FROM InformacionIngresos WHERE IngresoId = @Id";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@Id", ingresoId);

            try
            {
                miconexion.Open();
                comando.ExecuteNonQuery();
                miconexion.Close();
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return true;
        }

        public static List<int> GetAños()
        {
            string consulta = @"SELECT YEAR(fecha) FROM Ingresos
                              GROUP BY YEAR(fecha)";

            SqlCommand comando = new SqlCommand(consulta, miconexionBD);
            miconexionBD.Open();
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
            miconexionBD.Close();
            return lista;
        }

        public static List<int> GetMeses(int año)
        {
            string consulta = @"SELECT MONTH(fecha) 
                                FROM Ingresos
                                WHERE YEAR(fecha) = @año
                                GROUP BY MONTH(fecha)";

            SqlCommand comando = new SqlCommand(consulta, miconexionBD);
            comando.Parameters.AddWithValue("@año", año);
            miconexionBD.Open();
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
            miconexionBD.Close();
            return lista;
        }

    }
}
