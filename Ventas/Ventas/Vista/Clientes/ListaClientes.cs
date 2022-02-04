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

namespace Ventas.Vista.Clientes
{
    public partial class ListaClientes : Form
    {
        ClienteController controller = new ClienteController();

        public ListaClientes()
        {
            InitializeComponent();
        }

        private void ListaClientes_Load(object sender, EventArgs e)
        {
            MostrarClientesRegistrados();
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            NuevoCliente();
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EditarCliente(e);
        }

        ///<summary>
        ///Obtiene por medio del controlador una lista de los clientes existentes y los muestra en el grid
        ///</summary>
        ///<returns>void</returns>
        private void MostrarClientesRegistrados()
        {
            try
            {
                StringBuilder nombreCompleto;
                List<Cliente> listaClientes = controller.ObtenerClientes();
                dgvClientes.Rows.Clear();

                foreach (var cte in listaClientes)
                {
                    nombreCompleto = new StringBuilder();
                    nombreCompleto.Append(cte.Nombre);
                    nombreCompleto.Append(" ");
                    nombreCompleto.Append(cte.ApellidoPat);
                    nombreCompleto.Append(" ");
                    nombreCompleto.Append(cte.ApellidoMat);

                    dgvClientes.Rows.Add(cte.IdCliente, nombreCompleto.ToString(), Properties.Resources.editar,
                        cte.Nombre, cte.ApellidoPat, cte.ApellidoMat, cte.RFC);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///<summary>
        ///Carga la pantalla para agregar nuevo cliente
        ///</summary>
        ///<returns>void</returns>
        private void NuevoCliente()
        {
            try
            {
                NuevoCliente nuevo = new NuevoCliente();
                nuevo.ShowDialog();
                MostrarClientesRegistrados();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///<summary>
        ///Valida la celda donde se dio click y levanta la pantalla para editar un cliente.
        ///</summary>
        ///<returns>void</returns>
        private void EditarCliente(DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2 && e.RowIndex >= 0)
                {
                    Cliente _cliente = new Cliente();
                    _cliente.IdCliente = int.Parse(dgvClientes.Rows[e.RowIndex].Cells[0].Value.ToString());
                    _cliente.Nombre = dgvClientes.Rows[e.RowIndex].Cells[3].Value.ToString();
                    _cliente.ApellidoPat = dgvClientes.Rows[e.RowIndex].Cells[4].Value.ToString();
                    _cliente.ApellidoMat = dgvClientes.Rows[e.RowIndex].Cells[5].Value.ToString();
                    _cliente.RFC = dgvClientes.Rows[e.RowIndex].Cells[6].Value.ToString();

                    EditarCliente editar = new EditarCliente(_cliente);
                    editar.ShowDialog();
                    MostrarClientesRegistrados();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
