using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Modelo
{
    public static class ApiRest
    {
        ///<summary>
        ///Metodo generico para consumir el Back y consultar la BD
        ///</summary>
        ///<returns>string con formato json con la respuesta obtenida</returns>
        public static string Request(string uri, string method, string data,ref int status)
        {
            string respuesta = string.Empty;
            
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                if(method == "POST" || method == "PUT")
                {
                    byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                    request.ContentLength = dataBytes.Length;
                    request.ContentType = "application/json";
                    request.Method = method;

                    using (Stream requestBody = request.GetRequestStream())
                    {
                        requestBody.Write(dataBytes, 0, dataBytes.Length);
                    }
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    status = (int)response.StatusCode;
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        respuesta = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return respuesta;
        }
    }
}
