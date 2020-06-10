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
           List<vDocumento> documentos = documento_inner.getDocumentos( out string e );     
           if ( e != "OK" )
                return Ok( Json(e));

           return Ok( documentos);
        }

        [HttpGet("getDocsPersona/{id}")]
        public IActionResult getDocsPersona( int id )
        {
            List<vDocumentosPersona> result = documento_inner.GetDocumentosPersonas(id, out string msg);
            
            if ( msg != "OK" )
                return Ok( Json(msg) );
                
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult get( int id )
        {
           List<documento_inner> documentos = documento_inner.getDocumentosId( id, out string e );     
           if ( e != "OK" )
                return Ok(Json(e));

           return Ok( documentos);
        }

        [HttpPost]
        public IActionResult post(  [FromBody] documento_inner documento )
        {
            string result = documento_inner.add_documentos( documento,"dev" );    
       
            return Ok( Json(result) );
        }

        [HttpDelete("{id}")]
        public IActionResult delete( int id  )
        {
            string result = documento_inner.del_documento(id, "dev");
            return Ok(  Json( result));
        }   


    }


}