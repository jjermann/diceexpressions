using System;
using Antlr4.Runtime;
using DiceExpressions.Model;
using DiceExpressions.ModelHelpers;

namespace DiceExpressions.ModelHelper
{
    public static class DieParser
    {
        public static DensityExpressionResult<int> Parse(string densityStr)
        {
            if (string.IsNullOrEmpty(densityStr)) {
                var res = new DensityExpressionResult<int> {
                    ErrorString = "Empty expression string!"
                };
                return res;
            }
            try
            {
                var input = new AntlrInputStream(densityStr);
                var lexer = new DensityExpressionGrammarLexer(input);
                var tokens = new CommonTokenStream(lexer);
                var parser = new DensityExpressionGrammarParser(tokens);

                var ctx = parser.compileUnit();
                var visitor = new DieVisitor();
                var res = visitor.VisitCompileUnit(ctx);
                return res;
            } catch(Exception e)
            {
                var res = new DensityExpressionResult<int> {
                    ErrorString = e.Message
                };
                return res;
            }
        }
    }
}