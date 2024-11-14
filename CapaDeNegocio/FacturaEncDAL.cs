using CapaDeDatos;
using CapaDeEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FacturaEncDAL
{
    clsConexion cnn = new clsConexion();
    SqlCommand cmd = new SqlCommand();

    // Método para obtener todas las facturas de encabezado
    public DataTable GetFacturaEnc()
    {
        SqlDataReader read;
        DataTable dt = new DataTable();
        cmd.Parameters.Clear();
        cmd.Connection = cnn.Open();
        cmd.CommandText = "GetFacturaEnc";
        cmd.CommandType = CommandType.StoredProcedure;

        read = cmd.ExecuteReader();
        dt.Load(read);
        cnn.Close();
        return dt;
    }
    public int GetLastFacturaEncID()
    {
        int lastID = 0;
        try
        {
            cmd.Parameters.Clear();
            cmd.Connection = cnn.Open();
            cmd.CommandText = "GetLastFacturaEncID";
            cmd.CommandType = CommandType.StoredProcedure;

            // Ejecutar el comando y obtener el resultado
            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                lastID = Convert.ToInt32(result);
            }
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
        return lastID + 1;
    }
    public int ObtenerUltimaFacturaPorCliente(int idCliente)
    {
        int IDfacturaEnc = 0;

        try
        {
            if (cmd == null)
                cmd = new SqlCommand();

            cmd.Connection = cnn.Open();
            cmd.CommandText = "ObtenerUltimaFacturaPorCliente";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@id_cliente", SqlDbType.Int)).Value = idCliente;

            SqlParameter outputIdFactura = new SqlParameter("@id_factura", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputIdFactura);

            cmd.ExecuteNonQuery();

            IDfacturaEnc = (int)outputIdFactura.Value;
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

        return IDfacturaEnc;
    }

    // Método para insertar una nueva factura de encabezado
    public bool InsertFacturaEnc(clsFacturaEnc factura)
    {
        bool flag;
        try
        {
            cmd.CommandText = "InsertFacturaEnc";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Fecha", factura.Fecha);
            cmd.Parameters.AddWithValue("@IDCliente", factura.IDCliente);
            cmd.Parameters.AddWithValue("@Subtotal", factura.Subtotal);
            cmd.Parameters.AddWithValue("@IDDescuento", factura.IDDescuento);
            cmd.Parameters.AddWithValue("@IVA", factura.IVA);
            cmd.Parameters.AddWithValue("@Total", factura.Total);
            cmd.Parameters.AddWithValue("@IDEstado", factura.IDEstado);
            cmd.Parameters.AddWithValue("@IDSerie", factura.IDSerie);
            cmd.Parameters.AddWithValue("@IDMoneda", factura.IDMoneda);
            cmd.Parameters.AddWithValue("@IDTipoFac", factura.IDTipoFac);
            cmd.Parameters.AddWithValue("@NoFactura", factura.NoFactura);
            cmd.Parameters.AddWithValue("@Observaciones", factura.Observaciones);
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

    // Método para actualizar una factura de encabezado existente
    public bool UpdateFacturaEnc(clsFacturaEnc factura)
    {
        bool flag;
        try
        {
            cmd.CommandText = "UpdateFacturaEnc";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IDFacEnc", factura.IDFacEnc);
            cmd.Parameters.AddWithValue("@Fecha", factura.Fecha);
            cmd.Parameters.AddWithValue("@IDCliente", factura.IDCliente);
            cmd.Parameters.AddWithValue("@Subtotal", factura.Subtotal);
            cmd.Parameters.AddWithValue("@Descuento", factura.IDDescuento);
            cmd.Parameters.AddWithValue("@IVA", factura.IVA);
            cmd.Parameters.AddWithValue("@Total", factura.Total);
            cmd.Parameters.AddWithValue("@IDEstado", factura.IDEstado);
            cmd.Parameters.AddWithValue("@IDSerie", factura.IDSerie);
            cmd.Parameters.AddWithValue("@IDMoneda", factura.IDMoneda);
            cmd.Parameters.AddWithValue("@IDTipoFac", factura.IDTipoFac);
            cmd.Parameters.AddWithValue("@NoFactura", factura.NoFactura);
            cmd.Parameters.AddWithValue("@Observaciones", factura.Observaciones);
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

    // Método para eliminar una factura de encabezado
    public bool DeleteFacturaEnc(int idFacEnc)
    {
        bool flag;
        try
        {
            cmd.CommandText = "DeleteFacturaEnc";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IDFacEnc", idFacEnc);
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

