using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;

namespace Dominio
{
    public class Avion : IValidable
    {
        private string _fabricante;
        private string _modelo;
        private int _cantidadAsientos;
        private decimal _alcance;
        private decimal _costoKm;

        public string Fabricante
        {
            get { return _fabricante; }
        }

        public string Modelo
        {
            get { return _modelo; }
        }

        public int CantidadAsientos
        {
            get { return _cantidadAsientos; }
        }
        public decimal Alcance
        {
            get { return _alcance; }
        }
        public decimal CostoKm
        {
            get { return _costoKm; }
        }

        public void Validar()
        {
            this.ValidarFabricante();
            this.ValidarModelo();
            this.ValidarCantidadAsientos();
            this.ValidarAlcance();
            this.ValidarCostoKm();
        }

        private void ValidarFabricante()
        {
            if (string.IsNullOrWhiteSpace(this._fabricante))
            {
                throw new Exception("El nombre del fabricante no puede estar vacío.");
            }
        }

        private void ValidarModelo()
        {
            if (string.IsNullOrEmpty(this._modelo))
            {
                throw new Exception("El nombre del modelo no puede estar vacío.");
            }
        }

        private void ValidarCantidadAsientos()
        {
            if (this._cantidadAsientos < 0)
            {
                throw new Exception("La cantidad de asientos no puede ser negativa.");
            }
        }

        private void ValidarAlcance()
        {
            if (this._alcance < 0)
            {
                throw new Exception("El alcance no puede ser negativo.");
            }
        }

        private void ValidarCostoKm()
        {
            if (this._costoKm < 0)
            {
                throw new Exception("El costo por km no puede ser negativo.");
            }
        }

        public Avion(string fabricante,
            string modelo,
            int cantidadAsientos,
            decimal alcance,
            decimal costoKm)
        {
            this._fabricante = fabricante;
            this._modelo = modelo;
            this._cantidadAsientos = cantidadAsientos;
            this._alcance = alcance;
            this._costoKm = costoKm;
        }
    }
}
