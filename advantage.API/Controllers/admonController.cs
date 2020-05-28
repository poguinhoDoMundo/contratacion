using advantage.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class admonController : Controller
    {

            public admonController(  )
            {
            }
        

        [HttpPost]
        public IActionResult Post( [FromBody] Organizacion organizacion  )
        {
            
            if (  organizacion == null )
                return BadRequest();
            
            string result = Organizacion.addOrganizacion(organizacion );

            return  Ok( Json(result) );
        }


        [HttpGet]
        public IActionResult Get()
        {   
            var organizacion  = Organizacion.getOrganizacionMain();
            return Ok(  organizacion  );
        }


    }
}