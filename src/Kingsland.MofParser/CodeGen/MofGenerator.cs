﻿using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingsland.MofParser.CodeGen
{

    public sealed class MofGenerator
    {

        #region Dispatcher

        public static string ConvertToMof(AstNode node, MofQuirks quirks = MofQuirks.None)
        {
            if (node == null)
            {
                return null;
            }
            switch (node)
            {
                case MofSpecificationAst ast:
                    // 7.2 MOF specification
                    return MofGenerator.ConvertMofSpecificationAst(ast, quirks);
                case CompilerDirectiveAst ast:
                    // 7.3 Compiler directives
                    return MofGenerator.ConvertCompilerDirectiveAst(ast, quirks);
                case QualifierTypeDeclarationAst ast:
                    // 7.4 Qualifiers
                    return MofGenerator.ConvertQualifierTypeDeclarationAst(ast, quirks);
                case QualifierListAst ast:
                    // 7.4.1 QualifierList
                    return MofGenerator.ConvertQualifierListAst(ast, quirks);
                case ClassDeclarationAst ast:
                    // 7.5.2 Class declaration
                    return MofGenerator.ConvertClassDeclarationAst(ast, quirks);
                case PropertyDeclarationAst ast:
                    // 7.5.5 Property declaration
                    return MofGenerator.ConvertPropertyDeclarationAst(ast, quirks);
                case MethodDeclarationAst ast:
                    // 7.5.6 Method declaration
                    return MofGenerator.ConvertMethodDeclarationAst(ast, quirks);
                case ParameterDeclarationAst ast:
                    // 7.5.7 Parameter declaration
                    return MofGenerator.ConvertParameterDeclarationAst(ast, quirks);
                case ComplexValueArrayAst ast:
                    // 7.5.9 Complex type value
                    return MofGenerator.ConvertComplexValueArrayAst(ast, quirks);
                case ComplexValueAst ast:
                    // 7.5.9 Complex type value
                    return MofGenerator.ConvertComplexValueAst(ast, quirks);
                case PropertyValueListAst ast:
                    // 7.5.9 Complex type value
                    return MofGenerator.ConvertPropertyValueListAst(ast, quirks);
                case LiteralValueArrayAst ast:
                    // 7.6.1 Primitive type value
                    return MofGenerator.ConvertLiteralValueArrayAst(ast, quirks);
                case IntegerValueAst ast:
                    // 7.6.1.1 Integer value
                    return MofGenerator.ConvertIntegerValueAst(ast, quirks);
                case RealValueAst ast:
                    // 7.6.1.2 Real value
                    return MofGenerator.ConvertRealValueAst(ast, quirks);
                case StringValueAst ast:
                    // 7.6.1.3 String values
                    return MofGenerator.ConvertStringValueAst(ast, quirks);
                case BooleanValueAst ast:
                    // 7.6.1.5 Boolean value
                    return MofGenerator.ConvertBooleanValueAst(ast, quirks);
                case NullValueAst ast:
                    // 7.6.1.6 Null value
                    return MofGenerator.ConvertNullValueAst(ast, quirks);
                case InstanceValueDeclarationAst ast:
                    // 7.6.2 Complex type value
                    return MofGenerator.ConvertInstanceValueDeclarationAst(ast, quirks);
                case StructureValueDeclarationAst ast:
                    // 7.6.2 Complex type value
                    return MofGenerator.ConvertStructureValueDeclarationAst(ast, quirks);
                default:
                    // unknown
                    throw new InvalidOperationException(node.GetType().FullName);
            }

        }

        #endregion

        #region 7.2 MOF specification

        public static string ConvertMofSpecificationAst(MofSpecificationAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            for (var i = 0; i < node.Productions.Count; i++)
            {
                if (i > 0)
                {
                    source.AppendLine();
                }
                source.Append(MofGenerator.ConvertMofProductionAst(node.Productions[i], quirks));
            }
            return source.ToString();
        }

        public static string ConvertMofProductionAst(MofProductionAst node, MofQuirks quirks = MofQuirks.None)
        {
            switch (node)
            {
                case CompilerDirectiveAst ast:
                    return MofGenerator.ConvertCompilerDirectiveAst(ast, quirks);
                case StructureDeclarationAst ast:
                    return MofGenerator.ConvertStructureDeclarationAst(ast, quirks);
                case ClassDeclarationAst ast:
                    return MofGenerator.ConvertClassDeclarationAst(ast, quirks);
                //case AssociationDeclarationAst ast:
                //case EnumerationDeclarationAst ast:
                case InstanceValueDeclarationAst ast:
                    return MofGenerator.ConvertInstanceValueDeclarationAst(ast, quirks);
                case StructureValueDeclarationAst ast:
                    return MofGenerator.ConvertStructureValueDeclarationAst(ast, quirks);
                case QualifierTypeDeclarationAst ast:
                    return MofGenerator.ConvertQualifierTypeDeclarationAst(ast, quirks);
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region 7.3 Compiler directives

        public static string ConvertCompilerDirectiveAst(CompilerDirectiveAst node, MofQuirks quirks = MofQuirks.None)
        {
            return string.Format("!!!!!{0}!!!!!", node.GetType().Name);
        }

        #endregion

        #region 7.4 Qualifiers

        public static string ConvertQualifierTypeDeclarationAst(QualifierTypeDeclarationAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            if (!string.IsNullOrEmpty(node.Name.Name))
            {
                source.Append(node.Name.Name);
            }
            if (node.Initializer != null)
            {
                if (node.Initializer is LiteralValueAst)
                {
                    source.AppendFormat("({0})", MofGenerator.ConvertPrimitiveTypeValueAst(node.Initializer, quirks));
                }
                else if (node.Initializer is LiteralValueArrayAst)
                {
                    source.Append(MofGenerator.ConvertPrimitiveTypeValueAst(node.Initializer, quirks));
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            if (node.Flavors.Count > 0)
            {
                source.AppendFormat(": {0}", string.Join(" ", node.Flavors.ToArray()));
            }
            return source.ToString();
        }

        #endregion

        #region 7.4.1 QualifierList

        public static string ConvertQualifierListAst(QualifierListAst node, MofQuirks quirks = MofQuirks.None)
        {

            // [Abstract, OCL]

            var omitSpacesQuirkEnabled = (quirks & MofQuirks.OmitSpaceBetweenInOutQualifiersForParameterDeclarations) == MofQuirks.OmitSpaceBetweenInOutQualifiersForParameterDeclarations;

            var source = new StringBuilder();
            var lastQualifierName = default(string);

            source.Append("[");
            for (var i = 0; i < node.Qualifiers.Count; i++)
            {
                var thisQualifier = node.Qualifiers[i];
                var thisQualifierName = thisQualifier.QualifierName.GetNormalizedName();
                if (i > 0)
                {
                    source.Append(",");
                    if (!omitSpacesQuirkEnabled || (lastQualifierName != "in") || (thisQualifierName != "out"))
                    {
                        source.Append(" ");
                    }
                }
                source.Append(MofGenerator.ConvertQualifierValueAst(thisQualifier, quirks));
                lastQualifierName = thisQualifierName;
            }
            source.Append("]");

            return source.ToString();

        }

        public static string ConvertQualifierValueAst(QualifierValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            // Abstract
            // Description("an instance of a class that derives from the GOLF_Base class. ")
            // OCL{"-- the key property cannot be NULL", "inv: InstanceId.size() = 10"}
            var source = new StringBuilder();
            source.Append(node.QualifierName.Extent.Text);
            if (node.ValueInitializer != null)
            {
                source.Append("(");
                source.Append(MofGenerator.ConvertLiteralValueAst(node.ValueInitializer));
                source.Append(")");
            }
            else if (node.ValueArrayInitializer != null)
            {
                source.Append(MofGenerator.ConvertLiteralValueArrayAst(node.ValueArrayInitializer));
            }
            ///
            /// 7.4 Qualifiers
            ///
            /// NOTE A MOF v2 qualifier declaration has to be converted to MOF v3 qualifierTypeDeclaration because the
            /// MOF v2 qualifier flavor has been replaced by the MOF v3 qualifierPolicy.
            ///
            /// These aren't part of the MOF 3.0.1 spec, but we'll include them anyway for backward compatibility.
            ///
            if (node.Flavors.Count > 0)
            {
                source.Append(": ");
                source.Append(
                    string.Join(
                        " ",
                        node.Flavors.Select(f => f.Name)
                    )
                );
            }
            return source.ToString();
        }

        #endregion

        #region 7.5.1 Structure declaration

        public static string ConvertStructureDeclarationAst(StructureDeclarationAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            var source = new StringBuilder();
            if ((node.Qualifiers != null) && node.Qualifiers.Qualifiers.Count > 0)
            {
                source.AppendLine(MofGenerator.ConvertQualifierListAst(node.Qualifiers, quirks));
                source.Append(indent);
            }
            source.Append($"{Constants.STRUCTURE} {node.StructureName.Name}");
            if (node.SuperStructure != null)
            {
                source.Append($" : {node.SuperStructure.Name}");
            }
            source.AppendLine();
            source.Append(indent);
            source.AppendLine("{");
            foreach (var feature in node.Features)
            {
                source.Append(indent);
                source.Append("\t");
                source.AppendLine(MofGenerator.ConvertStructureFeatureAst(feature, quirks, indent + '\t'));
            }
            source.Append(indent);
            source.Append("};");
            return source.ToString();
        }

        public static string ConvertStructureFeatureAst(IStructureFeatureAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            switch (node)
            {
                case StructureDeclarationAst ast:
                    return MofGenerator.ConvertStructureDeclarationAst(ast, quirks, indent);
                //case EnumerationDeclarationAst ast:
                case PropertyDeclarationAst ast:
                    return MofGenerator.ConvertPropertyDeclarationAst(ast, quirks);
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region 7.5.2 Class declaration

        public static string ConvertClassDeclarationAst(ClassDeclarationAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            var source = new StringBuilder();
            if ((node.Qualifiers != null) && node.Qualifiers.Qualifiers.Count > 0)
            {
                source.AppendLine(MofGenerator.ConvertQualifierListAst(node.Qualifiers, quirks));
                source.Append(indent);
            }
            source.Append($"{Constants.CLASS} {node.ClassName.Name}");
            if (node.Superclass != null)
            {
                source.Append($" : {node.Superclass.Name}");
            }
            source.AppendLine();
            source.Append(indent);
            source.AppendLine("{");
            foreach (var feature in node.Features)
            {
                source.Append(indent);
                source.Append("\t");
                source.Append(MofGenerator.ConvertClassFeatureAst(feature, quirks, indent + '\t'));
				source.AppendLine();
            }
            source.Append(indent);
            source.Append("};");
            return source.ToString();
        }

        public static string ConvertClassFeatureAst(IClassFeatureAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            switch (node)
            {
                case IStructureFeatureAst ast:
                    return MofGenerator.ConvertStructureFeatureAst(ast, quirks, indent);
                case MethodDeclarationAst ast:
                    return MofGenerator.ConvertMethodDeclarationAst(ast, quirks);
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region 7.5.5 Property declaration

        public static string ConvertPropertyDeclarationAst(PropertyDeclarationAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            if ((node.Qualifiers != null) && node.Qualifiers.Qualifiers.Count > 0)
            {
                source.AppendFormat("{0} ", MofGenerator.ConvertQualifierListAst(node.Qualifiers, quirks));
            }
            source.AppendFormat("{0} ", node.ReturnType.Name);
            if (node.ReturnTypeIsRef)
            {
                source.AppendFormat("{0} ", node.ReturnTypeRef.Name);
            }
            source.Append(node.PropertyName.Name);
            if (node.ReturnTypeIsArray)
            {
                source.Append("[]");
            }
            if (node.Initializer != null)
            {
                source.AppendFormat(" = {0}", MofGenerator.ConvertPrimitiveTypeValueAst(node.Initializer, quirks));
            }
            source.Append(";");
            return source.ToString();
        }

        #endregion

        #region 7.5.6 Method declaration

        public static string ConvertMethodDeclarationAst(MethodDeclarationAst node, MofQuirks quirks = MofQuirks.None)
        {

            var prefixQuirkEnabled = (quirks & MofQuirks.PrefixSpaceBeforeQualifierlessMethodDeclarations) == MofQuirks.PrefixSpaceBeforeQualifierlessMethodDeclarations;

            var source = new StringBuilder();

            if ((node.Qualifiers != null) && node.Qualifiers.Qualifiers.Count > 0)
            {
                source.AppendFormat("{0}", MofGenerator.ConvertQualifierListAst(node.Qualifiers, quirks));
            }

            if (prefixQuirkEnabled || ((node.Qualifiers != null) && node.Qualifiers.Qualifiers.Count > 0))
            {
                source.AppendFormat(" ");
            }

            source.AppendFormat("{0} {1}", node.ReturnType.Name, node.Name.Name);

            if (node.Parameters.Count == 0)
            {
                source.Append("();");
            }
            else
            {
                var values = node.Parameters.Select(p => MofGenerator.ConvertParameterDeclarationAst(p, quirks)).ToArray();
                source.AppendFormat("({0});", string.Join(", ", values));
            }

            return source.ToString();

        }

        #endregion

        #region 7.5.7 Parameter declaration

        public static string ConvertParameterDeclarationAst(ParameterDeclarationAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            if (node.Qualifiers.Qualifiers.Count > 0)
            {
                source.AppendFormat("{0} ", MofGenerator.ConvertQualifierListAst(node.Qualifiers, quirks));
            }
            source.AppendFormat("{0} ", node.ParameterType.Name);
            if (node.ParameterIsRef)
            {
                source.AppendFormat("{0} ", node.ParameterRef.Name);
            }
            source.Append(node.ParameterName.Name);
            if (node.ParameterIsArray)
            {
                source.Append("[]");
            }
            if (node.DefaultValue != null)
            {
                source.AppendFormat(" = {0}", MofGenerator.ConvertToMof(node.DefaultValue, quirks));
            }
            return source.ToString();
        }

        #endregion

        #region 7.5.9 Complex type value

        public static string ConvertComplexTypeValueAst(ComplexTypeValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            switch (node)
            {
                case ComplexValueArrayAst ast:
                    return MofGenerator.ConvertComplexValueArrayAst(ast, quirks);
                case ComplexValueAst ast:
                    return MofGenerator.ConvertComplexValueAst(ast, quirks);
                default:
                    throw new NotImplementedException();
            }
        }

        public static string ConvertComplexValueArrayAst(ComplexValueArrayAst node, MofQuirks quirks = MofQuirks.None)
        {
            return string.Format("{{{0}}}", string.Join(", ", node.Values.Select(v => v.ToString()).ToArray()));
        }

        public static string ConvertComplexValueAst(ComplexValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            return string.Format("!!!!!{0}!!!!!", node.GetType().Name);
        }

        public static string ConvertPropertyValueListAst(PropertyValueListAst node, MofQuirks quirks = MofQuirks.None)
        {
            // {
            //     Reference = TRUE;
            // }
            var source = new StringBuilder();
            source.AppendLine("{");
            foreach (var propertyValue in node.PropertyValues)
            {
                source.AppendLine($"\t{propertyValue.Key} = {MofGenerator.ConvertPropertyValueAst(propertyValue.Value)};");
            }
            source.Append("}");
            return source.ToString();
        }

        public static string ConvertPropertyValueAst(PropertyValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            switch (node)
            {
                case PrimitiveTypeValueAst ast:
                    return MofGenerator.ConvertPrimitiveTypeValueAst(ast, quirks);
                case ComplexTypeValueAst ast:
                    return MofGenerator.ConvertComplexTypeValueAst(ast, quirks);
                //case ReferenceTypeValueAst ast:
                //case EnumTypeValueAst ast:
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region 7.6.1 Primitive type value

        public static string ConvertPrimitiveTypeValueAst(PrimitiveTypeValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            switch (node)
            {
                case LiteralValueAst ast:
                    return MofGenerator.ConvertLiteralValueAst(ast, quirks);
                case LiteralValueArrayAst ast:
                    return MofGenerator.ConvertLiteralValueArrayAst(ast, quirks);
                default:
                    throw new NotImplementedException();
            }
        }

        public static string ConvertLiteralValueAst(LiteralValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            switch (node)
            {
                case IntegerValueAst ast:
                    return MofGenerator.ConvertIntegerValueAst(ast, quirks);
                case RealValueAst ast:
                    return MofGenerator.ConvertRealValueAst(ast, quirks);
                case BooleanValueAst ast:
                    return MofGenerator.ConvertBooleanValueAst(ast, quirks);
                case NullValueAst ast:
                    return MofGenerator.ConvertNullValueAst(ast, quirks);
                case StringValueAst ast:
                    return MofGenerator.ConvertStringValueAst(ast, quirks);
                default:
                    throw new NotImplementedException();
            }
        }

        public static string ConvertLiteralValueArrayAst(LiteralValueArrayAst node, MofQuirks quirks = MofQuirks.None)
        {
            var values = node.Values.Select(v => MofGenerator.ConvertLiteralValueAst(v, quirks)).ToArray();
            return string.Format("{{{0}}}", string.Join(", ", values));
        }

        #endregion

        #region 7.6.1.1 Integer value

        public static string ConvertIntegerValueAst(IntegerValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            switch (node.Kind)
            {
                case IntegerKind.BinaryValue:
                    return "0" + Convert.ToString(node.Value, 2) + "b";
                case IntegerKind.OctalValue:
                    return "0" + Convert.ToString(node.Value, 8);
                case IntegerKind.HexValue:
                    return "0x" + Convert.ToString(node.Value, 16).ToUpperInvariant();
                case IntegerKind.DecimalValue:
                    return Convert.ToString(node.Value, 10);
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region 7.6.1.2 Real value

        public static string ConvertRealValueAst(RealValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            return node.Value.ToString();
        }

        #endregion

        #region 7.6.1.3 String values

        public static string ConvertStringValueAst(StringValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            var singleStringValues = node.StringLiteralValues
                                         .Select(n => $"\"{MofGenerator.EscapeString(n.Value)}\"")
                                         .ToList();
            return string.Join(" ", singleStringValues);
        }

        internal static string EscapeString(string value)
        {
            var escapeMap = new Dictionary<char, string>()
            {
                { '\\' , "\\\\" }, { '\"' , "\\\"" },  { '\'' , "\\\'" },
                { '\b', "\\b" }, { '\t', "\\t" },  { '\n', "\\n" }, { '\f', "\\f" }, { '\r', "\\r" }
            };
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            var escaped = new StringBuilder();
            foreach (var @char in value.ToCharArray())
            {
                if (escapeMap.ContainsKey(@char))
                {
                    escaped.Append(escapeMap[@char]);
                }
                else if ((@char >= 32) && (@char <= 126))
                {
                    escaped.Append(@char);
                }
                else
                {
                    throw new InvalidOperationException(new string(new char[] { @char }));
                }
            }
            return escaped.ToString();
        }

        #endregion

        #region 7.6.1.5 Boolean value

        public static string ConvertBooleanValueAst(BooleanValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            return node.Token.Extent.Text;
        }

        #endregion

        #region 7.6.1.6 Null value

        public static string ConvertNullValueAst(NullValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            return node.Token.Extent.Text;
        }

        #endregion

        #region 7.6.2 Complex type value

        public static string ConvertInstanceValueDeclarationAst(InstanceValueDeclarationAst node, MofQuirks quirks = MofQuirks.None)
        {
            // instance of myType as $Alias00000070
            // {
            //     Reference = TRUE;
            // };
            var source = new StringBuilder();
            // instance of myType as $Alias00000070
            source.Append(node.Instance.Extent.Text);
            source.Append(' ');
            source.Append(node.Of.Extent.Text);
            source.Append(' ');
            source.Append(node.TypeName.Name);
            if (node.Alias != null)
            {
                source.Append(' ');
                source.Append(node.As.Extent.Text);
                source.Append(' ');
                source.Append($"${node.Alias.Name}");
            }
            source.AppendLine();
            // {
            //     Reference = TRUE;
            // }
            source.Append(MofGenerator.ConvertPropertyValueListAst(node.PropertyValues));
            // ;
            source.Append(node.StatementEnd.Extent.Text);
            return source.ToString();
        }

        public static string ConvertStructureValueDeclarationAst(StructureValueDeclarationAst node, MofQuirks quirks = MofQuirks.None)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}
