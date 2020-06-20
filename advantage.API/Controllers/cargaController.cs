using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http.Headers;
using advantage.API.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;

namespace advantage.API.Controllers
{

    [Route("api/[controller]")]
    [Authorize( AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme ) ]
    public class cargaController:Controller
    {

        //OJOOOO, NO OLVIDAR CAMBIAR PARA ENCRIPTARLO    
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
        public async  Task<IActionResult> post( [FromBody] Carga carga ) 
        {
            string result = await Carga.add_carga(carga);    
            return Ok( Json(result) );
        }


    	[HttpGet("hasRevision/{user}/{docs}")]
        public async Task<IActionResult> hasRevision( int user, int docs )
        {
            
            ( bool result, string msg ) = await Carga.hasRevision(user,docs);   
            if ( msg != "OK" )
                return Ok(Json(msg));

            return Ok(Json(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> get( int id )
        {
            (List<vCarga> result, string msg) = await vCarga.getVCarga( id  );

            if ( msg != "OK" )
                return Ok(Json(msg) );

            return Ok( result );
        }
    }
}