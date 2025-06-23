using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;

namespace Dominio
{
    public class Aeropuerto : IValidable
    {
        private string _codigo;
        private string _ciudad;
        private decimal _costoOperacion;
        private decimal _costoTasas;

        public string Codigo
        {
            get { return _codigo; }
        }
        public string Ciudad
        {
            get { return _ciudad; }
        }
        public decimal CostoOperacion
        {
            get { return _costoOperacion; }
        }
        public decimal CostoTasas
        {
            get { return _costoTasas; }
        }

        public void Validar()
        {
            this.ValidarCodigoIATA();
            this.ValidarCiudad();
            this.ValidarCostoOperacion();
            this.ValidarCostoTasas();
        }

        private void ValidarCodigoIATA()
        {
            if (string.IsNullOrWhiteSpace(this._codigo) || this._codigo.Length != 3)
            {
                throw new Exception("El código IATA debe tener exactamente 3 caracteres.");
            }

            foreach (char caracter in this._codigo)
            {
                if (!char.IsLetter(caracter))
                {
                    throw new Exception("El código IATA no debe contener números.");
                }
            }
        }

        private void ValidarCiudad()
        {
            if (string.IsNullOrWhiteSpace(this._ciudad))
            {
                throw new Exception("El nombre de la ciudad no puede estar vacío.");
            }
        }

        private void ValidarCostoOperacion()
        {
            if (this._costoOperacion < 0)
            {
                throw new Exception("El costo de operación no puede ser negativo.");
            }
        }

        private void ValidarCostoTasas()
        {
            if (this._costoTasas < 0)
            {
                throw new Exception("El costo de las tasas no puede ser negativa.");
            }
        }

        public Aeropuerto(string codigo,
            string ciudad,
            decimal costoOperacion,
            decimal costoTasas)
        {
            this._codigo = codigo;
            this._ciudad = ciudad;
            this._costoOperacion = costoOperacion;
            this._costoTasas = costoTasas;
        }
        // Compara dos objetos Aeropuerto por su código IATA - recibe por parámetro cualquier tipo de objeto
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Aeropuerto)) return false;
            Aeropuerto otro = (Aeropuerto)obj;
            return this._codigo.Equals(otro._codigo);
        }
    }
}
