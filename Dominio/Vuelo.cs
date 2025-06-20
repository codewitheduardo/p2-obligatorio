using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;

namespace Dominio
{
    public class Vuelo : IValidable
    {
        private string _numero;
        private Ruta _ruta;
        private Avion _avion;
        private List<Dia> _frecuencia;

        public string Numero
        {
            get { return _numero; }
        }

        public Ruta Ruta
        {
            get { return _ruta; }
        }

        public Avion Avion
        {
            get { return _avion; }
        }

        public List<Dia> Frecuencia
        {
            get { return _frecuencia; }
        }

        public void Validar()
        {
            this.ValidarNumero();
            this.ValidarDistancia();
        }

        private void ValidarNumero()
        {
            if (string.IsNullOrWhiteSpace(this._numero) || this._numero.Length < 3)
            {
                throw new Exception("El número de vuelo debe tener un mínimo de 3 caracteres.");
            }

            int index = 0;

            foreach (char caracter in this._numero)
            {
                if (index < 2)
                {
                    if (!char.IsLetter(caracter))
                    {
                        throw new Exception("Los dos primeros caracteres deben ser letras.");
                    }
                }
                else
                {
                    if (!char.IsDigit(caracter))
                    {
                        throw new Exception("Los últimos caracteres deben ser números.");
                    }
                }
                index++;
            }

            int cantNumeros = this._numero.Length - 2;

            if (cantNumeros < 1 || cantNumeros > 4)
            {
                throw new Exception("La parte numérica debe tener entre 1 y 4 dígitos.");
            }
        }

        private void ValidarDistancia()
        {
            if (this._avion.Alcance < this._ruta.Distancia)
            {
                throw new Exception("El avión no tiene alcance suficiente para cubrir esta ruta.");
            }
        }

        public decimal ObtenerCostoPorAsiento()
        {
            return ((this._avion.CostoKm * this._ruta.Distancia) +
                (this._ruta.Salida.CostoOperacion + this._ruta.Llegada.CostoOperacion))
                / this._avion.CantidadAsientos;
        }

        public Vuelo(string numero, Ruta ruta, Avion avion, List<Dia> frecuencia)
        {
            this._numero = numero;
            this._ruta = ruta;
            this._avion = avion;
            this._frecuencia = new List<Dia>(frecuencia);
        }

        public override string ToString()
        {
            string dias = string.Join(", ", this._frecuencia);

            return
                "[ INFORMACIÓN DEL VUELO ]\n" +
                "-------------------------------\n" +
                $"* Número:        {this._numero}\n" +
                $"* Avión:         {this._avion.Modelo}\n" +
                $"* Ruta:          ({this._ruta.Salida.Codigo} - {this._ruta.Llegada.Codigo})\n" +
                $"* Frecuencia:    {dias}\n" +
                "-------------------------------\n";
        }
    }
}
