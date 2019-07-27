using Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BaseDeDatos
{
    public class OrganizarPersona
    {
        /// <summary>
        /// Registra los datos personales para un usuario
        /// </summary>
        /// <param name="usuario">usuario</param>
        /// <param name="transaccion">SQLTransaccion</param>
        /// <param name="conexion">conexion</param>
        public void RegistrarPersona(Usuario usuario, SqlTransaction transaccion, SqlConnection conexion)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                comando.Connection = conexion;
                comando.Transaction = transaccion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO Persona (idUsuario, Nombre, Apellido) VALUES(@usuario, @Nombre, @Apellido); ";
                comando.Parameters.AddWithValue("@usuario", usuario.User);
                comando.Parameters.AddWithValue("@Nombre", usuario.Persona.Nombre);
                comando.Parameters.AddWithValue("@Apellido", usuario.Persona.Apellido);
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
        /// Busca los datos personales para el usuario
        /// </summary>
        /// <param name="usuario">usuario</param>
        public void BuscarPersona(ref Usuario usuario)
        {
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                SqlCommand comando = new SqlCommand();
                ClaseConexion.Conectar();
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT Nombre, Apellido, idPersona from Persona where idUsuario like @usuario";
                comando.Parameters.AddWithValue("@usuario", "%" + usuario.User + "%");
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds, "Personas");
                if (ds.Tables["Personas"].Rows.Count != 0)
                {
                    usuario.Persona = new Persona();
                    usuario.Persona.Nombre = ds.Tables["Personas"].Rows[0]["Nombre"].ToString();
                    usuario.Persona.Apellido = ds.Tables["Personas"].Rows[0]["Apellido"].ToString();
                    usuario.Persona.Id = Convert.ToInt32(ds.Tables["Personas"].Rows[0]["idPersona"].ToString());
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
        /// Modifica los datos personales para un usuario
        /// </summary>
        /// <param name="usuario">usuario</param>
        /// <param name="transaccion">SQLTransaccion</param>
        /// <param name="conexion">conexion</param>
        public void ModificarPersona(Usuario usuario, SqlTransaction transaccion, SqlConnection conexion)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                comando.Connection = conexion;
                comando.Transaction = transaccion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "UPDATE Persona SET idUsuario = @usuario, Nombre = @Nombre, Apellido = @Apellido WHERE idPersona = @IdPersona;";
                comando.Parameters.AddWithValue("@usuario", usuario.User);
                comando.Parameters.AddWithValue("@Nombre", usuario.Persona.Nombre);
                comando.Parameters.AddWithValue("@Apellido", usuario.Persona.Apellido);
                comando.Parameters.AddWithValue("@IdPersona", usuario.Persona.Id);
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

        public static void BorrarPersona(Usuario usuario)
        {
            SqlCommand comando = new SqlCommand();
            try
            {
                ClaseConexion.Conectar();
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                comando.Connection = ClaseConexion.Conexion;
                SqlTransaction transaccion = comando.Connection.BeginTransaction();
                comando.Transaction = transaccion;
                comando.CommandText = "DELETE FROM Persona WHERE idPersona = @IdPersona;";
                comando.Parameters.AddWithValue("@IdPersona", usuario.Persona.Id);
                comando.ExecuteNonQuery();
                OrganizarUsuario.BorrarUsuario(usuario, transaccion,comando.Connection);
            }
            catch (SqlException ex)
            {
                comando.Connection.Close();
                throw new Exception();
            }
        }
    }
}
