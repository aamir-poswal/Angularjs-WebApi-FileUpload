using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;

namespace GAN_Developer_Test.Models
{
    public class FileViewModel
    {
        public string FileId { get; set; }
        public string FileGroup { get; set; }
        public string UploadDate { get; set; }
        public string FileName { get; set; }
    }
}