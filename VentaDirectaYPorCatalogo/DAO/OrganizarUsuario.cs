using Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BaseDeDatos
{
    public class OrganizarUsuario
    {
        /// <summary>
        /// Registra un usuario con su nombre de usuario y su contraseña
        /// </summary>
        /// <param name="usuario">usario</param>
        /// <param name="contraseña">contraseña</param>
        public void RegistraUsuario(Usuario usuario)
        {
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                SqlCommand comando = new SqlCommand();
                ClaseConexion.Conectar();
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO Usuarios (usuario, contraseña) VALUES(@usuario, @contraseña); ";
                comando.Parameters.AddWithValue("@usuario", usuario.User);
                comando.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
                comando.ExecuteNonQuery();
                comando.Connection.Close();
            }
            catch (SqlException)
            {
                ClaseConexion.Conexion.Close();
                throw new Exception();
            }
        }

        /// <summary>
        /// Inicia session. Devuelve verdadero si el usuario inicio sesion
        /// </summary>
        /// <param name="usuario">clase usuario con la contraseña y el usuario</param>
        /// <returns>verdadero sise inicio sssion, falso si no</returns>
        public bool IniciarSession(Usuario usuario)
        {
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                SqlCommand comando = new SqlCommand();
                ClaseConexion.Conectar();
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT * from Usuarios where usuario like '@usuario' AND password like '@contraseña'";
                comando.Parameters.AddWithValue("@usuario", usuario.User);
                comando.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
                SqlDataReader lector = comando.ExecuteReader();
                comando.Connection.Close();
                if (lector.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                ClaseConexion.Conexion.Close();
                return false;
            }
        }
    }
}
