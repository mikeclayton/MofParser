﻿using System.Collections.Generic;

namespace Kingsland.MofParser.Ast
{
    public sealed class MethodAst : MemberAst
    {
        public class Argument
        {
            public QualifierListAst Qualifiers { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public bool IsRef { get; set; }
            public AstNode DefaultValue { get; set; }
        }

        public string ReturnType { get; set; }
        public List<Argument> Arguments { get; private set; }

        public MethodAst()
        {
            Arguments = new List<Argument>();
        }

    }

}