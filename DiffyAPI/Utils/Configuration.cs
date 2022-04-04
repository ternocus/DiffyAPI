namespace DiffyAPI.Utils
{
    public static class Configuration
    {
        public static string ConnectionString()
        {
            return "Server=tcp:diffy-db.database.windows.net,1433;Initial Catalog=DiffyDatabase;Persist Security Info=False;" +
                                       "User ID=DiffyApp-api;Password=Aleyandro1996;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;" +
                                       "Connection Timeout=30;";
        }
    }
}
