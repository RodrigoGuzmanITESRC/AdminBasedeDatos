using System;
using System.Collections.Generic;

namespace ProyectoUsuarios.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string EMail { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contrasena { get; set; }
    }
}
