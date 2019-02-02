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
        public void RegistraUsuario(string usuario, string contraseña)
        {
            try
            {
                //Defino un SqlComand y llamo al metodo conectar para que se conecte y me devuelva la conexion
                SqlCommand comando = new SqlCommand();
                ClaseConexion.Conectar();
                comando.Connection = ClaseConexion.Conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO Usuarios (usuario, contraseña) VALUES(@usuario, @contraseña); ";
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@contraseña", contraseña);
                comando.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw new Exception();
            }
        }
    }
}
