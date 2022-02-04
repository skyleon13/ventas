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
    class ProductoController
    {
        ///<summary>
        ///Ejecuta el Api para obtener los productos existentes en la BD
        ///</summary>
        ///<returns>Lista de objetos tipo Producto con todos los productos existentes</returns>
        public List<Producto> ObtenerProductos()
        {
            List<Producto> lista = new List<Producto>();
            string respuesta = string.Empty;
            int status = 0;

            try
            {
                string uri = ConfigurationSettings.AppSettings.Get("apiventas") + "/api/productos";
                respuesta = ApiRest.Request(uri, "GET", null, ref status);
                lista = JsonConvert.DeserializeObject<List<Producto>>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }

            return lista;
        }

        ///<summary>
        ///Ejecuta el Api para registrar un nuevo producto en la BD
        ///</summary>
        ///<returns>Booleano indicando si el producto fue registrado con exito</returns>
        public bool AgregarProducto(Producto _producto)
        {
            bool exito = false;
            string data = string.Empty;
            string respuesta = string.Empty;
            int status = 0;

            try
            {
                string uri = ConfigurationSettings.AppSettings.Get("apiventas") + "/api/productos";
                data = JsonConvert.SerializeObject(_producto);
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
        ///Ejecuta el Api para actualizar un producto en la BD
        ///</summary>
        ///<returns>Booleano indicando si el producto fue actualizado con exito</returns>
        public bool ActualizarProducto(Producto _producto)
        {
            bool exito = false;
            string data = string.Empty;
            string respuesta = string.Empty;
            int status = 0;

            try
            {
                string uri = ConfigurationSettings.AppSettings.Get("apiventas") + "/api/productos/" + _producto.IdProducto;
                data = JsonConvert.SerializeObject(_producto);
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
