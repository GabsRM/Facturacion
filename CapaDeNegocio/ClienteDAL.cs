using CapaDeDatos;
using CapaDeEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class ClienteDAL
{
    clsConexion cnn = new clsConexion();
    SqlCommand cmd = new SqlCommand();


    public DataTable GetClientes()
    {

        
        SqlDataReader read;
        DataTable dt = new DataTable();
        cmd.Parameters.Clear();
        cmd.Connection = cnn.Open();
        cmd.CommandText = "GetClientes";
        cmd.CommandType = CommandType.StoredProcedure;
            
        read = cmd.ExecuteReader();
        dt.Load(read);
        cnn.Close();
        return dt;
    }

    public bool InsertCliente(clsCliente cliente)
    {
        bool flag;
        try
        {

            cmd.CommandText = "InsertCliente";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
            cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
            cmd.Parameters.AddWithValue("@IDConvenio", cliente.IDConvenio);
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

    public bool UpdateCliente(clsCliente cliente)
    {
        bool flag;
        try
        {
            cmd.CommandText = "UpdateCliente";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IDCliente", cliente.IDCliente);
            cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
            cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
            cmd.Parameters.AddWithValue("@IDConvenio", cliente.IDConvenio);
            
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

    public bool DeleteCliente(int idCliente)
    {
        bool flag;
        try
        {
            SqlConnection conn = cnn.Open();
            cmd.CommandText = "DeleteCliente";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IDCliente", idCliente);
            cmd.Connection = cnn.Open();
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            flag = true;
        }
        catch (Exception)
        {

            throw;
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
