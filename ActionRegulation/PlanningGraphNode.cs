using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRegulation
{
    abstract class PlanningGraphNode
    {
        public int Layer { get; set; }
        public List<Edge> IncomingEdges { get; set; }
        public List<Edge> OutgoingEdges { get; set; }

        public PlanningGraphNode(int layer)
        {
            Layer = layer;
            IncomingEdges = new List<Edge>();
            OutgoingEdges = new List<Edge>();
        }

        public void addIncomingEdge(Edge edge)
        {
            IncomingEdges.Add(edge);
        }

        public void addOutgoingEdge(Edge edge)
        {
            OutgoingEdges.Add(edge);
        }
    }

    class LiteralNode : PlanningGraphNode
    {
        public Literal Literal { get; set; }

        public LiteralNode(int layer, Literal literal) : base(layer)
        {
            Literal = literal;
        }

        public override bool Equals(object obj)
        {
            LiteralNode node = obj as LiteralNode;

            if (node == null)
                return false;
            return this.Literal.Equals(node.Literal);
        }

        public override int GetHashCode()
        {
            return this.Layer.GetHashCode();
        }
    }

    class ActionNode : PlanningGraphNode
    {
        public Action Action { get; set; }

        public ActionNode(int layer, Action action) : base(layer)
        {
            Action = action;
        }
    }
}
