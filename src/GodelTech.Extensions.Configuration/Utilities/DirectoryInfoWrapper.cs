using System.IO;

namespace GodelTech.Extensions.Configuration.Utilities
{
    internal class DirectoryInfoWrapper : IDirectoryInfoWrapper
    {
        private readonly DirectoryInfo _directoryInfo;

        public DirectoryInfoWrapper(DirectoryInfo directoryInfo)
        {
            _directoryInfo = directoryInfo;
        }

        public IDirectoryInfoWrapper Parent
        {
            get
            {
                if (_directoryInfo.Parent == null) return null;

                return new DirectoryInfoWrapper(_directoryInfo.Parent);
            }
        }

        public string FullName => _directoryInfo.FullName;

        public bool Exists => _directoryInfo.Exists;
    }
}
