﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Npgsql;


namespace advantage.API.Models
{
    public partial class documento_inner
    {

        public decimal id { get; set; }
        public decimal id_documento { get; set; }
        public string nombre { get; set; }
        public string ayuda { get; set; }


        public static async Task<( List<vDocumento>, string) > getDocumentos(   )
        {
            List<vDocumento> documentos = new List<vDocumento>();
            string e = "";

            string sql = "SELECT NEXTVAL('pbank.tmp') id ,d.id id_doc, d.nombre nombre_doc, di.nombre nombre_docs "
                           + ", pt.nombre nombre_per   " 
                           + "FROM   pbank.documento d "
                           + "INNER JOIN pbank.persona_tipo pt on pt.id = d.id_tipo "
                           + "LEFT JOIN pbank.documento_inner di on di.id_documento = d.id ";    

            try
            {
                using (  providersBankContext pBc = new providersBankContext() )


                documentos =  await pBc.vDocumento.FromSql(sql).ToListAsync();
                e = "OK";
            }
            catch( Exception ex )
            { 
                e = ex.Message;
            }

            return (documentos,e); 
        } 


        public static async Task<(List<documento_inner>,string)> getDocumentosId( int id  )
        {
            List<documento_inner> documentos = new List<documento_inner>();
            string e="";
            try
            {
                using (  providersBankContext pBc = new providersBankContext() )

                documentos = await pBc.documento_inner.Where(x=>x.id_documento==id).ToListAsync();
                e = "OK";
            }
            catch( Exception ex )
            { 
                e = ex.Message;
            }

            return (documentos,e); 
        } 


        public static async Task<string> del_documento( int id, string usuario )
        {
            string result = "";   
            NpgsqlParameter pI = new NpgsqlParameter( "iid",id );
            NpgsqlParameter pU = new NpgsqlParameter( "usuario", usuario );

            NpgsqlParameter pM = new NpgsqlParameter("msg", DbType.String) { Direction = ParameterDirection.Output   };    

            string sql = "SELECT pbank.docs_delete(@iid,  @usuario  )  "; 
            
            try 
            {
                using (  providersBankContext pBc = new providersBankContext()  )
                {
                    int total = await pBc.Database.ExecuteSqlCommandAsync(sql, pI, pU, pM );   
                    result = Convert.ToString( pM.Value);
                }
            }
            catch(  Exception e)
            {
                result = e.Message;
            } 

            return result;
        }


        public static async Task<string> add_documentos( documento_inner documento, string responsable )
        {
            string result = "";
            string sql = "SELECT pbank.documentos_add( @iid, @inombre, @iid_documento, @iayuda, " 
                       + " @usuario )  ";    

            NpgsqlParameter nO = new NpgsqlParameter( "iid",documento.id );
            NpgsqlParameter nN = new NpgsqlParameter( "inombre",documento.nombre );
            NpgsqlParameter nD = new NpgsqlParameter( "iid_documento",documento.id_documento );
            NpgsqlParameter nA = new NpgsqlParameter( "iayuda",documento.ayuda );
            NpgsqlParameter nU = new NpgsqlParameter( "usuario", responsable );

            NpgsqlParameter nM = new NpgsqlParameter( "msg", DbType.String ){ Direction = ParameterDirection.Output };

            try 
            {
                using (  providersBankContext prb = new providersBankContext()  )
                {
                    int total = await prb.Database.ExecuteSqlCommandAsync( sql,nO,nN,nD,nA,nU,nM );
                    result = Convert.ToString(nM.Value);
                }
            }
            catch( Exception ex )
            {
                result = ex.Message;
            } 

            return result;
        }


        public static async Task<(List<vDocumentosPersona>,string)> GetDocumentosPersonas( int id_persona)
        {
            List<vDocumentosPersona> docs = new List<vDocumentosPersona>();
            string ex = "";    
            string sql = "SELECT di.id, d.id id_doc, d.nombre nom_doc, di.nombre nom_docs,  "
                       + "CASE di.id "
                       + "      WHEN (  SELECT MIN( di1.id ) "
                       + "              FROM   pbank.documento_inner di1 "
                       + "              INNER JOIN pbank.documento d1 ON d1.id = di1.id_documento "
                       + "              WHERE d.id = d1.id  ) " 
                       + "        THEN '0' "
                       + "         ELSE '1' "
                       + "      END print, di.ayuda, u.id id_persona "
                       + "FROM pbank.documento d "
                       + "INNER JOIN pbank.documento_inner di ON d.id = di.id_documento "
                       + "INNER JOIN pbank.usuario u on u.id_persona = d.id_tipo "
                       + "WHERE u.id = @id_persona";    

            NpgsqlParameter nP = new NpgsqlParameter("id_persona",id_persona);

            try 
            {
                using (  providersBankContext pBc = new providersBankContext() )
                {
                     docs = await pBc.vDocumentosPersona.FromSql( sql,nP ).ToListAsync(); 
                     ex = "OK";  
                }
            }
            catch( Exception e)
            {
                ex = e.Message;
            }

            return (docs,ex);    
        }



    }

    public partial class vDocumento
    {
        public int id {get;set;}
        public int id_doc  {get;set;}
        public string nombre_doc {get;set;}
        public string nombre_docs{get;set;}
        public string nombre_per{get;set;}
    }

    public partial class vDocumentosPersona
    {
        public int id {get;set;}
        public int id_doc {get;set;}

        public string nom_doc{get;set;}

        public string nom_docs{get;set;}

        public string print {get;set;}

        public string ayuda {get;set;}

        public int id_persona {get;set;}
    }
}