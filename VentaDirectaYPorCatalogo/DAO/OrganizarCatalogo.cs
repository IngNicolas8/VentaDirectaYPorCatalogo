using Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace BaseDeDatos
{
    public class OrganizarCatalogo
    {
        /// <summary>
        /// Registra los catalogos de los producto
        /// </summary>
        /// <param name="catalogo">Catalogo</param>
        public void RegistrarCatalogo(Catalogo catalogo)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                ClaseConexion.Conectar();
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO Catalogo (anio, temporada, nombre) VALUES(@Fecha, @Temporada, @Nombre); ";
                CultureInfo cultura = new CultureInfo("en-US");
                comando.Parameters.AddWithValue("@Fecha", Convert.ToDateTime(catalogo.Fecha, cultura));
                comando.Parameters.AddWithValue("@Temporada", catalogo.Temporada);
                comando.Parameters.AddWithValue("@Nombre", catalogo.Nombre);
                comando.ExecuteNonQuery();
                comando.Connection.Close();
            }
            catch (SqlException ex)
            {
                comando.Transaction.Rollback();
                throw new Exception();
            }
        }

        /// <summary>
        /// Busca un catalogo deternminado
        /// </summary>
        /// <param name="catalogo">Objeto de tipo Catalogo con los datos de un catalogo</param>
        public void BuscarCatalogo(ref Catalogo catalogo)
        {
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                SqlCommand comando = new SqlCommand();
                ClaseConexion.Conectar();
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT idCatalogo, anio, temporada, nombre from Catalogo where nombre = @Nombre";
                comando.Parameters.AddWithValue("@Nombre", catalogo.Nombre);
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds, "Catalogo");
                if (ds.Tables["Catalogo"].Rows.Count != 0)
                {
                    catalogo.IdCatalogo = Convert.ToInt32(ds.Tables["Catalogo"].Rows[0]["idCatalogo"].ToString());
                    catalogo.Fecha = Convert.ToDateTime(ds.Tables["Catalogo"].Rows[0]["anio"].ToString());
                    catalogo.Temporada = ds.Tables["Catalogo"].Rows[0]["temporada"].ToString();
                    catalogo.Nombre = ds.Tables["Catalogo"].Rows[0]["nombre"].ToString();
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
        /// Registrar la relacion entre un producto y un catalogo
        /// </summary>
        /// <param name="producto"></param>
        /// <param name="transaccion"></param>
        /// <param name="connection"></param>
        internal void RegistrarProductosEnCatalogo(Producto producto, SqlTransaction transaccion, SqlConnection connection)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                comando.Connection = connection;
                comando.Transaction = transaccion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO ProductosXCatalogo (idProducto, idCatalogo) VALUES(@IdProducto, @IdCatalogo); ";
                comando.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                comando.Parameters.AddWithValue("@IdCatalogo", producto.Catalogo.IdCatalogo);
                comando.ExecuteNonQuery();
                comando.Transaction.Commit();
                comando.Connection.Close();
            }
            catch (SqlException)
            {
                comando.Transaction.Rollback();
                throw new Exception();
            }
        }

        /// <summary>
        /// Busca los catalogos segun un criterio de busqueda
        /// </summary>
        /// <param name="catalogo">Objeto de tipo de catalogo con los datos de un catalogo</param>
        /// <returns>Tabla los catalogos</returns>
        public DataTable BuscarCatalogos(Catalogo catalogo)
        {
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                SqlCommand comando = new SqlCommand();
                ClaseConexion.Conectar();
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandType = CommandType.Text;
                StringBuilder sQL = new StringBuilder();
                sQL.Append("SELECT idCatalogo as codigo, anio, temporada, nombre from Catalogo ");
                if (!catalogo.Fecha.ToString().Equals("1/1/0001 00:00:00"))
                {
                    sQL.Append("WHERE (nombre like @Nombre ");
                    sQL.Append("AND anio = @Fecha); ");
                    comando.Parameters.AddWithValue("@Nombre", "%" + catalogo.Nombre + "%");
                    comando.Parameters.AddWithValue("@Fecha", catalogo.Fecha);
                }
                else
                {
                    sQL.Append("WHERE nombre like @Nombre ");
                    comando.Parameters.AddWithValue("@Nombre", "%" + catalogo.Nombre + "%");
                }
                comando.CommandText = sQL.ToString();
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds, "Catalogo");
                if (ds.Tables["Catalogo"].Rows.Count != 0)
                {
                    DataTable tabla = ds.Tables["Catalogo"];
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

        public static void ModificarProductosDeCatalogo(Catalogo catalogo)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                ClaseConexion.Conectar();
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                comando.Connection = ClaseConexion.Conexion;
                comando.Transaction = comando.Connection.BeginTransaction();
                comando.CommandText = "DELETE FROM ProductosXCatalogo WHERE idCatalogo = @IdCatalogo;";
                comando.Parameters.AddWithValue("@IdCatalogo", catalogo.IdCatalogo);
                comando.ExecuteNonQuery();
                foreach(Producto producto in catalogo.Productos)
                {
                    producto.Catalogo = catalogo;
                    OrganizarCatalogo.RegistrarProductosEnCatalogoSeguro(producto, comando.Transaction, comando.Connection);
                }
                comando.Transaction.Commit();
                comando.Connection.Close();
            }
            catch (SqlException ex)
            {
                comando.Transaction.Rollback();
                comando.Connection.Close();
                throw new Exception();
            }
        }

        internal static void BorrarProductoDeUnCatalogo(Producto producto, SqlTransaction transaccion, SqlConnection conexion)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                comando.Connection = conexion;
                comando.Transaction = transaccion;
                comando.CommandText = "DELETE FROM ProductosXCatalogo WHERE idProducto = @IdProducto;";
                comando.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                comando.ExecuteNonQuery();
                comando.Transaction.Commit();
                comando.Connection.Close();
            }
            catch (SqlException ex)
            {
                comando.Transaction.Rollback();
                throw new Exception();
            }
        }

        /// <summary>
        /// Modifica los datos de un catalogo
        /// </summary>
        /// <param name="catalogo">Objeto de tipo de catalogo con los datos de un catalogo</param>
        public void ModificarCatalogos(Catalogo catalogo)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                ClaseConexion.Conectar();
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandType = CommandType.Text;
                StringBuilder sQL = new StringBuilder();
                sQL.Append("UPDATE Catalogo ");
                sQL.Append("SET temporada = @Temporada, nombre = @Nombre");
                sQL.Append(" WHERE idCatalogo = @IdCatalogo;");
                comando.CommandText = sQL.ToString();
                comando.Parameters.AddWithValue("@IdCatalogo", catalogo.IdCatalogo);
                comando.Parameters.AddWithValue("@Temporada", catalogo.Temporada);
                comando.Parameters.AddWithValue("@Nombre", catalogo.Nombre);
                comando.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                ClaseConexion.Conexion.Close();
                throw new Exception();
            }
        }

        /// <summary>
        /// Borra un producto
        /// </summary>
        /// <param name="catalogo">Objeto de tipo de catalogo con los datos de un catalogo</param>
        public static void BorrarCatalogo(Catalogo catalogo)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                ClaseConexion.Conectar();
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandText = "DELETE FROM Catalogo WHERE idCatalogo = @IdCatalogo;";
                comando.Parameters.AddWithValue("@IdCatalogo", catalogo.IdCatalogo);
                comando.ExecuteNonQuery();
                comando.Connection.Close();
            }
            catch (SqlException ex)
            {
                comando.Connection.Close();
                throw new Exception();
            }
        }

        /// <summary>
        /// Registrar la relacion entre un producto y un catalogo
        /// </summary>
        /// <param name="producto"></param>
        /// <param name="transaccion"></param>
        /// <param name="connection"></param>
        internal static void RegistrarProductosEnCatalogoSeguro(Producto producto, SqlTransaction transaccion, SqlConnection connection)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                comando.Connection = connection;
                comando.Transaction = transaccion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO ProductosXCatalogo (idProducto, idCatalogo) VALUES(@IdProducto, @IdCatalogo); ";
                comando.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                comando.Parameters.AddWithValue("@IdCatalogo", producto.Catalogo.IdCatalogo);
                comando.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw new Exception();
            }
        }
    }
}
