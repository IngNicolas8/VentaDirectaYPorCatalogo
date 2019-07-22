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
    }
}
