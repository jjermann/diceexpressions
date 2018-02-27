using System;
using System.Text.RegularExpressions;
using DiceExpressions.Model;

namespace DiceExpressions.ModelHelpers
{
    public class DieVisitor : DensityVisitor<int>
    {
        public override Density<int> VisitNumber(DensityExpressionGrammarParser.NumberContext ctx)
        {
            var number = int.Parse(ctx.NUMBER().GetText());
            var density = new Constant<int>(number);
            return density;
        }

        public override Density<int> VisitVariable(DensityExpressionGrammarParser.VariableContext ctx)
        {
            var variableStr = ctx.VARIABLE().GetText();
            var prefixNumMatch = new Regex(@"^(?<prefix>([a-z]|[A-Z])*)(?<n>[1-9][0-9]*)(?<postfix>([a-z]|[A-Z])*)$", RegexOptions.Singleline);

            if (prefixNumMatch.IsMatch(variableStr))
            {
                var match = prefixNumMatch.Match(variableStr);
                var prefix = match.Groups["prefix"].Value;
                var n = int.Parse(match.Groups["n"].Value);
                var postfix = match.Groups["postfix"].Value;
                Density<int> density;
                switch(prefix)
                {
                    case "d":
                        density = new Die(n);
                        break;
                    case "ad":
                        density = new Die(n).WithAdvantage();
                        break;
                    case "dd":
                        density = new Die(n).WithDisadvantage();
                        break;
                    default:
                        throw new NotImplementedException();
                }
                return density;
            } else
            {
                throw new NotImplementedException();
            }
        }

        // public override Density<int> ParseDensityFunction(string functionName, params string[] functionArguments)
        // {
        //     throw new NotImplementedException();
        // }
    }
}