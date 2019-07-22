using System;

namespace Clases
{
    public class Persona
    {
        private int id;

        //id de la persona
        public int Id { get => id; set => id = value; }

        private string nombre;

        //Nombre de la persona
        public string Nombre { get => nombre; set => nombre = value; }

        private string apellido;

        //apellido de la persona
        public string Apellido { get => apellido; set => apellido = value; }

        private string idUsuario;

        //id de usuario
        public string IdUsuario { get => idUsuario; set => idUsuario = value; }

        //Constructor sin parametros
        public Persona() { }

        //Constructor con parametros
        public Persona(string nombre, string apellido, string idUsuario, int id)
        {
            this.nombre = nombre;
            this.idUsuario = idUsuario;
            this.id = id;
            this.apellido = apellido;
        }
    }
}
