using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Ordenes
{
    public class OrdenPasajePorFecha : IComparer<Pasaje>
    {
        public int Compare(Pasaje? x, Pasaje? y)
        {
            return x.Fecha.CompareTo(y.Fecha);
        }
    }
}
