using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace NumeroFactorial.WebApi.Controllers
{
    public class FactController : ApiController
    {
        // GET api/fact
        public IEnumerable<string> Get()
        {
            return new string[] { "Coloque un número en la URL como parametro para sacar su factorial, ejemplo ", "'/api/fact/5'" };
        }

        // GET api/fact/5
        public HttpResponseMessage Get(ushort id)
        {   
            string msg = "";
            if (id >= 0)
            {
                string fact = "1";
                for (int i = 1; i <= id; i++)
                {
                    fact = strMult(fact, Convert.ToString(i));
                }

                msg += "<script>";
                msg += "Swal.fire('Factorial de "+id+"','" + Convert.ToString(fact) +"','success')";
                msg += "</script>";
            }

            var fileContents = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Content/Modal.html"));
            fileContents = fileContents.Replace(":msg:", msg);
            var response = new HttpResponseMessage();
            response.Content = new StringContent(fileContents);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/html");
            return response;
        }

        public string strMult(string numOne, string numTwo)
        { /* Función para multiplicar dos números en 'string' */

            /* Primario y Secundario */
            ushort pLength, sLength;
            string Primary, Secondary;

            /* El número primario es el de mayor longitud */
            if (numOne.Length >= numTwo.Length)
            {
                Primary = numOne;       // 1234
                Secondary = numTwo;     //  123
            }
            else
            {
                Primary = numTwo;       // 1234
                Secondary = numOne;     //  123
            }

            /* Longitud del Primario y Secundario */
            pLength = Convert.ToUInt16(Primary.Length);     // 1234 -> pLenght = 4
            sLength = Convert.ToUInt16(Secondary.Length);   // 123 -> sLenght = 3

            /* Contador de ayuda e inicialización del resultado en cero */
            ushort cnt = 0;
            string strResult = "0";

            for (int i = sLength-1; i >= 0 ; i--)
            {
                byte Two = Convert.ToByte(numTwo.Substring(i, 1));      // 123 -> 3,2,1
                string strAux = "";
                int aux = 0;
                for (int j = pLength - 1; j >= 0; j--)
                {
                    byte One = Convert.ToByte(Primary.Substring(j, 1));     // 1234 -> 4,3,2,1
                    string result = Convert.ToString((One * Two) + aux);        // (4 * 3) + aux = 12
                        
                    if(j!=0)
                    {
                        strAux = result.Substring(result.Length - 1) + strAux;      // 12 -> Se coloca el 2 y llevo 1 (strAux = 2, aux = 1)
                    }
                    else
                    {
                        strAux = result + strAux;
                    }
                                        
                    if (result.Length > 1)
                    {/* Cuando el resultado tiene 2 o más carácteres, ejemplo: 10, 100, 1000 */
                        aux = Convert.ToUInt16(result.Substring(0, result.Length - 1));     // 12 -> aux = 1
                    }
                    else
                    {
                        aux = 0;
                    }
                }

                strAux= addZero(strAux, cnt, true);
                /*  Se agregan ceros a la derecha
                * 
                *       123
                *      x 12
                *     ------
                *       246    -> 246
                *      123     -> 1230
                */

                strResult = strSum(strResult, strAux);
                /*  Se suman los resultados
                * 
                *       246    ->  246
                *      123     -> 1230
                *     ------
                *      1476
                */

                cnt++;
            }

            return strResult;
        }

        public string strSum(string numOne, string numTwo)
        {/* Función para sumar dos números en 'string' */

            /* Longitud del primario */
            ushort pLength = Convert.ToUInt16(numOne.Length);       // 123 -> pLength = 3

            /* Longitud del secundario */
            ushort sLength = Convert.ToUInt16(numTwo.Length);       // 12 -> sLength = 2

            if (pLength > sLength)
            {
                numTwo = addZero(numTwo, pLength - sLength);        // 12 -> 012
            }
            else if (pLength < sLength)
            {

                numOne = addZero(numOne, sLength - pLength);
            }

            string strReturn = "";
            ushort aux = 0;

            for (int i = numOne.Length - 1; i >= 0; i--)
            {
                byte One = Convert.ToByte(numOne.Substring(i, 1));      // 123 -> 3,2,1
                byte Two = Convert.ToByte(numTwo.Substring(i, 1));      // 012 -> 2,1,0
                byte Sum = Convert.ToByte(One + Two + aux);     //3+2 , 2+1 , 1+0
                aux = Convert.ToUInt16(Sum / 10);       // Sum=12, aux=1, strReturn=2
                if (i != 0)
                {
                    strReturn = Convert.ToString(Sum - 10 * aux) + strReturn;
                }
                else
                {
                    strReturn = Convert.ToString(Sum) + strReturn;
                }
            }
            return strReturn;
        }

        public string addZero(string num, int toAdd, bool inv=false)
        {/* Función para agregar ceros a la izquierda o derecha */
            if (toAdd > 0)
            {
                for (ushort i = 1; i <= toAdd; i++)
                {
                    if (inv)
                    {/* Ceros a la derecha*/
                        num = num + "0";
                    }
                    else
                    {/* Ceros a la izquierda*/
                        num = "0" + num;
                    }
                }
            }
            return num;
        }
    }
}
