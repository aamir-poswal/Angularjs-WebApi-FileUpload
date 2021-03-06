﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Data;
using Data.Service;
using GAN_Developer_Test.Models;

namespace GAN_Developer_Test.Controllers.api
{
    public class FileController : ApiController
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        [Route("api/file/upload")]
        public IHttpActionResult Upload(FilePostModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            if (string.IsNullOrEmpty(model.FileBase64String))
            {
                throw new ArgumentNullException("fileContent");
            }
            if (string.IsNullOrEmpty(model.FileName))
            {
                throw new ArgumentNullException("fileName");
            }
            var fileByte = Convert.FromBase64String(model.FileBase64String);
            var file = _fileService.SaveFile(model.Group, model.FileName, fileByte, model.ContentType);

            return this.Ok(file.Id);
        }

        [HttpGet]
        [Route("api/file/getAll")]
        public IHttpActionResult GetAll()
        {
            var files = _fileService.GetAll();
            var results = new List<FileViewModel>();
            if (files.Any())
            {
                foreach (var ganFile in files)
                {
                    var result = new FileViewModel();
                    result.FileId = ganFile.FileId;
                    result.FileName = ganFile.FileName;
                    result.UploadDate = ganFile.UploadDate;
                    result.FileGroup = ganFile.FileGroup.ToString();

                    results.Add(result);
                }
            }
            return this.Ok(results);
        }

        [HttpGet]
        [Route("api/file/download/{fileId}")]
        public System.Net.Http.HttpResponseMessage DownloadFile(string fileId)
        {
            if (string.IsNullOrEmpty(fileId))
            {
                throw new ArgumentNullException("fileId");
            }
            var ganFile = _fileService.DownloadFile(fileId);
            System.IO.Stream stream = null;
            stream = new System.IO.MemoryStream(ganFile.File);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new System.Net.Http.StreamContent(stream)
            };
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = ganFile.FileName
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
    }
}
