using MySqlConnector;
using System;

namespace LikeCountBE.Repos
{
    public class CountRepo : IDisposable
    {
        public MySqlConnection Connection { get; }

        public CountRepo(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}
