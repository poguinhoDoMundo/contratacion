using System;
using System.Collections.Generic;
using Npgsql;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace advantage.API.Models
{
    public partial class Carga
    {

        public decimal Id { get; set; }
        public decimal Id_documento { get; set; }
        public string Path { get; set; }
        public decimal Id_estado { get; set; }
        public decimal Id_usuario { get; set; }


        public static string add_carga( Carga carga )
        {
            string result = "";    

            try
            {
                string sql = "SELECT carga_add( @iid_documento, @ipath, @iid_usuario )";
                NpgsqlParameter nI = new NpgsqlParameter("iid_documento",carga.Id_documento);
                NpgsqlParameter nP = new NpgsqlParameter("ipath",carga.Path );
                NpgsqlParameter nU = new NpgsqlParameter("iid_usuario",carga.Id_usuario );
                
                NpgsqlParameter nM = new NpgsqlParameter("msg",DbType.String ) {  Direction = ParameterDirection.Output } ;
                using( providersBankContext _ctx = new providersBankContext()  )
                {
                    int total = _ctx.Database.ExecuteSqlCommand(sql,nI, nP, nU,nM);
                    result = Convert.ToString(nM.Value);
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

            return result;
        }


        public static bool hasRevision(  int user, int docs, out string msg )
        {
            scalarInt result= new scalarInt();    

            string sql = " SELECT COUNT(*) id "
                       + " FROM   pbank.carga " 
                       + " WHERE  id_documento = @iid_documento AND id_usuario = @iid_user "
		               + "        AND id_estado = 1";    

            NpgsqlParameter ni = new NpgsqlParameter("iid_documento", docs);
            NpgsqlParameter nu = new NpgsqlParameter("iid_user", user);

            try 
            {
                using( providersBankContext _ctx = new providersBankContext() )
                {
                    result = _ctx.scalarInt.FromSql( sql, ni, nu ).Single();        
                    msg = "OK";
                }
            }
            catch( Exception ex)
            {
                result.id = 0;
                msg = ex.Message;   
            }

            return(result.id==0)?false:true;
        }
    }

    public partial class scalarInt
    {
        public int id {get;set;}
    }

}
