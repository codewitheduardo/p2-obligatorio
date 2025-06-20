using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Ocasional : Cliente
    {
        private bool _elegibleRegalo;

        public bool ElegibleRegalo
        {
            get { return _elegibleRegalo; }
            set { _elegibleRegalo = value; }
        }

        public static bool GenerarElegibilidad()
        {
            return new Random().Next(0, 4) == 1;
        }

        public override decimal CalcularRecargo(Equipaje equipaje)
        {
            if (equipaje == Equipaje.CABINA)
            {
                return 0.10m;
            }
            else if (equipaje == Equipaje.BODEGA)
            {
                return 0.20m;
            }
            return 0m;
        }

        public Ocasional(string correo,
            string password,
            string documento,
            string nombre,
            string nacionalidad) : base(correo, password, documento, nombre, nacionalidad)
        {
            this._elegibleRegalo = GenerarElegibilidad();
        }

        public override string ToString()
        {
            string elegible;

            if (this._elegibleRegalo)
            {
                elegible = "Si";
            }
            else
            {
                elegible = "No";
            }
            return "[ CLIENTE OCASIONAL ]" +
                   "\n-----------------------------------------" +
                   $"\n* Nombre:        {this.Nombre}" +
                   $"\n* Correo:        {this.Correo}" +
                   $"\n* Nacionalidad:  {this.Nacionalidad}" +
                   $"\n* Regalo cabina: {elegible}" +
                   "\n-----------------------------------------\n";
        }
    }
}
