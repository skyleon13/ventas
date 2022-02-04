using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Entidades
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public string Modelo { get; set; }
        public decimal Precio { get; set; }
        public int Existencia { get; set; }
        public string ProductoModelo => $"{Descripcion} {Modelo}";
    }
}
