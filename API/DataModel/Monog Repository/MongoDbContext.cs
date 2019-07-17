using MongoDB.Driver;
using System.Configuration;
namespace DataModel
{
    public class MongoDbContext
    {
        public const string CONNECTION_STRING_NAME = "192.168.2.202:27017";
        public const string DATABASE_NAME = "KPMES";

        private static readonly IMongoClient _client;
        private static readonly IMongoDatabase _database;

        static MongoDbContext()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
            _client = new MongoClient("mongodb://192.168.2.202:27017");
            _database = _client.GetDatabase(DATABASE_NAME);
        }

        /// <summary>
        /// The private GetCollection method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return _database.GetCollection<TEntity>(typeof(TEntity).Name);
        }
    }
}