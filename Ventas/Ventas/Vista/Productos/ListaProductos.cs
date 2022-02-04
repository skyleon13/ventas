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

namespace Ventas.Vista.Productos
{
    public partial class ListaProductos : Form
    {
        ProductoController controller = new ProductoController();
        public ListaProductos()
        {
            InitializeComponent();
        }

        private void ListaProductos_Load(object sender, EventArgs e)
        {
            MostrarProductosRegistrados();
        }

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            NuevoProducto();
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EditarProducto(e);
        }

        ///<summary>
        ///Obtiene por medio del controlador una lista de todos los productos en la BD y los muestra en el grid
        ///</summary>
        ///<returns>void</returns>
        private void MostrarProductosRegistrados()
        {
            try
            {
                List<Producto> listaProductos = controller.ObtenerProductos();
                dgvProductos.Rows.Clear();

                foreach (var item in listaProductos)
                {
                    dgvProductos.Rows.Add(item.IdProducto,item.Descripcion,Properties.Resources.editar,
                        item.Modelo,item.Precio,item.Existencia);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Productos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///<summary>
        ///Carga la pantalla para registrar un nuevo producto
        ///</summary>
        ///<returns>void</returns>
        private void NuevoProducto()
        {
            try
            {
                NuevoProducto nuevo = new NuevoProducto();
                nuevo.ShowDialog();
                MostrarProductosRegistrados();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Productos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///<summary>
        ///Valida la celda seleccionada y muestra la pantalla para editar el producto
        ///</summary>
        ///<returns>void</returns>
        private void EditarProducto(DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2 && e.RowIndex >= 0)
                {
                    Producto _producto = new Producto();
                    _producto.IdProducto = int.Parse(dgvProductos.Rows[e.RowIndex].Cells[0].Value.ToString());
                    _producto.Descripcion = dgvProductos.Rows[e.RowIndex].Cells[1].Value.ToString();
                    _producto.Modelo = dgvProductos.Rows[e.RowIndex].Cells[3].Value.ToString();
                    _producto.Precio = Convert.ToDecimal(dgvProductos.Rows[e.RowIndex].Cells[4].Value.ToString());
                    _producto.Existencia = int.Parse(dgvProductos.Rows[e.RowIndex].Cells[5].Value.ToString());

                    EditarProducto editar = new EditarProducto(_producto);
                    editar.ShowDialog();
                    MostrarProductosRegistrados();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Productos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
