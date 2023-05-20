using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MultiMarcasAPP.Models;
using MultiMarcasAPP.ViewModels.PersonalViewModels;

namespace MultiMarcasAPP.Services.DAO
{
    public class TrabajadorDAO 
    {
        public static SqlConnection miconexion { get; } = new SqlConnection(DBConection.CadenaDeConexion);

        public static bool RequestAccess(string user, string password)
        {
            if (FindEmployeByUser(user))
            {
                if (ValidatePassword(user, password)) return true;
                else return false;
            }
            else return false;
        }

        public static bool FindEmployeByUser(string usuario)
        {
            string consulta = "SELECT * FROM trabajadores WHERE usuario = @usuario"; // Busca un Trabajador por medio del usuario
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@usuario", usuario);
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            bool valor = reader.HasRows;
            reader.Close();
            miconexion.Close();
            return valor;
        }

        public static bool ValidatePassword(string usuario, string contraseña)
        {
            string consulta = "SELECT * FROM trabajadores WHERE usuario = @usuario AND contraseña = @contraseña"; //Validamos que la contraseña sea la correcta
            SqlCommand comando = new SqlCommand(consulta, miconexion); 
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@contraseña", contraseña);
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            bool valor = reader.HasRows;
            reader.Close();
            miconexion.Close();
            return valor;
        }

        public static Trabajador GetEmployeByUser(string user)
        {
            string consulta = "SELECT * FROM trabajadores WHERE usuario = @usuario";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@usuario", user);
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            Trabajador empleado = new Trabajador();
            if (reader.HasRows)
            {
                reader.Read();

                empleado.Id = reader.GetInt32(0);
                empleado.Nombre = reader.GetString(1);
                empleado.Apellido = reader.GetString(2);
                empleado.Sexo = reader.GetString(3);
                empleado.Puesto = reader.GetString(4);
                empleado.Usuario = reader.GetString(5);
                if(!reader.IsDBNull(7)) empleado.Direccion = reader.GetString(7);
                if(!reader.IsDBNull(8)) empleado.Telefono = reader.GetString(8);
                if (!reader.IsDBNull(9)) empleado.Email = reader.GetString(9);
                if(!reader.IsDBNull(10)) empleado.Sueldo = reader.GetDecimal(10);
            }
            miconexion.Close();
            return empleado;
        }

        public static List<Trabajador> Get()
        {
            return GetAll();
        }

        private static List<Trabajador> GetAll()
        {
            string consulta = "SELECT * FROM trabajadores";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            List<Trabajador> lista = new();
            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    Trabajador trabajador = new Trabajador();
                    trabajador.Id = reader.GetInt32(0);
                    trabajador.Nombre = reader.GetString(1);
                    trabajador.Apellido = reader.GetString(2);
                    trabajador.Sexo = reader.GetString(3);
                    trabajador.Puesto = reader.GetString(4);
                    trabajador.Usuario = reader.GetString(5);
                    if (!reader.IsDBNull(7)) trabajador.Direccion = reader.GetString(7);
                    if (!reader.IsDBNull(8)) trabajador.Telefono = reader.GetString(8);
                    if (!reader.IsDBNull(9)) trabajador.Email = reader.GetString(9);
                    if (!reader.IsDBNull(10)) trabajador.Sueldo = reader.GetDecimal(10);
                    lista.Add(trabajador);
                }
            }
            miconexion.Close();
            return lista;
        }

        public static void Insert(Trabajador trabajador, string password)
        {
            string consulta = @"INSERT INTO trabajadores (nombre, apellido, sexo, puesto, usuario, contraseña, direccion, telefono, email, sueldo)
                                VALUES (@nombre, @apellido, @sexo, @puesto, @usuario, @contraseña, @direccion, @telefono, @email, @sueldo)";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@nombre", trabajador.Nombre);
            comando.Parameters.AddWithValue("@apellido", trabajador.Apellido);
            comando.Parameters.AddWithValue("@sexo", trabajador.Sexo);
            comando.Parameters.AddWithValue("@puesto", trabajador.Puesto);
            comando.Parameters.AddWithValue("@usuario", trabajador.Usuario);
            comando.Parameters.AddWithValue("@contraseña", password);
            if(!String.IsNullOrEmpty(trabajador.Direccion)) comando.Parameters.AddWithValue("@direccion", trabajador.Direccion);
            else comando.Parameters.AddWithValue("@direccion", DBNull.Value);
            if (!String.IsNullOrEmpty(trabajador.Telefono)) comando.Parameters.AddWithValue("@telefono", trabajador.Telefono);
            else comando.Parameters.AddWithValue("@telefono", DBNull.Value);
            if (!String.IsNullOrEmpty(trabajador.Email)) comando.Parameters.AddWithValue("@email", trabajador.Email);
            else comando.Parameters.AddWithValue("@email", DBNull.Value);
            if (trabajador.Sueldo > 0) comando.Parameters.AddWithValue("@sueldo", trabajador.Sueldo);
            else comando.Parameters.AddWithValue("@sueldo", DBNull.Value);

            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();

            InsertPrivilegios();
        }

        public static void Update(Trabajador trabajador)
        {
            string consulta = @"UPDATE trabajadores SET nombre = @nombre, apellido = @apellido, sexo = @sexo, puesto = @puesto, usuario = @usuario,
                                direccion = @direccion, telefono = @telefono, @email = email, sueldo = @sueldo WHERE trabajadorId = @id";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@nombre", trabajador.Nombre);
            comando.Parameters.AddWithValue("@apellido", trabajador.Apellido);
            comando.Parameters.AddWithValue("@sexo", trabajador.Sexo);
            comando.Parameters.AddWithValue("@puesto", trabajador.Puesto);
            comando.Parameters.AddWithValue("@usuario", trabajador.Usuario);
            if (!String.IsNullOrEmpty(trabajador.Direccion)) comando.Parameters.AddWithValue("@direccion", trabajador.Direccion);
            else comando.Parameters.AddWithValue("@direccion", DBNull.Value);
            if (!String.IsNullOrEmpty(trabajador.Telefono)) comando.Parameters.AddWithValue("@telefono", trabajador.Telefono);
            else comando.Parameters.AddWithValue("@telefono", DBNull.Value);
            if (!String.IsNullOrEmpty(trabajador.Email)) comando.Parameters.AddWithValue("@email", trabajador.Email);
            else comando.Parameters.AddWithValue("@email", DBNull.Value);
            if (trabajador.Sueldo > 0) comando.Parameters.AddWithValue("@sueldo", trabajador.Sueldo);
            else comando.Parameters.AddWithValue("@sueldo", DBNull.Value);
            comando.Parameters.AddWithValue("@id", trabajador.Id);

            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();
        }

        public static PrivilegiosTrabajador GetPrivilegios(int trabajadorId)
        {
            string consulta = "SELECT * FROM PrivilegiosTrabajadores WHERE trabajadorId = @id";
            SqlCommand comando = new SqlCommand(consulta, miconexion);
            comando.Parameters.AddWithValue("@id", trabajadorId);

            miconexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            PrivilegiosTrabajador p = new();
            using (reader)
            {
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        
                        p.Ventas = reader.GetByte(2) == 0 ? false : true;
                        p.CambiarPrecio = reader.GetByte(3) == 0 ? false : true;
                        p.Cotizaciones = reader.GetByte(4) == 0 ? false : true;
                        p.CambiarPrecio2 = reader.GetByte(5) == 0 ? false : true;
                        p.Productos = reader.GetByte(6) == 0 ? false : true;
                        p.CrearProducto = reader.GetByte(7) == 0 ? false : true;
                        p.EditarProducto = reader.GetByte(8) == 0 ? false : true;
                        p.EliminarProducto = reader.GetByte(9) == 0 ? false : true;
                        p.Stocks = reader.GetByte(10) == 0 ? false : true;
                        p.EditarStock = reader.GetByte(11) == 0 ? false : true;
                        p.Facturas = reader.GetByte(12) == 0 ? false : true;
                        p.EliminarFactura = reader.GetByte(13) == 0 ? false : true;
                        p.Personal = reader.GetByte(14) == 0 ? false : true;
                        p.PrivilegiosPersonal = reader.GetByte(15) == 0 ? false : true;
                        p.CrearPersonal = reader.GetByte(16) == 0 ? false : true;
                        p.EditarPersonal = reader.GetByte(17) == 0 ? false : true;
                        p.EliminarPersonal = reader.GetByte(18) == 0 ? false : true;
                        p.Utilidades = reader.GetByte(19) == 0 ? false : true;
                        p.Clientes = reader.GetByte(20) == 0 ? false : true;
                        p.CrearClientes = reader.GetByte(21) == 0 ? false : true;
                        p.EditarClientes = reader.GetByte(22) == 0 ? false : true;
                        p.EliminarClientes = reader.GetByte(23) == 0 ? false : true;
                        p.Categorias = reader.GetByte(24) == 0 ? false : true;
                        p.CrearCategoria = reader.GetByte(25) == 0 ? false : true;
                        p.EditarCategoria = reader.GetByte(26) == 0 ? false : true;
                        p.EliminarCategoria = reader.GetByte(27) == 0 ? false : true;
                        p.Proveedores = reader.GetByte(28) == 0 ? false : true;
                        p.CrearProveedor = reader.GetByte(29) == 0 ? false : true;
                        p.EditarProveedor = reader.GetByte(30) == 0 ? false : true;
                        p.EliminarProveedor = reader.GetByte(31) == 0 ? false : true;
                        p.Ingresos = reader.GetByte(32) == 0 ? false : true;
                        p.CrearIngreso = reader.GetByte(33) == 0 ? false : true;
                        p.EliminarIngreso = reader.GetByte(34) == 0 ? false : true;
                        p.Caja = reader.GetByte(35) == 0 ? false : true;
                        p.ContinuarCaja = reader.GetByte(36) == 0? false : true;
                        p.EliminarCaja = reader.GetByte(37) == 0 ? false : true;
                        p.Contabilidad = reader.GetByte(38) == 0 ? false : true;
                        p.Pagos = reader.GetByte(39) == 0 ? false : true;
                        p.Gastos = reader.GetByte(40) == 0 ? false : true;
                        p.InformacionIngresos = reader.GetByte(41) == 0 ? false : true;
                        p.DepositosInternos = reader.GetByte(42) == 0 ? false : true;
                        p.InyeccionCapital = reader.GetByte(43) == 0 ? false : true;
                    }
                }
            }
            miconexion.Close();
            return p;
        }

        public static void InsertPrivilegios()
        {
            string consulta = @"INSERT INTO PrivilegiosTrabajadores ( trabajadorId ,IsVentaAllow,IsCambiarPrecioAllow,IsCotizacionAllow
                                  ,IsCambiarPrecio2Allow,IsProductosAllow ,IsCrearProductosAllow,IsEditarProductoAllow,IsEliminarProductoAllow
                                  ,IsStocksAllow ,IsEditarStockAllow,IsFacturasAllow,IsEliminarFacturaAllow,IsPersonalAllow
                                  ,IsPrivilegioPersonalAllow,IsCrearPersonalAllow ,IsEditarPersonalAllow
                                  ,IsEliminarPersonalAllow,IsUtilidadesAllow
                                  ,IsClientesAllow ,IsCrearClienteAllow,IsEditarClienteAllow,IsEliminarClienteAllow,IsCategoriasAllow
                                  ,IsCrearCategoriaAllow,IsEditarCategoriaAllow,IsEliminarCategoriaAllow
                                  ,IsProveedoresAllow,IsCrearProveedorAllow ,IsEditarProveedorAllow,IsEliminarProveedorAllow
                                  ,IsIngresosAllow,IsNuevoIngresoAllow,IsEliminarIngresoAllow
                                  ,IsCajaAllow,IsContinuarCajaAllow,IsEliminarCajaAllow
                                  ,IsContabilidadAllow,IsPagosAllow,IsGastosAllow
                                  ,IsInfoIngresosAllow,IsDepositosInternosAllow,IsInyeccionCapitalAllow
                              )
                              VALUES ((SELECT TOP(1) trabajadorId FROM Trabajadores ORDER BY trabajadorId desc),0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0);";
            SqlCommand comando = new(consulta, miconexion);
            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();
        }

        public static void UpdatePrivilegios(int id, PrivilegiosPersonalViewModel privilegios)
        {
            string consulta = @"UPDATE PrivilegiosTrabajadores SET
                                  IsVentaAllow = @IsVentaAllow 
                                  ,IsCambiarPrecioAllow = @IsCambiarPrecioAllow 
                                  ,IsCotizacionAllow = @IsCotizacionAllow 
                                  ,IsCambiarPrecio2Allow = @IsCambiarPrecio2Allow 
                                  ,IsProductosAllow = @IsProductosAllow 
                                  ,IsCrearProductosAllow = @IsCrearProductosAllow 
                                  ,IsEditarProductoAllow = @IsEditarProductoAllow 
                                  ,IsEliminarProductoAllow = @IsEliminarProductoAllow 
                                  ,IsStocksAllow = @IsStocksAllow 
                                  ,IsEditarStockAllow = @IsEditarStockAllow 
                                  ,IsFacturasAllow = @IsFacturasAllow 
                                  ,IsEliminarFacturaAllow = @IsEliminarFacturaAllow 
                                  ,IsPersonalAllow = @IsPersonalAllow 
                                  ,IsPrivilegioPersonalAllow = @IsPrivilegioPersonalAllow 
                                  ,IsCrearPersonalAllow = @IsCrearPersonalAllow 
                                  ,IsEditarPersonalAllow = @IsEditarPersonalAllow 
                                  ,IsEliminarPersonalAllow = @IsEliminarPersonalAllow 
                                  ,IsUtilidadesAllow = @IsUtilidadesAllow 
                                  ,IsClientesAllow = @IsClientesAllow 
                                  ,IsCrearClienteAllow = @IsCrearClienteAllow 
                                  ,IsEditarClienteAllow = @IsEditarClienteAllow 
                                  ,IsEliminarClienteAllow = @IsEliminarClienteAllow 
                                  ,IsCategoriasAllow = @IsCategoriasAllow 
                                  ,IsCrearCategoriaAllow = @IsCrearCategoriaAllow 
                                  ,IsEditarCategoriaAllow = @IsEditarCategoriaAllow 
                                  ,IsEliminarCategoriaAllow = @IsEliminarCategoriaAllow 
                                  ,IsProveedoresAllow = @IsProveedoresAllow 
                                  ,IsCrearProveedorAllow = @IsCrearProveedorAllow 
                                  ,IsEditarProveedorAllow = @IsEditarProveedorAllow 
                                  ,IsEliminarProveedorAllow = @IsEliminarProveedorAllow 
                                  ,IsIngresosAllow = @IsIngresosAllow 
                                  ,IsNuevoIngresoAllow = @IsNuevoIngresoAllow 
                                  ,IsEliminarIngresoAllow = @IsEliminarIngresoAllow 
                                  ,IsCajaAllow = @IsCajaAllow 
                                  ,IsContinuarCajaAllow = @IsContinuarCajaAllow 
                                  ,IsEliminarCajaAllow = @IsEliminarCajaAllow 
                                  ,IsContabilidadAllow = @IsContabilidadAllow 
                                  ,IsPagosAllow = @IsPagosAllow 
                                  ,IsGastosAllow = @IsGastosAllow 
                                  ,IsInfoIngresosAllow = @IsInfoIngresosAllow 
                                  ,IsDepositosInternosAllow = @IsDepositosInternosAllow 
                                  ,IsInyeccionCapitalAllow = @IsInyeccionCapitalAllow 
                             WHERE trabajadorId = @id";
            SqlCommand comando = new(consulta, miconexion);
            comando.Parameters.AddWithValue("@IsVentaAllow", privilegios.Ventas);
            comando.Parameters.AddWithValue("@IsCambiarPrecioAllow", privilegios.CambiarPrecio);
            comando.Parameters.AddWithValue("@IsCotizacionAllow", privilegios.Cotizaciones);
            comando.Parameters.AddWithValue("@IsCambiarPrecio2Allow", privilegios.CambiarPrecio2);
            comando.Parameters.AddWithValue("@IsProductosAllow", privilegios.Productos);
            comando.Parameters.AddWithValue("@IsCrearProductosAllow", privilegios.CrearProducto);
            comando.Parameters.AddWithValue("@IsEditarProductoAllow", privilegios.EditarProducto);
            comando.Parameters.AddWithValue("@IsEliminarProductoAllow", privilegios.EliminarProducto);
            comando.Parameters.AddWithValue("@IsStocksAllow", privilegios.Stocks);
            comando.Parameters.AddWithValue("@IsEditarStockAllow", privilegios.EditarStock);
            comando.Parameters.AddWithValue("@IsFacturasAllow", privilegios.Facturas);
            comando.Parameters.AddWithValue("@IsEliminarFacturaAllow", privilegios.EliminarFactura);
            comando.Parameters.AddWithValue("@IsPersonalAllow", privilegios.Personal);
            comando.Parameters.AddWithValue("@IsPrivilegioPersonalAllow", privilegios.PrivilegiosPersonal);
            comando.Parameters.AddWithValue("@IsCrearPersonalAllow", privilegios.CrearPersonal);
            comando.Parameters.AddWithValue("@IsEditarPersonalAllow", privilegios.EditarPersonal);
            comando.Parameters.AddWithValue("@IsEliminarPersonalAllow", privilegios.EliminarPersonal);
            comando.Parameters.AddWithValue("@IsUtilidadesAllow", privilegios.Utilidades);
            comando.Parameters.AddWithValue("@IsClientesAllow", privilegios.Clientes);
            comando.Parameters.AddWithValue("@IsCrearClienteAllow", privilegios.CrearCliente);
            comando.Parameters.AddWithValue("@IsEditarClienteAllow", privilegios.EditarCliente);
            comando.Parameters.AddWithValue("@IsEliminarClienteAllow", privilegios.EliminarCliente);
            comando.Parameters.AddWithValue("@IsCategoriasAllow", privilegios.Categorias);
            comando.Parameters.AddWithValue("@IsCrearCategoriaAllow", privilegios.CrearCategoria);
            comando.Parameters.AddWithValue("@IsEditarCategoriaAllow", privilegios.EditarCategoria);
            comando.Parameters.AddWithValue("@IsEliminarCategoriaAllow", privilegios.EliminarCategoria);
            comando.Parameters.AddWithValue("@IsProveedoresAllow", privilegios.Proveedores);
            comando.Parameters.AddWithValue("@IsCrearProveedorAllow", privilegios.CrearProveedor);
            comando.Parameters.AddWithValue("@IsEditarProveedorAllow", privilegios.EditarProveedor);
            comando.Parameters.AddWithValue("@IsEliminarProveedorAllow", privilegios.EliminarProveedor);
            comando.Parameters.AddWithValue("@IsIngresosAllow", privilegios.Ingresos);
            comando.Parameters.AddWithValue("@IsNuevoIngresoAllow", privilegios.CrearIngreso);
            comando.Parameters.AddWithValue("@IsEliminarIngresoAllow", privilegios.EliminarIngreso);
            comando.Parameters.AddWithValue("@IsCajaAllow", privilegios.Caja);
            comando.Parameters.AddWithValue("@IsContinuarCajaAllow", privilegios.ContinuarCaja);
            comando.Parameters.AddWithValue("@IsEliminarCajaAllow", privilegios.EliminarCaja);
            comando.Parameters.AddWithValue("@IsContabilidadAllow", privilegios.Contabilidad);
            comando.Parameters.AddWithValue("@IsPagosAllow", privilegios.Pagos);
            comando.Parameters.AddWithValue("@IsGastosAllow", privilegios.Gastos);
            comando.Parameters.AddWithValue("@IsInfoIngresosAllow", privilegios.InformacionIngresos);
            comando.Parameters.AddWithValue("@IsDepositosInternosAllow", privilegios.DepositosInternos);
            comando.Parameters.AddWithValue("@IsInyeccionCapitalAllow", privilegios.InyeccionCapital);
            comando.Parameters.AddWithValue("@id", privilegios.Trabajador.trabajador.Id);

            miconexion.Open();
            comando.ExecuteNonQuery();
            miconexion.Close();
        }
    }
}
