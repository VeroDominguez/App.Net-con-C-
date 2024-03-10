using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
  
    public abstract class Usuario: IValidable
    {
        public int Id { get; }
        public static int UltimoId { get; set; } = 1;
        public string Email { get; set; }
        public string Contrasenia { get; set; }

        public Usuario()
        {
            Id = UltimoId;
            UltimoId++;
        }

        public Usuario(string email, string contrasenia)
        {
            Id = UltimoId;
            UltimoId++;
            Email = email;
            Contrasenia = contrasenia;
        }

        // se valida: que email no esté vacio, que el @ no esté ni al comienzo ni al final, que la contraseña no esté vacía y tenga un mínimo de 8 caracteres.
        public virtual void Validar()
        {
            try
            {
                ValidarEmail();
                ValidarArroba();
                ValidarContrasenia();
                ValidarContraseniaMin8();
            }
            catch

            {
                throw;
            }
        }        private void ValidarEmail()
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new Exception("El email no puede estar vacío.");
            }
        }

        private void ValidarArroba()
        {
            if (Email.Contains("@"))
            {
                if (Email.IndexOf("@") == 0 || Email.IndexOf("@") == Email.Length-1)
                {
                    throw new Exception("El @ no puede estar al comienzo o al final del e-mail.");
                }
            }
            else
            {
                throw new Exception("El e-mail debe contener un @.");
            }
        }

        private void ValidarContrasenia()
        {
            if(string.IsNullOrEmpty(Contrasenia))
            {
                throw new Exception("La contraseña no puede estar vacía.");
            }
        }

        private void ValidarContraseniaMin8()
        {
            if(Contrasenia.Length < 8)
            {
                throw new Exception("La contraseña debe un tener mínimo de 8 caracteres.");

            }
        }

        public abstract string GetTipo();


        // compara que los usuarios no tengan el mismo mail y en caso de coincidir me devuelve true, asociado a la función de Equals en Huesped.
        public override bool Equals(object? obj)
        {
            return obj is Usuario usuario &&
                Email == usuario.Email;

        }

    }
}
