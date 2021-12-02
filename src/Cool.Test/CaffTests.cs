using System.Linq;
using AutoMapper;
using Cool.Bll.CaffService;
using Cool.Common.Exceptions;
using Cool.Common.RequestContext;
using Cool.Dal;
using Cool.Test.Database;
using Cool.Test.Users;
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
            _mapper = Mock.Of<IMapper>();
        }

        [TestCleanup]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        private void CreateCaffServiceForUser(IRequestContext requestContext)
        {
            _caffService = new CaffService(requestContext, _dbContext, _logger, _mapper);
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
    }
}
