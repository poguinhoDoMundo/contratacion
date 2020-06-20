using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

using Npgsql;

namespace advantage.API.Models
{
    public partial class PersonaTipo
    {
        public decimal id { get; set; }
        public decimal id_Organizacion { get; set; }
        public string nombre { get; set; }
        public string ayuda { get; set; }


        public static async Task<string> updTipoPersona( PersonaTipo persona  )
        {
            string result="";
            
            string sql = " SELECT pbank.ADD_TIPO_PERSONA( :iid, :iid_organizacion, :inombre, :idescripcion)";    
            
            NpgsqlParameter nI = new NpgsqlParameter( "iid", persona.id );
            NpgsqlParameter nO = new NpgsqlParameter( "iid_organizacion", persona.id_Organizacion );
            NpgsqlParameter nN = new NpgsqlParameter( "inombre", persona.nombre );
            NpgsqlParameter nA = new NpgsqlParameter( "idescripcion", persona.ayuda );

            NpgsqlParameter nM = new NpgsqlParameter( "msg", DbType.String ){
                                               Direction = ParameterDirection.Output                             
                                                    };

            try{
                using ( providersBankContext pBc = new providersBankContext() )
                {
                    
                    int total = await pBc.Database.ExecuteSqlCommandAsync( sql,nI,nO,nN,nA,nM);
                    result = Convert.ToString(nM.Value);
                } 
            }
            catch(Exception ex)
            {
                result = ex.Message;    
            };
            return result;
        }



        public static async Task<string> delTipoPersona(  int id, string responsable  )
        {
            string result="";
            
            string sql = " SELECT pbank.tipo_persona_del( :iid, :responsable )";    
            
            NpgsqlParameter nI = new NpgsqlParameter( "iid", id );
            NpgsqlParameter nR = new NpgsqlParameter( "responsable", responsable );

            NpgsqlParameter nM = new NpgsqlParameter( "msg", DbType.String ){
                                               Direction = ParameterDirection.Output                             
                                                    };
            try
            {
                using ( providersBankContext pBc = new providersBankContext() )
                {
                    int total = await pBc.Database.ExecuteSqlCommandAsync( sql,nI,nR,nM);

                    result = Convert.ToString(nM.Value);
                } 
            }
            catch( Exception ex )
            {
                result = ex.Message;
            }

            return result;
        }

        public static async  Task<(List<PersonaTipo>, string)> getTipos()
        {
            List<PersonaTipo> result = new List<PersonaTipo>(); 
            string msg="";

            try 
            {
                using ( providersBankContext prC = new providersBankContext() )
                {
                    result = await prC.PersonaTipo.FromSql( "SELECT * FROM pbank.persona_tipo" ).ToListAsync();
                    msg = "OK";
                }
            }
            catch( Exception ex)
            {
                msg = ex.Message;
            }
            
            return (result,msg);
        }

    }
}
