using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    public class Premium : Cliente
    {
        private int _puntos;

        public int Puntos
        {
            get { return _puntos; }
            set { _puntos = value; }
        }

        public override decimal CalcularRecargo(Equipaje equipaje)
        {
            if (equipaje == Equipaje.BODEGA)
            {
                return 0.05m;
            }
            return 0m;
        }

        public Premium(string correo,
            string password,
            string documento,
            string nombre,
            string nacionalidad) : base(correo, password, documento, nombre, nacionalidad)
        {
            this._puntos = 0;
        }

        public override string ToString()
        {
            return "[ CLIENTE PREMIUM ]" +
                   "\n-----------------------------------------" +
                   $"\n* Nombre:        {this.Nombre}" +
                   $"\n* Correo:        {this.Correo}" +
                   $"\n* Nacionalidad:  {this.Nacionalidad}" +
                   $"\n* Puntos:        {this.Puntos}" +
                   "\n-----------------------------------------\n";
        }
    }
}
