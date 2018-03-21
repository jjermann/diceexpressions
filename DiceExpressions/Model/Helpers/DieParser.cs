using System;
using Antlr4.Runtime;
using DiceExpressions.Model;
using DiceExpressions.Model.AlgebraicDefaultImplementations;
using PType = System.Double;

namespace DiceExpressions.Model.Helpers
{
    using EType = DensityExpressionResult<FieldType<int,PType>, int, PType>;
    public static class DieParser
    {
        public static EType Parse(string densityStr)
        {
            if (string.IsNullOrEmpty(densityStr)) {
                var res = new DensityExpressionResult<FieldType<int,PType>,int,PType> {
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
                var res = new DensityExpressionResult<FieldType<int,PType>,int,PType> {
                    ErrorString = e.Message
                };
                return res;
            }
        }
    }
}