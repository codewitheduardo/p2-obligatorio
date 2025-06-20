using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;

namespace Dominio
{
    public class Administrador : Usuario, IValidable
    {
        private string _apodo;

        public string Apodo
        {
            get { return _apodo; }
        }

        public void Validar()
        {
            this.ValidarApodo();
        }

        private void ValidarApodo()
        {
            if (string.IsNullOrWhiteSpace(this._apodo))
            {
                throw new Exception("El apodo no puede estar vacío.");
            }
        }

        public Administrador
            (string correo,
            string password,
            string apodo) : base(correo, password)
        {
            this._apodo = apodo;
        }
    }
}
