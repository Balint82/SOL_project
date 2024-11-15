using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Sol_server_api.Helpers;

namespace Sol_server_api.Tests
{
    public class JWTServiceTests
    {
        [Fact]
        public void Generate_ValidLoginId_ReturnsJwtToken()
        {
            // Arrange
            string loginName = "janedoe";
            var jwtService = new JWTService();

            // Act
            var jwtToken = jwtService.GenerateSecurityToken(loginName);

            // Assert
            Assert.NotNull(jwtToken);
            // Add more assertions based on expected token structure or properties
        }

        [Fact]
        public void Verify_ValidJwtToken_ReturnsValidJwtSecurityToken()
        {
            // Arrange
            var jwtToken = "your_valid_jwt_token_here";
            var jwtService = new JWTService();

            // Act
            var validatedToken = jwtService.Verify(jwtToken);

            // Assert
            Assert.NotNull(validatedToken);
            // Add more assertions based on expected token properties or claims
        }
    }
}
