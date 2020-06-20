using System;
using System.Collections.Generic;
using Npgsql;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace advantage.API.Models
{
    public partial class Carga
    {

        public decimal Id { get; set; }
        public decimal Id_documento { get; set; }
        public string Path { get; set; }
        public decimal Id_estado { get; set; }
        public decimal Id_usuario { get; set; }


        public static async Task<string> add_carga( Carga carga )
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
                    int total = await _ctx.Database.ExecuteSqlCommandAsync(sql,nI, nP, nU,nM);
                    result = Convert.ToString(nM.Value);
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

            return result;
        }


        public static async Task<(bool, string)> hasRevision(  int user, int docs )
        {
            scalarInt result= new scalarInt();    
            string msg = "";

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
                    result = await _ctx.scalarInt.FromSql( sql, ni, nu ).SingleAsync();        
                    msg = "OK";
                }
            }
            catch( Exception ex)
            {
                result.id = 0;
                msg = ex.Message;   
            }

            return(result.id==0)?(false,msg):(true,msg);
        }

    }



    public partial class vCarga
    {

        public int id {get;set;} 
        public string nom_docs {get;set;}
        public string nom_doc{get;set;}
        public string path{get;set;}

        public string fecha{get;set;}
        public string nom_estado{get;set;}

        public static async Task<(List<vCarga>,string)>  getVCarga( int id_persona )
        {
            List<vCarga> carga = new List<vCarga>();    
            string msg="";
                string sql = " SELECT c.id, di.nombre nom_docs, "
	                       + " d.nombre nom_doc, c.path, TO_CHAR(c.fecha,'dd/mm/yyyy') fecha, ec.nombre nom_estado "
                           + " FROM pbank.carga c "
                           + " INNER JOIN pbank.estado_carga ec ON c.id_estado = ec.id "
                           + "  INNER JOIN pbank.documento_inner di ON di.id = c.id_documento "
                           + " INNER JOIN pbank.documento d ON d.id = di.id_documento "
                           + " WHERE c.id_usuario = @id_usuario ";
            
                NpgsqlParameter ni = new NpgsqlParameter("id_usuario", id_persona);
            try
            {

                using ( providersBankContext _ctx = new providersBankContext() ) 
                {
                    carga = await _ctx.vCarga.FromSql( sql,ni ).ToListAsync();
                    msg = "OK";
                }   

            }
            catch( Exception ex)
            {
                msg = ex.Message;
            }

            return (carga,msg);
        }

    }

    public partial class scalarInt
    {
        public int id {get;set;}
    }

}