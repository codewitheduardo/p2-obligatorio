using Dominio.Interfaces;

namespace Dominio
{
    public abstract class Usuario : IValidable
    {
        private string _correo;
        private string _password;

        public string Correo
        {
            get { return _correo; }
        }

        public string Password
        { 
            get { return _password; }
        }

        public void Validar()
        {
            this.ValidarCorreo();
            this.ValidarPassword();
        }

        private void ValidarCorreo()
        {
            if (string.IsNullOrWhiteSpace(this._correo))
            {
                throw new Exception("El correo no puede estar vacío.");
            }

            if (!this._correo.Contains("@"))
            {
                throw new Exception("El correo debe contener el caracter '@'");
            }
        }

        private void ValidarPassword()
        {
            if (string.IsNullOrWhiteSpace(this._password))
            {
                throw new Exception("La contraseña no puede estar vacía.");
            }

            if (this._password.Length < 8)
            {
                throw new Exception("La contraseña debe de tener un mínimo de 8 caracteres.");
            }

            int letras = 0;
            int numeros = 0;

            int i = 0;
            while (i < this._password.Length && (letras == 0 || numeros == 0))
            {
                if (char.IsLetter(this._password[i]))
                {
                    letras++;
                }

                if (char.IsDigit(this._password[i]))
                {
                    numeros++;
                }
                i++;
            }

            if (letras == 0)
            {
                throw new Exception("La contraseña debe de tener al menos una letra.");
            }
            if (numeros == 0)
            {
                throw new Exception("La contraseña debe de tener al menos un número.");
            }
        }

        public Usuario(string correo,
         string password)
        {
            this._correo = correo;
            this._password = password;
        }
    }
}
