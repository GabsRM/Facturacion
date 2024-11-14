using DevExpress.Xpo;
using System;
using System.Data.SqlClient;

namespace CapaDeDatos
{
    public class clsConexion : XPObject
    {
        private SqlConnection Conexion = new SqlConnection("Server=.;DataBase=FacturacionBasico;Integrated Security=true;TrustServerCertificate=true");

        public SqlConnection Open()
        {
            if (Conexion.State == System.Data.ConnectionState.Closed)
                Conexion.Open();
            return Conexion;
        }


       
            

        public SqlConnection Close()
        {
            if (Conexion.State == System.Data.ConnectionState.Open)
                Conexion.Close();
            return Conexion;
        }
    }

}