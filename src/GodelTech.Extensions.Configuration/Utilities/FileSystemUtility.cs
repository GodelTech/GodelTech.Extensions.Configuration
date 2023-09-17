using System.IO;

namespace GodelTech.Extensions.Configuration.Utilities
{
    internal class FileSystemUtility : IFileSystemUtility
    {
        public IDirectoryInfoWrapper CreateDirectoryInfo(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            var directoryInfoWrapper = new DirectoryInfoWrapper(directoryInfo);
            return directoryInfoWrapper;
        }

        public IFileInfoWrapper CreateFileInfo(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            var fileInfoWrapper = new FileInfoWrapper(fileInfo);
            return fileInfoWrapper;
        }
    }
}
