using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public string Get(ushort id)
        {
            if (id >= 0)
            {
                string fact = "1";
                for (int i = 1; i <= id; i++)
                {
                    fact = strMult(fact, Convert.ToString(i));
                }
                return fact;
            }

            return "";
        }

        public string strMult(string numOne, string numTwo)
        {
            // Longitud mínima, Primario y Secundario
            ushort pLength, sLength;
            string Primary, Secondary;

            if (numOne.Length >= numTwo.Length)
            {
                Primary = numOne;
                Secondary = numTwo;
            }
            else
            {
                Primary = numTwo;
                Secondary = numOne;
            }

            pLength = Convert.ToUInt16(Primary.Length);
            sLength = Convert.ToUInt16(Secondary.Length);

            // Primario y Secundario
            ushort cnt = 0;
            string strResult = "0";

            for (int i = sLength-1; i >= 0 ; i--) {
                byte Two = Convert.ToByte(numTwo.Substring(i, 1));
                string strAux = "";
                int aux = 0;
                for (int j = pLength - 1; j >= 0; j--) {
                    byte One = Convert.ToByte(Primary.Substring(j, 1));
                    string result = Convert.ToString((One * Two) + aux);
                        
                    if(j!=0){
                        strAux = result.Substring(result.Length - 1) + strAux;
                    }else{
                        strAux = result + strAux;
                    }
                                        
                    if (result.Length > 1)
                    {
                        aux = Convert.ToUInt16(result.Substring(0, result.Length - 1));
                    }
                    else {
                        aux = 0;
                    }
                }

                strAux= addZero(strAux, cnt, true);
                cnt++;
                strResult = strSum(strResult, strAux);                
            }

            return strResult;
        }

        public string strSum(string numOne, string numTwo)
        {
            // Longitud del primario
            ushort pLength = Convert.ToUInt16(numOne.Length);

            //Longitud del secundario
            ushort sLength = Convert.ToUInt16(numTwo.Length);

            if (pLength > sLength)
            {
                numTwo = addZero(numTwo, pLength - sLength);
            }
            else if (pLength < sLength)
            {
                numOne = addZero(numOne, sLength - pLength);
            }

            string strReturn = "";
            ushort aux = 0;

            for (int i = numOne.Length - 1; i >= 0; i--)
            {
                byte One = Convert.ToByte(numOne.Substring(i, 1));
                byte Two = Convert.ToByte(numTwo.Substring(i, 1));
                byte Sum = Convert.ToByte(One + Two + aux);
                aux = Convert.ToUInt16(Sum / 10);
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
        {
            if (toAdd > 0)
            {
                for (ushort i = 1; i <= toAdd; i++)
                {
                    if (inv)
                    {
                        num = num + "0";
                    }
                    else
                    {
                        num = "0" + num;
                    }
                }
            }
            return num;
        }
    }
}
