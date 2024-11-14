using CapaDeDatos;
using CapaDeEntidad;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDeNegocio
{
    public class clsNegocio : XPObject
    {/*
        clsConexion cnn = new clsConexion();


        SqlDataReader lectura;
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();

        public DataTable Listar(string storeProcedure)
        {
            cmd.Connection = cnn.Abrir();
            cmd.CommandText = storeProcedure;
            cmd.CommandType = CommandType.StoredProcedure;
            lectura = cmd.ExecuteReader();
            dt.Load(lectura);
            cnn.Cerrar();
            return dt;
        }

        public List<int> GetId(string storeProcedure)
        {
            List<int> listRange = new List<int>();
            try
            {
      
                cmd.Connection = cnn.Abrir(); 
                cmd.CommandText = storeProcedure;
                cmd.CommandType = CommandType.StoredProcedure;

             
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                      
                        listRange.Add(reader.GetInt32(0)); 
                    }
                }
            }
            catch (Exception ex)
            {
           
                Console.WriteLine("Error al obtener los ID: " + ex.Message);
            }
            finally
            {

                if (cmd.Connection != null && cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }

            return listRange;
        }

        public List<clsCliente> ObtenerClientes(string storeProcedure)
        {
            List<clsCliente> listRange = new List<clsCliente>();

            try
            {
  
                if (cmd == null)
                    cmd = new SqlCommand();

                cmd.Connection = cnn.Abrir();
                cmd.CommandText = storeProcedure;
                cmd.CommandType = CommandType.StoredProcedure;

      
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
          
                    while (reader.Read())
                    {
                   
                        clsCliente cliente = new clsCliente
                        {
                            IdCliente = reader.GetInt32(0),  
                            Nombre = reader.IsDBNull(1) ? null : reader.GetString(1)  
                        };

                        listRange.Add(cliente);
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error al obtener los clientes: " + ex.Message);
            }
            finally
            {
               
                if (cmd.Connection != null && cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }

            return listRange;
        }
        public int ObtenerUltimaFacturaPorCliente(int idCliente)
        {
            clsFacturaEnc factura = new clsFacturaEnc();

            try
            {
                if (cmd == null)
                    cmd = new SqlCommand();

                cmd.Connection = cnn.Abrir();
                cmd.CommandText = "ObtenerUltimaFacturaPorCliente";
                cmd.CommandType = CommandType.StoredProcedure;

                // Parámetro de entrada para el id_cliente
                cmd.Parameters.Add(new SqlParameter("@id_cliente", SqlDbType.Int)).Value = idCliente;

                // Parámetro de salida para el id_factura
                SqlParameter outputIdFactura = new SqlParameter("@id_factura", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdFactura);

                // Ejecutar el procedimiento almacenado
                cmd.ExecuteNonQuery();

                // Asignar el valor del parámetro de salida a la propiedad IdFactura de `factura`
                factura.IdFactura = (int)outputIdFactura.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la última factura: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión
                if (cmd.Connection != null && cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }

            return factura.IdFactura;
        }

        public List<clsProducto> ObtenerProductos(string storeProcedure)
        {
            List<clsProducto> listRange = new List<clsProducto>();

            try
            {
             
                if (cmd == null)
                    cmd = new SqlCommand();

           
                cmd.Connection = cnn.Abrir();
                cmd.CommandText = storeProcedure;
                cmd.CommandType = CommandType.StoredProcedure;

          
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {

                        clsProducto cliente = new clsProducto
                        {
                            IdProducto = reader.GetInt32(0),
                            NombreProducto = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Precio = reader.GetDecimal(2)
                        };

                        listRange.Add(cliente);
                    }
                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Error al obtener los clientes: " + ex.Message);
            }
            finally
            {
                
                if (cmd.Connection != null && cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }

            return listRange;
        }

        public List<clsProveedor> ObtenerProveedores(string storeProcedure)
        {
            List<clsProveedor> listRange = new List<clsProveedor>();

            try
            {

                if (cmd == null)
                    cmd = new SqlCommand();


                cmd.Connection = cnn.Abrir();
                cmd.CommandText = storeProcedure;
                cmd.CommandType = CommandType.StoredProcedure;


                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {

                        clsProveedor cliente = new clsProveedor
                        {
                            IdProveedor = reader.GetInt32(0),
                            NombreProveedor = reader.IsDBNull(1) ? null : reader.GetString(1)
                        };

                        listRange.Add(cliente);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error al obtener los clientes: " + ex.Message);
            }
            finally
            {

                if (cmd.Connection != null && cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }

            return listRange;
        }

        public DataTable LeerFacturaProductos(int idProducto)
        {
            DataTable dtProducto = new DataTable();
            cnn.Abrir();
            cmd.CommandText = "LeerFacturaProducto";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IdProducto", SqlDbType.Int)).Value = idProducto;
            cmd.Connection = cnn.Abrir();

            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(dtProducto);
            }       

            return dtProducto;
        }
 

        public bool InsertarClientes(clsCliente cliente)
        {
            bool valor;
            try
            {

                cmd.Connection = cnn.Abrir();
                cmd.CommandText = "InsertarClientes";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@TipoCliente", cliente.TipoCliente);
                cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cnn.Cerrar();
                valor = true;
            }
            catch (SqlException ex)
            {

                throw ex;
            }

            return valor;

        }
        public bool InsertarProveedor(clsProveedor proveedor)
        {
            bool valor;
            try
            {

                cmd.Connection = cnn.Abrir();
                cmd.CommandText = "InsertarProveedor";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreProveedor ", proveedor.NombreProveedor);
                cmd.Parameters.AddWithValue("@Direccion", proveedor.Direccion);
                cmd.Parameters.AddWithValue("@Telefono", proveedor.Telefono);
                cmd.Parameters.AddWithValue("@Email", proveedor.Email);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cnn.Cerrar();
                valor = true;
            }
            catch (SqlException ex)
            {

                throw ex;
            }

            return valor;

        }

        public bool InsertarProducto(clsProducto producto)
        {
            bool valor;
            try
            {

                cmd.Connection = cnn.Abrir();
                cmd.CommandText = "InsertarProducto";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreProducto ", producto.NombreProducto);
                cmd.Parameters.AddWithValue("@ClaseProducto", producto.ClaseProducto);
                cmd.Parameters.AddWithValue("@GrupoProducto", producto.GrupoProducto);
                cmd.Parameters.AddWithValue("@IdProveedor", producto.IdProveedor);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@Cantidad", producto.Cantidad);


                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cnn.Cerrar();
                valor = true;
            }
            catch (SqlException ex)
            {

                throw ex;
            }

            return valor;

        }
        public bool ActualizarClientes(clsCliente cliente)
        {
            bool valor;
            try
            {

                cmd.Connection = cnn.Abrir();
                cmd.CommandText = "ActualizarCliente";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCliente",cliente.IdCliente);
                cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@TipoCliente", cliente.TipoCliente);
                cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cnn.Cerrar();
                valor = true;
            }
            catch (SqlException ex)
            {

                throw ex;
            }

            return valor;

        }
        public bool ActualizarProveedor(clsProveedor proveedor)
        {
            bool valor;
            try
            {
                cmd.Connection = cnn.Abrir();
                cmd.CommandText = "ActualizarProveedor";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProveedor", proveedor.IdProveedor);
                cmd.Parameters.AddWithValue("@NombreProveedor", proveedor.NombreProveedor);
                cmd.Parameters.AddWithValue("@Direccion", proveedor.Direccion);
                cmd.Parameters.AddWithValue("@telefono", proveedor.Telefono);
                cmd.Parameters.AddWithValue("@Email", proveedor.Email);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cnn.Cerrar();
                valor = true;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            return valor;
        }

        public bool ActualizarProducto(clsProducto producto)
        {
            bool valor;
            try
            {
                cmd.Connection = cnn.Abrir();
                cmd.CommandText = "ActualizarProducto";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProducto ", producto.IdProducto);
                cmd.Parameters.AddWithValue("@NombreProducto ", producto.NombreProducto);
                cmd.Parameters.AddWithValue("@ClaseProducto", producto.ClaseProducto);
                cmd.Parameters.AddWithValue("@GrupoProducto", producto.GrupoProducto);
                cmd.Parameters.AddWithValue("@IdProveedor", producto.IdProveedor);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@Cantidad", producto.Cantidad);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cnn.Cerrar();
                valor = true;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            return valor;

        }

        public bool InsertarFactura(clsFacturaEnc factura)
        {
            bool valor;
            try
            {

                cmd.Connection = cnn.Abrir();
                cmd.CommandText = "InsertarFactura";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCliente", factura.IdCliente);
                cmd.Parameters.AddWithValue("@Fecha", factura.Fecha);
                cmd.Parameters.AddWithValue("@Total", factura.Total);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cnn.Cerrar();
                valor = true;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            return valor;
        }

        public bool InsertarDetalleFactura(clsDetalleFactura detalleFactura)
        {
            bool valor = false;

            try
            {
                cmd.Connection = cnn.Abrir();
                cmd.CommandText = "InsertarDetalleFactura";
                cmd.CommandType = CommandType.StoredProcedure;

                // Limpiar parámetros previos, si existen
                cmd.Parameters.Clear();

                // Agregar los parámetros esperados por el procedimiento almacenado
                cmd.Parameters.AddWithValue("@IdFactura", detalleFactura.IdFactura);
                cmd.Parameters.AddWithValue("@IdProducto", detalleFactura.IdProducto);
                cmd.Parameters.AddWithValue("@Cantidad", detalleFactura.Cantidad);
                cmd.Parameters.AddWithValue("@PrecioUnitario", detalleFactura.PrecioUnitario);

                // Ejecutar el procedimiento almacenado
                cmd.ExecuteNonQuery();

                valor = true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error al insertar detalle de factura: " + ex.Message);
                throw;
            }
            finally
            {
                // Cerrar la conexión después de cada ejecución
                cnn.Cerrar();
            }

            return valor;
        }


        public bool Eliminar(string IdProcedure,string commandText, int Id)
        {
            bool valor;
            try
            {
                cmd.Connection = cnn.Abrir();
                cmd.CommandText = commandText;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(IdProcedure, Id);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cnn.Cerrar();
                valor = true;
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return valor;
        }

        public bool EliminarProveedor(int Id)
        {
            bool valor;
            try
            {
                cmd.Connection = cnn.Abrir();
                cmd.CommandText = "EliminarProveedor";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProveedor", Id);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cnn.Cerrar();
                valor = true;
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return valor;
        }

        public bool EliminarCliente(int Id)
        {
            bool valor;
            try
            {
                cmd.Connection = cnn.Abrir();
                cmd.CommandText = "EliminarProducto";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@@IdProducto", Id);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cnn.Cerrar();
                valor = true;
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return valor;
        }*/
    }

        
}

