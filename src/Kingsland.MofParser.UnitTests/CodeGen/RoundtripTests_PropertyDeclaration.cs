﻿using Kingsland.MofParser.Tokens;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.5.5 Property declaration

        public static class PropertyDeclarationTests
        {

            [Test]
            public static void PropertyDeclarationShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tInteger Severity;\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                   // class GOLF_Base
                   .IdentifierToken("class")
                   .WhitespaceToken(" ")
                   .IdentifierToken("GOLF_Base")
                   .WhitespaceToken("\r\n")
                   // {
                   .BlockOpenToken()
                   .WhitespaceToken("\r\n\t")
                   // Integer Severity;
                   .IdentifierToken("Integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("Severity")
                   .StatementEndToken()
                   .WhitespaceToken("\r\n")
                   // };
                   .BlockCloseToken()
                   .StatementEndToken()
                   .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void PropertyDeclarationWithArrayTypeShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tInteger Severity[];\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                   // class GOLF_Base
                   .IdentifierToken("class")
                   .WhitespaceToken(" ")
                   .IdentifierToken("GOLF_Base")
                   .WhitespaceToken("\r\n")
                   // {
                   .BlockOpenToken()
                   .WhitespaceToken("\r\n\t")
                   // Integer Severity[];
                   .IdentifierToken("Integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("Severity")
                   .AttributeOpenToken()
                   .AttributeCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n")
                   // };
                   .BlockCloseToken()
                   .StatementEndToken()
                   .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void PropertyDeclarationWithDefaultValueShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tInteger Severity = 0;\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                   // class GOLF_Base
                   .IdentifierToken("class")
                   .WhitespaceToken(" ")
                   .IdentifierToken("GOLF_Base")
                   .WhitespaceToken("\r\n")
                   // {
                   .BlockOpenToken()
                   .WhitespaceToken("\r\n\t")
                   // Integer Severity = 0;
                   .IdentifierToken("Integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("Severity")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .IntegerLiteralToken(IntegerKind.DecimalValue, 0)
                   .StatementEndToken()
                   .WhitespaceToken("\r\n")
                   // };
                   .BlockCloseToken()
                   .StatementEndToken()
                   .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/28")]
            public static void PropertyDeclarationWithDeprecatedMof300IntegerReturnTypesAndQuirksDisabledShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tuint8 SeverityUint8;\r\n" +
                    "\tuint16 SeverityUint16;\r\n" +
                    "\tuint32 SeverityUint32;\r\n" +
                    "\tuint64 SeverityUint64;\r\n" +
                    "\tsint8 SeveritySint8;\r\n" +
                    "\tsint16 SeveritySint16;\r\n" +
                    "\tsint32 SeveritySint32;\r\n" +
                    "\tsint64 SeveritySint64;\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                   // class GOLF_Base
                   .IdentifierToken("class")
                   .WhitespaceToken(" ")
                   .IdentifierToken("GOLF_Base")
                   .WhitespaceToken("\r\n")
                   // {
                   .BlockOpenToken()
                   .WhitespaceToken("\r\n\t")
                    // uint8 SeverityUint8;
                    .IdentifierToken("uint8")
                    .WhitespaceToken(" ")
                    .IdentifierToken("SeverityUint8")
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // uint16 SeverityUint16;
                    .IdentifierToken("uint16")
                    .WhitespaceToken(" ")
                    .IdentifierToken("SeverityUint16")
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // uint32 SeverityUint32;
                    .IdentifierToken("uint32")
                    .WhitespaceToken(" ")
                    .IdentifierToken("SeverityUint32")
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // uint64 SeverityUint64;
                    .IdentifierToken("uint64")
                    .WhitespaceToken(" ")
                    .IdentifierToken("SeverityUint64")
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // sint8 SeveritySint8;
                    .IdentifierToken("sint8")
                    .WhitespaceToken(" ")
                    .IdentifierToken("SeveritySint8")
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // sint16 SeveritySint16;
                    .IdentifierToken("sint16")
                    .WhitespaceToken(" ")
                    .IdentifierToken("SeveritySint16")
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // sint32 SeveritySint32;
                    .IdentifierToken("sint32")
                    .WhitespaceToken(" ")
                    .IdentifierToken("SeveritySint32")
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // sint64 SeveritySint64;
                    .IdentifierToken("sint64")
                    .WhitespaceToken(" ")
                    .IdentifierToken("SeveritySint64")
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