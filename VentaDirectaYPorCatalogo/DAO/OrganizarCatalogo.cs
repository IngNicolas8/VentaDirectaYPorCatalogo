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
                comando.Parameters.AddWithValue("@Nombre", catalogo.Temporada);
                comando.ExecuteNonQuery();
                comando.Connection.Close();
            }
            catch (SqlException ex)
            {
                comando.Transaction.Rollback();
                throw new Exception();
            }
        }

        public void BuscarCatalogo(ref Catalogo catalogo)
        {
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                SqlCommand comando = new SqlCommand();
                ClaseConexion.Conectar();
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT idCatalogo, anio, temporada, nombre from Catalogo where idCatalogo = @IdCatalogo";
                comando.Parameters.AddWithValue("@IdCatalogo", catalogo.IdCatalogo);
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

        public DataTable BuscarCatalogos(Catalogo catalogo)
        {
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                SqlCommand comando = new SqlCommand();
                ClaseConexion.Conectar();
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT idCatalogo, anio, temporada, nombre from Catalogo where nombre like @Nombre or anio = @Fecha";
                comando.Parameters.AddWithValue("@Nombre", "%" + catalogo.Nombre + "%");
                comando.Parameters.AddWithValue("@Fecha", catalogo.Fecha);
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
                sQL.Append("SET anio = @Fecha, temporada = @Temporada, nombre = @Nombre");
                sQL.Append(" WHERE idCatalogo = @IdCatalogo;");
                comando.CommandText = sQL.ToString();
                comando.Parameters.AddWithValue("@IdCatalogo", catalogo.IdCatalogo);
                comando.Parameters.AddWithValue("@Fecha", catalogo.IdCatalogo);
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

        public static void BorrarTipoDeProducto(Catalogo catalogo)
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
    }
}
