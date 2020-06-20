using advantage.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class admonController : Controller
    {        

        [HttpPost]
        public async Task<IActionResult> Post( [FromBody] Organizacion organizacion  )
        {
            
            if (  organizacion == null )
                return BadRequest();
            
            string result = await Organizacion.addOrganizacion(organizacion );

            return  Ok( Json(result) );
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {   
            var organizacion  = await Organizacion.getOrganizacionMain();
            return Ok(  organizacion  );
        }


    }
}