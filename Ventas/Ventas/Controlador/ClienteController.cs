using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Ventas.Entidades;
using Ventas.Modelo;

namespace Ventas.Controlador
{
    class ClienteController
    {
        ///<summary>
        ///Ejecuta el Api para obtiener un listado de todos los clientes
        ///</summary>
        ///<returns>Lista de objetos tipo Cliente</returns>
        public List<Cliente> ObtenerClientes()
        {
            List<Cliente> lista = new List<Cliente>();
            string respuesta = string.Empty;
            int status = 0;

            try
            {
                string uri = ConfigurationSettings.AppSettings.Get("apiventas") + "/api/clientes";
                respuesta = ApiRest.Request(uri,"GET",null, ref status);
                lista = JsonConvert.DeserializeObject<List<Cliente>>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }

            return lista;
        }

        ///<summary>
        ///Ejecuta el Api para registrar un nuevo cliente en la BD
        ///</summary>
        ///<returns>Booleano indicando si el cliente fue insertado con exito</returns>
        public bool AgregarCliente(Cliente _cliente)
        {
            bool exito = false;
            string data = string.Empty;
            string respuesta = string.Empty;
            int status = 0;

            try
            {
                string uri = ConfigurationSettings.AppSettings.Get("apiventas") + "/api/clientes";
                data = JsonConvert.SerializeObject(_cliente);
                respuesta = ApiRest.Request(uri, "POST", data, ref status);
                if (status == 201 && !string.IsNullOrEmpty(respuesta))
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
        ///Ejecuta el Api para actualiza el cliente en la BD
        ///</summary>
        ///<returns>Booleano indicando si el cliente fue actualizado con exito</returns>
        public bool ActualizarCliente(Cliente _cliente)
        {
            bool exito = false;
            string data = string.Empty;
            string respuesta = string.Empty;            
            int status = 0;

            try
            {
                string uri = ConfigurationSettings.AppSettings.Get("apiventas") + "/api/clientes/" + _cliente.IdCliente;
                data = JsonConvert.SerializeObject(_cliente);
                respuesta = ApiRest.Request(uri,"PUT", data, ref status);
                if(status == 204)
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
