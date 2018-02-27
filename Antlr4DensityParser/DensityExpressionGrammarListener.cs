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

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="DensityExpressionGrammarParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.5-SNAPSHOT")]
[System.CLSCompliant(false)]
public interface IDensityExpressionGrammarListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="DensityExpressionGrammarParser.compileUnit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCompileUnit([NotNull] DensityExpressionGrammarParser.CompileUnitContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DensityExpressionGrammarParser.compileUnit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCompileUnit([NotNull] DensityExpressionGrammarParser.CompileUnitContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DensityExpressionGrammarParser.probability"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProbability([NotNull] DensityExpressionGrammarParser.ProbabilityContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DensityExpressionGrammarParser.probability"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProbability([NotNull] DensityExpressionGrammarParser.ProbabilityContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DensityExpressionGrammarParser.density"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDensity([NotNull] DensityExpressionGrammarParser.DensityContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DensityExpressionGrammarParser.density"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDensity([NotNull] DensityExpressionGrammarParser.DensityContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DensityExpressionGrammarParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTerm([NotNull] DensityExpressionGrammarParser.TermContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DensityExpressionGrammarParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTerm([NotNull] DensityExpressionGrammarParser.TermContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DensityExpressionGrammarParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFactor([NotNull] DensityExpressionGrammarParser.FactorContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DensityExpressionGrammarParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFactor([NotNull] DensityExpressionGrammarParser.FactorContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DensityExpressionGrammarParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAtom([NotNull] DensityExpressionGrammarParser.AtomContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DensityExpressionGrammarParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAtom([NotNull] DensityExpressionGrammarParser.AtomContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DensityExpressionGrammarParser.number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumber([NotNull] DensityExpressionGrammarParser.NumberContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DensityExpressionGrammarParser.number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumber([NotNull] DensityExpressionGrammarParser.NumberContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DensityExpressionGrammarParser.variable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVariable([NotNull] DensityExpressionGrammarParser.VariableContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DensityExpressionGrammarParser.variable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVariable([NotNull] DensityExpressionGrammarParser.VariableContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DensityExpressionGrammarParser.strVariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStrVariable([NotNull] DensityExpressionGrammarParser.StrVariableContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DensityExpressionGrammarParser.strVariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStrVariable([NotNull] DensityExpressionGrammarParser.StrVariableContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DensityExpressionGrammarParser.binaryBooleanOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBinaryBooleanOp([NotNull] DensityExpressionGrammarParser.BinaryBooleanOpContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DensityExpressionGrammarParser.binaryBooleanOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBinaryBooleanOp([NotNull] DensityExpressionGrammarParser.BinaryBooleanOpContext context);
}
