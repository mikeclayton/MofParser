﻿using Kingsland.MofParser.Tokens;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.5.9 Complex type value

        public static class ComplexValueArrayTests
        {

            [Test]
            public static void ComplexValueArrayWithOneItemShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {$MyAliasIdentifier};\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // instance of GOLF_ClubMember
                    .IdentifierToken("instance")
                    .WhitespaceToken(" ")
                    .IdentifierToken("of")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_ClubMember")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // LastPaymentDate = {$MyAliasIdentifier};
                    .IdentifierToken("LastPaymentDate")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .BlockOpenToken()
                    .AliasIdentifierToken("MyAliasIdentifier")
                    .BlockCloseToken()
                    .StatementEndToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void ComplexValueArrayWithMultipleItemsShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {$MyAliasIdentifier, $MyOtherAliasIdentifier};\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // instance of GOLF_ClubMember
                    .IdentifierToken("instance")
                    .WhitespaceToken(" ")
                    .IdentifierToken("of")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_ClubMember")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // LastPaymentDate = {$MyAliasIdentifier, $MyOtherAliasIdentifier};
                    .IdentifierToken("LastPaymentDate")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .BlockOpenToken()
                    .AliasIdentifierToken("MyAliasIdentifier")
                    .CommaToken()
                    .WhitespaceToken(" ")
                    .AliasIdentifierToken("MyOtherAliasIdentifier")
                    .BlockCloseToken()
                    .StatementEndToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

        }

        #endregion

    }

}