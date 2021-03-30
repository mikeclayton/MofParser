﻿using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Text;
using System.Linq;

namespace Kingsland.MofParser.EditMof
{

    class Program
    {

        static void Main(string[] args)
        {

            Program.ModifyTokens();

        }

        private static void ModifyTokens()
        {

            // this example shows a way to modify a token stream to change the value of a property.
            // for example, changing this line:
            //
            // Name = "Web-Server";
            //
            // to
            //
            // Name = "Another-Web-Server";

            const string sourceText = @"
instance of MSFT_RoleResource as $MSFT_RoleResource1ref
{
    ResourceID = ""[WindowsFeature]IIS"";
    Ensure = ""Present"";
    SourceInfo = ""D:\\dsc\\MyServerConfig.ps1::6::9::WindowsFeature"";
    Name = ""Web-Server"";
    ModuleName = ""PSDesiredStateConfiguration"";
    ModuleVersion = ""1.0"";
};";

            // turn the text into a stream of characters for lexing
            var reader = SourceReader.From(sourceText);

            // lex the characters into a sequence of tokens
            var tokens = Lexer.Lex(reader);

            // find the "Name" identifier token
            var name = tokens
                .OfType<IdentifierToken>()
                .First(t => t.Name == "Name");

            // find the "Name" value token
            var oldValue = tokens
                .SkipWhile(t => !object.ReferenceEquals(t, name))
                .Skip(1)
                .OfType<StringLiteralToken>()
                .First();

            // build the token to replace into the token stream
            var newValue = TokenFactory.StringLiteralToken("Another-Web-Server");

            // replace the token
            tokens[tokens.IndexOf(oldValue)] = newValue;

            // generate the new source text
            var newSource = TokenSerializer.ConvertToMofText(tokens);

        }

    }

}
