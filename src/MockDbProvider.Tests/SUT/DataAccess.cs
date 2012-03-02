﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Common;
using System.Data;

namespace MockDbProvider.Tests.SUT
{
    class DataAccess
    {
        private static ConnectionStringSettings GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["db"];
        }
        private static DbProviderFactory GetDbProvider()
        {
            var config = GetConnectionString();
            var factory = DbProviderFactories.GetFactory(config.ProviderName);
            return factory;

        }
        private static DbConnection CreateConnection()
        {
            var config = GetConnectionString();
            var factory = GetDbProvider();
            var conn = factory.CreateConnection();
            conn.ConnectionString = config.ConnectionString;
            return conn;
        }
        
        
        public static DataTable GetAllUsers()
        {
            var table = new DataTable();

            using (var conn = CreateConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select * from users";
                    using (var adapter = GetDbProvider().CreateDataAdapter())
                    {
                        adapter.SelectCommand = cmd;                        
                        adapter.Fill(table);
                    }
                }
            }
            return table;
        }

        public static DataSet GetAllOrders()
        {
            var ds = new DataSet();
            ds.Tables.Add(new DataTable("customers"));
            ds.Tables.Add(new DataTable("orders"));

            using (var conn = CreateConnection())
            {
                using (var customersDA = GetDbProvider().CreateDataAdapter())
                {
                    customersDA.SelectCommand.CommandText = "select * from customers";
                    customersDA.SelectCommand.Connection = conn;
                    customersDA.Fill(ds.Tables["customers"]);
                }
                using (var ordersDA = GetDbProvider().CreateDataAdapter())
                {
                    ordersDA.SelectCommand.CommandText = "select * from orders";
                    ordersDA.SelectCommand.Connection = conn;
                    ordersDA.Fill(ds.Tables["orders"]);
                }
            }
            return ds;
        }

        public static int GetUserId(string username, string password)
        {
            using (var conn = CreateConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select userid from users where username=@username and password=@password";
                    var p1 = cmd.CreateParameter();                    
                    p1.ParameterName = "@username";
                    p1.Value = username;
                    cmd.Parameters.Add(p1);

                    var p2 = cmd.CreateParameter();
                    p2.ParameterName = "@password";
                    cmd.Parameters.Add(p2);

                    return (int)cmd.ExecuteScalar();
                }
            }
        }
        
    }
}
