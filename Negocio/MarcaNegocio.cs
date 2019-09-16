using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data.SqlClient;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            SqlDataReader lector;
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            try
            {
                Marca MarcaAux;
                conexion.ConnectionString = "Data source=desktop-evqhp8a\\sqlexpress; initial catalog=COMERCIO;integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                //la consulta como aparece en sql server
                comando.CommandText = "	select idMarca,nombre from marca";
                // busca el lugar de la conexion 
                comando.Connection = conexion;
                //abro coneccion antes de ejecutar la consulta
                conexion.Open();

                lector = comando.ExecuteReader();

                while (lector.Read())
                {

                    MarcaAux = new Marca();
                    MarcaAux.Id = lector.GetInt32(0);
                    MarcaAux.Nombre = lector.GetString(1);
                    lista.Add(MarcaAux);


                }
                return lista;
            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
