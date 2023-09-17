using System.IO;

namespace GodelTech.Extensions.Configuration.Utilities
{
    /// <summary>
    /// Wrapper for <see cref="FileInfo"/>.
    /// </summary>
    public interface IFileInfoWrapper
    {
        /// <summary>
        /// Exists.
        /// </summary>
        bool Exists { get; }
    }
}
