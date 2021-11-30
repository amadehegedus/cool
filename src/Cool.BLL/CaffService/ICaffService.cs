using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cool.Common.DTOs;

namespace Cool.Bll.CaffService
{
    public interface ICaffService
    {
        public Task<List<CaffDto>> GetAllCaffs();

        public Task<List<CaffDto>> GetCaffsByTags(List<string> tags);

        public Task UploadCaff(UploadCaffDto dto);

        public Task<byte[]> DownloadCaff(int caffId);

        public Task DeleteCaff(int caffId);

        public Task AddTag(int caffId, string tag);

        public Task RemoveTag(int tagId);

        public Task AddComment(int caffId, string comment);

        public Task RemoveComment(int commentId);
    }
}
