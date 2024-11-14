﻿using CapaDeDatos;
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
    public class DescuentoDAL
    {
        clsConexion cnn = new clsConexion();
        SqlCommand cmd = new SqlCommand();

        // Método para obtener todos los descuentos
        public DataTable GetDescuento()
        {
            SqlDataReader read;
            DataTable dt = new DataTable();
            cmd.Parameters.Clear();
            cmd.Connection = cnn.Open();
            cmd.CommandText = "GetDescuento";
            cmd.CommandType = CommandType.StoredProcedure;

            read = cmd.ExecuteReader();
            dt.Load(read);
            cnn.Close();
            return dt;
        }

        // Método para insertar un nuevo descuento
        public bool InsertDescuento(int descuento)
        {
            bool flag;
            try
            {
                cmd.CommandText = "InsertDescuento";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Descuento", descuento);
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

        // Método para actualizar un descuento existente
        public bool UpdateDescuento(int idDescuento, int descuento)
        {
            bool flag;
            try
            {
                cmd.CommandText = "UpdateDescuento";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDDescuento", idDescuento);
                cmd.Parameters.AddWithValue("@Descuento", descuento);
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

        // Método para eliminar un descuento
        public bool DeleteDescuento(int idDescuento)
        {
            bool flag;
            try
            {
                cmd.CommandText = "DeleteDescuento";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDDescuento", idDescuento);
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
