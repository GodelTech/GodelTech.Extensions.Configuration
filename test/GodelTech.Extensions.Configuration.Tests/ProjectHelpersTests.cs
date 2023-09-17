using System;
using System.IO;
using System.Reflection;
using GodelTech.Extensions.Configuration.Utilities;
using Moq;
using Xunit;

namespace GodelTech.Extensions.Configuration.Tests
{
    public class ProjectHelpersTests
    {
        private readonly string _applicationBasePath;

        private readonly Mock<IFileSystemUtility> _mockFileSystemUtility;
        private readonly Mock<IDirectoryInfoWrapper> _mockDirectoryInfoWrapper;
        private readonly Mock<IFileInfoWrapper> _mockFileInfoWrapper;

        public ProjectHelpersTests()
        {
            _applicationBasePath = AppContext.BaseDirectory;

            _mockFileSystemUtility = new Mock<IFileSystemUtility>(MockBehavior.Strict);
            _mockDirectoryInfoWrapper = new Mock<IDirectoryInfoWrapper>(MockBehavior.Strict);
            _mockFileInfoWrapper = new Mock<IFileInfoWrapper>(MockBehavior.Strict);
        }

        [Fact]
        public void GetProjectPath_WhenStartupAssemblyIsNull_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => ProjectHelpers.GetProjectPath(
                    "src",
                    null
                )
            );

            Assert.Equal("startupAssembly", exception.ParamName);
        }

        [Fact]
        public void GetProjectPath_WhenParentIsNull_ThrowsDirectoryNotFoundException()
        {
            // Arrange
            _mockFileSystemUtility
                .Setup(x => x.CreateDirectoryInfo(_applicationBasePath))
                .Returns(_mockDirectoryInfoWrapper.Object);

            _mockDirectoryInfoWrapper
                .SetupSequence(x => x.Parent)
                .Returns(() => null);

            // Act & Assert
            var exception = Assert.Throws<DirectoryNotFoundException>(
                () => ProjectHelpers.GetProjectPath(
                    "src",
                    typeof(ProjectHelpers).GetTypeInfo().Assembly,
                    _mockFileSystemUtility.Object
                )
            );

            Assert.Equal($"Project root could not be located using the application root {_applicationBasePath}.", exception.Message);
        }

        [Fact]
        public void GetProjectPath_WhenProjectDirectoryInfoNotExist_ThrowsDirectoryNotFoundException()
        {
            // Arrange
            _mockFileSystemUtility
                .Setup(x => x.CreateDirectoryInfo(_applicationBasePath))
                .Returns(_mockDirectoryInfoWrapper.Object);

            _mockDirectoryInfoWrapper
                .SetupSequence(x => x.Parent)
                .Returns(_mockDirectoryInfoWrapper.Object);

            _mockDirectoryInfoWrapper
                .SetupSequence(x => x.FullName)
                .Returns("TestFullName");

            _mockFileSystemUtility
                .SetupSequence(x => x.CreateDirectoryInfo(Path.Combine("TestFullName", "src")))
                .Returns(_mockDirectoryInfoWrapper.Object);

            _mockDirectoryInfoWrapper
                .SetupSequence(x => x.Exists)
                .Returns(false);

            // Act & Assert
            var exception = Assert.Throws<DirectoryNotFoundException>(
                () => ProjectHelpers.GetProjectPath(
                    "src",
                    typeof(ProjectHelpers).GetTypeInfo().Assembly,
                    _mockFileSystemUtility.Object
                )
            );

            Assert.Equal($"Project root could not be located using the application root {_applicationBasePath}.", exception.Message);
        }

        [Fact]
        public void GetProjectPath_WhenProjectFileInfoExists_Success()
        {
            // Arrange
            _mockFileSystemUtility
                .Setup(x => x.CreateDirectoryInfo(_applicationBasePath))
                .Returns(_mockDirectoryInfoWrapper.Object);

            _mockDirectoryInfoWrapper
                .SetupSequence(x => x.Parent)
                .Returns(_mockDirectoryInfoWrapper.Object);

            _mockDirectoryInfoWrapper
                .SetupSequence(x => x.FullName)
                .Returns("TestFullName")
                .Returns(Path.Combine("TestFullName", "src"))
                .Returns(Path.Combine("TestFullName", "src"));

            _mockFileSystemUtility
                .SetupSequence(x => x.CreateDirectoryInfo(Path.Combine("TestFullName", "src")))
                .Returns(_mockDirectoryInfoWrapper.Object);

            _mockDirectoryInfoWrapper
                .SetupSequence(x => x.Exists)
                .Returns(true);

            _mockFileSystemUtility
                .SetupSequence(
                    x => x.CreateFileInfo(
                        Path.Combine(
                            "TestFullName",
                            "src",
                            "GodelTech.Extensions.Configuration",
                            "GodelTech.Extensions.Configuration.csproj"
                        )
                    )
                )
                .Returns(_mockFileInfoWrapper.Object);

            _mockFileInfoWrapper
                .SetupSequence(x => x.Exists)
                .Returns(true);

            // Act
            var result = ProjectHelpers.GetProjectPath(
                "src",
                typeof(ProjectHelpers).GetTypeInfo().Assembly,
                _mockFileSystemUtility.Object
            );

            // Assert
            Assert.Equal(
                Path.Combine(
                    "TestFullName",
                    "src",
                    "GodelTech.Extensions.Configuration"
                ),
                result
            );
        }

        [Fact]
        public void GetProjectPath_WhenProjectFileInfoNotExist_ThrowsDirectoryNotFoundException()
        {
            // Arrange
            _mockFileSystemUtility
                .Setup(x => x.CreateDirectoryInfo(_applicationBasePath))
                .Returns(_mockDirectoryInfoWrapper.Object);

            _mockDirectoryInfoWrapper
                .SetupSequence(x => x.Parent)
                .Returns(_mockDirectoryInfoWrapper.Object);

            _mockDirectoryInfoWrapper
                .SetupSequence(x => x.FullName)
                .Returns("TestFullName")
                .Returns(Path.Combine("TestFullName", "src"))
                .Returns(Path.Combine("TestFullName", "src"));

            _mockFileSystemUtility
                .SetupSequence(x => x.CreateDirectoryInfo(Path.Combine("TestFullName", "src")))
                .Returns(_mockDirectoryInfoWrapper.Object);

            _mockDirectoryInfoWrapper
                .SetupSequence(x => x.Exists)
                .Returns(true);

            _mockFileSystemUtility
                .SetupSequence(
                    x => x.CreateFileInfo(
                        Path.Combine(
                            "TestFullName",
                            "src",
                            "GodelTech.Extensions.Configuration",
                            "GodelTech.Extensions.Configuration.csproj"
                        )
                    )
                )
                .Returns(_mockFileInfoWrapper.Object);

            _mockFileInfoWrapper
                .SetupSequence(x => x.Exists)
                .Returns(false);

            // Act & Assert
            var exception = Assert.Throws<DirectoryNotFoundException>(
                () => ProjectHelpers.GetProjectPath(
                    "src",
                    typeof(ProjectHelpers).GetTypeInfo().Assembly,
                    _mockFileSystemUtility.Object
                )
            );

            Assert.Equal($"Project root could not be located using the application root {_applicationBasePath}.", exception.Message);
        }
    }
}
