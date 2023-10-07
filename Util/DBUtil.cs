using System.Data.SqlClient;

namespace ProductsDBApp.Util
{
    public static class DBUtil
    {
        private static SqlConnection? conn;

        public static SqlConnection? GetConnection()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var config = builder.Build();
            string url = config.GetConnectionString("defaultConnection");
            try
            {
                conn = new SqlConnection(url);
                
            }
            catch(Exception)
            {
                Console.Error.WriteLine("Unable to establish a db connection");
            }
            return conn;
        }

        public static void CloseConnection()
        {
            conn!.Close();
        }
    }
}
