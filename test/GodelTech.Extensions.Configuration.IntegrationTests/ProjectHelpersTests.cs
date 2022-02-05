using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Xunit;

namespace GodelTech.Extensions.Configuration.IntegrationTests
{
    public class ProjectHelpersTests
    {
        private readonly string _applicationBasePath;

        public ProjectHelpersTests()
        {
            _applicationBasePath = AppContext.BaseDirectory;
        }

        [Fact]
        public void GetProjectPath_Success()
        {
            // Arrange
            var path = new Regex(@"\S+\\GodelTech.Extensions.Configuration\\").Match(_applicationBasePath).Value;
            if (string.IsNullOrWhiteSpace(path))
            {
                path = new Regex(@"\S+\\GodelTech.Extensions.Configuration.IntegrationTests").Match(_applicationBasePath).Value;
                path = path.Replace(@"\test\GodelTech.Extensions.Configuration.IntegrationTests", string.Empty);
            }

            // Act & Assert
            Assert.Equal(
                Path.Combine(path, "src", "GodelTech.Extensions.Configuration"),
                ProjectHelpers.GetProjectPath("src", typeof(ProjectHelpers).GetTypeInfo().Assembly)
            );
        }

        [Fact]
        public void GetProjectPath_WhenDirectoryNotFound_ThrowsDirectoryNotFoundException()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<DirectoryNotFoundException>(
                () => ProjectHelpers.GetProjectPath(
                    "incorrect",
                    typeof(ProjectHelpers).GetTypeInfo().Assembly
                )
            );

            Assert.Equal($"Project root could not be located using the application root {_applicationBasePath}.", exception.Message);
        }

        [Fact]
        public void GetProjectPath_WhenProjectNotFound_ThrowsDirectoryNotFoundException()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<DirectoryNotFoundException>(
                () => ProjectHelpers.GetProjectPath(
                    Path.Combine("src", "GodelTech.Extensions.Configuration"),
                    typeof(ProjectHelpers).GetTypeInfo().Assembly
                )
            );

            Assert.Equal($"Project root could not be located using the application root {_applicationBasePath}.", exception.Message);
        }
    }
}