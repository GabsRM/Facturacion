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
    public class ConvenioDAL
    {
        clsConexion cnn = new clsConexion();
        SqlCommand cmd = new SqlCommand();

        public DataTable GetConvenio()
        {

            SqlDataReader read;
            DataTable dt = new DataTable();

            cmd.Connection = cnn.Open();
            cmd.CommandText = "GetConvenio";
            cmd.CommandType = CommandType.StoredProcedure;

            read = cmd.ExecuteReader();
            dt.Load(read);
            cnn.Close();
            return dt;
        }
        /*

        public void InsertConvenio(clsConvenio convenio)
        {
            using (SqlConnection conn = cnn.Open())
            {
                SqlCommand cmd = new SqlCommand("InsertConvenio", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Convenio", convenio.ConvenioDescripcion);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateConvenio(clsConvenio convenio)
        {
            using (SqlConnection conn = cnn.Open())
            {
                SqlCommand cmd = new SqlCommand("UpdateConvenio", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDConvenio", convenio.IDConvenio);
                cmd.Parameters.AddWithValue("@Convenio", convenio.ConvenioDescripcion);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteConvenio(int idConvenio)
        {
            using (SqlConnection conn = cnn.Open())
            {
                SqlCommand cmd = new SqlCommand("DeleteConvenio", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDConvenio", idConvenio);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        */
    }
        

}
