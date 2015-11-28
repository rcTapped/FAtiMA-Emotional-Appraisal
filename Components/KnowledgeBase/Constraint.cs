using System;
using System.Text.RegularExpressions;
using KnowledgeBase.Exceptions;
using KnowledgeBase.WellFormedNames;

namespace KnowledgeBase
{
	public sealed class Constraint
	{
		private const string REGEX_PATTERN = @"^([^=!]+)(=|!=)([^=!]+)$";
		private static readonly Regex PARSE_REGEX = new Regex(REGEX_PATTERN,RegexOptions.Singleline);

		private readonly bool m_different;
		public Symbol Variable
		{
			get;
			private set;
		}
		public Name Value
		{
			get;
			private set;
		}
		public ConstraintType Type {
			get { return m_different ? ConstraintType.Different : ConstraintType.Equals; }
		}

		private Constraint(Symbol variable, Name value, bool different)
		{
			Variable = variable;
			Value = value;
			m_different = different;
		}

		public Constraint(Symbol variable, Name value, ConstraintType type)
		{
			if (!variable.IsVariable)
				throw new ArgumentException("not a variable.", "variable");
			if(value.IsUniversal)
				throw new ArgumentException("Universal symbol * is not allowed in a constraint.", "value");

			if (value.Equals(variable))
			{
				if(type == ConstraintType.Different)
					throw new InvalidOperationException("Equal variables cannot be diferent");
			}
			else
			{
				if (value.ContainsVariable(variable))
					throw new InvalidOperationException(string.Format("{0}->{1} will create a cyclical reference.", variable, value));
			}

			this.Variable = variable;
			this.Value = value;
			m_different = type == ConstraintType.Different;
		}

		public Constraint(Constraint other)
		{
			m_different = other.m_different;
			Variable = (Symbol) other.Variable.Clone();
			Value = (Name) other.Value.Clone();
		}

		public Constraint Invert()
		{
			if (!m_different && Variable.Equals(Value))
				throw new InvalidOperationException("Equal variables cannot be diferent");
			return new Constraint(Variable,Value,!m_different);
		}

		public override bool Equals(object obj)
		{
			Constraint c = obj as Constraint;
			if (c == null)
				return false;

			return m_different == c.m_different && Variable.Equals(c.Variable) && Value.Equals(c.Value);
		}

		public override int GetHashCode()
		{
			int hash = Variable.GetHashCode() ^ Value.GetHashCode();
			return m_different ? ~hash : hash;
		}

		public override string ToString()
		{
			return Variable + (m_different ? "!=" : "=") + Value;
		}

		public static Constraint Parse(string str)
		{
			var m = PARSE_REGEX.Match(str);
			if(!m.Success)
				throw new ParsingException("Unable to parse {0} as a constraint",str);

			Name n1 = Name.Parse(m.Groups[1].Value);
			Name n2 = Name.Parse(m.Groups[3].Value);

			if(n1.IsUniversal || n2.IsUniversal)
				throw new ParsingException("Constraint cannot contain the universal symbol *", str);

			var equals = (m.Groups[2].Value == "=");

			var b = n1.IsVariable;
			if (b == n2.IsVariable)
			{
				if (!b)
					throw new ParsingException("Constraint must contain at least one variable");

				if (n1.Equals(n2) && !equals)
					throw new ParsingException("Equal variables cannot be diferent");
			}

			Symbol variable;
			Name value;
			if (b)
			{
				variable = (Symbol) n1;
				value = n2;
			}
			else
			{
				variable = (Symbol)n2;
				value = n1;
			}

			if (!variable.Equals(value) && value.ContainsVariable(variable))
				throw new ParsingException("{0}->{1} will create a cyclical reference.", variable, value);

			return new Constraint(variable,value,!equals);
		}

#region Operators

		public static explicit operator Constraint(string definition)
		{
			return Parse(definition);
		}

		public static Constraint operator !(Constraint c)
		{
			return c.Invert();
		}

#endregion

		public enum ConstraintType : byte
		{
			Equals,
			Different
		}
	}
}