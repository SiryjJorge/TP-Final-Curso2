using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()

        {
            List<Articulo> lista = new List<Articulo>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=CATALOGO_DB; integrated security=true"; 
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select Codigo, Nombre, A.Descripcion, ImagenUrl, Precio, M.Descripcion Marca, C.Descripcion Categoria, C.Id IdCategoria, M.Id IdMarca, A.Id from ARTICULOS A, MARCAS M, CATEGORIAS C where A.IdMarca = M.Id and A.IdCategoria = C.Id";
                comando.Connection = conexion;
                
                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read()) 
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)lector["Id"];
                    if(!(lector["Codigo"] is DBNull))
                        aux.Codigo = (string)lector["Codigo"];                  
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Precio = (decimal)lector["Precio"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    if (!(lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)lector["ImagenUrl"];                                     
                    aux.Marca = new MarcaDelProducto();
                    aux.Marca.Id = (int)lector["IdMarca"];
                    aux.Marca.Descripcion = (string)lector["Marca"];                  
                    aux.Categoria = new CategoriaDelProducto();
                    aux.Categoria.Descripcion = (string)lector["Categoria"];
                    aux.Categoria.Id = (int)lector["IdCategoria"];

                    lista.Add(aux);
                }

                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    
        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                datos.setConsulta("insert into ARTICULOS (Nombre, Descripcion, Precio, ImagenUrl, IdMarca, IdCategoria) values (@nombre, @descripcion, @precio, @ImagenUrl, @idMarca, @idCategoria)");
                datos.setParametro("@nombre", nuevo.Nombre);
                datos.setParametro("@descripcion", nuevo.Descripcion);
                datos.setParametro("@precio", nuevo.Precio);
                datos.setParametro("@ImagenUrl", nuevo.ImagenUrl);
                datos.setParametro("@idMarca", nuevo.Marca.Id);
                datos.setParametro("@idCategoria", nuevo.Categoria.Id);
                
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(Articulo modificado) 
        {
           
            AccesoDatos datos = new AccesoDatos();


            try
            {
                datos.setConsulta("update ARTICULOS set Nombre = @nombre, Descripcion = @desc, ImagenUrl = @img, Precio = @precio, IdMarca = @IdMarca, IdCategoria = @IdCategoria where Id = @Id");
                datos.setParametro("@nombre", modificado.Nombre);
                datos.setParametro("@desc", modificado.Descripcion);
                datos.setParametro("@img", modificado.ImagenUrl);
                datos.setParametro("@precio", modificado.Precio);
                datos.setParametro("@IdMarca", modificado.Marca.Id);
                datos.setParametro("@IdCategoria", modificado.Categoria.Id);
                datos.setParametro("@Id", modificado.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }
        
        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setConsulta("delete from ARTICULOS where id = @id");
                datos.setParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
           
            try
            {
                string consulta = "select Codigo, Nombre, A.Descripcion, ImagenUrl, Precio, M.Descripcion Marca, C.Descripcion Categoria, C.Id IdCategoria, M.Id IdMarca, A.Id from ARTICULOS A, MARCAS M, CATEGORIAS C where A.IdMarca = M.Id and A.IdCategoria = C.Id And ";

                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Precio > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "Precio < " + filtro;
                            break;
                        default:
                            consulta += "Precio = " + filtro;
                            break;
                    }
                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Nombre like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "Nombre like '% " + filtro + "'";
                            break;
                        default:
                            consulta += "Nombre like '%"+ filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "A.Descripcion like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "A.Descripcion like '% " + filtro + "'";
                            break;
                        default:
                            consulta += "A.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }

                datos.setConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["Id"];
                    if (!(datos.Lector["Codigo"] is DBNull))
                        aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Marca = new MarcaDelProducto();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new CategoriaDelProducto();
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];

                    lista.Add(aux);
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