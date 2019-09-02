using Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BaseDeDatos
{
    public class OrganizarProducto
    {
        /// <summary>
        /// Registra un usuario con su nombre de usuario y su contraseña
        /// </summary>
        /// <param name="usuario">usario</param>
        /// <param name="contraseña">contraseña</param>
        public void RegistrarProducto(Producto producto)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                ClaseConexion.Conectar();
                OrganizarCatalogo organizarCatalogo = new OrganizarCatalogo();
                comando.Connection = ClaseConexion.Conexion;
                SqlTransaction transaccion = comando.Connection.BeginTransaction();
                comando.Transaction = transaccion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO Producto (nombre, tipoProducto, imagen, precio) VALUES(@Nombre, @TipoProductp @Imagen, @Precio); ";
                comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
                comando.Parameters.AddWithValue("@TipoProducto", producto.TipoDeProducto);
                comando.Parameters.AddWithValue("@Imagen", producto.Imagen);
                comando.Parameters.AddWithValue("@Precio", producto.Precio);
                comando.ExecuteNonQuery();
                comando.Transaction.Commit();
                comando.Connection.Close();
            }
            catch (SqlException ex)
            {
                comando.Transaction.Commit();
                ClaseConexion.Conexion.Close();
                throw new Exception();
            }
        }
        
        /// <summary>
        /// Busca el producto 
        /// </summary>
        /// <returns>retorna el usuario</returns>
        public void BuscarProducto(ref Producto producto)
        {
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                SqlCommand comando = new SqlCommand();
                ClaseConexion.Conectar();
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandType = CommandType.Text;
                StringBuilder sQL = new StringBuilder();
                sQL.Append("SELECT idProducto, nombre, tipoProducto, imagen, precio FROM Producto ");
                sQL.Append("WHERE nombre LIKE @Nombre ");
                comando.CommandText = sQL.ToString();
                comando.Parameters.AddWithValue("@Nombre", "%" + producto.Nombre + "%");
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds, "Producto");
                if (ds.Tables["Producto"].Rows.Count != 0)
                {
                    producto.IdProducto = Convert.ToInt32(ds.Tables["Producto"].Rows[0]["idProducto"].ToString());
                    producto.Nombre = ds.Tables["Producto"].Rows[0]["nombre"].ToString();
                    producto.TipoDeProducto = new TipoDeProducto();
                    producto.TipoDeProducto.IdTipoDeProducto = Convert.ToInt32(ds.Tables["Producto"].Rows[0]["tipoProducto"].ToString());
                    producto.Precio = float.Parse(ds.Tables["Producto"].Rows[0]["precio"].ToString());
                    comando.Connection.Close();
                }
                else
                {
                    comando.Connection.Close();
                }
            }
            catch (SqlException ex)
            {
                ClaseConexion.Conexion.Close();
            }
        }

        /// <summary>
        /// Metodo para buscar losproductos de un catalogo
        /// </summary>
        /// <param name="catalogo">catalogo</param>
        /// <returns>tabla con los  productos</returns>
        public DataTable BuscarProductosPorCatalogo(Catalogo catalogo)
        {
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                SqlCommand comando = new SqlCommand();
                ClaseConexion.Conectar();
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT Producto.idProducto as Codigo, Producto.nombre from Producto, ProductosXCatalogo where Producto.idProducto = ProductosXCatalogo.idProducto AND ProductosXCatalogo.idCatalogo = @IdCatalogo";
                comando.Parameters.AddWithValue("@IdCatalogo", catalogo.IdCatalogo);
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds, "Productos");
                if (ds.Tables["Productos"].Rows.Count != 0)
                {
                    DataTable tabla = ds.Tables["Productos"];
                    return tabla;
                }
                else
                {
                    comando.Connection.Close();
                    return new DataTable();
                }
            }
            catch (SqlException ex)
            {
                ClaseConexion.Conexion.Close();
                return new DataTable();
            }
        }

        /// <summary>
        /// Busca los usuarios
        /// </summary>
        /// <returns>retorna los usuarios</returns>
        public DataTable BuscarProductos(Producto producto)
        {
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                SqlCommand comando = new SqlCommand();
                ClaseConexion.Conectar();
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT idProducto as Codigo, nombre from Producto where nombre like @Nombre";
                comando.Parameters.AddWithValue("@Nombre", "%" + producto.Nombre + "%");
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds, "Productos");
                if (ds.Tables["Productos"].Rows.Count != 0)
                {
                    DataTable tabla = ds.Tables["Productos"];
                    return tabla;
                }
                else
                {
                    comando.Connection.Close();
                    return new DataTable();
                }
            }
            catch (SqlException ex)
            {
                ClaseConexion.Conexion.Close();
                return new DataTable();
            }
        }

        public void BorrarProducto(Producto producto)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                ClaseConexion.Conectar();
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                comando.Connection = ClaseConexion.Conexion;
                SqlTransaction transaction = comando.Connection.BeginTransaction();
                comando.Transaction = transaction;
                comando.CommandText = "DELETE FROM Producto WHERE idProducto = @IdProducto;";
                comando.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                comando.ExecuteNonQuery();
                OrganizarCatalogo.BorrarProductoDeUnCatalogo(producto, comando.Transaction, comando.Connection);
                comando.Connection.Close();
            }
            catch (SqlException ex)
            {
                comando.Connection.Close();
                throw new Exception();
            }
        }

        /// <summary>
        /// Modificar un producto
        /// </summary>
        /// <param name="usuario">usario</param>
        public void ModificarProducto(Producto producto)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                ClaseConexion.Conectar();
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandType = CommandType.Text;
                StringBuilder sQL = new StringBuilder();
                sQL.Append("UPDATE Producto ");
                sQL.Append("SET nombre = @Nombre, tipoProducto = @TipoDeProducto");
                if (producto.Imagen != "")
                {
                    sQL.Append(", imagen = @Imagen");
                    sQL.Append(", precio = @Precio ");
                    sQL.Append(" WHERE idProducto = @IdProducto;");
                    comando.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                    comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    comando.Parameters.AddWithValue("@TipoDeProducto", producto.TipoDeProducto);
                    comando.Parameters.AddWithValue("@Imagen", producto.Imagen);
                }
                else
                {
                    sQL.Append(", precio = @Precio ");
                    sQL.Append(" WHERE idProducto = @IdProducto;");
                    comando.CommandText = sQL.ToString();
                    comando.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                    comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    comando.Parameters.AddWithValue("@TipoDeProducto", producto.TipoDeProducto.IdTipoDeProducto);
                    comando.Parameters.AddWithValue("@Precio", producto.Precio);
                }
                comando.ExecuteNonQuery();
                comando.Connection.Close();
            }
            catch (SqlException ex)
            {
                ClaseConexion.Conexion.Close();
                throw new Exception();
            }
        }
    }
}
