using System.IO;

namespace GodelTech.Extensions.Configuration.Utilities
{
    /// <summary>
    /// Wrapper for <see cref="DirectoryInfo"/>.
    /// </summary>
    public interface IDirectoryInfoWrapper
    {
        /// <summary>
        /// Parent.
        /// </summary>
        IDirectoryInfoWrapper Parent { get; }

        /// <summary>
        /// Full name.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Exists.
        /// </summary>
        bool Exists { get; }
    }
}
