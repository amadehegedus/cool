using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

using System.Threading.Tasks;
using Cool.Common.DTOs;
using Cool.Common.Enums;
using Cool.Common.Exceptions;
using Cool.Common.RequestContext;
using Cool.Dal;
using Cool.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cool.Bll.CaffService
{
    public class CaffService : ICaffService
    {
        private const string ParserPath= "../NativeParser/NativeParser.exe";
        private const string CaffFilesPath = "../NativeParser/";

        private readonly IRequestContext _requestContext;
        private readonly CoolDbContext _dbContext;
        private readonly ILogger<CaffService> _logger;

        public CaffService(IRequestContext requestContext, CoolDbContext dbContext, ILogger<CaffService> logger)
        {
            _requestContext = requestContext;
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<CaffDto>> GetAllCaffs()
        {
            throw new NotImplementedException();
        }

        public Task<List<CaffDto>> GetCaffsByTags(List<string> tags)
        {
            throw new NotImplementedException();
        }

        public Task UploadCaff(UploadCaffDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> DownloadCaff(int caffId)
        {
            return Task.Run(() =>
            {
                byte[] response = new byte[0];
                if (!File.Exists($"{CaffFilesPath}{caffId}.caff"))
                    return response;
                GenerateImages(caffId);
                Bitmap img = new Bitmap($"{CaffFilesPath}{caffId}.caff-bitmap1.bmp");
                using (var stream = new MemoryStream())
                {
                    img.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    response = stream.ToArray();
                }
                img.Dispose();
                return response;
            });
        }

        public Task DeleteCaff(int caffId)
        {
            throw new NotImplementedException();
        }

        public async Task AddTag(int caffId, string tag)
        {
            _logger.LogDebug("User {username} is adding the tag {tag} to CaffId={caffId}", _requestContext.UserName, tag, caffId);
            var caff = await _dbContext.Caffs.SingleOrDefaultAsync(c => c.Id == caffId);

            if(caff == null)
                throw new NotFoundException($"Caff {caffId} could not be found!");

            if(_requestContext.UserName != caff.Creator && _requestContext.Role != Role.Admin)
                throw new BadRequestException($"{_requestContext.UserName} is not the creator of the Caff, and they aren't an admin, can't add tag!");

            _dbContext.Tags.Add(new Tag
            {
                CaffId = caffId,
                Text = tag,
            });
            await _dbContext.SaveChangesAsync();

            _logger.LogDebug("Tag {tag} successfully added to CaffId={caffId} by {username}", tag, caffId, _requestContext.UserName);
        }

        public async Task RemoveTag(int tagId)
        {
            _logger.LogDebug("User {username} is removing tagId={tagid}", _requestContext.UserName, tagId);

            var tag = await _dbContext.Tags.Include(t => t.Caff).SingleOrDefaultAsync(t => t.Id == tagId);

            if(tag == null)
                throw new NotFoundException($"Tag {tagId} could not be found!");

            if(_requestContext.UserName != tag.Caff.Creator && _requestContext.Role != Role.Admin)
                throw new BadRequestException($"{_requestContext.UserName} is not the creator of the Caff, and they aren't an admin, can't delete the tag!");

            _dbContext.Tags.Remove(tag);
            await _dbContext.SaveChangesAsync();

            _logger.LogDebug("TagId={id} successfully removed by {userName}", tagId, _requestContext.UserName);
        }

        public async Task AddComment(int caffId, string comment)
        {
            _logger.LogDebug("User {username} is adding the comment {comment} to CaffId={caffId}", _requestContext.UserName, comment, caffId);
            var caff = await _dbContext.Caffs.SingleOrDefaultAsync(c => c.Id == caffId);

            if (caff == null)
                throw new NotFoundException($"Caff {caffId} could not be found!");

            _dbContext.Comments.Add(new Comment
            {
                CaffId = caffId,
                UserName = _requestContext.UserName,
                Message = comment,
                TimeStamp = DateTime.Now,
            });

            await _dbContext.SaveChangesAsync();
            _logger.LogDebug("Comment {c} successfully added to CaffId={caffId} by {username}", comment, caffId, _requestContext.UserName);
        }

        public async Task RemoveComment(int commentId)
        {
            _logger.LogDebug("User {username} is removing commentId={commentid}", _requestContext.UserName, commentId);

            var comment = await _dbContext.Comments.Include(t => t.Caff).SingleOrDefaultAsync(c => c.Id == commentId);

            if (comment == null)
                throw new NotFoundException($"Comment {commentId} could not be found!");

            if (_requestContext.UserName != comment.UserName && _requestContext.Role != Role.Admin)
                throw new BadRequestException($"{_requestContext.UserName} is not the creator of the comment, and they aren't an admin, can't delete the comment!");

            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();

            _logger.LogDebug("CommentId={id} successfully removed by {userName}", commentId, _requestContext.UserName);
        }

        private void GenerateImages(int caffId)
        {
            if (!File.Exists($"{CaffFilesPath}{caffId}.caff") || File.Exists($"{CaffFilesPath}{caffId}.caff-bitmap1.bmp"))
                return;
            ProcessStartInfo startInfo = new ProcessStartInfo(ParserPath);
            startInfo.Arguments = $"{CaffFilesPath}{caffId}.caff";
            startInfo.CreateNoWindow = true;
            Process process = new Process
            {
                StartInfo = startInfo
            };
            process.Start();
            process.WaitForExit();
        }
    }
}
