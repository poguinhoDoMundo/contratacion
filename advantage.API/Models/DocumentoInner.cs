using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace advantage.API.Models
{
    public partial class documento_inner
    {

        public decimal id { get; set; }
        public decimal id_documento { get; set; }
        public string nombre { get; set; }
        public string ayuda { get; set; }


        public static List<documento_inner> getDocumentos( out string e  )
        {
            List<documento_inner> documentos = new List<documento_inner>();

            try
            {
                using (  providersBankContext pBc = new providersBankContext() )

                documentos = pBc.documento_inner.ToList();
                e = "OK";
            }
            catch( Exception ex )
            { 
                e = ex.Message;
            }

            return documentos; 
        } 


        public static string add_documentos( documento_inner documento, string responsable )
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
                    int total = prb.Database.ExecuteSqlCommand( sql,nO,nN,nD,nA,nU,nM );
                    result = Convert.ToString(nM.Value);
                }
            }
            catch( Exception ex )
            {
                result = ex.Message;
            } 

            return result;
        }
    }
}