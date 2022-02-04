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

namespace Ventas.Vista.Configuracion
{
    public partial class Configuracion : Form
    {
        Entidades.Configuracion config;
        ConfiguracionController controller = new ConfiguracionController();
        public Configuracion()
        {
            InitializeComponent();
        }

        private void Configuracion_Load(object sender, EventArgs e)
        {
            MostrarConfiguracion();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(config.IdConfiguracion > 0)
            {
                ActualizarConfiguracion();
            }
            else
            {
                RegistrarConfiguracion();
            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CerrarVentana();
        }

        ///<summary>
        ///Muestra en pantalla la configuracion existente
        ///</summary>
        ///<returns>void</returns>
        private void MostrarConfiguracion()
        {
            try
            {
                config = controller.ObtenerConfiguracion();
                if(config.IdConfiguracion > 0)
                {
                    txtTasa.Text = config.Tasa.ToString();
                    txtEnganche.Text = config.Enganche.ToString();
                    txtPlazo.Text = config.Plazo.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///<summary>
        ///Registra por medio del controlador la configuracion en la BD
        ///</summary>
        ///<returns>void</returns>
        private void RegistrarConfiguracion()
        {
            try
            {
                if (ValidarDatosCapturados())
                {
                    config.Tasa = Decimal.Round(Convert.ToDecimal(txtTasa.Text.Trim()), 2);
                    config.Enganche = int.Parse(txtEnganche.Text.Trim());
                    config.Plazo = int.Parse(txtPlazo.Text.Trim());

                    if (controller.RegistrarConfiguracion(config))
                    {
                        MessageBox.Show("La configuración ha sido registrada", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///<summary>
        ///Actualiza por medio del controlador la configuracion existente en la BD
        ///</summary>
        ///<returns>void</returns>
        private void ActualizarConfiguracion()
        {
            try
            {
                if(ValidarDatosCapturados())
                {
                    config.Tasa = Decimal.Round(Convert.ToDecimal(txtTasa.Text.Trim()),2);
                    config.Enganche = int.Parse(txtEnganche.Text.Trim());
                    config.Plazo = int.Parse(txtPlazo.Text.Trim());

                    if (controller.ActualizarConfiguracion(config))
                    {
                        MessageBox.Show("La configuración ha sido registrada", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///<summary>
        ///Valida que los datos obligatorios esten capturados en pantalla
        ///</summary>
        ///<returns>boleano indicando si la captura es correcta</returns>
        private bool ValidarDatosCapturados()
        {
            bool respuesta = false;

            try
            {
                if(string.IsNullOrEmpty(txtTasa.Text.Trim()))
                {
                    MessageBox.Show("Favor de ingresar TASA FINANCIAMIENTO es obligatorio", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTasa.Focus();
                }
                else if(string.IsNullOrEmpty(txtEnganche.Text.Trim()))
                {
                    MessageBox.Show("Favor de ingresar % ENGANCHE es obligatorio", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEnganche.Focus();
                }
                else if(string.IsNullOrEmpty(txtPlazo.Text.Trim()))
                {
                    MessageBox.Show("Favor de ingresar PLAZO es obligatorio", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPlazo.Focus();
                }
                else
                {
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return respuesta;
        }

        ///<summary>
        ///Pregunta si desea cerrar la ventana y cierra la pantalla
        ///</summary>
        ///<returns>void</returns>
        private void CerrarVentana()
        {
            DialogResult result = MessageBox.Show("Desea salir de la pantalla actual?", "Configuracion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
        ///Valida que el dato capturado sea solo numeros y un punto decimal
        ///</summary>
        ///<returns>void</returns>
        private void SoloNumerosDecimal(KeyPressEventArgs e)
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
                if (txtTasa.Text.Trim().Contains("."))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void txtTasa_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumerosDecimal(e);
        }

        private void txtEnganche_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros(e);
        }

        private void txtPlazo_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros(e);
        }

        #endregion DatosCajasDeTexto
    }
}
