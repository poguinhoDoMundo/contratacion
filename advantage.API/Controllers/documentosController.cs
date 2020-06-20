using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using advantage.API.Models;
using System.Threading.Tasks;

namespace advantage.API.Controllers
{

    [Route("api/[controller]")]
    public class documentosController:Controller
    {


        [HttpGet]
        public async Task<IActionResult> get()
        {
           (List<vDocumento> documentos, string e) = await documento_inner.getDocumentos();     
           if ( e != "OK" )
                return Ok( Json(e));

           return Ok( documentos);
        }

        [HttpGet("getDocsPersona/{id}")]
        public async Task<IActionResult> getDocsPersona( int id )
        {
            (List<vDocumentosPersona> result,string msg) = await documento_inner.GetDocumentosPersonas(id );
            
            if ( msg != "OK" )
                return Ok( Json(msg) );
                
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> get( int id )
        {
           (List<documento_inner> documentos, string e )= await documento_inner.getDocumentosId( id );     
           if ( e != "OK" )
                return Ok(Json(e));

           return Ok( documentos);
        }

        [HttpPost]
        public async Task<IActionResult> post(  [FromBody] documento_inner documento )
        {
            string result = await documento_inner.add_documentos( documento,"dev" );    
       
            return Ok( Json(result) );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete( int id  )
        {
            string result = await documento_inner.del_documento(id, "dev");
            return Ok(  Json( result));
        }   


    }


}