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
using Antlr4.Runtime.Tree;
using System.Collections.Generic;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.5-SNAPSHOT")]
[System.CLSCompliant(false)]
public partial class DensityExpressionGrammarParser : Parser {
	public const int
		NUMBER=1, VARIABLE=2, CALL=3, SEP=4, LPAREN=5, RPAREN=6, LBRACK=7, RBRACK=8, 
		PLUS=9, MINUS=10, TIMES=11, DIV=12, EQ=13, NEQ=14, LT=15, LE=16, GT=17, 
		GE=18, WS=19;
	public const int
		RULE_compileUnit = 0, RULE_probability = 1, RULE_density = 2, RULE_multiDensityList = 3, 
		RULE_term = 4, RULE_factor = 5, RULE_atom = 6, RULE_number = 7, RULE_variable = 8, 
		RULE_strVariable = 9, RULE_binaryBooleanOp = 10;
	public static readonly string[] ruleNames = {
		"compileUnit", "probability", "density", "multiDensityList", "term", "factor", 
		"atom", "number", "variable", "strVariable", "binaryBooleanOp"
	};

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

	public override string SerializedAtn { get { return _serializedATN; } }

	public DensityExpressionGrammarParser(ITokenStream input)
		: base(input)
	{
		_interp = new ParserATNSimulator(this,_ATN);
	}
	public partial class CompileUnitContext : ParserRuleContext {
		public ITerminalNode Eof() { return GetToken(DensityExpressionGrammarParser.Eof, 0); }
		public DensityContext density() {
			return GetRuleContext<DensityContext>(0);
		}
		public ProbabilityContext probability() {
			return GetRuleContext<ProbabilityContext>(0);
		}
		public CompileUnitContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_compileUnit; } }
		public override void EnterRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.EnterCompileUnit(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.ExitCompileUnit(this);
		}
	}

	[RuleVersion(0)]
	public CompileUnitContext compileUnit() {
		CompileUnitContext _localctx = new CompileUnitContext(_ctx, State);
		EnterRule(_localctx, 0, RULE_compileUnit);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 24;
			_errHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(_input,0,_ctx) ) {
			case 1:
				{
				State = 22; density();
				}
				break;

			case 2:
				{
				State = 23; probability();
				}
				break;
			}
			State = 26; Match(Eof);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ProbabilityContext : ParserRuleContext {
		public DensityContext[] density() {
			return GetRuleContexts<DensityContext>();
		}
		public DensityContext density(int i) {
			return GetRuleContext<DensityContext>(i);
		}
		public BinaryBooleanOpContext binaryBooleanOp() {
			return GetRuleContext<BinaryBooleanOpContext>(0);
		}
		public ProbabilityContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_probability; } }
		public override void EnterRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.EnterProbability(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.ExitProbability(this);
		}
	}

	[RuleVersion(0)]
	public ProbabilityContext probability() {
		ProbabilityContext _localctx = new ProbabilityContext(_ctx, State);
		EnterRule(_localctx, 2, RULE_probability);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 28; density();
			State = 29; binaryBooleanOp();
			State = 30; density();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class DensityContext : ParserRuleContext {
		public TermContext[] term() {
			return GetRuleContexts<TermContext>();
		}
		public TermContext term(int i) {
			return GetRuleContext<TermContext>(i);
		}
		public ITerminalNode[] PLUS() { return GetTokens(DensityExpressionGrammarParser.PLUS); }
		public ITerminalNode PLUS(int i) {
			return GetToken(DensityExpressionGrammarParser.PLUS, i);
		}
		public ITerminalNode[] MINUS() { return GetTokens(DensityExpressionGrammarParser.MINUS); }
		public ITerminalNode MINUS(int i) {
			return GetToken(DensityExpressionGrammarParser.MINUS, i);
		}
		public DensityContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_density; } }
		public override void EnterRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.EnterDensity(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.ExitDensity(this);
		}
	}

	[RuleVersion(0)]
	public DensityContext density() {
		DensityContext _localctx = new DensityContext(_ctx, State);
		EnterRule(_localctx, 4, RULE_density);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 32; term();
			State = 37;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==PLUS || _la==MINUS) {
				{
				{
				State = 33;
				_la = _input.La(1);
				if ( !(_la==PLUS || _la==MINUS) ) {
				_errHandler.RecoverInline(this);
				} else {
					if (_input.La(1) == TokenConstants.Eof) {
						matchedEOF = true;
					}

					_errHandler.ReportMatch(this);
					Consume();
				}
				State = 34; term();
				}
				}
				State = 39;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class MultiDensityListContext : ParserRuleContext {
		public ITerminalNode LBRACK() { return GetToken(DensityExpressionGrammarParser.LBRACK, 0); }
		public DensityContext[] density() {
			return GetRuleContexts<DensityContext>();
		}
		public DensityContext density(int i) {
			return GetRuleContext<DensityContext>(i);
		}
		public ITerminalNode RBRACK() { return GetToken(DensityExpressionGrammarParser.RBRACK, 0); }
		public ITerminalNode[] SEP() { return GetTokens(DensityExpressionGrammarParser.SEP); }
		public ITerminalNode SEP(int i) {
			return GetToken(DensityExpressionGrammarParser.SEP, i);
		}
		public MultiDensityListContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_multiDensityList; } }
		public override void EnterRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.EnterMultiDensityList(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.ExitMultiDensityList(this);
		}
	}

	[RuleVersion(0)]
	public MultiDensityListContext multiDensityList() {
		MultiDensityListContext _localctx = new MultiDensityListContext(_ctx, State);
		EnterRule(_localctx, 6, RULE_multiDensityList);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 40; Match(LBRACK);
			State = 41; density();
			State = 46;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==SEP) {
				{
				{
				State = 42; Match(SEP);
				State = 43; density();
				}
				}
				State = 48;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			State = 49; Match(RBRACK);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class TermContext : ParserRuleContext {
		public FactorContext[] factor() {
			return GetRuleContexts<FactorContext>();
		}
		public FactorContext factor(int i) {
			return GetRuleContext<FactorContext>(i);
		}
		public ITerminalNode[] TIMES() { return GetTokens(DensityExpressionGrammarParser.TIMES); }
		public ITerminalNode TIMES(int i) {
			return GetToken(DensityExpressionGrammarParser.TIMES, i);
		}
		public ITerminalNode[] DIV() { return GetTokens(DensityExpressionGrammarParser.DIV); }
		public ITerminalNode DIV(int i) {
			return GetToken(DensityExpressionGrammarParser.DIV, i);
		}
		public TermContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_term; } }
		public override void EnterRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.EnterTerm(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.ExitTerm(this);
		}
	}

	[RuleVersion(0)]
	public TermContext term() {
		TermContext _localctx = new TermContext(_ctx, State);
		EnterRule(_localctx, 8, RULE_term);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 51; factor();
			State = 56;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==TIMES || _la==DIV) {
				{
				{
				State = 52;
				_la = _input.La(1);
				if ( !(_la==TIMES || _la==DIV) ) {
				_errHandler.RecoverInline(this);
				} else {
					if (_input.La(1) == TokenConstants.Eof) {
						matchedEOF = true;
					}

					_errHandler.ReportMatch(this);
					Consume();
				}
				State = 53; factor();
				}
				}
				State = 58;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class FactorContext : ParserRuleContext {
		public ITerminalNode MINUS() { return GetToken(DensityExpressionGrammarParser.MINUS, 0); }
		public FactorContext factor() {
			return GetRuleContext<FactorContext>(0);
		}
		public AtomContext atom() {
			return GetRuleContext<AtomContext>(0);
		}
		public FactorContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_factor; } }
		public override void EnterRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.EnterFactor(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.ExitFactor(this);
		}
	}

	[RuleVersion(0)]
	public FactorContext factor() {
		FactorContext _localctx = new FactorContext(_ctx, State);
		EnterRule(_localctx, 10, RULE_factor);
		try {
			State = 62;
			_errHandler.Sync(this);
			switch (_input.La(1)) {
			case MINUS:
				EnterOuterAlt(_localctx, 1);
				{
				State = 59; Match(MINUS);
				State = 60; factor();
				}
				break;
			case NUMBER:
			case VARIABLE:
			case LPAREN:
			case LBRACK:
				EnterOuterAlt(_localctx, 2);
				{
				State = 61; atom();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class AtomContext : ParserRuleContext {
		public NumberContext number() {
			return GetRuleContext<NumberContext>(0);
		}
		public VariableContext variable() {
			return GetRuleContext<VariableContext>(0);
		}
		public MultiDensityListContext multiDensityList() {
			return GetRuleContext<MultiDensityListContext>(0);
		}
		public ITerminalNode LPAREN() { return GetToken(DensityExpressionGrammarParser.LPAREN, 0); }
		public DensityContext density() {
			return GetRuleContext<DensityContext>(0);
		}
		public ITerminalNode RPAREN() { return GetToken(DensityExpressionGrammarParser.RPAREN, 0); }
		public AtomContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_atom; } }
		public override void EnterRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.EnterAtom(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.ExitAtom(this);
		}
	}

	[RuleVersion(0)]
	public AtomContext atom() {
		AtomContext _localctx = new AtomContext(_ctx, State);
		EnterRule(_localctx, 12, RULE_atom);
		try {
			State = 71;
			_errHandler.Sync(this);
			switch (_input.La(1)) {
			case NUMBER:
				EnterOuterAlt(_localctx, 1);
				{
				State = 64; number();
				}
				break;
			case VARIABLE:
				EnterOuterAlt(_localctx, 2);
				{
				State = 65; variable();
				}
				break;
			case LBRACK:
				EnterOuterAlt(_localctx, 3);
				{
				State = 66; multiDensityList();
				}
				break;
			case LPAREN:
				EnterOuterAlt(_localctx, 4);
				{
				State = 67; Match(LPAREN);
				State = 68; density();
				State = 69; Match(RPAREN);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class NumberContext : ParserRuleContext {
		public ITerminalNode NUMBER() { return GetToken(DensityExpressionGrammarParser.NUMBER, 0); }
		public NumberContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_number; } }
		public override void EnterRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.EnterNumber(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.ExitNumber(this);
		}
	}

	[RuleVersion(0)]
	public NumberContext number() {
		NumberContext _localctx = new NumberContext(_ctx, State);
		EnterRule(_localctx, 14, RULE_number);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 73; Match(NUMBER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class VariableContext : ParserRuleContext {
		public ITerminalNode VARIABLE() { return GetToken(DensityExpressionGrammarParser.VARIABLE, 0); }
		public VariableContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_variable; } }
		public override void EnterRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.EnterVariable(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.ExitVariable(this);
		}
	}

	[RuleVersion(0)]
	public VariableContext variable() {
		VariableContext _localctx = new VariableContext(_ctx, State);
		EnterRule(_localctx, 16, RULE_variable);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 75; Match(VARIABLE);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class StrVariableContext : ParserRuleContext {
		public ITerminalNode VARIABLE() { return GetToken(DensityExpressionGrammarParser.VARIABLE, 0); }
		public StrVariableContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_strVariable; } }
		public override void EnterRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.EnterStrVariable(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.ExitStrVariable(this);
		}
	}

	[RuleVersion(0)]
	public StrVariableContext strVariable() {
		StrVariableContext _localctx = new StrVariableContext(_ctx, State);
		EnterRule(_localctx, 18, RULE_strVariable);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 77; Match(VARIABLE);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class BinaryBooleanOpContext : ParserRuleContext {
		public ITerminalNode EQ() { return GetToken(DensityExpressionGrammarParser.EQ, 0); }
		public ITerminalNode NEQ() { return GetToken(DensityExpressionGrammarParser.NEQ, 0); }
		public ITerminalNode GT() { return GetToken(DensityExpressionGrammarParser.GT, 0); }
		public ITerminalNode LT() { return GetToken(DensityExpressionGrammarParser.LT, 0); }
		public ITerminalNode GE() { return GetToken(DensityExpressionGrammarParser.GE, 0); }
		public ITerminalNode LE() { return GetToken(DensityExpressionGrammarParser.LE, 0); }
		public BinaryBooleanOpContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_binaryBooleanOp; } }
		public override void EnterRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.EnterBinaryBooleanOp(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IDensityExpressionGrammarListener typedListener = listener as IDensityExpressionGrammarListener;
			if (typedListener != null) typedListener.ExitBinaryBooleanOp(this);
		}
	}

	[RuleVersion(0)]
	public BinaryBooleanOpContext binaryBooleanOp() {
		BinaryBooleanOpContext _localctx = new BinaryBooleanOpContext(_ctx, State);
		EnterRule(_localctx, 20, RULE_binaryBooleanOp);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 79;
			_la = _input.La(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << EQ) | (1L << NEQ) | (1L << LT) | (1L << LE) | (1L << GT) | (1L << GE))) != 0)) ) {
			_errHandler.RecoverInline(this);
			} else {
				if (_input.La(1) == TokenConstants.Eof) {
					matchedEOF = true;
				}

				_errHandler.ReportMatch(this);
				Consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x3\x15T\x4\x2\t\x2"+
		"\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4\t\t"+
		"\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x3\x2\x3\x2\x5\x2\x1B\n\x2\x3\x2\x3\x2\x3"+
		"\x3\x3\x3\x3\x3\x3\x3\x3\x4\x3\x4\x3\x4\a\x4&\n\x4\f\x4\xE\x4)\v\x4\x3"+
		"\x5\x3\x5\x3\x5\x3\x5\a\x5/\n\x5\f\x5\xE\x5\x32\v\x5\x3\x5\x3\x5\x3\x6"+
		"\x3\x6\x3\x6\a\x6\x39\n\x6\f\x6\xE\x6<\v\x6\x3\a\x3\a\x3\a\x5\a\x41\n"+
		"\a\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x5\bJ\n\b\x3\t\x3\t\x3\n\x3\n\x3"+
		"\v\x3\v\x3\f\x3\f\x3\f\x2\x2\x2\r\x2\x2\x4\x2\x6\x2\b\x2\n\x2\f\x2\xE"+
		"\x2\x10\x2\x12\x2\x14\x2\x16\x2\x2\x5\x3\x2\v\f\x3\x2\r\xE\x3\x2\xF\x14"+
		"P\x2\x1A\x3\x2\x2\x2\x4\x1E\x3\x2\x2\x2\x6\"\x3\x2\x2\x2\b*\x3\x2\x2\x2"+
		"\n\x35\x3\x2\x2\x2\f@\x3\x2\x2\x2\xEI\x3\x2\x2\x2\x10K\x3\x2\x2\x2\x12"+
		"M\x3\x2\x2\x2\x14O\x3\x2\x2\x2\x16Q\x3\x2\x2\x2\x18\x1B\x5\x6\x4\x2\x19"+
		"\x1B\x5\x4\x3\x2\x1A\x18\x3\x2\x2\x2\x1A\x19\x3\x2\x2\x2\x1B\x1C\x3\x2"+
		"\x2\x2\x1C\x1D\a\x2\x2\x3\x1D\x3\x3\x2\x2\x2\x1E\x1F\x5\x6\x4\x2\x1F "+
		"\x5\x16\f\x2 !\x5\x6\x4\x2!\x5\x3\x2\x2\x2\"\'\x5\n\x6\x2#$\t\x2\x2\x2"+
		"$&\x5\n\x6\x2%#\x3\x2\x2\x2&)\x3\x2\x2\x2\'%\x3\x2\x2\x2\'(\x3\x2\x2\x2"+
		"(\a\x3\x2\x2\x2)\'\x3\x2\x2\x2*+\a\t\x2\x2+\x30\x5\x6\x4\x2,-\a\x6\x2"+
		"\x2-/\x5\x6\x4\x2.,\x3\x2\x2\x2/\x32\x3\x2\x2\x2\x30.\x3\x2\x2\x2\x30"+
		"\x31\x3\x2\x2\x2\x31\x33\x3\x2\x2\x2\x32\x30\x3\x2\x2\x2\x33\x34\a\n\x2"+
		"\x2\x34\t\x3\x2\x2\x2\x35:\x5\f\a\x2\x36\x37\t\x3\x2\x2\x37\x39\x5\f\a"+
		"\x2\x38\x36\x3\x2\x2\x2\x39<\x3\x2\x2\x2:\x38\x3\x2\x2\x2:;\x3\x2\x2\x2"+
		";\v\x3\x2\x2\x2<:\x3\x2\x2\x2=>\a\f\x2\x2>\x41\x5\f\a\x2?\x41\x5\xE\b"+
		"\x2@=\x3\x2\x2\x2@?\x3\x2\x2\x2\x41\r\x3\x2\x2\x2\x42J\x5\x10\t\x2\x43"+
		"J\x5\x12\n\x2\x44J\x5\b\x5\x2\x45\x46\a\a\x2\x2\x46G\x5\x6\x4\x2GH\a\b"+
		"\x2\x2HJ\x3\x2\x2\x2I\x42\x3\x2\x2\x2I\x43\x3\x2\x2\x2I\x44\x3\x2\x2\x2"+
		"I\x45\x3\x2\x2\x2J\xF\x3\x2\x2\x2KL\a\x3\x2\x2L\x11\x3\x2\x2\x2MN\a\x4"+
		"\x2\x2N\x13\x3\x2\x2\x2OP\a\x4\x2\x2P\x15\x3\x2\x2\x2QR\t\x4\x2\x2R\x17"+
		"\x3\x2\x2\x2\b\x1A\'\x30:@I";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
