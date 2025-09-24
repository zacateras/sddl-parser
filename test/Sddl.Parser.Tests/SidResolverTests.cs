using System;
using Xunit;

namespace Sddl.Parser.Tests
{
    public class SidResolverTests
    {
        [Fact]
        public void Sid_WithoutOptions_ShouldUseLegacyBehavior()
        {
            // Arrange
            var unknownSidString = "S-1-5-21-1234567890-1234567890-1234567890-1001";

            // Act
            var sid = new Sid(unknownSidString);

            // Assert
            Assert.Equal(unknownSidString, sid.Raw);
            Assert.Equal($"Unknown({unknownSidString})", sid.Alias);
            Assert.False(sid.IsValid);
            Assert.Single(sid.Errors);
        }

        [Fact]
        public void Sid_WithDirectoryServiceDisabled_ShouldUseLegacyBehavior()
        {
            // Arrange
            var unknownSidString = "S-1-5-21-1234567890-1234567890-1234567890-1001";
            var options = new SidResolverOptions { EnableDirectoryServiceLookup = false };

            // Act
            var sid = new Sid(unknownSidString, options);

            // Assert
            Assert.Equal(unknownSidString, sid.Raw);
            Assert.Equal($"Unknown({unknownSidString})", sid.Alias);
            Assert.False(sid.IsValid);
            Assert.Single(sid.Errors);
        }

        [Fact]
        public void Sid_WithCustomResolver_ShouldUseCustomResolver()
        {
            // Arrange
            var unknownSidString = "S-1-5-21-1234567890-1234567890-1234567890-1001";
            var customResolver = new TestSidResolver();
            var options = new SidResolverOptions 
            { 
                EnableDirectoryServiceLookup = true,
                SidResolver = customResolver
            };

            // Act
            var sid = new Sid(unknownSidString, options);

            // Assert
            Assert.Equal(unknownSidString, sid.Raw);
            Assert.Equal("TestDomain\\TestUser", sid.Alias);
            Assert.True(sid.IsValid);
            Assert.Empty(sid.Errors);
        }

        [Fact]
        public void Sid_WithCustomResolverReturningNull_ShouldFallbackToUnknown()
        {
            // Arrange
            var unknownSidString = "S-1-5-21-9999999999-9999999999-9999999999-9999";
            var customResolver = new TestSidResolver();
            var options = new SidResolverOptions 
            { 
                EnableDirectoryServiceLookup = true,
                SidResolver = customResolver
            };

            // Act
            var sid = new Sid(unknownSidString, options);

            // Assert
            Assert.Equal(unknownSidString, sid.Raw);
            Assert.Equal($"Unknown({unknownSidString})", sid.Alias);
            Assert.False(sid.IsValid);
            Assert.Single(sid.Errors);
        }

        [Fact]
        public void Sid_KnownSid_ShouldIgnoreResolver()
        {
            // Arrange
            var knownSidString = "SY";
            var customResolver = new TestSidResolver();
            var options = new SidResolverOptions 
            { 
                EnableDirectoryServiceLookup = true,
                SidResolver = customResolver
            };

            // Act
            var sid = new Sid(knownSidString, options);

            // Assert
            Assert.Equal(knownSidString, sid.Raw);
            Assert.Equal("Local System", sid.Alias);
            Assert.True(sid.IsValid);
            Assert.Empty(sid.Errors);
        }

        [Fact]
        public void Sddl_WithSidResolverOptions_ShouldPassOptionsToSids()
        {
            // Arrange
            var sddlString = "O:S-1-5-21-1234567890-1234567890-1234567890-1001G:DAD:(A;;FA;;;S-1-5-21-1234567890-1234567890-1234567890-1001)";
            var customResolver = new TestSidResolver();
            var options = new SidResolverOptions 
            { 
                EnableDirectoryServiceLookup = true,
                SidResolver = customResolver
            };

            // Act
            var sddl = new Sddl(sddlString, SecurableObjectType.Unknown, options);

            // Assert
            Assert.NotNull(sddl.Owner);
            Assert.Equal("TestDomain\\TestUser", sddl.Owner.Alias);
            Assert.True(sddl.Owner.IsValid);
            
            Assert.NotNull(sddl.Dacl);
            Assert.NotEmpty(sddl.Dacl.Aces);
            Assert.NotNull(sddl.Dacl.Aces[0].AceSid);
            Assert.Equal("TestDomain\\TestUser", sddl.Dacl.Aces[0].AceSid.Alias);
            Assert.True(sddl.Dacl.Aces[0].AceSid.IsValid);
        }

        [Fact]
        public void DirectoryServiceSidResolver_WithValidSid_ShouldHandleGracefully()
        {
            // Arrange
            var resolver = new DirectoryServiceSidResolver();
            var validSidString = "S-1-1-0"; // Everyone - should be resolvable

            // Act
            var result = resolver.ResolveSid(validSidString);

            // Assert - either returns a name or null, both are acceptable
            // We can't assert specific behavior since it depends on the system
            Assert.True(result == null || !string.IsNullOrEmpty(result));
        }

        [Fact]
        public void DirectoryServiceSidResolver_WithInvalidSid_ShouldReturnNull()
        {
            // Arrange
            var resolver = new DirectoryServiceSidResolver();
            var invalidSidString = "not-a-sid";

            // Act
            var result = resolver.ResolveSid(invalidSidString);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DirectoryServiceSidResolver_WithNullSid_ShouldReturnNull()
        {
            // Arrange
            var resolver = new DirectoryServiceSidResolver();

            // Act
            var result = resolver.ResolveSid(null);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DirectoryServiceSidResolver_WithEmptySid_ShouldReturnNull()
        {
            // Arrange
            var resolver = new DirectoryServiceSidResolver();

            // Act
            var result = resolver.ResolveSid(string.Empty);

            // Assert
            Assert.Null(result);
        }
    }

    // Test helper class
    public class TestSidResolver : ISidResolver
    {
        public string ResolveSid(string sidString)
        {
            if (sidString == "S-1-5-21-1234567890-1234567890-1234567890-1001")
                return "TestDomain\\TestUser";
            return null;
        }
    }
}