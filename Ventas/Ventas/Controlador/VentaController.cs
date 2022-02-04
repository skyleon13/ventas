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
    class VentaController
    {
        ///<summary>
        ///Ejecuta el Api para obtener un listado de las ventas en la BD
        ///</summary>
        ///<returns>Lista de objetos tipo Venta con todo el listado existente</returns>
        public List<Venta> ObtenerVentas()
        {
            List<Venta> lista = new List<Venta>();
            string respuesta = string.Empty;
            int status = 0;

            try
            {
                string uri = ConfigurationSettings.AppSettings.Get("apiventas") + "/api/ventas";
                respuesta = ApiRest.Request(uri, "GET", null, ref status);
                lista = JsonConvert.DeserializeObject<List<Venta>>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }

            return lista;
        }

        ///<summary>
        ///Ejecuta el Api para registrar una nueva venta en la BD
        ///</summary>
        ///<returns>Booleano indicando si la venta fue registrada con exito</returns>
        public bool AgregarVenta(Venta _venta, ref int idVenta)
        {
            bool exito = false;
            string data = string.Empty;
            string respuesta = string.Empty;
            int status = 0;
            Venta venta;

            try
            {
                string uri = ConfigurationSettings.AppSettings.Get("apiventas") + "/api/ventas";
                data = JsonConvert.SerializeObject(_venta);
                respuesta = ApiRest.Request(uri, "POST", data, ref status);
                if (status == 201 && !string.IsNullOrEmpty(respuesta))
                {
                    venta = JsonConvert.DeserializeObject<Venta>(respuesta);
                    idVenta = venta.IdVenta;
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
        ///Ejecuta el Api para eliminar una venta en la BD
        ///</summary>
        ///<returns>Booleano indicando si la venta fue eliminada con exito</returns>
        public bool EliminarVenta(int idVenta)
        {
            bool exito = false;
            string data = string.Empty;
            string respuesta = string.Empty;
            int status = 0;

            try
            {
                string uri = ConfigurationSettings.AppSettings.Get("apiventas") + "/api/ventas/" + idVenta;
                respuesta = ApiRest.Request(uri, "DELETE", data, ref status);
                if (status == 200 && !string.IsNullOrEmpty(respuesta))
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
