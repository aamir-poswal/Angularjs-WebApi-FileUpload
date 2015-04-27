using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace Data.Service
{
    public interface IFileService
    {
        GANFile SaveFile(FileGroup group, string fileName, byte[] file,
                                    string contentType);
        List<GANFile> GetAll();
        GANFile DownloadFile(string fileId);
    }

    public class FileService : IFileService
    {
        public GANFile SaveFile(FileGroup group, string fileName, byte[] file, string contentType)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName");
            if (file == null || file.Length == 0)
                throw new ArgumentNullException("file");

            var ganFile = new GANFile()
            {
                FileName = fileName,
                FileGroup = group,
                File = file,
                ContentLength = file.Length,
                ContentType = contentType,
                UploadDate = DateTime.Now.ToString("g")
            };

            var opts = new MongoGridFSCreateOptions()
            {
                ContentType = contentType,
                UploadDate = DateTime.UtcNow,
                Metadata = new BsonDocument()
                    .Add("FileGroup", new BsonInt32((int)group))
            };

            using (var stream = new MemoryStream(ganFile.File))
            {
                stream.Position = 0;
                var info = Files.GridFs.Upload(stream, ganFile.FileName, opts);
                ganFile.FileId = info.Id.ToString();
            }

            return ganFile.Save();

        }

        public List<GANFile> GetAll()
        {
            return GANFile.AsQueryable().ToList();
        }

        public GANFile DownloadFile(string fileId)
        {
            var ganFile = GANFile.AsQueryable().FirstOrDefault(q => q.FileId == fileId);
            byte[] doc = null;

            // Read the file document from GridFS into a memory stream so we can convert it to a byte array.
            using (MemoryStream output = new MemoryStream())
            {
                MongoGridFSFileInfo info = Files.GridFs.FindOneById(new BsonObjectId(new ObjectId(ganFile.FileId)));
                info.GridFS.Download(output, info);
                doc = output.ToArray();
            }

            // Assign the file property of our attachment and return the resulting instance.
            ganFile.File = doc;
            return ganFile;
        }
    }
}
