using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace Data
{
    public static class Files
    {
        /// <summary>
        /// The instance of MongoGridFS used for accessing the Files database.
        /// </summary>
        private static MongoGridFS _gridFs = null;

        /// <summary>
        /// One-time static initialization of the _gridFs variable for the static Files class.
        /// </summary>
        static Files()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MongoServerSettings"].ConnectionString;
            MongoUrl conBuilder = new MongoUrl(connectionString);
            MongoClient client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();
            MongoDatabase db = server.GetDatabase(conBuilder.DatabaseName ?? "Files");

            _gridFs = db.GridFS;
        }//ctor

        /// <summary>
        /// Gets the GridFS instance for the Files database in MongoDB used for uploading, downloading, reading and writing files.
        /// </summary>
        public static MongoGridFS GridFs { get { return _gridFs; } }

        /// <summary>
        /// Gets a file contents by its Id. This is NOT to be used by the UI under any circumstances, UI should use the respective services
        /// that know how to apply proper access rules, etc.
        /// </summary>
        /// <param name="fileId">The ObjectId that represents the file to retrieve</param>
        /// <returns>A readable stream for the file.</returns>
        public static Stream ReadFileById(string fileId)
        {
            MongoGridFSFileInfo info = Files.GridFs.FindOneById(new BsonObjectId(new ObjectId(fileId)));
            if (!info.Exists)
                throw new FileNotFoundException();

            return info.OpenRead();
        }
    }
}
