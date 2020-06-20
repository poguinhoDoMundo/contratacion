using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using advantage.API.Models;
using System.Threading.Tasks;

namespace advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class documentoController:Controller
    {

        [HttpPost]
        public async Task<IActionResult> Post( [FromBody] Documento documento )
        {
            var result = await Documento.add_documento(documento, "dev");            
            return Ok( Json(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> get(int id)
        {

            (Documento doc,string msg) = await Documento.get(id   );
            if ( msg != "OK" )
                return Ok( Json(msg));
            return Ok(doc);
        }

    
    }


}