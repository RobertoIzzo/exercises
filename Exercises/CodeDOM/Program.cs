﻿using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;

namespace CodeDOM
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
            CodeNamespace codeNamespace = new CodeNamespace("CodeDOM");
            codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
            CodeTypeDeclaration myclass = new CodeTypeDeclaration("Pippo");
            CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
            codeMemberMethod.Name = "DoSOmething";
            CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression("Console"), "WriteLine",new CodePrimitiveExpression("hello"));
            CodeMethodInvokeExpression codeMethodInvokeExpression1 = new CodeMethodInvokeExpression(
             new CodeTypeReferenceExpression("Console"), "ReadLine");
            codeCompileUnit.Namespaces.Add(codeNamespace);
            codeNamespace.Types.Add(myclass);
            myclass.Members.Add(codeMemberMethod);
            codeMemberMethod.Statements.Add(codeMethodInvokeExpression);
            codeMemberMethod.Statements.Add(codeMethodInvokeExpression1);

            CSharpCodeProvider provider = new CSharpCodeProvider();
            using (StreamWriter stream = new StreamWriter("../../autogenerated/Pippo.cs", false))
            {
                IndentedTextWriter tw = new IndentedTextWriter(stream, "   ");
                provider.GenerateCodeFromCompileUnit(codeCompileUnit,tw,new CodeGeneratorOptions());
                tw.Close();
            }
        }
    }
}