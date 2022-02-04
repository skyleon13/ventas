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
    public partial class NuevaVenta : Form
    {
        ClienteController cliente = new ClienteController();
        ProductoController producto = new ProductoController();
        private Entidades.Producto prod;
        ConfiguracionController configController = new ConfiguracionController();
        private Entidades.Configuracion config;
        VentaController ventas = new VentaController();
        private Entidades.Venta venta;

        private static List<Cliente> listaClientes;
        private static List<Producto> listaProductos;
        private decimal enganche = 0;
        private decimal bonificacion = 0;
        private decimal total = 0;

        public NuevaVenta()
        {
            InitializeComponent();
            config = configController.ObtenerConfiguracion();
        }

        private void NuevaVenta_Load(object sender, EventArgs e)
        {
            ObtenerClientes();
            ObtenerProductos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarProducto();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarVenta();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CerrarVentana();
        }

        private void dgvPedido_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6 && e.RowIndex >= 0)
            {
                EliminarProducto(e.RowIndex);
            }
        }

        ///<summary>
        ///Obtiene por medo del controlador la lista de clistes y los carga al combo clientes
        ///</summary>
        ///<returns>void</returns>
        private void ObtenerClientes()
        {
            try
            {
                listaClientes = cliente.ObtenerClientes();
                cbCliente.Items.Clear();

                cbCliente.DataSource = listaClientes;
                cbCliente.ValueMember = "IdCliente";
                cbCliente.DisplayMember = "NombreCompleto";

                cbCliente.AutoCompleteCustomSource = LoadAutoCompleteClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///<summary>
        ///Genera una coleccion de las sugerencias para mostrar en el combo de clientes
        ///</summary>
        ///<returns>AutoCompleteStringCollection con la informacion</returns>
        public static AutoCompleteStringCollection LoadAutoCompleteClientes()
        {
            AutoCompleteStringCollection stringCol = new AutoCompleteStringCollection();

            foreach (Cliente item in listaClientes)
            {
                stringCol.Add(item.NombreCompleto);
            }

            return stringCol;
        }

        ///<summary>
        ///Obtiene por medio el controlador la lista de productos y los carga en el combo productos
        ///</summary>
        ///<returns>void</returns>
        private void ObtenerProductos()
        {
            try
            {
                listaProductos = producto.ObtenerProductos();
                cbProducto.Items.Clear();

                cbProducto.DataSource = listaProductos;
                cbProducto.ValueMember = "IdProducto";
                cbProducto.DisplayMember = "ProductoModelo";

                cbProducto.AutoCompleteCustomSource = LoadAutoCompleteProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///<summary>
        ///Genera una coleccion de las sugerencias para mostrar en el combo de productos
        ///</summary>
        ///<returns>AutoCompleteStringCollection con la informacion</returns>
        public static AutoCompleteStringCollection LoadAutoCompleteProductos()
        {
            AutoCompleteStringCollection stringCol = new AutoCompleteStringCollection();

            foreach (Producto item in listaProductos)
            {
                stringCol.Add(item.ProductoModelo);
            }

            return stringCol;
        }


        ///<summary>
        ///Registra un nuevo producto en el pedido actual
        ///</summary>
        ///<returns>void</returns>
        private void AgregarProducto()
        {
            try
            {
                int index = 0;
                decimal precio = 0;
                decimal importe = 0;

                Producto prod = (Producto)cbProducto.SelectedItem;
                if(prod != null)
                {
                    int cantidad = RevisarCantidadEnPedido(prod.IdProducto, ref index);

                    if (ValidarExistenciaProducto(prod, cantidad))
                    {
                        if (cantidad > 0)
                        {
                            dgvPedido.Rows.RemoveAt(index);
                        }

                        cantidad++;
                        precio = CalcularPrecioProducto(prod.Precio);
                        precio = decimal.Round(precio, 2);
                        importe = precio * cantidad;
                        importe = decimal.Round(importe, 2);

                        dgvPedido.Rows.Add(prod.IdProducto, prod.Descripcion, prod.Modelo,
                                    cantidad, precio, importe, Properties.Resources.eliminar,
                                    prod.Existencia, prod.Precio);

                        CalcularTotales();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///<summary>
        ///Valida que la existencia del producto sea disponible para agregar al pedido
        ///</summary>
        ///<returns>boleano indicando si se puede agregar el producto</returns>
        private bool ValidarExistenciaProducto(Producto prod, int cantidadEnPedido)
        {
            if((prod.Existencia-cantidadEnPedido) < 1)
            {
                MessageBox.Show("El producto seleccionado no cuenta con existencia, favor de verificar","Venta",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false;
            }
            else
            {
                return true;
            }
        }

        ///<summary>
        ///Revisa si el pedido ya cuenta con el producto agregado
        ///</summary>
        ///<returns>int con la cantidad actual en el pedido</returns>
        private int RevisarCantidadEnPedido(int idProducto, ref int index)
        {
            int cantidadEnPedido = 0;
            try
            {
                foreach (DataGridViewRow item in dgvPedido.Rows)
                {
                    if (int.Parse(item.Cells[0].Value.ToString()) == idProducto)
                    {
                        cantidadEnPedido = int.Parse(item.Cells[3].Value.ToString());
                        index = dgvPedido.Rows.IndexOf(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return cantidadEnPedido;
        }

        ///<summary>
        ///Calcula el precio del producto con la formula correspondiente
        ///</summary>
        ///<returns>decimal con el precio del producto</returns>
        private decimal CalcularPrecioProducto(decimal precioProducto)
        {
            decimal precio = 0;

            try
            {
                precio = precioProducto * (1 + (config.Tasa * config.Plazo) / 100);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return precio;
        }

        ///<summary>
        ///Elimina el producto seleccionado del pedido actual
        ///</summary>
        ///<returns>void</returns>
        private void EliminarProducto(int index)
        {
            try
            {
                dgvPedido.Rows.RemoveAt(index);
                CalcularTotales();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///<summary>
        ///Calcula el enganche,bonificacion y total con las formulas correspondientes
        ///</summary>
        ///<returns>void</returns>
        private void CalcularTotales()
        {
            decimal importes = 0;

            try
            {
                enganche = 0;
                bonificacion = 0;
                total = 0;
                foreach (DataGridViewRow item in dgvPedido.Rows)
                {
                    importes += Convert.ToDecimal(item.Cells[5].Value.ToString());
                }
                enganche = Convert.ToDecimal(config.Enganche) / 100 * importes;
                enganche = decimal.Round(enganche, 2);
                txtEnganche.Text = enganche.ToString();

                bonificacion = enganche * ((config.Tasa * Convert.ToDecimal(config.Plazo)) / 100);
                bonificacion = decimal.Round(bonificacion, 2);
                txtBonificacion.Text = bonificacion.ToString();

                total = importes - enganche - bonificacion;
                total = decimal.Round(total, 2);
                txtTotal.Text = total.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///<summary>
        ///Registra por medio del contrlador una nueva venta en la BD
        ///</summary>
        ///<returns>void</returns>
        private void GuardarVenta()
        {
            try
            {
                Cliente cliente = (Cliente)cbCliente.SelectedItem;
                int items = dgvPedido.Rows.Count;
                int idNuevaVenta = 0;

                if(cliente != null && cliente.IdCliente > 0 && items > 0)
                {
                    venta = new Venta();
                    venta.IdCliente = cliente.IdCliente;
                    venta.Enganche = enganche;
                    venta.Bonificacion = bonificacion;
                    venta.Total = total;

                    if (ventas.AgregarVenta(venta, ref idNuevaVenta))
                    {
                        if (BajarExistencias())
                        {
                            MessageBox.Show("Tu venta ha sido registrada correctamente");
                            this.Close();
                        }
                        else
                        {
                            ventas.EliminarVenta(idNuevaVenta);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///<summary>
        ///Actualiza por medio del controlador las existencias de los productos vendidos
        ///</summary>
        ///<returns>boleando indicando si se actualizaron con exito</returns>
        private bool BajarExistencias()
        {
            bool resultado = true;
            int existenciaAnterior = 0;
            int existenciaVenta = 0;
            int existenciaNueva = 0;
            try
            {
                foreach (DataGridViewRow item in dgvPedido.Rows)
                {
                    existenciaAnterior = int.Parse(item.Cells[7].Value.ToString());
                    existenciaVenta = int.Parse(item.Cells[3].Value.ToString());
                    existenciaNueva = existenciaAnterior - existenciaVenta;

                    prod = new Producto();
                    prod.IdProducto = int.Parse(item.Cells[0].Value.ToString());
                    prod.Descripcion = item.Cells[1].Value.ToString();
                    prod.Modelo = item.Cells[2].Value.ToString();
                    prod.Precio = decimal.Parse(item.Cells[8].Value.ToString());
                    prod.Existencia = existenciaNueva;

                    if (!producto.ActualizarProducto(prod))
                    {
                        resultado = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return resultado;
        }

        ///<summary>
        ///Pregunta al usuario si desea cerrar la ventana y cierra la pantalla
        ///</summary>
        ///<returns>void</returns>
        private void CerrarVentana()
        {
            DialogResult result = MessageBox.Show("Desea salir de la pantalla actual?", "Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
