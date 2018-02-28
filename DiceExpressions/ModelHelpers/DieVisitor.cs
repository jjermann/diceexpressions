using System;
using System.Text.RegularExpressions;
using System.Linq;
using DiceExpressions.Model;

namespace DiceExpressions.ModelHelpers
{
    public class DieVisitor : DensityVisitor<int>
    {
        private static Regex _variableMatch;
        static DieVisitor()
        {
            var prefixNumMatch = @"(?<prefixNumMatch>(?<prefixNum>[a-zA-Z]+)(?<nPrefixNum>[1-9][0-9]*))";
            var prefixMatch = @"(?<prefix>[a-zA-Z][a-zA-Z]*)";
            var baseTypeMatch = @"(?<baseType>[a-zA-Z])";
            var typeMatch = $"(?<type>{prefixMatch}?{baseTypeMatch})";
            var nMatch = @"(?<n>[1-9][0-9]*)";
            var postfixMatch = @"(?<postfix>[a-zA-Z]+)";
            var regexMatch = $"^{prefixNumMatch}?{typeMatch}{nMatch}{postfixMatch}?$";

            _variableMatch = new Regex(regexMatch, RegexOptions.Singleline);
        }

        public override int VisitNumber(DensityExpressionGrammarParser.NumberContext ctx)
        {
            return int.Parse(ctx.NUMBER().GetText());
        }

        public override Density<int> VisitVariable(DensityExpressionGrammarParser.VariableContext ctx)
        {
            var variableStr = ctx.VARIABLE().GetText();

            if (_variableMatch.IsMatch(variableStr))
            {
                var match = _variableMatch.Match(variableStr);
                var matchDict = Enumerable.Range(0, match.Groups.Count)
                    .Where(i => !match.Groups[i].Name.All(char.IsDigit))
                    .ToDictionary(
                        i => match.Groups[i].Name,
                        i => match.Groups[i].Value);

                var hasPrefixNum = !string.IsNullOrEmpty(matchDict["prefixNumMatch"]);
                var prefixNum = matchDict["prefixNum"];
                var nPrefixNum = matchDict["nPrefixNum"];
                var prefix = matchDict["prefix"];
                var baseType = matchDict["baseType"];
                var nStr = matchDict["n"];
                var postfix = matchDict["postfix"];

                var hasPrefix = hasPrefixNum || !string.IsNullOrEmpty(prefix);
                var hasPostFix = !string.IsNullOrEmpty(postfix);
                if (hasPostFix)
                {
                    throw new NotImplementedException();
                }
                if (baseType != "d")
                {
                    throw new NotImplementedException();
                }
                var n = int.Parse(nStr);
                var baseDensity = new Die(n);
                if (hasPrefix)
                {
                    var prefixType = hasPrefixNum ? prefixNum : prefix;
                    var nPrefix = hasPrefixNum ? int.Parse(nPrefixNum) : 1;
                    if (prefixType == "a")
                    {
                        return baseDensity.WithAdvantage(nPrefix);
                    }
                    if (prefixType == "d")
                    {
                        return baseDensity.WithDisadvantage(nPrefix);
                    }
                    throw new NotImplementedException();
                } else
                {
                    return baseDensity;
                }
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