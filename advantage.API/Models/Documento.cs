using System;
using System.Collections.Generic;
using Npgsql;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace advantage.API.Models
{
    public partial class Documento
    {
        public decimal id { get; set; }
        public decimal id_tipo { get; set; }
        public decimal id_organizacion { get; set; }
        public string nombre { get; set; }

        public static string add_documento( Documento documento, string usuario )
        {
            string result = "";   

            string sql = "SELECT pbank.documento_add( @iid, @inombre, "
                       + " @iid_organizacion, @id_entidad, @usuario)";

            NpgsqlParameter pI = new NpgsqlParameter("iid", documento.id  );
            NpgsqlParameter pN = new NpgsqlParameter("inombre", documento.nombre  );
            NpgsqlParameter pO = new NpgsqlParameter("iid_organizacion", documento.id_organizacion  );
            NpgsqlParameter pE = new NpgsqlParameter("id_entidad", documento.id_tipo  );
            NpgsqlParameter pU = new NpgsqlParameter("usuario", usuario  );

            NpgsqlParameter pM = new NpgsqlParameter("@msg", DbType.String){ Direction = ParameterDirection.Output };

            try
            {
                 using( providersBankContext pBc = new providersBankContext() )
                 {
                     int total = pBc.Database.ExecuteSqlCommand(sql,pI, pN, pO,pE,pU,pM);
                     result = Convert.ToString( pM.Value) ;
                 }   
            }
            catch( Exception e )
            {
                result = e.Message;
            }

            return result;
        }

        public static Documento get( int id, out string msg )
        {
            Documento doc= new Documento();    

            try 
            {
                using ( providersBankContext pbc = new providersBankContext()  ) 
                {
                    string sql = "SELECT * FROM pbank.documento WHERE id=@id";
                    NpgsqlParameter nI = new NpgsqlParameter("id",id );
                    doc = pbc.Documento.FromSql(sql,nI).First();
                    msg = "OK";
                }
            }
            catch( Exception ex) 
            {
                msg = ex.Message;
            }

             return doc;
        }

    }
}
