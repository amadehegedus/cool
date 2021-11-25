using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cool.Common.DTOs;

namespace Cool.Bll.CaffService
{
    public class CaffService : ICaffService
    {
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
            throw new NotImplementedException();
        }

        public Task DeleteCaff(int caffId)
        {
            throw new NotImplementedException();
        }

        public Task AddTag(int caffId, string tag)
        {
            throw new NotImplementedException();
        }

        public Task RemoveTag(int tagId)
        {
            throw new NotImplementedException();
        }

        public Task AddComment(string comment)
        {
            throw new NotImplementedException();
        }

        public Task RemoveComment(int commentId)
        {
            throw new NotImplementedException();
        }
    }
}
