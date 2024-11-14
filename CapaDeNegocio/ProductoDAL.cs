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
    public class ProductoDAL
    {
        clsConexion cnn = new clsConexion();
        SqlCommand cmd = new SqlCommand();

        // Método para obtener todos los productos
        public DataTable GetProductos()
        {
            SqlDataReader read;
            DataTable dt = new DataTable();
            cmd.Parameters.Clear();
            cmd.Connection = cnn.Open();
            cmd.CommandText = "GetProductos";
            cmd.CommandType = CommandType.StoredProcedure;

            read = cmd.ExecuteReader();
            dt.Load(read);
            cnn.Close();
            return dt;
        }
        public DataTable GetProductoInfoById(int idProducto)
        {
            SqlDataReader read;
            DataTable dt = new DataTable();
            cmd.Parameters.Clear();
            cmd.Connection = cnn.Open();
            cmd.CommandText = "GetProductoInfoById";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IDProducto", idProducto);

            read = cmd.ExecuteReader();
            dt.Load(read);
            cnn.Close();
            return dt;
        }

        // Método para insertar un nuevo producto
        public bool InsertProducto(clsProducto producto)
        {
            bool flag;
            try
            {
                cmd.CommandText = "InsertProducto";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@IDGrupo", producto.IDGrupo);
                cmd.Parameters.AddWithValue("@IDMarca", producto.IDMarca);
                cmd.Parameters.AddWithValue("@PrecioCompra", producto.PrecioCompra);
                cmd.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                cmd.Parameters.AddWithValue("@Costo", producto.Costo);
                cmd.Parameters.AddWithValue("@CodigoBarra", producto.CodigoBarra);
                cmd.Parameters.AddWithValue("@Referencia", producto.Referencia);
                cmd.Parameters.AddWithValue("@IDProveedor", producto.IDProveedor);
                cmd.Parameters.AddWithValue("@Existencias", producto.Existencias);

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

        // Método para actualizar un producto existente
        public bool UpdateProducto(clsProducto producto)
        {
            bool flag;
            try
            {
                cmd.CommandText = "UpdateProducto";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDProducto", producto.IDProducto);
                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@IDGrupo", producto.IDGrupo);
                cmd.Parameters.AddWithValue("@IDMarca", producto.IDMarca);
                cmd.Parameters.AddWithValue("@PrecioCompra", producto.PrecioCompra);
                cmd.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                cmd.Parameters.AddWithValue("@Costo", producto.Costo);
                cmd.Parameters.AddWithValue("@CodigoBarra", producto.CodigoBarra);
                cmd.Parameters.AddWithValue("@Referencia", producto.Referencia);
                cmd.Parameters.AddWithValue("@IDProveedor", producto.IDProveedor);
                cmd.Parameters.AddWithValue("@Existencias", producto.Existencias);

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

        // Método para eliminar un producto
        public bool DeleteProducto(int idProducto)
        {
            bool flag;
            try
            {
                cmd.CommandText = "DeleteProducto";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDProducto", idProducto);
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
