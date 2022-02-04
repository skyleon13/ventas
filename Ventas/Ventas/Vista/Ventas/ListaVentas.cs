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
using Ventas.Entidades;

namespace Ventas.Vista.Ventas
{
    public partial class ListaVentas : Form
    {
        VentaController controller = new VentaController();
        public ListaVentas()
        {
            InitializeComponent();
        }

        private void ListaVentas_Load(object sender, EventArgs e)
        {
            MostrarVentas();
        }

        private void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            NuevaVenta();
        }

        ///<summary>
        ///Obtiene por medio del controlador todas las ventas registradas y las muestra en el grid
        ///</summary>
        ///<returns>void</returns>
        private void MostrarVentas()
        {
            try
            {
                List<Venta> listaVentas = controller.ObtenerVentas();
                dgvVentas.Rows.Clear();

                foreach (var item in listaVentas)
                {
                    dgvVentas.Rows.Add(item.IdVenta, item.IdCliente,item.Enganche, item.Bonificacion, item.Total);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///<summary>
        ///Carga la pantalla para realizar una nueva venta
        ///</summary>
        ///<returns>void</returns>
        private void NuevaVenta()
        {
            try
            {
                NuevaVenta venta = new NuevaVenta();
                venta.ShowDialog();
                MostrarVentas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
