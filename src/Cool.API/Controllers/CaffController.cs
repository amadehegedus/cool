using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cool.Api.Authentication;
using Cool.Bll.CaffService;
using Cool.Common.DTOs;
using Cool.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cool.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = AuthenticationSchemes.JwtBearer, Roles = nameof(Role.User) + "," + nameof(Role.Admin))]
    public class CaffController : ControllerBase
    {
        private readonly ICaffService _caffService;

        public CaffController(ICaffService caffService)
        {
            _caffService = caffService;
        }

        [HttpGet]
        public async Task<List<CaffDto>> GetAllCaffs()
        {
            return await _caffService.GetAllCaffs();
        }

        [HttpGet]
        public async Task<List<CaffDto>> GetOwnCaffs()
        {
            return await _caffService.GetOwnCaffs();
        }

        [HttpGet]
        public async Task<List<CaffDto>> GetCaffsByTags(List<string> tags)
        {
            return await _caffService.GetCaffsByTags(tags);
        }

        [HttpPost]
        public async Task<int> UploadCaff(UploadCaffDto dto) => await _caffService.UploadCaff(dto);

        [HttpGet]
        public async Task<byte[]> DownloadCaff(int caffId)
        {
            return await _caffService.DownloadCaff(caffId);
        }

        [HttpDelete]
        public async Task DeleteCaff(int caffId)
        {
            await _caffService.DeleteCaff(caffId);
        }

        [HttpPost]
        public async Task AddTag(int caffId, string tag)
        {
            await _caffService.AddTag(caffId, tag);
        }

        [HttpDelete]
        public async Task RemoveTag(int tagId)
        {
            await _caffService.RemoveTag(tagId);
        }

        [HttpPost]
        public async Task AddComment(int caffId, string comment)
        {
            await _caffService.AddComment(caffId, comment);
        }

        [HttpDelete]
        public async Task RemoveComment(int commentId)
        {
            await _caffService.RemoveComment(commentId);
        }
    }
}
