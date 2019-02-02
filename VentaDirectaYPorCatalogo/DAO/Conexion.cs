using System;
using System.Configuration;
using System.Data.SqlClient;

namespace BaseDeDatos
{
    /// <summary>
    /// Clase con los metodos de conexion
    /// </summary>
    public static class ClaseConexion
    {
        /// <summary>
        /// Conexion
        /// </summary>
        private static SqlConnection conexion;

        public static SqlConnection Conexion { get => conexion; set => conexion = value; }

        /// <summary>
        /// Metodo que busca la cadena de  conexon del web.config y realiza la conexion
        /// </summary>
        /// <returns>retorna verdadero si se conecto falso en caso contrario</returns>
        public static void Conectar()
        {
            try
            {
                Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        
    }
}
