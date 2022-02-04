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
    public partial class EditarProducto : Form
    {
        private Producto productoActualizar;
        ProductoController controller = new ProductoController();

        public EditarProducto(Producto _producto)
        {
            InitializeComponent();
            productoActualizar = _producto;
        }

        private void EditarProducto_Load(object sender, EventArgs e)
        {
            txtId.Text = productoActualizar.IdProducto.ToString();
            txtDescripcion.Text = productoActualizar.Descripcion;
            txtModelo.Text = productoActualizar.Modelo;
            txtPrecio.Text = productoActualizar.Precio.ToString();
            txtExistencia.Text = productoActualizar.Existencia.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ActualizarProducto();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CerrarVentana();
        }

        ///<summary>
        ///Actualiza por medio del controlador un producto en la BD
        ///</summary>
        ///<returns>void</returns>
        private void ActualizarProducto()
        {
            try
            {
                productoActualizar.Descripcion = txtDescripcion.Text.Trim();
                productoActualizar.Modelo = txtModelo.Text.Trim();
                productoActualizar.Precio = Convert.ToDecimal(txtPrecio.Text.Trim());
                productoActualizar.Existencia = int.Parse(txtExistencia.Text.Trim());

                if (controller.ActualizarProducto(productoActualizar))
                {
                    MessageBox.Show("El producto ha sido actualizado correctamente", "Editar Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Editar Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///<summary>
        ///Valida que los datos obligatorios esten capturados en pantalla
        ///</summary>
        ///<returns>boleano indicando si los datos son correctos</returns>
        private bool ValidarDatosCapturados()
        {
            bool respuesta = false;

            try
            {
                if (string.IsNullOrEmpty(txtDescripcion.Text.Trim()))
                {
                    MessageBox.Show("Favor de ingresar DESCRIPCION es obligatorio", "Editar Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDescripcion.Focus();
                }
                else if (string.IsNullOrEmpty(txtModelo.Text.Trim()))
                {
                    MessageBox.Show("Favor de ingresar MODELO es obligatorio", "Editar Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtModelo.Focus();
                }
                else if (string.IsNullOrEmpty(txtPrecio.Text.Trim()))
                {
                    MessageBox.Show("Favor de ingresar PRECIO es obligatorio", "Editar Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPrecio.Focus();
                }
                else if (string.IsNullOrEmpty(txtExistencia.Text.Trim()))
                {
                    MessageBox.Show("Favor de ingresar EXISTENCIA es obligatorio", "Editar Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtExistencia.Focus();
                }
                else
                {
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Editar Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return respuesta;
        }

        ///<summary>
        ///Pregunta al usuario si desea cerrar la ventana y cierra la pantalla
        ///</summary>
        ///<returns>void</returns>
        private void CerrarVentana()
        {
            DialogResult result = MessageBox.Show("Desea salir de la pantalla actual?", "Editar Producto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        #region DatosCajasDeTexto
        ///<summary>
        ///Valida que el dato capturado sea solo numeros
        ///</summary>
        ///<returns>void</returns>
        private void SoloNumeros(KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        ///<summary>
        ///Valida que el dato capturado sea numeros y letras
        ///</summary>
        ///<returns>void</returns>
        private void SoloTextoYNumeros(KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) &&
                !(char.IsNumber(e.KeyChar)) &&
                (e.KeyChar != (char)Keys.Space) &&
                (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        ///<summary>
        ///Valida que el dato capturado sea numeros y un punto decimal
        ///</summary>
        ///<returns>void</returns>
        private void SoloNumerosDecimal(KeyPressEventArgs e, TextBox caja)
        {
            if (!(char.IsNumber(e.KeyChar)) &&
                (e.KeyChar != (char)Keys.Back) &&
                (e.KeyChar != (char)46))
            {
                e.Handled = true;
                return;
            }
            else if (e.KeyChar == (char)46)
            {
                if (caja.Text.Trim().Contains("."))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloTextoYNumeros(e);
        }

        private void txtModelo_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloTextoYNumeros(e);
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumerosDecimal(e, txtPrecio);
        }

        private void txtExistencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros(e);
        }

        #endregion DatosCajasDeTexto
    }
}
