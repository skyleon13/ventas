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
    public partial class NuevoCliente : Form
    {
        private Cliente clienteNuevo;
        ClienteController controller = new ClienteController();
        public NuevoCliente()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            AgregarCliente();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CerrarVentana();
        }

        ///<summary>
        ///Valida que esten los datos capturados y a traves del controlador agrega el cliente a la BD
        ///</summary>
        ///<returns>void</returns>
        private void AgregarCliente()
        {
            try
            {
                if(ValidarDatosCapturados())
                {
                    clienteNuevo = new Cliente();
                    clienteNuevo.Nombre = txtNombre.Text.Trim();
                    clienteNuevo.ApellidoPat = txtApellidoPat.Text.Trim();
                    clienteNuevo.ApellidoMat = txtApellidoMat.Text.Trim();
                    clienteNuevo.RFC = txtRfc.Text.Trim();

                    if (controller.AgregarCliente(clienteNuevo))
                    {
                        MessageBox.Show("El cliente ha sido registrado correctaente", "Nuevo Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nuevo Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        ///<summary>
        ///Valida que todos los campos obligatorios tengan informacion capturada
        ///</summary>
        ///<returns>boleano indicando si la captura es correcta</returns>
        private bool ValidarDatosCapturados()
        {
            bool respuesta = false;

            try
            {
                if(string.IsNullOrEmpty(txtNombre.Text.Trim()))
                {
                    MessageBox.Show("Favor de ingresar NOMBRE es obligatorio","Nuevo Cliente",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    txtNombre.Focus();
                }
                else if (string.IsNullOrEmpty(txtApellidoPat.Text.Trim()))
                {
                    MessageBox.Show("Favor de ingresar APELLIDO PATERNO es obligatorio", "Nuevo Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtApellidoPat.Focus();
                }
                else if(string.IsNullOrEmpty(txtApellidoMat.Text.Trim()))
                {
                    MessageBox.Show("Favor de ingresar APELLIDO MATERNO es obligatorio", "Nuevo Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtApellidoMat.Focus();
                }
                else if(string.IsNullOrEmpty(txtRfc.Text.Trim()))
                {
                    MessageBox.Show("Favor de ingresar RFC es obligatorio", "Nuevo Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRfc.Focus();
                }
                else
                {
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nuevo Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return respuesta;
        }

        ///<summary>
        ///Pregunta si desea cerrar la ventana y de ser asi cierra la pantalla
        ///</summary>
        ///<returns>void</returns>
        private void CerrarVentana()
        {
            DialogResult result = MessageBox.Show("Desea salir de la pantalla actual?", "Nuevo Cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }


        #region DatosCajaDeTexto
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
        ///Valida que el dato tecleado sea numeros y letras
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
        #endregion DatosCajaDeTexto
    }
}
