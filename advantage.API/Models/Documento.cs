using System;
using System.Collections.Generic;
using Npgsql;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace advantage.API.Models
{
    public partial class Documento
    {
        public decimal id { get; set; }
        public decimal id_tipo { get; set; }
        public decimal id_organizacion { get; set; }
        public string nombre { get; set; }




        public static async Task<string> add_documento( Documento documento, string usuario )
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
                     int total = await pBc.Database.ExecuteSqlCommandAsync(sql,pI, pN, pO,pE,pU,pM);
                     result = Convert.ToString( pM.Value) ;
                 }   
            }
            catch( Exception e )
            {
                result = e.Message;
            }

            return result;
        }

        public static async Task<(Documento,string)> get( int id )
        {
            Documento doc= new Documento();    
            string msg="";

            try 
            {
                using ( providersBankContext pbc = new providersBankContext()  ) 
                {
                    string sql = "SELECT * FROM pbank.documento WHERE id=@id";
                    NpgsqlParameter nI = new NpgsqlParameter("id",id );
                    doc = await pbc.Documento.FromSql(sql,nI).FirstAsync();
                    msg = "OK";
                }
            }
            catch( Exception ex) 
            {
                msg = ex.Message;
            }

             return (doc,msg);
        }

    }
}
