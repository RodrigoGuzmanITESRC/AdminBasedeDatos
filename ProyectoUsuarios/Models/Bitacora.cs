using System;
using System.Collections.Generic;

namespace ProyectoUsuarios.Models
{
    public partial class Bitacora
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int UsuarioId { get; set; }
        public string Descripcion { get; set; }
    }
}
