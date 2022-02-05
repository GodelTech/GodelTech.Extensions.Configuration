using System;
using Xunit;

namespace GodelTech.Extensions.Configuration.Tests
{
    public class ProjectHelpersTests
    {
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
    }
}