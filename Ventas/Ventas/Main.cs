using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ventas.Controlador;
using Ventas.Vista.Clientes;
using Ventas.Vista.Configuracion;
using Ventas.Vista.Productos;
using Ventas.Vista.Ventas;

namespace Ventas
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ListaClientes clientes = new ListaClientes();
            clientes.ShowDialog();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            ListaProductos productos = new ListaProductos();
            productos.ShowDialog();
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            Configuracion config = new Configuracion();
            config.ShowDialog();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            ListaVentas ventas = new ListaVentas();
            ventas.ShowDialog();
        }
    }
}
