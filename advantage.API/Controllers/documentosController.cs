using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using advantage.API.Models;


namespace advantage.API.Controllers
{

    [Route("api/[controller]")]
    public class documentosController:Controller
    {


        [HttpGet]
        public IActionResult get()
        {
           List<documento_inner> documentos = documento_inner.getDocumentos( out string e );     
           if ( e != "OK" )
                return Ok(e);

           return Ok( documentos);
        }

        [HttpPost]
        public IActionResult post(  [FromBody] documento_inner documento )
        {
            string result = documento_inner.add_documentos( documento,"dev" );    
       
            return Ok( Json(result) );
        } 


    }


}