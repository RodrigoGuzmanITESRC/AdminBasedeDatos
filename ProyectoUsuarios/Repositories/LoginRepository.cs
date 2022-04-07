using Microsoft.EntityFrameworkCore;
using ProyectoUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUsuarios.Repositories
{
    public class LoginRepository
    {
        usuariosContext context = new usuariosContext();

        public void Login(Usuario u)
        {
            context.Database.ExecuteSqlRaw($"call sp_InicioSesion('{u.EMail}','{u.Contrasena}')");
        }

        public static string GetMD5Hash(string input)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            StringBuilder s = new StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string hash = s.ToString();
            return hash;
        }

        public bool ValidateLogin(Usuario u)
        {
            if (context.Usuario.Any(x => x.EMail == u.EMail && x.Contrasena == GetMD5Hash(u.Contrasena)))
            {
                return true;
            }
            else
            {
                throw new ArgumentException("Usuario o contrasena incorrectos");
            }
        }
    }
}
