﻿using Kingsland.MofParser.Parsing;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed record PragmaToken : SyntaxToken
    {

        #region Constructors

        public PragmaToken()
            : this(SourceExtent.Empty)
        {
        }

        public PragmaToken(SourcePosition start, SourcePosition end, string text)
            : this(new SourceExtent(start, end, text))
        {
        }

        public PragmaToken(SourceExtent extent)
            : base(extent)
        {
        }

        #endregion

        #region SyntaxToken Interface

        public override string GetSourceString()
        {
            return (this.Extent != SourceExtent.Empty) ?
                this.Extent.Text :
                Constants.PRAGMA;
        }

        #endregion
    
    }

}
