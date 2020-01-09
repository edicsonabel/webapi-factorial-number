﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NumeroFactorial.WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] {"Coloque un número en la URL como parametro para sacar su factorial, ejemplo ", "'/api/values/5'"};
        }

        // GET api/values/5
        public string Get(ushort id)
        {
            if (id >= 0)
            {
                ulong fact = 1;
                for (ushort i = 1; i <= id; i++)
                {
                    fact *= i;
                }
                return "" + fact;
            }

            return "Número mal colocado";
        }     

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

    }
}