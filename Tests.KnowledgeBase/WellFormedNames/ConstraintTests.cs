using System;
using KnowledgeBase;
using KnowledgeBase.Exceptions;
using KnowledgeBase.WellFormedNames;
using NUnit.Framework;

namespace Tests.KnowledgeBase.WellFormedNames
{
	[TestFixture]
	public class ConstraintTests
	{
		[TestCase("[x]", "3", Constraint.ConstraintType.Equals)]
		[TestCase("[x]", "john", Constraint.ConstraintType.Equals)]
		[TestCase("[y]", "Like(John,Sandy)", Constraint.ConstraintType.Different)]
		[TestCase("[_554]", "[long_ass_variable_name_35]", Constraint.ConstraintType.Different)]
		[TestCase("[_32]", "Hana", Constraint.ConstraintType.Equals)]
		[TestCase("[x]", "[x]", Constraint.ConstraintType.Equals)]
		public void Test_Constructor_Valid(string variable, string value, Constraint.ConstraintType type)
		{
			new Constraint((Symbol)variable, (Name) value, type);
		}

		[TestCase("[x]", "[x]", Constraint.ConstraintType.Different, ExpectedException = typeof(InvalidOperationException))]
		[TestCase("[x]","*", Constraint.ConstraintType.Different, ExpectedException = typeof(ArgumentException))]
		[TestCase("victor", "victor", Constraint.ConstraintType.Equals, ExpectedException = typeof(ArgumentException))]
		[TestCase("[x]", "A([x])", Constraint.ConstraintType.Different, ExpectedException = typeof(InvalidOperationException))]
		public void Test_Constructor_Invalid(string variable, string value, Constraint.ConstraintType type)
		{
			new Constraint((Symbol)variable, (Name)value, type);
		}

		[TestCase("[x]=3","[x]","3",false)]
		[TestCase("[x]=John","[x]","john",false)]
		[TestCase("[Y]!=Like(John,Sandy)","[y]","Like(John,Sandy)",true)]
		[TestCase("[_554]!=[long_ass_variable_name_35]", "[_554]","[long_ass_variable_name_35]",true)]
		[TestCase("Hana=[_32]", "[_32]", "Hana", false)]
		[TestCase("[x]=[x]", "[x]", "[x]", false)]
		public void Test_Parse_Valid(string str,string variable, string value, bool different)
		{
			var c = (Constraint)str;
			Assert.NotNull(c);
			Assert.AreEqual(c.Variable,(Name)variable);
			Assert.AreEqual(c.Value, (Name)value);
			Assert.AreEqual(c.Type, (different ? Constraint.ConstraintType.Different : Constraint.ConstraintType.Equals));
		}

		[TestCase("[x]!=[x]",ExpectedException = typeof(ParsingException))]
		[TestCase("*!=[x]", ExpectedException = typeof(ParsingException))]
		[TestCase("victor=victor", ExpectedException = typeof(ParsingException))]
		[TestCase("victor!=john", ExpectedException = typeof(ParsingException))]
		[TestCase("[x]==john", ExpectedException = typeof(ParsingException))]
		[TestCase("[x]==???", ExpectedException = typeof(ParsingException))]
		[TestCase("[x]!=???", ExpectedException = typeof(ParsingException))]
		[TestCase("[x]!=A([x])", ExpectedException = typeof(ParsingException))]
		public void Test_Parse_Invalid(string str)
		{
			Constraint.Parse(str);
		}

		[TestCase("[x]=3")]
		[TestCase("[x]=John")]
		[TestCase("[Y]!=Like(John,Sandy)")]
		[TestCase("[_554]!=[long_ass_variable_name_35]")]
		[TestCase("Hana=[_32]")]
		public void Test_Clone_Constructor(string str)
		{
			var c1 = (Constraint)str;
			var c2 = new Constraint(c1);
			Assert.AreNotSame(c1,c2);
			Assert.AreEqual(c1.GetHashCode(),c2.GetHashCode());
			Assert.AreEqual(c1,c2);
		}

		[TestCase("[x]=3")]
		[TestCase("3=[x]")]
		[TestCase("[z]=[x]")]
		public void Test_Invert_Valid(string str)
		{
			var c1 = (Constraint)str;
			var c2 = !c1;
			Assert.AreNotSame(c1, c2);
			Assert.AreNotEqual(c1, c2);
			var h1 = c1.GetHashCode();
			var h2 = c2.GetHashCode();
			Assert.AreNotEqual(h1,h2);
			var h = h1 & h2;
			Assert.AreEqual(h,0);
			Assert.AreNotEqual(c1.Type,c2.Type);
		}

		[TestCase("[x]=[x]",ExpectedException = typeof(InvalidOperationException))]
		public void Test_Invert_Invalid(string str)
		{
			var c1 = (Constraint)str;
			c1.Invert();
		}

		[TestCase("[x]=3")]
		[TestCase("[x]=John")]
		[TestCase("[Y]!=Like(John,Sandy)")]
		[TestCase("[_554]!=[long_ass_variable_name_35]")]
		[TestCase("Hana=[_32]")]
		public void Test_ToString(string str)
		{
			var c1 = (Constraint)str;
			var c2 = (Constraint) c1.ToString();
			Assert.AreNotSame(c1, c2);
			Assert.AreEqual(c1.GetHashCode(), c2.GetHashCode());
			Assert.AreEqual(c1, c2);
		}

		[Test]
		public void Test_NotEqualToNull()
		{
			var c = new Constraint((Symbol) "[s]", (Name) "3", Constraint.ConstraintType.Equals);
			Assert.False(c.Equals(null));
		}
	}
}