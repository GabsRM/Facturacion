using CapaDeDatos;
using CapaDeEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocio
{
    public class ProveedorDAL
    {
        clsConexion cnn = new clsConexion();
        SqlCommand cmd = new SqlCommand();

        // Método para obtener todos los proveedores
        public DataTable GetProveedores()
        {
            SqlDataReader read;
            DataTable dt = new DataTable();
            cmd.Parameters.Clear();
            cmd.Connection = cnn.Open();
            cmd.CommandText = "GetProveedores";
            cmd.CommandType = CommandType.StoredProcedure;

            read = cmd.ExecuteReader();
            dt.Load(read);
            cnn.Close();
            return dt;
        }

        // Método para insertar un nuevo proveedor
        public bool InsertProveedor(clsProveedor proveedor)
        {
            bool flag;
            try
            {
                cmd.CommandText = "InsertProveedor";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", proveedor.Nombre);
                cmd.Parameters.AddWithValue("@Direccion", proveedor.Direccion);
                cmd.Parameters.AddWithValue("@Telefono", proveedor.Telefono);
                cmd.Connection = cnn.Open();

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd.Connection != null && cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return flag;
        }

        // Método para actualizar un proveedor existente
        public bool UpdateProveedor(clsProveedor proveedor)
        {
            bool flag;
            try
            {
                cmd.CommandText = "UpdateProveedor";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDProveedor", proveedor.IDProveedor);
                cmd.Parameters.AddWithValue("@Nombre", proveedor.Nombre);
                cmd.Parameters.AddWithValue("@Direccion", proveedor.Direccion);
                cmd.Parameters.AddWithValue("@Telefono", proveedor.Telefono);
                cmd.Connection = cnn.Open();

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd.Connection != null && cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return flag;
        }

        // Método para eliminar un proveedor
        public bool DeleteProveedor(int idProveedor)
        {
            bool flag;
            try
            {
                cmd.CommandText = "DeleteProveedor";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDProveedor", idProveedor);
                cmd.Connection = cnn.Open();

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd.Connection != null && cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return flag;
        }
    }

}
