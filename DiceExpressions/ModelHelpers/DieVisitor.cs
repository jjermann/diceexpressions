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
            var prefixNumMatch = new Regex(@"^(?<prefix>([a-z]|[A-Z])*)(?<n>[1-9][0-9]*)$", RegexOptions.Singleline);

            if (prefixNumMatch.IsMatch(variableStr))
            {
                var match = prefixNumMatch.Match(variableStr);
                var prefix = match.Groups["prefix"].Value;
                var n = int.Parse(match.Groups["n"].Value);
                switch(prefix)
                {
                    case "d":
                        return new Die(n);
                    case "ad":
                        return new Die(n).WithAdvantage();
                    case "dd":
                        return new Die(n).WithAdvantage();
                    default:
                        throw new NotImplementedException();
                }
            } else
            {
                throw new NotImplementedException();
            }
        }
    }
}