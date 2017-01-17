﻿using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class StringValueAst : LiteralValueAst
    {

        #region Constructors

        private StringValueAst()
        {
        }

        #endregion

        #region Properties

        public string Value
        {
            get;
            private set;
        }

        #endregion

        #region Parsing Methods

        internal new static StringValueAst Parse(ParserState state)
        {
            return new StringValueAst
            {
                Value = state.Read<StringLiteralToken>().Value
            };
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertToMof(this);
        }

        #endregion

    }

}
