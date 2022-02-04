using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Entidades
{
    class Venta
    {
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public decimal Enganche { get; set; }
        public decimal Bonificacion { get; set; }
        public decimal Total { get; set; }
    }
}
