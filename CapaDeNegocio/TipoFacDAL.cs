using CapaDeDatos;
using CapaDeEntidad;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocio
{
    public class TipoFacDAL
    {
        clsConexion cnn = new clsConexion();
        SqlCommand cmd = new SqlCommand();

        // Método para obtener todos los tipos de factura
        public DataTable GetTiposFactura()
        {
            SqlDataReader read;
            DataTable dt = new DataTable();
            cmd.Parameters.Clear();
            cmd.Connection = cnn.Open();
            cmd.CommandText = "GetTipoFac";
            cmd.CommandType = CommandType.StoredProcedure;

            read = cmd.ExecuteReader();
            dt.Load(read);
            cnn.Close();
            return dt;
        }

        // Método para insertar un nuevo tipo de factura
        public bool InsertTipoFac(string tipoFactura)
        {
            bool flag;
            try
            {
                cmd.CommandText = "InsertTipoFac";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TipoFactura", tipoFactura);
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

        // Método para actualizar un tipo de factura existente
        public bool UpdateTipoFac(int idTipoFac, string tipoFactura)
        {
            bool flag;
            try
            {
                cmd.CommandText = "UpdateTipoFac";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDTipoFac", idTipoFac);
                cmd.Parameters.AddWithValue("@TipoFactura", tipoFactura);
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

        // Método para eliminar un tipo de factura
        public bool DeleteTipoFac(int idTipoFac)
        {
            bool flag;
            try
            {
                cmd.CommandText = "DeleteTipoFac";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDTipoFac", idTipoFac);
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
