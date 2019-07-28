using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Clases;

namespace BaseDeDatos
{
    public class OrganizarTipoDeProducto
    {
        /// <summary>
        /// Registra los tipos de producto
        /// </summary>
        /// <param name="usuario">usuario</param>
        /// <param name="transaccion">SQLTransaccion</param>
        /// <param name="conexion">conexion</param>
        public void RegistrarTipoDeProducto(TipoDeProducto tipoDeProducto)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                ClaseConexion.Conectar();
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO TipoDeProducto (descripcion, nombre) VALUES(@Descripcion, @Nombre); ";
                comando.Parameters.AddWithValue("@Nombre", tipoDeProducto.Nombre);
                comando.Parameters.AddWithValue("@Descripcion", tipoDeProducto.Descripcion);
                comando.ExecuteNonQuery();
                comando.Connection.Close();
            }
            catch (SqlException ex)
            {
                comando.Transaction.Rollback();
                throw new Exception();
            }
        }

        public void BuscarTipoDeProducto(ref TipoDeProducto tipoDeProducto)
        {
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                SqlCommand comando = new SqlCommand();
                ClaseConexion.Conectar();
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT idTipoDeProducto, nombre, descripcion from TipoDeProducto where nombre like @Nombre";
                comando.Parameters.AddWithValue("@Nombre", "%" + tipoDeProducto.Nombre + "%");
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds, "TipoDeProducto");
                if (ds.Tables["TipoDeProducto"].Rows.Count != 0)
                {
                    tipoDeProducto.Nombre = ds.Tables["TipoDeProducto"].Rows[0]["nombre"].ToString();
                    tipoDeProducto.Descripcion = ds.Tables["TipoDeProducto"].Rows[0]["descripcion"].ToString();
                    tipoDeProducto.IdTipoDeProducto = Convert.ToInt32(ds.Tables["TipoDeProducto"].Rows[0]["idTipoDeProducto"].ToString());
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

        public DataTable BuscarTiposDeProducto(TipoDeProducto tipoDeProducto)
        {
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                SqlCommand comando = new SqlCommand();
                ClaseConexion.Conectar();
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT descripcion, nombre from TipoDeProducto where nombre like @Nombre";
                comando.Parameters.AddWithValue("@Nombre", "%" + tipoDeProducto.Nombre + "%");
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds, "TipoDeProducto");
                if (ds.Tables["TipoDeProducto"].Rows.Count != 0)
                {
                    DataTable tabla = ds.Tables["TipoDeProducto"];
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

        public void ModificarTipoDeProductoAModificar(TipoDeProducto tipoDeProducto)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                ClaseConexion.Conectar();
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandType = CommandType.Text;
                StringBuilder sQL = new StringBuilder();
                sQL.Append("UPDATE TipoDeProducto ");
                sQL.Append("SET nombre = @Nombre, descripcion = @Descripcion");
                sQL.Append(" WHERE idTipoDeProducto = @IdTipoDeProducto;");
                comando.CommandText = sQL.ToString();
                comando.Parameters.AddWithValue("@IdTipoDeProducto", tipoDeProducto.IdTipoDeProducto);
                comando.Parameters.AddWithValue("@Nombre", tipoDeProducto.Nombre);
                comando.Parameters.AddWithValue("@Descripcion", tipoDeProducto.Descripcion);
                comando.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                ClaseConexion.Conexion.Close();
                throw new Exception();
            }
        }

        public static void BorrarTipoDeProducto(TipoDeProducto tipoDeProducto)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                ClaseConexion.Conectar();
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandText = "DELETE FROM TipoDeProducto WHERE idTipoDeProducto = @IdTipoDeProducto;";
                comando.Parameters.AddWithValue("@IdTipoDeProducto", tipoDeProducto.IdTipoDeProducto);
                comando.ExecuteNonQuery();
                comando.Connection.Close();
            }
            catch (SqlException ex)
            {
                comando.Connection.Close();
                throw new Exception();
            }
        }
    }
}
