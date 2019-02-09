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
        public string IniciarSession(Usuario usuario)
        {
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                SqlCommand comando = new SqlCommand();
                ClaseConexion.Conectar();
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT rol from Usuarios where usuario like @usuario AND contraseña like @contraseña";
                comando.Parameters.AddWithValue("@usuario", usuario.User);
                comando.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds, "Usuarios");
                if (ds.Tables["Usuarios"].Rows.Count != 0)
                {
                    string rol = ds.Tables["Usuarios"].Rows[0]["rol"].ToString();
                    comando.Connection.Close();
                    return rol;
                }
                else
                {
                    comando.Connection.Close();
                    return "desconocido";
                }
            }
            catch (SqlException ex)
            {
                ClaseConexion.Conexion.Close();
                return "desconocido";
            }
        }

        /// <summary>
        /// Busca el usuario 
        /// </summary>
        /// <returns>retorna el usuario</returns>
        public bool BuscarUsuario(ref Usuario usuario)
        {
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                SqlCommand comando = new SqlCommand();
                ClaseConexion.Conectar();
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT usuario, rol from Usuarios where usuario like @usuario";
                comando.Parameters.AddWithValue("@usuario", "%" + usuario.User + "%");
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds, "Usuarios");
                if (ds.Tables["Usuarios"].Rows.Count != 0)
                {
                    usuario.User = ds.Tables["Usuarios"].Rows[0]["usuario"].ToString();
                    usuario.Rol = ds.Tables["Usuarios"].Rows[0]["rol"].ToString();
                    comando.Connection.Close();
                    return true;
                }
                else
                {
                    comando.Connection.Close();
                    return false;
                }
            }
            catch (SqlException ex)
            {
                ClaseConexion.Conexion.Close();
                return false;
            }
        }
    }
}
