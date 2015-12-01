using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase;
using NUnit.Framework;
using Utilities;

namespace Tests.KnowledgeBase
{
	[TestFixture]
	public class ConstraintSetTests
	{
		private class TestFactory
		{
			public IEnumerable<TestCaseData> Test_Add_Batch_Cases()
			{
				yield return new TestCaseData(new[]
				{
					"[x]=[x]"
				}, false);
				//yield return new TestCaseData(new[]
				//{
				//	"[x]=3",
				//	"[y]=4"
				//}, false);
				//yield return new TestCaseData(new[]
				//{
				//	"[x]=3",
				//	"[y]=4",
				//	"[x]=3"
				//}, false);
				//yield return new TestCaseData(new[]
				//{
				//	"[x]=3",
				//	"[y]=5",
				//	"[x]=4"
				//}, true);
				//yield return new TestCaseData(new[]
				//{
				//	"[x]=[y]",
				//	"[x]=3"
				//}, false);
				//yield return new TestCaseData(new[]
				//{
				//	"[x]=3",
				//	"[x]=[y]"
				//}, false);
				//yield return new TestCaseData(new[]
				//{
				//	"[x]=3",
				//	"[x]=[y]",
				//	"[y]=4"
				//}, true);
				//yield return new TestCaseData(new[]
				//{
				//	"[x]=3",
				//	"[y]=3",
				//	"[x]=[w]",
				//	"[z]=[y]",
				//	"[x]=[y]"
				//}, false);
				//yield return new TestCaseData(new[]
				//{
				//	"[x]=3",
				//	"[y]=4",
				//	"[x]=[w]",
				//	"[z]=[y]",
				//	"[x]=[y]"
				//}, true);

				////Inequalities
				//yield return new TestCaseData(new[]
				//{
				//	"[y]!=3",
				//	"[x]=3",
				//	"[y]=4"
				//}, false);
				//yield return new TestCaseData(new[]
				//{
				//	"[x]=3",
				//	"[y]!=3",
				//	"[y]=4"
				//}, false);
				//yield return new TestCaseData(new[]
				//{
				//	"[x]=3",
				//	"[y]=4",
				//	"[y]!=3",
				//}, false);
				//yield return new TestCaseData(new[]
				//{
				//	"[x]=3",
				//	"[y]!=4",
				//	"[y]=[x]",
				//}, false);

				//yield return new TestCaseData(new[]
				//{
				//	"[x]=3",
				//	"[y]=3",
				//	"[y]!=3",
				//}, true);
				//yield return new TestCaseData(new[]
				//{
				//	"[x]=3",
				//	"[y]=3",
				//	"[y]!=[x]",
				//}, true);
				//yield return new TestCaseData(new[]
				//{
				//	"[y]!=[x]",
				//	"[x]=3",
				//	"[y]=3",
				//}, true);
				//yield return new TestCaseData(new[]
				//{
				//	"[y]!=[x]",
				//	"[x]=3",
				//	"[y]=4",
				//	"[y]=[z]",
				//	"[x]=[z]",
				//}, true);

				yield return new TestCaseData(new[]
				{
					"[x]!=3",
					"[x]=[z]",
					"[w]=[z]",
					"[x]=[t]",
					"[y]=[z]",
					"[y]=3",
				}, true);

				yield return new TestCaseData(new[]
				{
					"[x]=3",
					"[x]=[z]",
					"[w]=[z]",
					"[x]=[t]",
					"[a]!=[w]",
					"[a]!=[x]",
					"[y]!=[z]",
					"[y]=3",
				}, true);

				yield return new TestCaseData(new[]
				{
					"[x]=[z]", "[a]!=[w]", "[y]!=[z]", "[w]=[z]", "[y]=3", "[x]=[t]", "[a]!=[x]", "[x]=3"
				}, true);

				////Complex
				//yield return new TestCaseData(new[]
				//{
				//	"[a]=6",
				//	"[f]=3",
				//	"3!=[c]",
				//	"[d]=3",
				//	"[g]=5",
				//	"[a]=[e]",
				//	"[e]!=5",
				//	"[b]!=6",
				//	"[a]=[c]",
				//	"[a]!=3",
				//	"[g]!=[f]",
				//	"[f]!=5",
				//	"[b]!=6",
				//	"[c]=6",
				//	"[b]=5",
				//}, false);
			}
		}

		[TestCaseSource(typeof(TestFactory),"Test_Add_Batch_Cases")]
		public void Test_Add_Batch(IEnumerable<string> constraints, bool shouldFail)
		{
			const int iterations = 1000;
			Constraint[] elements = constraints.Select(s => (Constraint) s).ToArray();
			var set = new ConstraintSet();
			int it = iterations;
			while (it>0)
			{
				bool fail = false;
				try
				{
					foreach (var c in elements)
					{
						if (set.Add(c))
						{
							if (shouldFail)
								fail = true;
						}
						else
						{
							if (shouldFail)
								Assert.Pass();
						}
					}

					if (fail || shouldFail)
					{
						Console.WriteLine("Iteration {0}",(iterations - it));
						Console.Write(elements.Select(s => '\"' + s.ToString() + '\"').AggregateToString(", "));
						Assert.Fail();	
					}
				}
				catch (SuccessException)
				{
					
				}
	
				set.Clear();
				elements = elements.OrderBy(s => Guid.NewGuid()).ToArray();
				it--;
			}
		}
	}
}