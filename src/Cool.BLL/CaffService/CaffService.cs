using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

using System.Threading.Tasks;
using Cool.Common.DTOs;

namespace Cool.Bll.CaffService
{
    public class CaffService : ICaffService
    {
        private const string ParserPath= "../NativeParser/NativeParser.exe";
        private const string CaffFilesPath = "../NativeParser/";

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
