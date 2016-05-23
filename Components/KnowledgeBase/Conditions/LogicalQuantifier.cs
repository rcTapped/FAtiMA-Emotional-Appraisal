﻿namespace KnowledgeBase.Conditions
{
	/// <summary>
	/// Represents logical quantification modes
	/// </summary>
	public enum LogicalQuantifier : byte
	{
		/// <summary>
		/// Sets of conditions evaluated in this mode, return true if at least on possible case is considered valid.
		/// </summary>
		Existential,
		/// <summary>
		/// Sets of conditions evaluated in this mode, return true only if all the possible cases are considered valid.
		/// </summary>
		Universal
	}
}