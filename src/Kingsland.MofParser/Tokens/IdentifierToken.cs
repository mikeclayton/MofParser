﻿using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class IdentifierToken : SyntaxToken
    {

        public IdentifierToken(SourceExtent extent, string name)
            : base(extent)
        {
            this.Name = name;
        }

        public string Name
        {
            get;
            private set;
        }

        public string GetNormalizedName()
        {
            var name = this.Name;
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }
            return name.ToLowerInvariant();
        }

    }

}
