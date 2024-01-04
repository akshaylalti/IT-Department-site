using System;

namespace DeparmentAPI.Model
{
    public class FileModel
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
