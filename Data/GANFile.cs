using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data
{
    public class GANFile : BaseEntity<GANFile>
    {
        [BsonRequired, BsonRepresentation(BsonType.ObjectId)]
        public string FileId { get; set; }

        [BsonRequired]
        public FileGroup FileGroup { get; set; }

        public string ContentType { get; set; }

        public long ContentLength { get; set; }

        [BsonRequired]
        public string FileName { get; set; }
        
        [BsonRequired]
        public byte[] File { get; set; }

        public string UploadDate { get; set; }
    }
}
