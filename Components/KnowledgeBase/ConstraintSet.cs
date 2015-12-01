using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.WellFormedNames;
using Utilities;

namespace KnowledgeBase
{
	public sealed class ConstraintSet
	{
		private Dictionary<Name, HashSet<Node>> m_links;
		private HashSet<Constraint> m_constraints;

		public ConstraintSet()
		{
			m_links=new Dictionary<Name, HashSet<Node>>();
			m_constraints=new HashSet<Constraint>();
		}

		public bool Add(Constraint constraint)
		{
			if (constraint.Variable.Equals(constraint.Value))
				return true;

			if (m_constraints.Contains(constraint))
				return true;

			if (!CanAdd(constraint))
				return false;

			LinkConstraint(constraint);
			m_constraints.Add(constraint);
			return true;
		}

		public void Clear()
		{
			m_links.Clear();
			m_constraints.Clear();
		}

		private bool CanAdd(Constraint c)
		{
			Name v1 = GetValue(c.Variable);
			if (v1 == null)
				return !FindInequality(c.Variable, c.Value.IsVariable?GetValue((Symbol)c.Value):c.Value, c.Type == Constraint.ConstraintType.Equals);

			Name v2;
			if (c.Value.IsVariable)
			{
				v2 = GetValue((Symbol) c.Value);
				if (v2 == null)
					return !FindInequality((Symbol) c.Value, v1, c.Type == Constraint.ConstraintType.Equals);
			}
			else
				v2 = c.Value;

			var result = v1.Equals(v2);
			return c.Type == Constraint.ConstraintType.Equals ? result : !result;
		}

		private void LinkConstraint(Constraint c)
		{
			HashSet<Node> nodes;
			if (!m_links.TryGetValue(c.Variable, out nodes))
			{
				nodes = new HashSet<Node>();
				m_links[c.Variable] = nodes;
			}
			nodes.Add(new Node(c.Value, c.Type == Constraint.ConstraintType.Equals));

			if (!m_links.TryGetValue(c.Value, out nodes))
			{
				nodes = new HashSet<Node>();
				m_links[c.Value] = nodes;
			}
			nodes.Add(new Node(c.Variable, c.Type == Constraint.ConstraintType.Equals));
		}

		private Name GetValue(Symbol variable)
		{
			HashSet<Symbol> explored = new HashSet<Symbol>();
			Queue<Symbol> next = new Queue<Symbol>();
			next.Enqueue(variable);
			while (next.Count>0)
			{
				var v = next.Dequeue();
				explored.Add(v);
				HashSet<Node> nodes;
				if (m_links.TryGetValue(v, out nodes))
				{
					foreach (var e in nodes.Where(n => n.Equal).Select(n => n.Link))
					{
						if (!e.IsVariable)
							return e;
						if (!explored.Contains((Symbol)e))
							next.Enqueue((Symbol)e);
					}
				}
			}
			return null;
		}

		private bool AreEqual(Symbol v1, Symbol v2)
		{
			if (v1.Equals(v2))
				return true;

			HashSet<Symbol> explored = new HashSet<Symbol>();
			Queue<Symbol> next = new Queue<Symbol>();
			next.Enqueue(v1);
			while (next.Count > 0)
			{
				var v = next.Dequeue();
				explored.Add(v);
				HashSet<Node> nodes;
				if (m_links.TryGetValue(v, out nodes))
				{
					foreach (var e in nodes.Where(n => n.Equal && n.Link.IsVariable).Select(n => (Symbol)n.Link))
					{
						if (e.Equals(v2))
							return true;
						if (!explored.Contains(e))
							next.Enqueue(e);
					}
				}
			}
			return false;
		}

		private bool FindInequality(Symbol variable, Name value, bool equal)
		{
			if (value == null)
				return false;

			if (equal)
				return FindInequality2(variable, value);

			HashSet<Node> nodes;
			if (m_links.TryGetValue(value, out nodes))
			{
				foreach (var l in nodes.Where(n => !n.Equal).Select(n => (Symbol)n.Link))
				{
					if (AreEqual(variable, l))
						return true;
				}
			}
			return false;
		}

		private bool FindInequality2(Symbol variable, Name value)
		{
			HashSet<Node> nodes;
			if (m_links.TryGetValue(variable, out nodes))
			{
				foreach (var l in nodes.Where(n => n.Link.IsVariable))
				{
					if (l.Equal)
					{
						if(fi)
					}
					var v = GetValue(l);
					if (v != null && v.Equals(value))
						return true;
				}
			}
		}

		private class Node
		{
			public readonly Name Link;
			public readonly bool Equal;

			public Node(Name link, bool equal)
			{
				Link = link;
				Equal = equal;
			}

			public override int GetHashCode()
			{
				var h = Link.GetHashCode();
				return Equal ? h : ~h;
			}

			public override bool Equals(object obj)
			{
				var n = obj as Node;
				if (n == null)
					return false;
				return Link.Equals(n.Link) && (n.Equal == Equal);
			}
		}
	}
}