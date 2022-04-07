using Microsoft.EntityFrameworkCore;
using ProyectoUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUsuarios.Repositories
{
    public class UsuariosRepository
    {
        usuariosContext context = new usuariosContext();
        public IEnumerable<Usuario> GetAll()
        {
            return context.Usuario.OrderBy(x => x.Nombre);
        }

        public Usuario GetById(Usuario u)
        {
            return context.Usuario.FirstOrDefault(x => x.EMail == u.EMail);
        }

        public void Insert(Usuario u)
        {
            context.Database.ExecuteSqlRaw($"call sp_RegistrarUsuarioBitactora" +
                $"('{u.EMail}','{u.Nombre}','{u.Direccion}', '{u.Telefono}','{u.Contrasena}');");

        }
        public void Delete(Usuario u)
        {
            context.Remove(u);
            context.SaveChanges();
        }

        public void Update(Usuario u)
        {
            Usuario usuario = context.Usuario.FirstOrDefault(x => x.Id == u.Id);
            if (usuario != null)
            {
                usuario.EMail = u.EMail;
                usuario.Nombre = u.Nombre;
                usuario.Direccion = u.Direccion;
                usuario.Telefono = u.Telefono;
                usuario.Contrasena = u.Contrasena;
                context.SaveChanges();
            }
        }

        public bool Validate(Usuario u)
        {
            if (string.IsNullOrWhiteSpace(u.Nombre))
            {
                throw new ArgumentException("Debe especificar el nombre del usuario");
            }
            if (string.IsNullOrWhiteSpace(u.Direccion))
            {
                throw new ArgumentException("Debe especificar la direccion del usuario");
            }
            if (string.IsNullOrWhiteSpace(u.Telefono))
            {
                throw new ArgumentException("Debe especificar el telefono del usuario");
            }
            if (string.IsNullOrWhiteSpace(u.EMail))
            {
                throw new ArgumentException("Debe especificar el correo electronico del usuario");
            }
            if (string.IsNullOrWhiteSpace(u.Contrasena))
            {
                throw new ArgumentException("Debe especificar la contraseña del usuario");
            }
            return true;
        }
    }
}
