using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using advantage.API.Models;

namespace advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class documentoController:Controller
    {

        [HttpPost]
        public IActionResult Post( [FromBody] Documento documento )
        {
            string result = Documento.add_documento(documento, "dev");            
            return Ok( Json(result));
        }

        [HttpGet("{id}")]
        public IActionResult get(int id)
        {
            Documento doc = Documento.get(id, out string msg  );
            if ( msg != "OK" )
                return Ok( Json(msg));
            return Ok(doc);
        }

    
    }


}