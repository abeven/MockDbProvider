Mock DbProviderFactory
======================

For projects where refactoring the code base into a more testable form is difficult or impossible due to time, philosophical or managerial constraints.

Given a traditional .NET data access method:

	public static class DataAccess 
	{	
		public static int GetUserId(string username, string password)
				{
					using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
					{
						using (var cmd = new SqlCommand(conn))
						{
							cmd.CommandText = "select userid from users where username=@username and password=@password";
							cmd.Parameters.AddWithValue("@username", username);
							cmd.Parameters.AddWithValue("@password", password);
							conn.Open();
							var result = (int)cmd.ExecuteScalar();
							conn.Close();
							return result;
						}
					}
				}
	}

And that we can refactor to use a System.Data.DbProviderFactory:

	public static class DataAccess 
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
	
Or in a project that user a helper library like the MS Enterprise Library Data Access Block:

    public static class DataAccess
    {
        public static int GetCustomers(string lastName)
        {
            var db = DatabaseFactory.CreateDatabase("db")			
			using(var cmd = db.GetSqlStringCommand("select userid from users where username=@username and password=@password"))
			{
				db.AddInParameter(cmd, @username", DbType.String, username);
				db.AddInParameter(cmd, "@password", DbType.String, password);
				return (int)db.ExecuteScalar(cmd).Tables[0];
			}			
        }
    }
	
We can write tests without having an actual database anywhere:

App.config:
-----------
	<?xml version="1.0" encoding="utf-8" ?>
	<configuration>
	  <connectionStrings>
		<add name="db" connectionString="blah" providerName="MockDbProvider"/>
	  </connectionStrings>
	  <system.data>
		<DbProviderFactories>
		  <add name="Mock Data Provider"
		   invariant="MockDbProvider"
		   description="Mock Data Provider"
		   type="MockDbProvider.MockDbProviderFactory, MockDbProvider, Version=1.0.0.0, Culture=neutral, PublicKeyToken=60312927800a44b2"
		/>
		</DbProviderFactories>
	  </system.data>
</configuration>
