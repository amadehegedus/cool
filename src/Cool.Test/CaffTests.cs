using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Cool.Bll.CaffService;
using Cool.Bll.Mappings;
using Cool.Common.DTOs;
using Cool.Common.Exceptions;
using Cool.Common.RequestContext;
using Cool.Dal;
using Cool.Test.Database;
using Cool.Test.Users;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Cool.Test
{
    [TestClass]
    public class CaffTests
    {
        private CoolDbContext _dbContext;
        private ICaffService _caffService;
        private ILogger<CaffService> _logger;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _dbContext = TestDb.GetContext();
            _logger = Mock.Of<ILogger<CaffService>>();
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new Mappings()));
            _mapper = mapperConfig.CreateMapper();

            Directory.CreateDirectory("../../../CaffTestFiles");
            File.Copy("../../../../NativeParser/1.caff", "../../../CaffTestFiles/1.caff", true);
            File.Copy("../../../../NativeParser/1.caff-bitmap1.bmp", "../../../CaffTestFiles/1.caff-bitmap1.bmp", true);
            File.Copy("../../../../NativeParser/2.caff", "../../../CaffTestFiles/2.caff", true);
            File.Copy("../../../../NativeParser/1.caff-bitmap2.bmp", "../../../CaffTestFiles/2.caff-bitmap1.bmp", true);
        }

        [TestCleanup]
        public void TearDown()
        {
            _dbContext.Dispose();
            DirectoryInfo di = new DirectoryInfo("../../../CaffTestFiles");
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

        private void CreateCaffServiceForUser(IRequestContext requestContext)
        {
            _caffService = new CaffService(requestContext, _dbContext, _logger, _mapper);

            _caffService.SetCaffFilesPath("../../../CaffTestFiles/");           
            _caffService.SetParserPath("../../../../NativeParser/NativeParser.exe");
        }

        [TestMethod]
        public void CreatorCanAddTag()
        {
            CreateCaffServiceForUser(new User1RequestContext());

            _caffService.AddTag(1, "new");

            Assert.AreEqual(3, _dbContext.Tags.Count(t => t.CaffId == 1));
            Assert.IsTrue(_dbContext.Tags.Any(t => t.CaffId == 1 && t.Text == "new"));
        }

        [TestMethod]
        public void OtherUserCantAddTag()
        {
            CreateCaffServiceForUser(new User2RequestContext());

            _caffService.AddTag(1, "new");

            Assert.AreEqual(2, _dbContext.Tags.Count(t => t.CaffId == 1));
            Assert.IsTrue(_dbContext.Tags.Where(t => t.CaffId == 1).All(t => t.Text != "new"));
        }

        [TestMethod]
        public void AdminCanAddTag()
        {
            CreateCaffServiceForUser(new AdminRequestContext());

            _caffService.AddTag(1, "new");

            Assert.AreEqual(3, _dbContext.Tags.Count(t => t.CaffId == 1));
            Assert.IsTrue(_dbContext.Tags.Any(t => t.CaffId == 1 && t.Text == "new"));
        }


        [TestMethod]
        public void CreatorCanDeleteTag()
        {
            CreateCaffServiceForUser(new User1RequestContext());

            _caffService.RemoveTag(1);

            Assert.AreEqual(1, _dbContext.Tags.Count(t => t.CaffId == 1));
        }

        [TestMethod]
        public void OtherUserCantDeleteTag()
        {
            CreateCaffServiceForUser(new User2RequestContext());

            _caffService.RemoveTag(1);

            Assert.AreEqual(2, _dbContext.Tags.Count(t => t.CaffId == 1));
        }

        [TestMethod]
        public void AdminCanDeleteTag()
        {
            CreateCaffServiceForUser(new AdminRequestContext());

            _caffService.RemoveTag(1);

            Assert.AreEqual(1, _dbContext.Tags.Count(t => t.CaffId == 1));
        }

        [TestMethod]
        public void CreatorCanAddComment()
        {
            CreateCaffServiceForUser(new User1RequestContext());

            _caffService.AddComment(1, "comment");

            Assert.AreEqual(3, _dbContext.Comments.Count(c => c.CaffId == 1));
            Assert.IsTrue(_dbContext.Comments.Any(c => c.CaffId == 1 && c.Message == "comment"));
        }

        [TestMethod]
        public void OtherUserCanAddComment()
        {
            CreateCaffServiceForUser(new User2RequestContext());

            _caffService.AddComment(1, "comment");

            Assert.AreEqual(3, _dbContext.Comments.Count(c => c.CaffId == 1));
            Assert.IsTrue(_dbContext.Comments.Any(c => c.CaffId == 1 && c.Message == "comment"));
        }

        [TestMethod]
        public void AdminCanAddComment()
        {
            CreateCaffServiceForUser(new AdminRequestContext());

            _caffService.AddComment(1, "comment");

            Assert.AreEqual(3, _dbContext.Comments.Count(c => c.CaffId == 1));
            Assert.IsTrue(_dbContext.Comments.Any(c => c.CaffId == 1 && c.Message == "comment"));
        }

        [TestMethod]
        public void CommenterCanRemoveComment()
        {
            CreateCaffServiceForUser(new User1RequestContext());

            _caffService.RemoveComment(1);

            Assert.AreEqual(1, _dbContext.Comments.Count(c => c.CaffId == 1));
        }

        [TestMethod]
        public void OtherUserCantRemoveComment()
        {
            CreateCaffServiceForUser(new User2RequestContext());

            _caffService.RemoveComment(1);

            Assert.AreEqual(2, _dbContext.Comments.Count(c => c.CaffId == 1));
        }

        [TestMethod]
        public void AdminCanRemoveComment()
        {
            CreateCaffServiceForUser(new User1RequestContext());

            _caffService.RemoveComment(1);

            Assert.AreEqual(1, _dbContext.Comments.Count(c => c.CaffId == 1));
        }

        /*
        [TestMethod]
        public void UserCanUploadCaff()
        {
            CreateCaffServiceForUser(new User1RequestContext());

            var stream = new MemoryStream(File.ReadAllBytes("../../../CaffTestFiles/2.caff"));
            FormFile formFile = new FormFile(stream, 0, stream.Length, null, "2.caff")
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream"
            };
            UploadCaffDto dto = new UploadCaffDto { File = formFile };

            int id =  _caffService.UploadCaff(dto).Result;

            Assert.AreEqual(3, _dbContext.Caffs.Count());
            Assert.IsTrue(_dbContext.Caffs.Any(c => c.Id == id && c.Creator == "testUser1"));
            Assert.IsTrue(File.Exists(_dbContext.Caffs.First(c => c.Id == id).FilePath));
            Assert.IsTrue(File.Exists(_dbContext.Caffs.First(c => c.Id == id).FilePath+"-bitmap1.bmp"));
        }
        */

        [TestMethod]
        public void UserCanDownloadCaff()
        {
            CreateCaffServiceForUser(new User1RequestContext());

            var stream = new MemoryStream(File.ReadAllBytes("../../../CaffTestFiles/2.caff"));
            FormFile formFile = new FormFile(stream, 0, stream.Length, "File", "2.caff");
            UploadCaffDto dto = new UploadCaffDto { File = formFile };

            int id = _caffService.UploadCaff(dto).Result;

            var result = _caffService.DownloadCaff(id).Result;
            var expected = stream.ToArray();
            Assert.IsTrue(expected.SequenceEqual(result.Item1));
        }

        [TestMethod]
        public void UserCanGetAllCaffs()
        {
            CreateCaffServiceForUser(new User1RequestContext());
            var bitmap1 = File.ReadAllBytes("../../../CaffTestFiles/1.caff-bitmap1.bmp");
            var bitmap2 = File.ReadAllBytes("../../../CaffTestFiles/2.caff-bitmap1.bmp");

            var result = _caffService.GetAllCaffs().Result;

            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result[0].PreviewBitmap.SequenceEqual(bitmap1));
            Assert.IsTrue(result[1].PreviewBitmap.SequenceEqual(bitmap2));
            Assert.AreEqual(result[0].Id, 1);
            Assert.AreEqual(result[1].Id, 2);
            Assert.AreEqual(result[0].Creator, "testUser1");
            Assert.AreEqual(result[1].Creator, "testUser2");
        }

        [TestMethod]
        public void UserCanGetOwnCaffs()
        {
            CreateCaffServiceForUser(new User1RequestContext());
            var bitmap1 = File.ReadAllBytes("../../../CaffTestFiles/1.caff-bitmap1.bmp");

            var result = _caffService.GetOwnCaffs().Result;

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0].PreviewBitmap.SequenceEqual(bitmap1));
            Assert.AreEqual(result[0].Id, 1);
            Assert.AreEqual(result[0].Creator, "testUser1");
        }

        [TestMethod]
        public void UserCanGetCaffsByTags()
        {
            CreateCaffServiceForUser(new User1RequestContext());
            var bitmap1 = File.ReadAllBytes("../../../CaffTestFiles/2.caff-bitmap1.bmp");

            var result = _caffService.GetCaffsByTags(new List<string>
            { "Special" }).Result;

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0].PreviewBitmap.SequenceEqual(bitmap1));
            Assert.AreEqual(result[0].Id, 2);
            Assert.AreEqual(result[0].Creator, "testUser2");
        }

        [TestMethod]
        public void UserCantGetCaffsByTags()
        {
            CreateCaffServiceForUser(new User1RequestContext());

            var result = _caffService.GetCaffsByTags(new List<string>
            { "Special", "Not exists" }).Result;

            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void UserCanDeleteCaff()
        {
            CreateCaffServiceForUser(new User1RequestContext());

            _caffService.DeleteCaff(1);

            Assert.IsTrue(_dbContext.Caffs.Count() == 1);
        }

        [TestMethod]
        public void OtherUserCantDeleteCaff()
        {
            CreateCaffServiceForUser(new User2RequestContext());

            _caffService.DeleteCaff(1);

            Assert.IsTrue(_dbContext.Caffs.Count() == 2);
        }

        [TestMethod]
        public void AdminCanDeleteCaff()
        {
            CreateCaffServiceForUser(new AdminRequestContext());

            _caffService.DeleteCaff(1);
            _caffService.DeleteCaff(2);

            Assert.IsTrue(_dbContext.Caffs.Count() == 0);
        }
    }
}
