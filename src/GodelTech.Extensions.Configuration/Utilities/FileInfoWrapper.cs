using System.IO;

namespace GodelTech.Extensions.Configuration.Utilities
{
    internal class FileInfoWrapper : IFileInfoWrapper
    {
        private readonly FileInfo _fileInfo;

        public FileInfoWrapper(FileInfo fileInfo)
        {
            _fileInfo = fileInfo;
        }

        public bool Exists => _fileInfo.Exists;
    }
}
