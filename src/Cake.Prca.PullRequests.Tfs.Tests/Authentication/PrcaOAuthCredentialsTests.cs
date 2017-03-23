﻿namespace Cake.Prca.PullRequests.Tfs.Tests.Authentication
{
    using Shouldly;
    using Tfs.Authentication;
    using Xunit;

    public class PrcaOAuthCredentialsTests
    {
        public sealed class ThePrcaOAuthCredentialsCtor
        {
            [Fact]
            public void Should_Throw_If_Access_Token_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new PrcaOAuthCredentials(null));

                // Then
                result.IsArgumentNullException("accessToken");
            }

            [Fact]
            public void Should_Throw_If_Access_Token_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => new PrcaOAuthCredentials(string.Empty));

                // Then
                result.IsArgumentOutOfRangeException("accessToken");
            }

            [Fact]
            public void Should_Throw_If_Access_Token_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => new PrcaOAuthCredentials(" "));

                // Then
                result.IsArgumentOutOfRangeException("accessToken");
            }

            [Fact]
            public void Should_Set_Access_Token()
            {
                // Given
                const string accessToken = "foo";

                // When
                var credentials = new PrcaOAuthCredentials(accessToken);

                // Then
                credentials.AccessToken.ShouldBe(accessToken);
            }
        }
    }
}
