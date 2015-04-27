using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Data;
using Data.Service;
using GAN_Developer_Test.Controllers.api;
using GAN_Developer_Test.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTest
{
    [TestClass]
    public class FileControllerTest
    {
        private List<GANFile> _ganFiles;
        private GANFile _ganFile1;
        private GANFile _ganFile2;

        [TestInitialize]
        public void Initialize()
        {
            _ganFiles = new List<GANFile>();
            _ganFile1 = new GANFile()
                {
                    FileId = "553e627ad900da2980da723f",
                    ContentLength = 96,
                    ContentType = "text/plain",
                    File = null,
                    FileGroup = FileGroup.Commmunication,
                    FileName = "BlogtoFollow.txt",
                    UploadDate = "4/27/2015 9:23 PM"
                };
            _ganFile2 = new GANFile()
            {
                FileId = "553e62b7d900da2980da7244",
                ContentLength = 2942,
                ContentType = "",
                File = null,
                FileGroup = FileGroup.Commmunication,
                FileName = "CoverLetter.txt",
                UploadDate = "4/28/2015 9:24 PM"
            };
            _ganFiles.Add(_ganFile1);
            _ganFiles.Add(_ganFile2);
        }

        [TestMethod]
        public void Get()
        {
            // Arrange
            var mockFileService = new Mock<IFileService>();
            mockFileService.Setup(x => x.GetAll()).Returns(_ganFiles);

            var controller = new FileController(mockFileService.Object);

            // Act
            IHttpActionResult actionResult = controller.GetAll();
            var contentResult = actionResult as OkNegotiatedContentResult<List<FileViewModel>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);

        }

    }
}
