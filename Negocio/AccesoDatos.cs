using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Negocio
{
    public class AccesoDatos
    {
        public SqlDataReader lector { get; set; }
        public SqlConnection cn { get; set; }
        public SqlCommand cm { get; set; }

        public AccesoDatos()
        {
            cn = new SqlConnection("Data source=desktop-evqhp8a\\sqlexpress; initial catalog=COMERCIO;integrated security=sspi");
            cm = new SqlCommand();
            cm.Connection = cn;
        }


        //metodo para setear la consulta
        public void setearQuery(string consulta)
        {
            cm.CommandType = System.Data.CommandType.Text;
            // cargo la consulta enviada por parametro
            cm.CommandText = consulta;  
        }

        public void agregarParametro(string nombre, object valor)
        {
            cm.Parameters.AddWithValue(nombre, valor);
        }

        public void ejecutarAccion()
        {

            try
            {
                //abro coneccion
                cn.Open();
                cm.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
            }
        }
    }
}
