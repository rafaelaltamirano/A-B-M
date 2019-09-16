using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data.SqlClient;

namespace Negocio
{
     public class CategoriaNegocio
    {
        public  List<Categoria> listar()
        {
            List<Categoria> lista = new List<Categoria>();
            SqlDataReader lector;
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            try
            {
                Categoria CatAux;
                conexion.ConnectionString = "Data source=desktop-evqhp8a\\sqlexpress; initial catalog=COMERCIO;integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                //la consulta como aparece en sql server
                comando.CommandText = "	select idCategoria,nombre from categoria";
                // busca el lugar de la conexion 
                comando.Connection = conexion;
                //abro coneccion antes de ejecutar la consulta
                conexion.Open();

                lector = comando.ExecuteReader();

                while (lector.Read())
                {

                    CatAux = new Categoria();
                    CatAux.Id = lector.GetInt32(0);
                    CatAux.Nombre = lector.GetString(1);
                    lista.Add(CatAux);
                

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
