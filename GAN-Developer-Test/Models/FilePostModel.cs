using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;

namespace GAN_Developer_Test.Models
{
    public class FilePostModel
    {
        public FileGroup Group { get; set; }
        public string ContentType { get; set; }
        public string FileBase64String { get; set; }
        public string FileName { get; set; }
    }
}