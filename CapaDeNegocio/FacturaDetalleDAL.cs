using CapaDeDatos;
using CapaDeEntidad;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocio
{
    public class FacturaDetalleDAL
    {
        clsConexion cnn = new clsConexion();
        SqlCommand cmd = new SqlCommand();

        // Método para obtener todos los detalles de factura
        public DataTable GetFacturaDetalles()
        {
            SqlDataReader read;
            DataTable dt = new DataTable();
            cmd.Parameters.Clear();
            cmd.Connection = cnn.Open();
            cmd.CommandText = "GetFacturaDetalles";
            cmd.CommandType = CommandType.StoredProcedure;

            read = cmd.ExecuteReader();
            dt.Load(read);
            cnn.Close();
            return dt;
        }

        // Método para obtener detalles de factura por ID de factura
        public DataTable GetFacturaDetallesByFacturaID(int idFacturaEnc)
        {
            SqlDataReader read;
            DataTable dt = new DataTable();
            cmd.Parameters.Clear();
            cmd.Connection = cnn.Open();
            cmd.CommandText = "GetFacturaDetallesByFacturaID";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IDFacEnc", idFacturaEnc);

            read = cmd.ExecuteReader();
            dt.Load(read);
            cnn.Close();
            return dt;
        }

        // Método para insertar un nuevo detalle de factura
        public bool InsertFacturaDetalle(clsFacturaDetalle detalle)
        {
            bool flag;
            try
            {
                cmd.CommandText = "InsertFacturaDetalle";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDFacEnc", detalle.IDFacEnc);
                cmd.Parameters.AddWithValue("@IDProducto", detalle.IDProducto);
                cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                cmd.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                cmd.Parameters.AddWithValue("@Subtotal", detalle.Subtotal);
                cmd.Parameters.AddWithValue("@Descuento", detalle.Descuento);
                cmd.Parameters.AddWithValue("@IVA", detalle.IVA);
                cmd.Parameters.AddWithValue("@Total", detalle.Total);
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

        // Método para actualizar un detalle de factura existente
        public bool UpdateFacturaDetalle(clsFacturaDetalle detalle)
        {
            bool flag;
            try
            {
                cmd.CommandText = "UpdateFacturaDetalle";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDFacDet", detalle.IDFacDet);
                cmd.Parameters.AddWithValue("@IDFacEnc", detalle.IDFacEnc);
                cmd.Parameters.AddWithValue("@IDProducto", detalle.IDProducto);
                cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                cmd.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                cmd.Parameters.AddWithValue("@Subtotal", detalle.Subtotal);
                cmd.Parameters.AddWithValue("@Descuento", detalle.Descuento);
                cmd.Parameters.AddWithValue("@IVA", detalle.IVA);
                cmd.Parameters.AddWithValue("@Total", detalle.Total);
                cmd.Parameters.AddWithValue("@CostoPromedio", detalle.CostoPromedio);
                cmd.Parameters.AddWithValue("@PrecioCompra", detalle.PrecioCompra);
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

        // Método para eliminar un detalle de factura
        public bool DeleteFacturaDetalle(int idFacDet)
        {
            bool flag;
            try
            {
                cmd.CommandText = "DeleteFacturaDetalle";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDFacDet", idFacDet);
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
