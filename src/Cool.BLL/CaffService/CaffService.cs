using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cool.Common.DTOs;
using Cool.Common.Enums;
using Cool.Common.Exceptions;
using Cool.Common.RequestContext;
using Cool.Dal;
using Cool.Dal.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cool.Bll.CaffService
{
    public class CaffService : ICaffService
    {
        private const string ParserPath = "NativeParser.exe";
        private const string CaffFilesPath = "../CaffFiles/";

        private readonly IRequestContext _requestContext;
        private readonly CoolDbContext _dbContext;
        private readonly ILogger<CaffService> _logger;
        private readonly IMapper _mapper;

        public CaffService(IRequestContext requestContext, CoolDbContext dbContext, ILogger<CaffService> logger, IMapper mapper)
        {
            _requestContext = requestContext;
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<CaffDto>> GetAllCaffs()
        {
            _logger.LogDebug("User {username} is querying every caff", _requestContext.UserName);

            var caffs = await _dbContext.Caffs.OrderByDescending(c => c.CreationTime).ToListAsync();
            List<CaffDto> caffDtos = _mapper.Map<List<CaffDto>>(caffs);
            var tags = await _dbContext.Tags.ToListAsync();
            var comments = await _dbContext.Comments.ToListAsync();      

            for(int i=0; i<caffDtos.Count; i++)
            {
                CaffDto caff = caffDtos[i];
                var cafftags = tags.FindAll(x => x.CaffId == caff.Id);
                caff.Tags.AddRange(_mapper.Map<List<TagDto>>(cafftags));

                var caffcomments = comments.FindAll(x => x.CaffId == caff.Id);
                caff.Comments.AddRange(_mapper.Map<List<CommentDto>>(caffcomments));

                if (!File.Exists($"{caffs[i].FilePath}-bitmap1.bmp"))
                    throw new NotFoundException($"Preview of Caff {caffs[i].Id} could not be found!");
                caff.PreviewBitmap = File.ReadAllBytes($"{caffs[i].FilePath}-bitmap1.bmp");
            }

            _logger.LogDebug("User {username} successfully queryied every caff", _requestContext.UserName);

            return caffDtos;
        }

        public async Task<List<CaffDto>> GetOwnCaffs()
        {
            _logger.LogDebug("User {username} is querying every caff of the user", _requestContext.UserName);

            var caffs = await _dbContext.Caffs.Where(x => x.Creator == _requestContext.UserName)
            .OrderByDescending(c => c.CreationTime).ToListAsync();
            List<CaffDto> caffDtos = _mapper.Map<List<CaffDto>>(caffs);
            var tags = await _dbContext.Tags.ToListAsync();
            var comments = await _dbContext.Comments.ToListAsync();

            for (int i = 0; i < caffDtos.Count; i++)
            {
                CaffDto caff = caffDtos[i];
                var cafftags = tags.FindAll(x => x.CaffId == caff.Id);
                caff.Tags.AddRange(_mapper.Map<List<TagDto>>(cafftags));

                var caffcomments = comments.FindAll(x => x.CaffId == caff.Id);
                caff.Comments.AddRange(_mapper.Map<List<CommentDto>>(caffcomments));

                if (!File.Exists($"{caffs[i].FilePath}-bitmap1.bmp"))
                    throw new NotFoundException($"Preview of Caff {caffs[i].Id} could not be found!");
                caff.PreviewBitmap = File.ReadAllBytes($"{caffs[i].FilePath}-bitmap1.bmp");
            }

            _logger.LogDebug("User {username} successfully queryied every caff of the user", _requestContext.UserName);

            return caffDtos;
        }

        public async Task<List<CaffDto>> GetCaffsByTags(List<string> tags)
        {
            _logger.LogDebug("User {username} is querying caffs by tags {tags}", _requestContext.UserName, tags);

            var caffs = await _dbContext.Caffs.OrderByDescending(c => c.CreationTime).ToListAsync();
            List<CaffDto> caffDtos = _mapper.Map<List<CaffDto>>(caffs);
            List<CaffDto> filtered = new List<CaffDto>();
            var dbtags = await _dbContext.Tags.ToListAsync();
            var comments = await _dbContext.Comments.ToListAsync();

            for (int i = 0; i < caffDtos.Count; i++)
            {
                CaffDto caff = caffDtos[i];
                var cafftags = dbtags.FindAll(x => x.CaffId == caff.Id);
                if(CompareTags(cafftags, tags))
                {
                    caff.Tags.AddRange(_mapper.Map<List<TagDto>>(cafftags));
                    
                    var caffcomments = comments.FindAll(x => x.CaffId == caff.Id);
                    caff.Comments.AddRange(_mapper.Map<List<CommentDto>>(caffcomments));

                    if (!File.Exists($"{caffs[i].FilePath}-bitmap1.bmp"))
                        throw new NotFoundException($"Preview of Caff {caffs[i].Id} could not be found!");
                    caff.PreviewBitmap = File.ReadAllBytes($"{caffs[i].FilePath}-bitmap1.bmp");
                    
                    filtered.Add(caff);
                }           
            }

            _logger.LogDebug("User {username} successfully queryied caffs by tags {tags}", _requestContext.UserName, tags);

            return filtered;
        }

        public async Task<int> UploadCaff(UploadCaffDto dto)
        {
            _logger.LogDebug("User {username} is uploading a caff", _requestContext.UserName);
            Directory.CreateDirectory(CaffFilesPath);
            
            Caff caff = new Caff
            {
                Creator = _requestContext.UserName,
                CreationTime = DateTime.Now,
                FilePath = $"{CaffFilesPath}{Guid.NewGuid()}.caff",
            };
            
            foreach(string tag in dto.Tags)
            {
                caff.Tags.Add(new Tag { Text = tag });
            }

            _dbContext.Caffs.Add(caff);

            await GenerateImages(caff, dto.File);
            await _dbContext.SaveChangesAsync();

            _logger.LogDebug("Caff {caffid} successfully uploaded by {username}", caff.Id, _requestContext.UserName);

            return caff.Id;
        }

        public async Task<(byte[], string)> DownloadCaff(int caffId)
        {
            _logger.LogDebug("User {username} is downloading CaffId={caffid}", _requestContext.UserName, caffId);

            var caff = await _dbContext.Caffs.SingleOrDefaultAsync(c => c.Id == caffId);

            if (caff == null)
                throw new NotFoundException($"Caff {caffId} could not be found!");

            if(!File.Exists(caff.FilePath))
                throw new NotFoundException($"Caff file of {caffId} could not be found!");

            _logger.LogDebug("User {username} downloaded CaffId={caffid}", _requestContext.UserName, caffId);

            return (File.ReadAllBytes(caff.FilePath), caff.FilePath.Split('\\')[^1]);           
        }

        public async Task DeleteCaff(int caffId)
        {
            _logger.LogDebug("User {username} is removing CaffId={caffid}", _requestContext.UserName, caffId);

            var caff = await _dbContext.Caffs.SingleOrDefaultAsync(c => c.Id == caffId);          

            if (caff == null)
                throw new NotFoundException($"Caff {caffId} could not be found!");

            if (_requestContext.UserName != caff.Creator && _requestContext.Role != Role.Admin)
                throw new BadRequestException($"{_requestContext.UserName} is not the creator of the Caff, and they aren't an admin, can't delete the caff!");

            var tags = await _dbContext.Tags.Where(x => x.CaffId == caffId).ToListAsync();
            var comments = await _dbContext.Comments.Where(x => x.CaffId == caffId).ToListAsync();

            _dbContext.Caffs.Remove(caff);
            _dbContext.Tags.RemoveRange(tags);
            _dbContext.Comments.RemoveRange(comments);
            await _dbContext.SaveChangesAsync();

            DirectoryInfo taskDirectory = new DirectoryInfo(CaffFilesPath);
            FileInfo[] taskFiles = taskDirectory.GetFiles($"{caff.FilePath.Substring(CaffFilesPath.Length)}*");
            foreach (FileInfo file in taskFiles)
                file.Delete();

            _logger.LogDebug("CaffId={id} successfully removed by {userName}", caffId, _requestContext.UserName);
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

        private async Task GenerateImages(Caff caff, IFormFile file)
        {
            await Task.Run(() =>
            {
                using (Stream fileStream = new FileStream(caff.FilePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                ProcessStartInfo startInfo = new ProcessStartInfo(ParserPath);
                startInfo.Arguments = caff.FilePath;
                startInfo.CreateNoWindow = true;
                startInfo.RedirectStandardOutput = true;
                Process process = new Process
                {
                    StartInfo = startInfo
                };
                process.Start();
                while (!process.StandardOutput.EndOfStream)
                {
                    string line = process.StandardOutput.ReadLine();
                    _logger.LogDebug(line);
                }
                process.WaitForExit();
                if (!File.Exists($"{caff.FilePath}-bitmap1.bmp"))
                {
                    File.Delete(caff.FilePath);
                    throw new BadRequestException("Uploaded file is invalid. Preview generation failed.");
                }
            });           
        }

        private bool CompareTags(List<Tag> tags, List<string> tagnames)
        {
            foreach(string tag in tagnames)
            {
                if (tags.Find(x => x.Text == tag) == null)
                    return false;
            }
            return true;
        }
    }
}
