using PAI.Selfhosted.WebApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PAI.Selfhosted.WebApi
{
    public class SandwichController : ApiController
    {
        static Random rnd = new Random(Guid.NewGuid().GetHashCode());

        [HttpGet]
        public ICollection<Sandwich> Get()
        {
            List<Sandwich> result = new List<Sandwich>();

            for (int i = 0; i < 10; i++)
            {
                result.Add(new Sandwich { Id = i, Name = "Mexico Specialite #" + i, Price = Math.Round(rnd.NextDouble() * 5, 2) + 5 });
            }

            return result;
        }

        public IHttpActionResult Get(int id)
        {
            return Ok(new Sandwich { Id = 123, Name = "Mexico Specialite", Price = Math.Round(rnd.NextDouble() * 5, 2) + 5 });
        }
    }
}
