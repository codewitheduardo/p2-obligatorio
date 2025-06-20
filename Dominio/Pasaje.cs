using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;

namespace Dominio
{
    public class Pasaje : IValidable, IComparable<Pasaje>
    {
        private static int s_ultimoId = 1;
        private int _id;
        private Vuelo _vuelo;
        private DateTime _fecha;
        private Cliente _pasajero;
        private Equipaje _equipaje;
        private decimal _precio;

        public int Id
        {
            get { return _id; }
        }

        public Vuelo Vuelo
        {
            get { return _vuelo; }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
        }

        public Cliente Pasajero
        {
            get { return _pasajero; }
        }

        public Equipaje Equipaje
        {
            get { return _equipaje; }
        }

        public decimal Precio
        {
            get { return _precio; }
        }

        public void Validar()
        {
            this.ValidarFecha();
        }

        private void ValidarFecha()
        {
            if (this._fecha == DateTime.MinValue)
            {
                throw new Exception("Debe de ingresar una fecha para realizar la compra.");
            }

            if (this._fecha < DateTime.Today)
            {
                throw new Exception("La fecha del pasaje no puede ser anterior a la fecha actual.");
            }

            Dia diaPasaje = (Dia)this._fecha.DayOfWeek;

            if (!this._vuelo.Frecuencia.Contains(diaPasaje))
            {
                throw new Exception($"La fecha del pasaje no coincide con la frecuencia del vuelo.");
            }
        }

        public decimal CalcularPrecio()
        {
            decimal costoBase = this._vuelo.ObtenerCostoPorAsiento();
            decimal margenGanancias = costoBase * 0.25m;
            decimal recargoEquipaje = costoBase * this._pasajero.CalcularRecargo(this._equipaje);

            decimal subtotal = costoBase + margenGanancias + recargoEquipaje;
            decimal tasas = this._vuelo.Ruta.Salida.CostoTasas + this._vuelo.Ruta.Llegada.CostoTasas;

            return subtotal + tasas;
        }

        public Pasaje(Vuelo vuelo, DateTime fecha, Cliente pasajero, Equipaje equipaje)
        {
            this._id = Pasaje.s_ultimoId++;
            this._vuelo = vuelo;
            this._fecha = fecha;
            this._pasajero = pasajero;
            this._equipaje = equipaje;
            this._precio = this.CalcularPrecio();
        }

        public override string ToString()
        {
            return "[ INFORMACIÓN DEL PASAJE ]" +
                   "\n-----------------------------------------" +
                   $"\n* ID:               #{this.Id}" +
                   $"\n* Pasajero:         {this.Pasajero.Nombre}" +
                   $"\n* Precio:           {this.Precio:F2}" +
                   $"\n* Fecha:            {this.Fecha.ToString("dd MMM yyyy")}" +
                   $"\n* Número de vuelo:  {this.Vuelo.Numero}" +
                   "\n-----------------------------------------\n";
        }

        public int CompareTo(Pasaje? other)
        {
            return this._precio.CompareTo(other._precio) * -1;
        }
    }
}
