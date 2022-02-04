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
    public partial class EditarCliente : Form
    {
        private Cliente clienteActualizar;
        ClienteController controller = new ClienteController();

        public EditarCliente(Cliente _cliente)
        {
            InitializeComponent();
            clienteActualizar = _cliente;
        }

        private void EditarCliente_Load(object sender, EventArgs e)
        {
            txtId.Text = clienteActualizar.IdCliente.ToString();
            txtNombre.Text = clienteActualizar.Nombre;
            txtApellidoPat.Text = clienteActualizar.ApellidoPat;
            txtApellidoMat.Text = clienteActualizar.ApellidoMat;
            txtRfc.Text = clienteActualizar.RFC;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ActualizarCliente();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CerrarVentana();
        }

        ///<summary>
        ///Actualiza por medio del controlador un cliente en la BD
        ///</summary>
        ///<returns>void</returns>
        private void ActualizarCliente()
        {
            try
            {
                if(ValidarDatosCapturados())
                {
                    clienteActualizar.Nombre = txtNombre.Text.Trim();
                    clienteActualizar.ApellidoPat = txtApellidoPat.Text.Trim();
                    clienteActualizar.ApellidoMat = txtApellidoMat.Text.Trim();
                    clienteActualizar.RFC = txtRfc.Text.Trim();

                    if (controller.ActualizarCliente(clienteActualizar))
                    {
                        MessageBox.Show("El cliente ha sido actualizado correctaente", "Nuevo Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nuevo Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///<summary>
        ///Valida que todos los datos obligatorios esten capturados en pantalla
        ///</summary>
        ///<returns>boleano indicando si la captura es correcta</returns>
        private bool ValidarDatosCapturados()
        {
            bool respuesta = false;
            try
            {
                if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                {
                    MessageBox.Show("Favor de ingresar NOMBRE es obligatorio", "Editar Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNombre.Focus();
                }
                else if (string.IsNullOrEmpty(txtApellidoPat.Text.Trim()))
                {
                    MessageBox.Show("Favor de ingresar APELLIDO PATERNO es obligatorio", "Editar Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtApellidoPat.Focus();
                }
                else if (string.IsNullOrEmpty(txtApellidoMat.Text.Trim()))
                {
                    MessageBox.Show("Favor de ingresar APELLIDO MATERNO es obligatorio", "Editar Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtApellidoMat.Focus();
                }
                else if (string.IsNullOrEmpty(txtRfc.Text.Trim()))
                {
                    MessageBox.Show("Favor de ingresar RFC es obligatorio", "Editar Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRfc.Focus();
                }
                else
                {
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Editar Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return respuesta;
        }

        ///<summary>
        ///Pregunta si desea cerrarse la ventana y cierra la pantalla actual
        ///</summary>
        ///<returns>void</returns>
        private void CerrarVentana()
        {
            DialogResult result = MessageBox.Show("Desea salir de la pantalla actual?", "Editar Cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }


        #region DatosCajasDeTexto
        ///<summary>
        ///Valida que el dato tecleado sea solo letras
        ///</summary>
        ///<returns>void</returns>
        private void SoloTexto(KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) &&
                (e.KeyChar != (char)Keys.Space) && 
                (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        ///<summary>
        ///Vaida que el dato tecleado sea solo numeros y letras
        ///</summary>
        ///<returns>void</returns>
        private void SoloNumerosYTexto(KeyPressEventArgs e)
        {
            if (!(char.IsLetterOrDigit(e.KeyChar)) &&
                (e.KeyChar != (char)Keys.Space) && 
                (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloTexto(e);
        }

        private void txtApellidoPat_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloTexto(e);
        }

        private void txtApellidoMat_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloTexto(e);
        }

        private void txtRfc_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumerosYTexto(e);
        }
        #endregion DatosCajasDeTexto
    }
}
