using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Entidades
{
    public class Configuracion
    {
        public int IdConfiguracion { get; set; }
        public decimal Tasa { get; set; }
        public int Enganche { get; set; }
        public int Plazo { get; set; }
    }
}
