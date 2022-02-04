using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Entidades;
using Ventas.Modelo;

namespace Ventas.Controlador
{
    class ConfiguracionController
    {
        ///<summary>
        ///Ejecuta el Api para obtener la configuracion existente en la BD
        ///</summary>
        ///<returns>Objeto de tipo Configuracion con la configuracion obtenida</returns>
        public Configuracion ObtenerConfiguracion()
        {
            List<Configuracion> listaconfig = new List<Configuracion>();
            Configuracion config = new Configuracion();
            string respuesta = string.Empty;
            int status = 0;

            try
            {
                string uri = ConfigurationSettings.AppSettings.Get("apiventas") + "/api/configuracion";
                respuesta = ApiRest.Request(uri, "GET", null, ref status);
                listaconfig = JsonConvert.DeserializeObject<List<Configuracion>>(respuesta);
                if(listaconfig.Count > 0)
                {
                    config = listaconfig[0];
                }
            }
            catch (Exception)
            {
                throw;
            }

            return config;
        }

        ///<summary>
        ///Ejecuta el Api para registrar la configuracion en la BD
        ///</summary>
        ///<returns>Booleano indicando si la configuracion fue actualizada con exito</returns>
        public bool RegistrarConfiguracion(Configuracion _configuracion)
        {
            bool exito = false;
            string data = string.Empty;
            string respuesta = string.Empty;
            int status = 0;

            try
            {
                string uri = ConfigurationSettings.AppSettings.Get("apiventas") + "/api/configuracion";
                data = JsonConvert.SerializeObject(_configuracion);
                respuesta = ApiRest.Request(uri, "POST", data, ref status);
                if (status == 201)
                {
                    exito = true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return exito;
        }

        ///<summary>
        ///Ejecuta el Api para actualizar la configuracion registrada en la BD
        ///</summary>
        ///<returns>Booleano indicando si la configuracion fue actualizada con exito</returns>
        public bool ActualizarConfiguracion(Configuracion _configuracion)
        {
            bool exito = false;
            string data = string.Empty;
            string respuesta = string.Empty;
            int status = 0;

            try
            {
                string uri = ConfigurationSettings.AppSettings.Get("apiventas") + "/api/configuracion/"+_configuracion.IdConfiguracion;
                data = JsonConvert.SerializeObject(_configuracion);
                respuesta = ApiRest.Request(uri, "PUT", data, ref status);
                if (status == 204)
                {
                    exito = true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return exito;
        }
    }
}
