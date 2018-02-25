using System;
using Antlr4.Runtime;
using DiceExpressions.Model;
using DiceExpressions.ModelHelpers;

namespace DiceExpressions.ModelHelper
{
    public static class DieParser
    {
        public static Density<int> Parse(string densityStr)
        {
            if (string.IsNullOrEmpty(densityStr)) {
                return null;
            }
            try
            {
                var input = new AntlrInputStream(densityStr);
                var lexer = new DensityExpressionGrammarLexer(input);
                var tokens = new CommonTokenStream(lexer);
                var parser = new DensityExpressionGrammarParser(tokens);

                var ctx = parser.compileUnit();
                var visitor = new DieVisitor();
                var density = visitor.VisitCompileUnit(ctx);
                return density;
            } catch(Exception)
            {
                return null;
            }
        }
    }
}