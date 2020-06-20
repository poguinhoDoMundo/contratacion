using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace advantage.API.Models
{
    public class Account 
    {

        public string user{get;set;} 
        public string pass{get;set;}

        public static async Task<(bool,string)> is_user(  Account account )
        {
            scalarBool result = new scalarBool();
            string msg="";
            string sql = "SELECT pbank.is_user( @user , @pass ) id";
            NpgsqlParameter nu = new NpgsqlParameter( "user", account.user );
            NpgsqlParameter np = new NpgsqlParameter( "pass", account.pass );

            try
            {
                using ( providersBankContext _ctx = new providersBankContext() )
                {
                      result = await _ctx.scalarBool.FromSql(sql,nu,np).SingleAsync();
                      msg = "OK";
                }
            }
            catch( Exception ex)
            {
                msg = ex.Message;
                return (false,msg);
            }

            return (result.id, msg);
        }
    }

    public class scalarBool
    {
        public bool id{get;set;}
    } 
}