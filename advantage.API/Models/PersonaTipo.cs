using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Npgsql;

namespace advantage.API.Models
{
    public partial class PersonaTipo
    {
        public decimal id { get; set; }
        public decimal id_Organizacion { get; set; }
        public string nombre { get; set; }
        public string ayuda { get; set; }


        public static string updTipoPersona( PersonaTipo persona  )
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

            using ( providersBankContext pBc = new providersBankContext() )
            {
                int total = pBc.Database.ExecuteSqlCommand( sql,nI,nO,nN,nA,nM);

                result = Convert.ToString(nM.Value);
            } 
            
            return result;
        }



        public static string delTipoPersona(  int id, string responsable  )
        {
            string result="";
            
            string sql = " SELECT pbank.tipo_persona_del( :iid, :responsable )";    
            
            NpgsqlParameter nI = new NpgsqlParameter( "iid", id );
            NpgsqlParameter nR = new NpgsqlParameter( "responsable", responsable );

            NpgsqlParameter nM = new NpgsqlParameter( "msg", DbType.String ){
                                               Direction = ParameterDirection.Output                             
                                                    };

            using ( providersBankContext pBc = new providersBankContext() )
            {
                int total = pBc.Database.ExecuteSqlCommand( sql,nI,nR,nM);

                result = Convert.ToString(nM.Value);
            } 
            
            return result;
        }

        public static List<PersonaTipo> getTipos()
        {
            List<PersonaTipo> result = new List<PersonaTipo>(); 

            using ( providersBankContext prC = new providersBankContext() )
            {
                result = prC.PersonaTipo.FromSql( "SELECT * FROM pbank.persona_tipo" ).ToList();
            }

            return result;
        }

    }
}
