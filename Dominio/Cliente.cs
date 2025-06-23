using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;

namespace Dominio
{
    public abstract class Cliente : Usuario, IValidable, IComparable<Cliente>
    {
        private string _documento;
        private string _nombre;
        private string _nacionalidad;

        public string Documento
        {
            get { return _documento; }
        }

        public string Nombre
        {
            get { return _nombre; }
        }

        public string Nacionalidad
        {
            get { return _nacionalidad; }
        }

        public void Validar()
        {
            this.ValidarDocumento();
            this.ValidarNombre();
            this.ValidarNacionalidad();
        }

        private void ValidarDocumento()
        {
            if (string.IsNullOrWhiteSpace(this._documento))
            {
                throw new Exception("El documento de identidad no puede estar vacío.");
            }
        }

        private void ValidarNombre()
        {
            if (string.IsNullOrWhiteSpace(this._nombre))
            {
                throw new Exception("El nombre no puede estar vacío.");
            }
        }

        private void ValidarNacionalidad()
        {
            if (string.IsNullOrWhiteSpace(this._nacionalidad))
            {
                throw new Exception("La nacionalidad no puede estar vacía.");
            }
        }

        public abstract decimal CalcularRecargo(Equipaje equipaje);

        public Cliente(string correo,
            string password,
            string documento,
            string nombre,
            string nacionalidad) : base(correo, password)
        {
            this._documento = documento;
            this._nombre = nombre;
            this._nacionalidad = nacionalidad;
        }

        //Compara clientes por documento - recibe por parámetro cualquier tipo de objeto
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Cliente)) return false;
            Cliente otro = (Cliente)obj;
            return this._documento.Equals(otro._documento);
        }

        public int CompareTo(Cliente? other)
        {
            return this._documento.CompareTo(other._documento);
        }
    }
}
