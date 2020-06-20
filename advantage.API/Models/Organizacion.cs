using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace advantage.API.Models
{
    public partial class Organizacion:DbContext
    {
        public decimal Id { get; set; }
        public string Nit { get; set; }
        public string Razon { get; set; }
        public string Descripcion { get; set; }
        public decimal IdOrganizacion { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Cargo { get; set; }

        public static async Task<string> addOrganizacion ( Organizacion organizacion  )
        {           
            string result=""; 
            try
            {
                string sql = " SELECT pbank.add_organizacion( "
                           + ":oid, " 
	                       + ":init, " 
	                       + ":irazon, " 
	                       + ":idescripcion, " 
	                       + ":icedula, " 
	                       + ":inombre, " 
	                       + ":iphone, " 
	                       + ":imail, " 
	                       + ":icargo )";

                NpgsqlParameter pI = new NpgsqlParameter( "oid", organizacion.Id.ToString() );
                NpgsqlParameter pN =  new NpgsqlParameter( "init", organizacion.Nit   ) ;
                NpgsqlParameter pR =  new NpgsqlParameter( "irazon", organizacion.Razon  ) ;
                NpgsqlParameter pD =  new NpgsqlParameter( "idescripcion", organizacion.Descripcion ) ;
                NpgsqlParameter pC =  new NpgsqlParameter( "icedula", organizacion.Cedula) ;
                NpgsqlParameter pNo =  new NpgsqlParameter( "inombre", organizacion.Nombre  ) ;
                NpgsqlParameter pP =  new NpgsqlParameter( "iphone", organizacion.Phone  ) ;
                NpgsqlParameter pM =  new NpgsqlParameter( "imail", organizacion.Mail  ) ;
                NpgsqlParameter pCa =  new NpgsqlParameter( "icargo", organizacion.Cargo  ) ;

                NpgsqlParameter pO =  new NpgsqlParameter("msg", DbType.String) { Direction = ParameterDirection.Output };


                using ( providersBankContext prC = new providersBankContext() )
                {    
                    int total = await prC.Database.ExecuteSqlCommandAsync( sql, pI,pN,pR,pD,pC,pNo,pP,pM,pCa,pO  );
                    result = Convert.ToString( pO.Value);
                }
            }

            catch { throw; }
            return result;
        }

        public static async Task<List<OrganizacionMain>> getOrganizacionMain()
        {
            List<OrganizacionMain> organizacionMain = new List<OrganizacionMain>();

            try
            {
                using ( providersBankContext prC = new providersBankContext() )
                {
                    organizacionMain = await prC.OrganizacionMain.FromSql<OrganizacionMain>("SELECT * FROM pbank.organizacion").ToListAsync();
                }
            }
            catch{}

            return organizacionMain;
        }

    }
}


public class OrganizacionMain
{
        public decimal id { get; set; }
        public string nit { get; set; }
        public string razon { get; set; }
        public string descripcion { get; set; }


}