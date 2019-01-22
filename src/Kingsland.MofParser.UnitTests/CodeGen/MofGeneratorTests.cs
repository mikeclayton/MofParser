﻿using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Source;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static class RoundtripTests
    {

        #region 7.3 Compiler directives

        public static class CompilerDirectiveTests
        {

            [Test]
            public static void CompilerDirectiveShouldRoundtrip()
            {
                var expectedMof =
                    "#pragma include (\"GlobalStructs/GOLF_Address.mof\")";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void CompilerDirectiveWithMultipleSingleStringsShouldRoundtrip()
            {
                var expectedMof =
                    "#pragma include (\"GlobalStructs\" \"/\" \"GOLF_Address.mof\")";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.5.1 Structure declaration

        public static class StructureDeclarationTests
        {

            [Test]
            public static void EmptyStructureDeclarationShouldRoundtrip()
            {
                var expectedMof =
                    "structure Sponsor\r\n" +
                    "{\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void StructureDeclarationWithSuperstructureShouldRoundtrip()
            {
                var expectedMof =
                    "structure Sponsor : GOLF_MySupestructure\r\n" +
                    "{\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void StructureDeclarationWithStructureFeaturesShouldRoundtrip()
            {
                var expectedMof =
                    "structure Sponsor\r\n" +
                    "{\r\n" +
                    "\tstring Name;\r\n" +
                    "\tGOLF_Date ContractSignedDate;\r\n" +
                    "\treal32 ContractAmount;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        public static class StructureFeatureTests
        {

            [Test]
            public static void StructureFeatureWithQualifierShouldRoundtrip()
            {
                var expectedMof =
                    "structure Sponsor\r\n" +
                    "{\r\n" +
                    "\t[Description(\"Monthly salary in $US\")] string Name;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void InvalidStructureFeatureShouldThrow()
            {
                var expectedMof =
                    "structure Sponsor\r\n" +
                    "{\r\n" +
                    "\t100\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                Assert.Throws<UnexpectedTokenException>(
                    () => {
                        var actualAst = Parser.Parse(actualTokens);
                    },
                    "Unexpected token found at Position 23, Line Number 3, Column Number 2.\r\n" +
                    "Token Type: 'IntegerLiteralToken'\r\n" +
                    "Token Text: '100'"
                );
            }

            [Test]
            public static void StructureFeatureWithStructureDeclarationShouldRoundtrip()
            {
                var expectedMof =
                    "structure Sponsor\r\n" +
                    "{\r\n" +
                    "\tstructure Nested\r\n" +
                    "\t{\r\n" +
                    "\t};\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void StructureFeatureWithEnumerationDeclarationShouldRoundtrip()
            {
                var expectedMof =
                    "structure Sponsor\r\n" +
                    "{\r\n" +
                    "\tenumeration MonthsEnum : Integer\r\n" +
                    "\t{\r\n" +
                    "\t};\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void StructureFeatureWithPropertyDeclarationShouldRoundtrip()
            {
                var expectedMof =
                    "structure Sponsor\r\n" +
                    "{\r\n" +
                    "\tstring Name;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.5.2 Class declaration

        public static class ClassDeclarationTests
        {

            [Test]
            public static void EmptyClassDeclarationShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void ClassDeclarationWithSuperclassShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Base : GOLF_Superclass\r\n" +
                    "{\r\n" +
                    "\tstring InstanceID;\r\n" +
                    "\tstring Caption = Null;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void ClassDeclarationWithClassFeaturesShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tstring InstanceID;\r\n" +
                    "\tstring Caption = Null;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void ClassDeclarationsWithQualifierListShouldRoundtrip()
            {
                var expectedMof =
                    "[Abstract, OCL{\"-- the key property cannot be NULL\", \"inv: InstanceId.size() = 10\"}]\r\n" +
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\t[Description(\"an instance of a class that derives from the GOLF_Base class. \"), Key] string InstanceID;\r\n" +
                    "\t[Description(\"A short textual description (one- line string) of the\"), MaxLen(64)] string Caption = Null;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void ClassDeclarationsAstWithMofV2QualifierFlavorsShouldRoundtrip()
            {
                var expectedMof =
                    "[Locale(1033): ToInstance, UUID(\"{BE46D060-7A7C-11d2-BC85-00104B2CF71C}\"): ToInstance]\r\n" +
                    "class Win32_PrivilegesStatus : __ExtendedStatus\r\n" +
                    "{\r\n" +
                    "\t[read: ToSubClass, MappingStrings{\"Win32API|AccessControl|Windows NT Privileges\"}: ToSubClass] string PrivilegesNotHeld[];\r\n" +
                    "\t[read: ToSubClass, MappingStrings{\"Win32API|AccessControl|Windows NT Privileges\"}: ToSubClass] string PrivilegesRequired[];\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        public static class ClassFeatureTests
        {

            [Test]
            public static void ClassFeatureWithQualifiersShouldRoundtrip()
            {
                var expectedMof =
                    "class Sponsor\r\n" +
                    "{\r\n" +
                    "\t[Description(\"Monthly salary in $US\")] string Name;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void InvalidClassFeatureShouldThrow()
            {
                var expectedMof =
                    "class Sponsor\r\n" +
                    "{\r\n" +
                    "\t100\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                Assert.Throws<UnexpectedTokenException>(
                    () => {
                        var actualAst = Parser.Parse(actualTokens);
                    },
                    "Unexpected token found at Position 23, Line Number 3, Column Number 2.\r\n" +
                    "Token Type: 'IntegerLiteralToken'\r\n" +
                    "Token Text: '100'"
                );
            }

            [Test]
            public static void ClassFeatureWithStructureDeclarationShouldRoundtrip()
            {
                var expectedMof =
                    "class Sponsor\r\n" +
                    "{\r\n" +
                    "\tstructure Nested\r\n" +
                    "\t{\r\n" +
                    "\t};\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void ClassFeatureWithEnumerationDeclarationShouldRoundtrip()
            {
                var expectedMof =
                    "class Sponsor\r\n" +
                    "{\r\n" +
                    "\tenumeration MonthsEnum : Integer\r\n" +
                    "\t{\r\n" +
                    "\t};\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void ClassFeatureWithPropertyDeclarationShouldRoundtrip()
            {
                var expectedMof =
                    "class Sponsor\r\n" +
                    "{\r\n" +
                    "\tstring Name;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.5.3 Association declaration

        public static class AssociationDeclarationTests
        {

            [Test]
            public static void EmptyAssociationDeclarationShouldRoundtrip()
            {
                var expectedMof =
                    "association GOLF_MemberLocker\r\n" +
                    "{\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void AssociationDeclarationWithSuperassociationShouldRoundtrip()
            {
                var expectedMof =
                    "association GOLF_MemberLocker : GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tGOLF_ClubMember REF Member;\r\n" +
                    "\tGOLF_Locker REF Locker;\r\n" +
                    "\tGOLF_Date AssignedOnDate;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void AssociationDeclarationWithClassFeaturesShouldRoundtrip()
            {
                var expectedMof =
                    "association GOLF_MemberLocker\r\n" +
                    "{\r\n" +
                    "\tGOLF_ClubMember REF Member;\r\n" +
                    "\tGOLF_Locker REF Locker;\r\n" +
                    "\tGOLF_Date AssignedOnDate;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.5.4 Enumeration declaration

        public static class EnumerationDeclarationTests
        {

            [Test]
            public static void EmptyIntegerEnumerationDeclarationShouldRoundtrip()
            {
                var expectedMof =
                    "enumeration MonthsEnum : Integer\r\n" +
                    "{\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void EmptyStringEnumerationDeclarationShouldRoundtrip()
            {
                var expectedMof =
                    "enumeration MonthsEnum : String\r\n" +
                    "{\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void EmptyInheritedEnumerationDeclarationShouldRoundtrip()
            {
                var expectedMof =
                    "enumeration MonthsEnum : GOLF_MyEnum\r\n" +
                    "{\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void EnumerationDeclarationWithoutValuesShouldRoundtrip()
            {
                var expectedMof =
                    "enumeration MonthsEnum : String\r\n" +
                    "{\r\n" +
                    "\tJanuary,\r\n" +
                    "\tFebruary,\r\n" +
                    "\tMarch,\r\n" +
                    "\tApril,\r\n" +
                    "\tMay,\r\n" +
                    "\tJune,\r\n" +
                    "\tJuly,\r\n" +
                    "\tAugust,\r\n" +
                    "\tSeptember,\r\n" +
                    "\tOctober,\r\n" +
                    "\tNovember,\r\n" +
                    "\tDecember\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        public static class EnumElementTests
        {

            [Test]
            public static void EnumElementWithQualifiersShouldRoundtrip()
            {
                var expectedMof =
                    "enumeration MonthsEnum : integer\r\n" +
                    "{\r\n" +
                    "\t[Description(\"myDescription\")] January = 1\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/41")]
            public static void IntegerEnumElementShouldRoundtrip()
            {
                var expectedMof =
                    "enumeration MonthsEnum : integer\r\n" +
                    "{\r\n" +
                    "\tJanuary = 1\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void StringEnumElementWithoutValueShouldRoundtrip()
            {
                var expectedMof =
                    "enumeration GOLF_StatesEnum : string\r\n" +
                    "{\r\n" +
                    "\tAL\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void StringEnumElementWithValueShouldRoundtrip()
            {
                var expectedMof =
                    "enumeration GOLF_StatesEnum : string\r\n" +
                    "{\r\n" +
                    "\tAL = \"Alabama\"\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.5.5 Property declaration

        public static class PropertyDeclarationTests
        {

            [Test]
            public static void PropertyDeclarationShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tInteger Severity;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void PropertyDeclarationWithArrayTypeShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tInteger Severity[];\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void PropertyDeclarationWithDefaultValueShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tInteger Severity = 0;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.5.6 Method declaration

        public static class MethodDeclarationTests
        {

            [Test]
            public static void MethodDeclarationWithNoParametersShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Club\r\n" +
                    "{\r\n" +
                    "\tInteger GetMembersWithOutstandingFees();\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void MethodDeclarationWithParameterShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Club\r\n" +
                    "{\r\n" +
                    "\tInteger GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers);\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void MethodDeclarationWithArrayParameterShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Club\r\n" +
                    "{\r\n" +
                    "\tInteger GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers[]);\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void MethodDeclarationsWithRefParameterShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Club\r\n" +
                    "{\r\n" +
                    "\tInteger GetMembersWithOutstandingFees(GOLF_ClubMember REF lateMembers);\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/27")]
            public static void ClassDeclarationsWithMethodDeclarationWithEnumParameterShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Professional : GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tGOLF_ResultCodeEnum GetNumberOfProfessionals(Integer NoOfPros, GOLF_Club Club, ProfessionalStatusEnum Status = Professional);\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/28"),
             Ignore("https://github.com/mikeclayton/MofParser/issues/28")]
            public static void ClassDeclarationsWithMethodDeclarationWithDeprecatedPrimtitiveValueTypeInitializerShouldRoundtrip()
            {
                var expectedMof =
                    "class Win32_SoftwareFeature : CIM_SoftwareFeature\r\n" +
                    "{\r\n" +
                    "\tuint32 Reinstall(uint16 ReinstallMode = 1);\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/37")]
            public static void MethodDeclarationsWithArrayReturnTypeShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Club\r\n" +
                    "{\r\n" +
                    "\tInteger[] GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers);\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/38")]
            public static void MethodDeclarationWithMultipleParametersShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Professional : GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tGOLF_ResultCodeEnum GetNumberOfProfessionals(Integer NoOfPros, GOLF_Club Club, ProfessionalStatusEnum Status = Professional);\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.5.9 Complex type value

        public static class ComplexTypeValueTests
        {

            [Test]
            public static void ComplexTypeValueWithComplexValuePropertyShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = $MyAliasIdentifier;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void ComplexTypeValueWithComplexValueArrayPropertyShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {$MyAliasIdentifier};\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        public static class ComplexValueTests
        {

            [Test]
            public static void ComplexValuePropertyShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = $MyAliasIdentifier;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void ComplexValuePropertyWithValueOfShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = value of GOLF_Date\r\n" +
                    "\t{\r\n" +
                    "\t\tMonth = July;\r\n" +
                    "\t};\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        public static class ComplexValueArrayTests
        {

            [Test]
            public static void ComplexValueArrayWithOneItemShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {$MyAliasIdentifier};\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void ComplexValueArrayWithMultipleItemsShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {$MyAliasIdentifier, $MyOtherAliasIdentifier};\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.6.1 Primitive type value

        public static class LiteralValueArrayTests
        {

            [Test]
            public static void LiteralValueArrayWithOneItemShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {1};\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void LiteralValueArrayWithMultipleItemsShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {1, 2};\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        public static class LiteralValueTests
        {

            [Test]
            public static void IntegerLiteralValueShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = 1;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void RealLiteralValueShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = 0.5;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void BooleanLiteralValueShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = true;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void NullLiteralValueShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = null;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void StringLiteralValueShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = \"aaa\";\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.6.1.1 Integer values

        public static class IntegerValueTests
        {

            [Test]
            public static void IntegerValueShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = 100;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test, Ignore("")]
            public static void PositiveIntegerValueShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = +100;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void NegativeIntegerValueShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = -100;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
            public static void IntegerValuePropertiesInOtherBasesShouldRoundtrip()
            {
                var expectedMof =
                    "instance of myType as $Alias00000070\r\n" +
                    "{\r\n" +
                    "\tMyBinaryValue1 = 0101010b;\r\n" +
                    "\tMyBinaryValue2 = +0101010b;\r\n" +
                    "\tMyBinaryValue3 = -0101010b;\r\n" +
                    "\tMyOctalValue1 = 000444444;\r\n" +
                    "\tMyOctalValue2 = +000444444;\r\n" +
                    "\tMyOctalValue3 = -000444444;\r\n" +
                    "\tMyHexValue1 = 0x00ABC123;\r\n" +
                    "\tMyHexValue2 = +0x00ABC123;\r\n" +
                    "\tMyHexValue3 = -0x00ABC123;\r\n" +
                    "\tMyDecimalValue1 = 12345;\r\n" +
                    "\tMyDecimalValue2 = +12345;\r\n" +
                    "\tMyDecimalValue3 = -12345;\r\n" +
                    "\tMyRealValue1 = 00123.45;\r\n" +
                    "\tMyRealValue2 = +00123.45;\r\n" +
                    "\tMyRealValue3 = -123.45;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }


        }

        #endregion

        #region 7.6.1.1 Real values

        public static class RealValueTests
        {

            [Test]
            public static void RealValueShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = 0.5;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
            public static void PositiveRealValueShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = +0.5;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void NegativeRealValueShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = -0.5;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
            public static void RealValueWithNoFractionShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = 5.0;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
            public static void RealValueWithTrailingZerosShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = 0.50;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
            public static void RealValueWithNoIntegerPartShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = .5;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.6.1.3 String values

        public static class StringValueTests
        {

            [Test]
            public static void SingleStringValueShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = \"Instance of John Doe\";\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void MultistringValueShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = \"Instance\" \"of\" \"John\" \"Doe\";\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/20")]
            public static void StringValueWithSingleQuoteShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = \"Instance of John Doe\\\'s GOLF_ClubMember object\";\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.6.1.5 Boolean value

        public static class BooleanValueTests
        {

            [Test]
            public static void BooleanValueAstShouldRoundtrip()
            {
                var expectedMof =
                    "instance of myType as $Alias00000070\r\n" +
                    "{\r\n" +
                    "\tReference = TRUE;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.6.2 Complex type value

        public static class InstanceValueDeclarationTests
        {

            [Test]
            public static void InstanceValueDeclarationWithNoPropertiesShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void InstanceValueDeclarationWithChildPropertiesShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tFirstName = \"John\";\r\n" +
                    "\tLastName = \"Doe\";\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void InstanceValueDeclarationWithAliasShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember as $MyAliasIdentifier\r\n" +
                    "{\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        public static class StructureValueDeclarationTests
        {

            [Test]
            public static void StructureValueDeclarationWithNoPropertiesShouldRoundtrip()
            {
                var expectedMof =
                    "value of GOLF_ClubMember as $MyAliasIdentifier\r\n" +
                    "{\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void StructureValueDeclarationWithChildPropertiesShouldRoundtrip()
            {
                var expectedMof =
                    "value of GOLF_ClubMember as $MyAliasIdentifier\r\n" +
                    "{\r\n" +
                    "\tFirstName = \"John\";\r\n" +
                    "\tLastName = \"Doe\";\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        //[Test]
        //public static void InstanceValueDeclarationShouldRoundtrip()
        //{
        //    var expectedMof =
        //        "instance of GOLF_ClubMember\r\n" +
        //        "{\r\n" +
        //        "\tCaption = \"Instance of John Doe\\\'s GOLF_ClubMember object\";\r\n" +
        //        "};";
        //    var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
        //    var actualAst = Parser.Parse(actualTokens);
        //    var actualMof = MofGenerator.ConvertToMof(actualAst);
        //    Assert.AreEqual(expectedMof, actualMof);
        //}

        //    [Test]
        //    public static void ClassDeclarationsAstWithNumericPropertiesShouldRoundtrip()
        //    {
        //        var expectedMof =
        //            "instance of myType as $Alias00000070\r\n" +
        //            "{\r\n" +
        //            "\tMyBinaryValue = 0101010b;\r\n" +
        //            "\tMyOctalValue = 0444444;\r\n" +
        //            "\tMyHexValue = 0xABC123;\r\n" +
        //            "\tMyDecimalValue = 12345;\r\n" +
        //            "\tMyRealValue = 123.45;\r\n" +
        //            "};";
        //        var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
        //        var actualAst = Parser.Parse(actualTokens);
        //        var actualMof = MofGenerator.ConvertToMof(actualAst);
        //        Assert.AreEqual(expectedMof, actualMof);
        //    }

        //    [Test(Description = "https://github.com/mikeclayton/MofParser/issues/26"),
        //     Ignore("https://github.com/mikeclayton/MofParser/issues/26")]
        //    public static void InstanceValueDeclarationsWithInstanceValuePropertyShouldRoundtrip()
        //    {
        //        var expectedMof =
        //            "instance of GOLF_ClubMember\r\n" +
        //            "{\r\n" +
        //            "\tLastPaymentDate = instance of GOLF_Date\r\n" +
        //            "\t{\r\n" +
        //            "\tYear = 2011;\r\n" +
        //            "\tMonth = July;\r\n" +
        //            "\tDay = 31;\r\n" +
        //            "\t};\r\n" +
        //            "}";
        //        var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
        //        var actualAst = Parser.Parse(actualTokens);
        //        var actualMof = MofGenerator.ConvertToMof(actualAst);
        //        Assert.AreEqual(expectedMof, actualMof);
        //    }

        //    [Test]
        //    public static void InstanceValueDeclarationWithStructureValueDeclarationPropertyShouldRoundtrip()
        //    {
        //        var expectedMof =
        //            "instance of GOLF_ClubMember\r\n" +
        //            "{\r\n" +
        //            "\tCaption = \"Instance of John Doe\\\'s GOLF_ClubMember object\";\r\n" +
        //            "\tMemberAddress = value of GOLF_Address\r\n" +
        //            "\t{\r\n" +
        //            "\t\tState = \"IL\";\r\n" +
        //            "\t\tCity = \"Oak Park\";\r\n" +
        //            "\t\tStreet = \"Oak Park Av.\";\r\n" +
        //            "\t\tStreetNo = \"1177\";\r\n" +
        //            "\t\tApartmentNo = \"3B\";\r\n" +
        //            "\t};\r\n" +
        //            "};";
        //        var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
        //        var actualAst = Parser.Parse(actualTokens);
        //        var actualMof = MofGenerator.ConvertToMof(actualAst);
        //        Assert.AreEqual(expectedMof, actualMof);
        //    }

        //    [Test]
        //    public static void StructureValueDeclarationShouldRoundtrip()
        //    {
        //        var expectedMof =
        //            "value of GOLF_PhoneNumber as $JohnDoesPhoneNo\r\n" +
        //            "{\r\n" +
        //            "\tAreaCode = {\"9\", \"0\", \"7\"};\r\n" +
        //            "\tNumber = {\"7\", \"4\", \"7\", \"4\", \"8\", \"8\", \"4\"};\r\n" +
        //            "};";
        //        var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
        //        var actualAst = Parser.Parse(actualTokens);
        //        var actualMof = MofGenerator.ConvertToMof(actualAst);
        //        Assert.AreEqual(expectedMof, actualMof);
        //    }


        #endregion

        #region 7.6.3 Enum type value

        public static class EnumTypeValueTests
        {

            [Test]
            public static void EnumTypeValueWithEnumValueShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tYear = 2011;\r\n" +
                    "\tMonth = July;\r\n" +
                    "\tDay = 31;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void EnumTypeValueWithEnumValueArrayShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tYear = 2011;\r\n" +
                    "\tMonth = {June};\r\n" +
                    "\tDay = 31;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        public static class EnumValueTests
        {

            [Test]
            public static void UnqalifiedEnumValueShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tYear = 2011;\r\n" +
                    "\tMonth = July;\r\n" +
                    "\tDay = 31;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void QualifiedEnumValueShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tYear = 2011;\r\n" +
                    "\tMonth = MonthEnums.July;\r\n" +
                    "\tDay = 31;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        public static class EnumValueArrayTests
        {

            [Test]
            public static void EnumValueArrayWithSingleEnumValueShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tYear = 2011;\r\n" +
                    "\tMonth = {June};\r\n" +
                    "\tDay = 31;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            public static void EnumValueArrayWithMultipleEnumValuesShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = {January, February};\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/25")]
            public static void EnumValueArrayWithQualifiedEnumValuesShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = {MonthEnums.July};\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region Roundtrip Test Cases

        //[TestFixture]
        //public static class ConvertToMofMethodTestCasesWmiWinXp
        //{
        //    [Test, TestCaseSource(typeof(ConvertToMofMethodTestCasesWmiWinXp), "GetTestCases")]
        //    public static void ConvertToMofMethodTestsFromDisk(string mofFilename)
        //    {
        //        ConvertToMofTests.MofGeneratorRoundtripTest(mofFilename);
        //    }
        //    public static IEnumerable<TestCaseData> GetTestCases
        //    {
        //        get
        //        {
        //            return TestUtils.GetMofTestCase("Parsing\\WMI\\WinXp");
        //        }
        //    }
        //}

        //[TestFixture]
        //public static class ConvertToMofMethodGolfExamples
        //{
        //    //[Test, TestCaseSource(typeof(ConvertToMofMethodGolfExamples), "GetTestCases")]
        //    public static void ConvertToMofMethodTestsFromDisk(string mofFilename)
        //    {
        //        ConvertToMofTests.MofGeneratorRoundtripTest(mofFilename);
        //    }
        //    public static IEnumerable<TestCaseData> GetTestCases
        //    {
        //        get
        //        {
        //            return TestUtils.GetMofTestCase("Parsing\\DSP0221_3.0.1");
        //        }
        //    }
        //}

        //private static void MofGeneratorRoundtripTest(string mofFilename)
        //{
        //    var expectedMof = File.ReadAllText(mofFilename);
        //    var reader = SourceReader.From(expectedMof);
        //    var tokens = Lexing.Lexer.Lex(reader);
        //    var ast = Parser.Parse(tokens);
        //    var actualMof = MofGenerator.ConvertToMof(ast);
        //    Assert.AreEqual(expectedMof, actualMof);
        //}

        #endregion

    }

}