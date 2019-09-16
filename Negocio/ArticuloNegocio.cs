using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {

        public List<Articulo> listar()
        {
            Articulo aux;
            // creo una lista de articulo para almacenar los datos obtenidos de la base de datos 
            List<Articulo> lista = new List<Articulo>();
            // comando donde voy a cargar la consulta(instancio mi obj comando)
            SqlCommand comando = new SqlCommand();
            // conexion dodne voy a cargar la conexion(instancio mi obj conexion)
            SqlConnection conexion = new SqlConnection();
            // lector donde voy a guardar el resultado de la consulta(instancio obj lector)
            SqlDataReader lector;
            try
            {
                //parametrizo la conexion, le digo a donde me voy a conectar en la base de datos
                conexion.ConnectionString = "Data source=desktop-evqhp8a\\sqlexpress; initial catalog=COMERCIO;integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                //la consulta como aparece en sql server
                comando.CommandText = " Select A.Id,A.Modelo,A.Descripcion,M.Nombre Marca ,C.NOMBRE Categoria,A.Imagen,A.Costo,A.Precio,a.Dolar,a.Iva from articulos as A inner join marca as m on m.idMarca = a.idmarca inner join categoria as c on c.idCategoria = a.idcategoria   ";

                // busca el lugar de la conexion 
                comando.Connection = conexion;
                //abro coneccion antes de ejecutar la consulta
                conexion.Open();
                // devuelve objeto sqlDataReader
                lector = comando.ExecuteReader();

                //transformo el contenido de lector que es idioma sql a objetos manipulables para .net
                while (lector.Read())// mientras lea 
                {
                    //creo un nuevo articulo
                    aux = new Articulo();
                    // a partir de aca le cargo todos los datos
                    aux.Id = lector.GetInt32(0);
                    aux.Modelo = lector.GetString(1);
                    aux.Descripcion = lector.GetString(2);
                    //en cada iteracion instancio un nuevo obj marca
                    aux.Marca = new Marca();               
                    //asigno los datos obtenidos en el lector a la marca del articulo aux
                    aux.Marca.Nombre = lector["Marca"].ToString();
                    //en cada iteracion instancio un nuevo obj categoria
                    aux.Categoria = new Categoria();
                    //asigno los datos obtenidos en el lector a la categoria del articulo aux
                    aux.Categoria.Nombre = lector["Categoria"].ToString();
                    // convierto el contenido de imagen a string
                    aux.Imagen = lector["Imagen"].ToString();
                    //muestro el y convierto resto
                    aux.Costo = lector.GetDouble(6);
                    aux.Precio = lector.GetDouble(7);
                    aux.Dolar = (bool)lector["Dolar"];
                    aux.Iva = lector.GetDouble(9);
                    //termina de cargar y lo voy guardando en lista a dichos objetos
                    lista.Add(aux);
                }

                return lista;// cuando termina el while devuelvo la lista 
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }

        public void agregar(Articulo articulo)
        {
            SqlCommand cm = new SqlCommand();
            SqlConnection cn = new SqlConnection();
            try
            {
                //parametrizo la conexion, le digo a donde me voy a conectar en la base de datos
                cn.ConnectionString = "Data source=desktop-evqhp8a\\sqlexpress; initial catalog=COMERCIO;integrated security=sspi";
                cm.CommandType = System.Data.CommandType.Text;
                //la consulta como aparece en sql server
                cm.CommandText = "insert into Articulos values (@Modelo,@Descripcion,@idMarca,@idCategoria,@Url,@Costo,@Precio,@Dolar,@Iva,@Estado)";
                // limpiamos los parametros 
                cm.Parameters.Clear();
                // agrego un valor a la variable 
                cm.Parameters.AddWithValue("@Modelo", articulo.Modelo);
                cm.Parameters.AddWithValue("@Descripcion", articulo.Descripcion);
                cm.Parameters.AddWithValue("@idMarca", articulo.Marca.Id);
                cm.Parameters.AddWithValue("@idCategoria", articulo.Categoria.Id);
                cm.Parameters.AddWithValue("@Url", articulo.Imagen);
                cm.Parameters.AddWithValue("@Costo", articulo.Costo);
                cm.Parameters.AddWithValue("@Precio", articulo.Precio);
                cm.Parameters.AddWithValue("@Dolar", articulo.Dolar);
                cm.Parameters.AddWithValue("@Iva", articulo.Iva);
                cm.Parameters.AddWithValue("@Estado", articulo.Estado);
                // busco la conexion
                cm.Connection = cn;
                //Abro la conexion
               
                cn.Open();
                cm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
            }
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearQuery("update articulos set estado=0 where id =" + id);
                datos.ejecutarAccion();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public void modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearQuery("update articulos set modelo=@Modelo,descripcion=@Descripcion,idMarca=@idMarca,idCategoria=@idCategoria,imagen=@Imagen,Costo=@Costo,Precio=@Precio,Iva=@Iva,Dolar=@Dolar where id=@Id");
                datos.agregarParametro("@Id", articulo.Id);
                datos.agregarParametro("@Modelo", articulo.Modelo);
                datos.agregarParametro("@Descripcion", articulo.Descripcion);
                datos.agregarParametro("@idMarca", articulo.Marca.Id);
                datos.agregarParametro("@idCategoria", articulo.Categoria.Id);
                datos.agregarParametro("@Imagen", articulo.Imagen);
                datos.agregarParametro("@Costo", articulo.Costo);
                datos.agregarParametro("@Precio", articulo.Precio);
                datos.agregarParametro("@Dolar", articulo.Dolar);
                datos.agregarParametro("@Iva", articulo.Iva);

                datos.ejecutarAccion();
            }

            catch(Exception ex)
            {

                throw ex;
            }
       
        }
    }
}
