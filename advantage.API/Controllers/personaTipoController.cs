using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

using advantage.API.Models;


namespace advantage.API.Controllers 
{

    [Route("api/[controller]")]
    public class tiposController:Controller
    {
        
        [HttpPost]
        public async Task<IActionResult> Post( [FromBody] PersonaTipo tipo   )
        {
            string result="";     
            result = await PersonaTipo.updTipoPersona(tipo);

            return Ok( Json(result) );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete( int id )
        {
            string responsable="dev";
            string result= await PersonaTipo.delTipoPersona(id, responsable);  
            return Ok( Json( result ) );
        } 

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            (List<PersonaTipo> data, string msg) = await PersonaTipo.getTipos();    
            if ( msg != "OK" )
                return Ok(msg);

            return Ok(data);
        }


    }


}
