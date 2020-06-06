using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http.Headers;
using advantage.API.Models;

namespace advantage.API.Controllers
{

    [Route("api/[controller]")]
    public class cargaController:Controller
    {

        [HttpPost("postPdf"),DisableRequestSizeLimit]        
        public IActionResult postPdf()
        {
            try
                {
                    var file = Request.Form.Files[0];
                    var folderName = Path.Combine("Resources", "documentos");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            
                    if (file.Length > 0)
                    {                                        
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);
            
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
            
                        return Ok(new { dbPath });
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error interno del servidor: {ex}");
                }
        }


        [HttpPost]
        public IActionResult post( [FromBody] Carga carga ) 
        {
            string result = Carga.add_carga(carga);    
            return Ok( Json(result) );
        }

    	[HttpGet("hasRevision/{user}/{docs}")]
        public IActionResult hasRevision( int user, int docs )
        {
            bool result  = Carga.hasRevision(user,docs, out string msg);   
            if ( msg != "OK" )
                return Ok(Json(msg));

            return Ok(Json(result));
        }


    }
}