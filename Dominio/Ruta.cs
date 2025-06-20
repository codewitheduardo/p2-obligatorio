using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;

namespace Dominio
{
    public class Ruta : IValidable
    {
        private static int s_ultimoId = 1;
        private int _id;
        private Aeropuerto _salida;
        private Aeropuerto _llegada;
        private decimal _distancia;

        public int Id
        {
            get { return _id; }
        }

        public Aeropuerto Salida
        {
            get { return _salida; }
        }
        public Aeropuerto Llegada
        {
            get { return _llegada; }
        }
        public decimal Distancia
        {
            get { return _distancia; }
        }

        public void Validar()
        {
            this.ValidarDistancia();
            this.ValidarAeropuertos();
        }

        private void ValidarDistancia()
        {
            if (this._distancia < 0)
            {
                throw new Exception("La distancia de la ruta no puede ser negativa.");
            }
        }

        private void ValidarAeropuertos()
        {
            if (this._salida == this._llegada)
            {
                throw new Exception("El aeropuerto de salida no puede ser el mismo que el de llegada.");
            }
        }

        public Ruta(Aeropuerto salida,
            Aeropuerto llegada,
            decimal distancia)
        {
            this._id = Ruta.s_ultimoId++;
            this._salida = salida;
            this._llegada = llegada;
            this._distancia = distancia;
        }
    }
}
