using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace Sol_server_api.Tests
{
    public class SecureKeyGeneratorTests
    {
        [Fact]
        public void GenerateSecureKey_ValidKeySize_ReturnsBase64String()
        {
            // Arrange
            int keySizeInBytes = 32; // Kulcsméret bájtban

            // Act
            var secureKey = SecureKeyGenerator.GenerateSecureKey(keySizeInBytes);

            // Assert
            Assert.NotNull(secureKey); // Ellenőrzi, hogy nem null a generált kulcs
            Assert.Equal(keySizeInBytes, Convert.FromBase64String(secureKey).Length); // Ellenőrzi, hogy a kulcs mérete megfelelő
        }
    }
}