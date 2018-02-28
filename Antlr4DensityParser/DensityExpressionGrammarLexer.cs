//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.5-SNAPSHOT
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from DensityExpressionGrammar.g4 by ANTLR 4.6.5-SNAPSHOT

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.5-SNAPSHOT")]
[System.CLSCompliant(false)]
public partial class DensityExpressionGrammarLexer : Lexer {
	public const int
		NUMBER=1, VARIABLE=2, CALL=3, SEP=4, LPAREN=5, RPAREN=6, LBRACK=7, RBRACK=8, 
		PLUS=9, MINUS=10, TIMES=11, DIV=12, EQ=13, NEQ=14, LT=15, LE=16, GT=17, 
		GE=18, WS=19;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"NUMBER", "VARIABLE", "VALID_ID_START", "VALID_ID_CHAR", "VALID_NUMBER_START", 
		"VALID_NUMBER_CHAR", "CALL", "SEP", "LPAREN", "RPAREN", "LBRACK", "RBRACK", 
		"PLUS", "MINUS", "TIMES", "DIV", "EQ", "NEQ", "LT", "LE", "GT", "GE", 
		"WS"
	};


	public DensityExpressionGrammarLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, null, null, "'.'", "','", "'('", "')'", "'['", "']'", "'+'", "'-'", 
		"'*'", "'/'", "'=='", null, "'<'", null, "'>'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "NUMBER", "VARIABLE", "CALL", "SEP", "LPAREN", "RPAREN", "LBRACK", 
		"RBRACK", "PLUS", "MINUS", "TIMES", "DIV", "EQ", "NEQ", "LT", "LE", "GT", 
		"GE", "WS"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[System.Obsolete("Use Vocabulary instead.")]
	public static readonly string[] tokenNames = GenerateTokenNames(DefaultVocabulary, _SymbolicNames.Length);

	private static string[] GenerateTokenNames(IVocabulary vocabulary, int length) {
		string[] tokenNames = new string[length];
		for (int i = 0; i < tokenNames.Length; i++) {
			tokenNames[i] = vocabulary.GetLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = vocabulary.GetSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}

		return tokenNames;
	}

	[System.Obsolete("Use IRecognizer.Vocabulary instead.")]
	public override string[] TokenNames
	{
		get
		{
			return tokenNames;
		}
	}

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "DensityExpressionGrammar.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2\x15\x85\b\x1\x4"+
		"\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b"+
		"\x4\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4"+
		"\x10\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15"+
		"\t\x15\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x3\x2\x3\x2\x3\x2\a\x2\x35"+
		"\n\x2\f\x2\xE\x2\x38\v\x2\x5\x2:\n\x2\x3\x3\x3\x3\a\x3>\n\x3\f\x3\xE\x3"+
		"\x41\v\x3\x3\x4\x5\x4\x44\n\x4\x3\x5\x3\x5\x5\x5H\n\x5\x3\x6\x3\x6\x3"+
		"\a\x3\a\x5\aN\n\a\x3\b\x3\b\x3\t\x3\t\x3\n\x3\n\x3\v\x3\v\x3\f\x3\f\x3"+
		"\r\x3\r\x3\xE\x3\xE\x3\xF\x3\xF\x3\x10\x3\x10\x3\x11\x3\x11\x3\x12\x3"+
		"\x12\x3\x12\x3\x13\x3\x13\x3\x13\x3\x13\x3\x13\x3\x13\x5\x13m\n\x13\x3"+
		"\x14\x3\x14\x3\x15\x3\x15\x3\x15\x3\x15\x5\x15u\n\x15\x3\x16\x3\x16\x3"+
		"\x17\x3\x17\x3\x17\x3\x17\x5\x17}\n\x17\x3\x18\x6\x18\x80\n\x18\r\x18"+
		"\xE\x18\x81\x3\x18\x3\x18\x2\x2\x2\x19\x3\x2\x3\x5\x2\x4\a\x2\x2\t\x2"+
		"\x2\v\x2\x2\r\x2\x2\xF\x2\x5\x11\x2\x6\x13\x2\a\x15\x2\b\x17\x2\t\x19"+
		"\x2\n\x1B\x2\v\x1D\x2\f\x1F\x2\r!\x2\xE#\x2\xF%\x2\x10\'\x2\x11)\x2\x12"+
		"+\x2\x13-\x2\x14/\x2\x15\x3\x2\x4\x5\x2\x43\\\x61\x61\x63|\x4\x2\v\f\xF"+
		"\xF\x8A\x2\x3\x3\x2\x2\x2\x2\x5\x3\x2\x2\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3"+
		"\x2\x2\x2\x2\x13\x3\x2\x2\x2\x2\x15\x3\x2\x2\x2\x2\x17\x3\x2\x2\x2\x2"+
		"\x19\x3\x2\x2\x2\x2\x1B\x3\x2\x2\x2\x2\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2"+
		"\x2\x2!\x3\x2\x2\x2\x2#\x3\x2\x2\x2\x2%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2"+
		")\x3\x2\x2\x2\x2+\x3\x2\x2\x2\x2-\x3\x2\x2\x2\x2/\x3\x2\x2\x2\x3\x39\x3"+
		"\x2\x2\x2\x5;\x3\x2\x2\x2\a\x43\x3\x2\x2\x2\tG\x3\x2\x2\x2\vI\x3\x2\x2"+
		"\x2\rM\x3\x2\x2\x2\xFO\x3\x2\x2\x2\x11Q\x3\x2\x2\x2\x13S\x3\x2\x2\x2\x15"+
		"U\x3\x2\x2\x2\x17W\x3\x2\x2\x2\x19Y\x3\x2\x2\x2\x1B[\x3\x2\x2\x2\x1D]"+
		"\x3\x2\x2\x2\x1F_\x3\x2\x2\x2!\x61\x3\x2\x2\x2#\x63\x3\x2\x2\x2%l\x3\x2"+
		"\x2\x2\'n\x3\x2\x2\x2)t\x3\x2\x2\x2+v\x3\x2\x2\x2-|\x3\x2\x2\x2/\x7F\x3"+
		"\x2\x2\x2\x31:\a\x32\x2\x2\x32\x36\x5\v\x6\x2\x33\x35\x5\r\a\x2\x34\x33"+
		"\x3\x2\x2\x2\x35\x38\x3\x2\x2\x2\x36\x34\x3\x2\x2\x2\x36\x37\x3\x2\x2"+
		"\x2\x37:\x3\x2\x2\x2\x38\x36\x3\x2\x2\x2\x39\x31\x3\x2\x2\x2\x39\x32\x3"+
		"\x2\x2\x2:\x4\x3\x2\x2\x2;?\x5\a\x4\x2<>\x5\t\x5\x2=<\x3\x2\x2\x2>\x41"+
		"\x3\x2\x2\x2?=\x3\x2\x2\x2?@\x3\x2\x2\x2@\x6\x3\x2\x2\x2\x41?\x3\x2\x2"+
		"\x2\x42\x44\t\x2\x2\x2\x43\x42\x3\x2\x2\x2\x44\b\x3\x2\x2\x2\x45H\x5\a"+
		"\x4\x2\x46H\x4\x32;\x2G\x45\x3\x2\x2\x2G\x46\x3\x2\x2\x2H\n\x3\x2\x2\x2"+
		"IJ\x4\x33;\x2J\f\x3\x2\x2\x2KN\x5\v\x6\x2LN\a\x32\x2\x2MK\x3\x2\x2\x2"+
		"ML\x3\x2\x2\x2N\xE\x3\x2\x2\x2OP\a\x30\x2\x2P\x10\x3\x2\x2\x2QR\a.\x2"+
		"\x2R\x12\x3\x2\x2\x2ST\a*\x2\x2T\x14\x3\x2\x2\x2UV\a+\x2\x2V\x16\x3\x2"+
		"\x2\x2WX\a]\x2\x2X\x18\x3\x2\x2\x2YZ\a_\x2\x2Z\x1A\x3\x2\x2\x2[\\\a-\x2"+
		"\x2\\\x1C\x3\x2\x2\x2]^\a/\x2\x2^\x1E\x3\x2\x2\x2_`\a,\x2\x2` \x3\x2\x2"+
		"\x2\x61\x62\a\x31\x2\x2\x62\"\x3\x2\x2\x2\x63\x64\a?\x2\x2\x64\x65\a?"+
		"\x2\x2\x65$\x3\x2\x2\x2\x66g\a>\x2\x2gm\a@\x2\x2hi\a@\x2\x2im\a>\x2\x2"+
		"jk\a#\x2\x2km\a?\x2\x2l\x66\x3\x2\x2\x2lh\x3\x2\x2\x2lj\x3\x2\x2\x2m&"+
		"\x3\x2\x2\x2no\a>\x2\x2o(\x3\x2\x2\x2pq\a>\x2\x2qu\a?\x2\x2rs\a?\x2\x2"+
		"su\a>\x2\x2tp\x3\x2\x2\x2tr\x3\x2\x2\x2u*\x3\x2\x2\x2vw\a@\x2\x2w,\x3"+
		"\x2\x2\x2xy\a?\x2\x2y}\a>\x2\x2z{\a>\x2\x2{}\a?\x2\x2|x\x3\x2\x2\x2|z"+
		"\x3\x2\x2\x2}.\x3\x2\x2\x2~\x80\t\x3\x2\x2\x7F~\x3\x2\x2\x2\x80\x81\x3"+
		"\x2\x2\x2\x81\x7F\x3\x2\x2\x2\x81\x82\x3\x2\x2\x2\x82\x83\x3\x2\x2\x2"+
		"\x83\x84\b\x18\x2\x2\x84\x30\x3\x2\x2\x2\r\x2\x36\x39?\x43GMlt|\x81\x3"+
		"\b\x2\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
