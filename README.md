Mock DbProviderFactory
======================

For projects where refactoring the code base into a more testable form is difficult or impossible due to time, philosophical or managerial constraints.

Given:

	public static class DataAccess {
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

We can write a test like: