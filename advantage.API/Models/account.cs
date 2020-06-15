using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Npgsql;
using Microsoft.EntityFrameworkCore;

namespace advantage.API.Models
{
    public class Account 
    {

        public string user{get;set;} 
        public string pass{get;set;}

        public static bool is_user(  Account account, out string msg )
        {
            scalarBool result = new scalarBool();
            string sql = "SELECT pbank.is_user( @user , @pass ) id";
            NpgsqlParameter nu = new NpgsqlParameter( "user", account.user );
            NpgsqlParameter np = new NpgsqlParameter( "pass", account.pass );

            try
            {
                using ( providersBankContext _ctx = new providersBankContext() )
                {
                      result = _ctx.scalarBool.FromSql(sql,nu,np).Single();
                      msg = "OK";
                }
            }
            catch( Exception ex)
            {
                msg = ex.Message;
                return false;
            }

            return true;
        }
    }

    public class scalarBool
    {
        public bool id{get;set;}
    } 
}