using System.IO;

namespace GodelTech.Extensions.Configuration.Utilities
{
    /// <summary>
    /// File system utility.
    /// </summary>
    public interface IFileSystemUtility
    {
        /// <summary>
        /// Creates an <see cref="DirectoryInfo"/> instance.
        /// </summary>
        /// <param name="path">The directory path.</param>
        /// <returns>The <see cref="IDirectoryInfoWrapper"/> instance.</returns>
        IDirectoryInfoWrapper CreateDirectoryInfo(string path);

        /// <summary>
        /// Creates an <see cref="FileInfo"/> instance.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>The <see cref="IFileInfoWrapper"/> instance.</returns>
        IFileInfoWrapper CreateFileInfo(string fileName);
    }
}
