﻿using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;
using System;

namespace Kingsland.MofParser.Ast
{

    /// <summary>
    /// </summary>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.5.7 Parameter declaration
    ///
    ///     parameterDeclaration      = [ qualifierList ] ( primitiveParamDeclaration /
    ///                                 complexParamDeclaration /
    ///                                 enumParamDeclaration /
    ///                                 referenceParamDeclaration )
    ///
    ///     primitiveParamDeclaration = primitiveType parameterName [ array ]
    ///                                 [ "=" primitiveTypeValue ]
    ///
    ///     complexParamDeclaration   = structureOrClassName parameterName [ array ]
    ///                                 [ "=" ( complexTypeValue / aliasIdentifier ) ]
    ///
    ///     enumParamDeclaration      = enumName parameterName [ array ]
    ///                                 [ "=" enumValue ]
    ///
    ///     referenceParamDeclaration = classReference parameterName [ array ]
    ///                                 [ "=" referenceTypeValue ]
    ///
    ///     parameterName             = IDENTIFIER
    ///
    /// 7.5.6 Method declaration
    ///
    ///     classReference            = DT_REFERENCE
    ///
    /// 7.5.10 Reference type declaration
    ///
    ///     DT_REFERENCE              = className REF
    ///
    /// </remarks>
    public sealed record ParameterDeclarationAst : AstNode
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.QualifierList = new QualifierListAst();
            }

            public QualifierListAst QualifierList
            {
                get;
                set;
            }

            public IdentifierToken? ParameterType
            {
                get;
                set;
            }

            public IdentifierToken? ParameterRef
            {
                get;
                set;
            }

            public IdentifierToken? ParameterName
            {
                get;
                set;
            }

            public bool ParameterIsArray
            {
                get;
                set;
            }

            public PropertyValueAst? DefaultValue
            {
                get;
                set;
            }

            public ParameterDeclarationAst Build()
            {
                return new ParameterDeclarationAst(
                    this.QualifierList,
                    this.ParameterType ?? throw new InvalidOperationException(
                        $"{nameof(this.ParameterType)} property must be set before calling {nameof(Build)}."
                    ),
                    this.ParameterRef,
                    this.ParameterName ?? throw new InvalidOperationException(
                        $"{nameof(this.ParameterName)} property must be set before calling {nameof(Build)}."
                    ),
                    this.ParameterIsArray,
                    this.DefaultValue
                );
            }

        }

        #endregion

        #region Constructors

        internal ParameterDeclarationAst(
            QualifierListAst qualifierList,
            IdentifierToken parameterType,
            IdentifierToken? parameterRef,
            IdentifierToken parameterName,
            bool parameterIsArray,
            PropertyValueAst? defaultValue
        )
        {
            this.QualifierList = qualifierList;
            this.ParameterType = parameterType;
            this.ParameterRef = parameterRef;
            this.ParameterName = parameterName;
            this.ParameterIsArray = parameterIsArray;
            this.DefaultValue = defaultValue;
        }

        #endregion

        #region Properties

        public QualifierListAst QualifierList
        {
            get;
            private init;
        }

        public IdentifierToken ParameterType
        {
            get;
            private init;
        }

        public IdentifierToken ParameterName
        {
            get;
            private init;
        }

        public bool ParameterIsRef
        {
            get
            {
                return (this.ParameterRef != null);
            }
        }

        public IdentifierToken? ParameterRef
        {
            get;
            private init;
        }

        public bool ParameterIsArray
        {
            get;
            private init;
        }

        public PropertyValueAst? DefaultValue
        {
            get;
            private init;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return AstMofGenerator.ConvertParameterDeclarationAst(this);
        }

        #endregion

    }

}

