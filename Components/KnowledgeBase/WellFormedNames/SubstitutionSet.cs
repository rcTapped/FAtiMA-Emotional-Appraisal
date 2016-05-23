﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace KnowledgeBase.WellFormedNames
{
	/// <summary>
	/// Class representing a set of Substitution objects.
	/// 
	/// SubstitutionSet objects cannot contain conflicting substitutions, like:
	///  - [x] -> John
	///  - [x] -> Mary
	/// </summary>
	[Serializable]
	public sealed class SubstitutionSet : IEnumerable<Substitution>
	{
		private Dictionary<Name,Name> m_substitutions = new Dictionary<Name, Name>();

		/// <summary>
		/// Creates an empty SubstitutionSet
		/// </summary>
		public SubstitutionSet() {
		}

		/// <summary>
		/// Creates a new SubstitutionSet, given a set of Substitution objects.
		/// </summary>
		/// <param name="substitutions">The set of Substitution objects.</param>
		/// <exception cref="ArgumentException">Thrown if the given set will create substitution conflicts.</exception>
		/// @{
		public SubstitutionSet(params Substitution[] substitutions)
			: this((IEnumerable<Substitution>)substitutions)
		{}

		public SubstitutionSet(IEnumerable<Substitution> substitutions)
		{
			if(!AddSubstitutions(substitutions))
				throw new ArgumentException("The given substitutions will generate a conflict.", nameof(substitutions));
		}
		/// @}

		/// <summary>
		/// Gets/Sets the Name to substitute for a give variable.
		/// </summary>
		/// <param name="variable">The Name of the variable of the substitution value to get</param>
		/// <returns>The Name that will substitute the given variable Name,
		/// or null if the variable is not contained within this SubstitutionSet.</returns>
		/// <exception cref="ArgumentException">Thrown if the given Name does not represent a variable.</exception>
		public Name this[Name variable]
		{
			get
			{
				if (!variable.IsVariable)
					throw new ArgumentException("The given Name is not a variable.");

				Name r;
				return m_substitutions.TryGetValue(variable, out r) ? r : null;
			}
		}

		/// <summary>
		/// Tells if a given variable is contained within this SubstitutionSet.
		/// </summary>
		/// <param name="variable">The Name of variable to test.</param>
		/// <exception cref="ArgumentException">Thrown if the given Name is not a variable definition.</exception>
		public bool Contains(Name variable)
		{
			if (!variable.IsVariable)
				throw new ArgumentException("The given Name is not a variable.",nameof(variable));

			return m_substitutions.ContainsKey(variable);
		}

		/// <summary>
		/// Returns how many substitutions are in this set.
		/// </summary>
		public int Count()
		{
			return m_substitutions.Count;
		}

		/// <summary>
		/// Adds a new Substitution to this set.
		/// The adding process might fail, if the addition of the new Substitution would create a conflict.
		/// </summary>
		/// <param name="substitution">The Substitution to add to this set.</param>
		/// <returns>True if the substitution was sucessfully added to the set. False otherwise.</returns>
		/// <remarks>
		/// If the given Substitution already exists in this set, this method will return true as if it
		/// was successfully added, but it would not produce any changes to the underlying set.
		/// </remarks>
		public bool AddSubstitution(Substitution substitution)
		{
			bool canAdd;
			if (TestConflict(substitution, this, out canAdd))
				return false;

			if (canAdd)
				AddSub(substitution.Variable,substitution.Value);

			return true;
		}

		/// <summary>
		/// Merge a Substitution set with this one.
		/// The merging will only ocurr if no conflicts between the two sets are found.
		/// </summary>
		/// <param name="substitution">The SubstitutionSet to merge with this one.</param>
		/// <returns>True if the substitutions was sucessfully merged. False otherwise.</returns>
		public bool AddSubstitutions(SubstitutionSet substitutions)
		{
			if (Conflicts(substitutions))
				return false;

			foreach (var s in substitutions)
				AddSub(s.Variable, s.Value);

			return true;
		}

		/// <summary>
		/// Adds a set of Substitution objects to this set.
		/// If conflics are detected, the original SubstitutionSet object in not changed.
		/// </summary>
		/// <param name="substitutions">The Substitution objects set to add.</param>
		/// <returns>True if all the substitutions were added to this object. False if conflics were detected.</returns>
		public bool AddSubstitutions(IEnumerable<Substitution> substitutions)
		{
			bool rollback = false;
			List<Name> added = ObjectPool<List<Name>>.GetObject();
			try
			{
				foreach (var s in substitutions)
				{
					bool canAdd;
					if (TestConflict(s, this, out canAdd))
					{
						rollback = true;
						break;
					}

					if (canAdd)
					{
						AddSub(s.Variable, s.Value);
						added.Add(s.Variable);
					}
				}

				if (rollback)
				{
					foreach (var s in added)
						m_substitutions.Remove(s);
				}

				return !rollback;
			}
			finally
			{
				added.Clear();
				ObjectPool<List<Name>>.Recycle(added);
			}
		}

		private static bool TestConflict(Substitution subs, SubstitutionSet substitutions, out bool canAdd)
		{
			canAdd = true;
			Name value;
			if (!substitutions.m_substitutions.TryGetValue(subs.Variable, out value))
				return false;

			canAdd = false;
			var G1 = value.MakeGround(substitutions);
			var G2 = subs.Value.MakeGround(substitutions);
			return !G1.Equals(G2);	//Conflict!!!
		}

		private void AddSub(Name variable, Name value)
		{
			m_substitutions.Add(variable, value);
			//if(added!=null)
			//	added.Add(variable);

			//if (value.IsVariable)
			//{
			//	m_substitutions.Add(value,variable);
			//	if (added != null)
			//		added.Add(value);
			//}
		}

		/// <summary>
		/// Determines if a given Substitution object will conflict with this set.
		/// </summary>
		/// <param name="substitution">The Substitution object to test.</param>
		public bool Conflicts(Substitution substitution)
		{
			bool aux;
			return TestConflict(substitution,this, out aux);
		}

		/// <summary>
		/// Determines if a given SubstitutionSet object will conflict with this set.
		/// </summary>
		/// <param name="substitution">The SubstitutionSet object to test.</param>
		public bool Conflicts(SubstitutionSet substitutions)
		{
			foreach (var pair in substitutions.m_substitutions)
			{
				Name value;
				if (m_substitutions.TryGetValue(pair.Key, out value))
				{
					var g1 = pair.Value.MakeGround(substitutions).MakeGround(this);
					var g2 = value.MakeGround(this).MakeGround(substitutions);
					if (!g1.Equals(g2))
						return true;
				}
			}
			return false;
		}
		
		/// <summary>
		/// Returns the enumerator of this set.
		/// </summary>
		public IEnumerator<Substitution> GetEnumerator()
		{
			return m_substitutions.Select(e => new Substitution(e.Key, e.Value)).GetEnumerator();
		}

		/// <summary>
		/// Returns the enumerator of this set.
		/// </summary>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		private IEnumerable<Substitution> GetGroundedSubstitutions()
		{
			if (m_substitutions.Count > 0)
				return m_substitutions.Select(e => new Substitution(e.Key, e.Value.MakeGround(this))).Distinct();
			return Enumerable.Empty<Substitution>();
		}

		/// @cond DOXYGEN_SHOULD_SKIP_THIS

		public override int GetHashCode()
		{
			//This is a random value to represent an empty set,
			//since it does not have elements to calculate an hash,
			//and two empty sets are equal.
			const int emptyHashCode = 0x0fc43f9;

			var set = GetGroundedSubstitutions();
			if (!set.Any())
				return emptyHashCode;

			var hashs = set.Select(s => s.GetHashCode());
			var h = hashs.Aggregate((v1, v2) => v1 ^ v2);
			return emptyHashCode ^ h;
		}

		public override bool Equals(object obj)
		{
			SubstitutionSet other = obj as SubstitutionSet;
			if (other == null)
				return false;

			HashSet<Substitution> aux1 = ObjectPool<HashSet<Substitution>>.GetObject();
			HashSet<Substitution> aux2 = ObjectPool<HashSet<Substitution>>.GetObject();
			try 
			{	        
				aux1.UnionWith(this.GetGroundedSubstitutions());
				aux2.UnionWith(other.GetGroundedSubstitutions());
				var b = aux1.SetEquals(aux2);
				return b;
			}
			finally
			{
				aux1.Clear();
				ObjectPool<HashSet<Substitution>>.Recycle(aux1);
				aux2.Clear();
				ObjectPool<HashSet<Substitution>>.Recycle(aux2);
			}
		}

		public override string ToString()
		{
			StringBuilder builder = ObjectPool<StringBuilder>.GetObject();
			try
			{
				builder.Append("(");
				bool addComma = false;
				foreach (var e in m_substitutions)
				{
					if (addComma)
						builder.Append(", ");

					builder.AppendFormat("{0}/{1}", e.Key, e.Value);
					addComma = true;
				}
				builder.Append(")");
				return builder.ToString();
			}
			finally
			{
				builder.Length = 0;
				ObjectPool<StringBuilder>.Recycle(builder);
			}
		}

		/// @endcond
	}
}