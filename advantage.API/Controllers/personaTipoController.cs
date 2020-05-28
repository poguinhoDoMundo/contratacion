using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using advantage.API.Models;


namespace advantage.API.Controllers 
{

    [Route("api/[controller]")]
    public class tiposController:Controller
    {
        
        [HttpPost]
        public IActionResult Post( [FromBody] PersonaTipo tipo   )
        {
            string result="";     
            result = PersonaTipo.updTipoPersona(tipo);

            return Ok( Json(result) );
        }

        [HttpDelete("{id}")]
        public IActionResult delete( int id )
        {
            string responsable="dev";
            string result=  PersonaTipo.delTipoPersona(id, responsable);  
            return Ok( Json( result ) );
        } 

        [HttpGet]
        public IActionResult Get()
        {
            List<PersonaTipo> data = PersonaTipo.getTipos();    
            return Ok(data);
        }


    }


}
