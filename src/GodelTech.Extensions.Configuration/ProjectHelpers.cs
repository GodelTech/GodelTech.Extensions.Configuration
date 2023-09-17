using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using GodelTech.Extensions.Configuration.Utilities;

[assembly: CLSCompliant(true)]
[assembly: InternalsVisibleTo("GodelTech.Extensions.Configuration.Tests")]
namespace GodelTech.Extensions.Configuration
{
    /// <summary>
    /// ProjectHelpers.
    /// </summary>
    public static class ProjectHelpers
    {
        /// <summary>
        /// Gets the full path to the target project.
        /// </summary>
        /// <param name="projectRelativePath">
        /// The parent directory of the target project.
        /// e.g. src, samples, test, or test/Websites
        /// </param>
        /// <param name="startupAssembly">The target project's assembly.</param>
        /// <param name="fileSystemUtility">The file system utility.</param>
        /// <returns>The full path to the target project.</returns>
        public static string GetProjectPath(
            string projectRelativePath,
            Assembly startupAssembly,
            IFileSystemUtility fileSystemUtility = default(FileSystemUtility))
        {
            if (startupAssembly == null) throw new ArgumentNullException(nameof(startupAssembly));

            fileSystemUtility = fileSystemUtility ?? new FileSystemUtility();

            // Get name of the target project which we want to test
            var projectName = startupAssembly.GetName().Name;

            // Get currently executing test project path
            var applicationBasePath = AppContext.BaseDirectory;

            // Find the path to the target project
            var directoryInfo = fileSystemUtility.CreateDirectoryInfo(applicationBasePath);
            do
            {
                directoryInfo = directoryInfo.Parent;

                if (directoryInfo == null) break;

                var projectDirectoryInfo = fileSystemUtility.CreateDirectoryInfo(Path.Combine(directoryInfo.FullName, projectRelativePath));
                if (projectDirectoryInfo.Exists)
                {
                    var projectFileInfo = fileSystemUtility.CreateFileInfo(Path.Combine(projectDirectoryInfo.FullName, projectName, $"{projectName}.csproj"));
                    if (projectFileInfo.Exists)
                    {
                        return Path.Combine(projectDirectoryInfo.FullName, projectName);
                    }
                }
            }
            while (directoryInfo.Parent != null);

            throw new DirectoryNotFoundException($"Project root could not be located using the application root {applicationBasePath}.");
        }
    }
}
