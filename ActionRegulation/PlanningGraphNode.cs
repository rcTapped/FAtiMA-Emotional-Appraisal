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
        public List<PlanningGraphNode> Mutex { get; set; }

        public PlanningGraphNode(int layer)
        {
            Layer = layer;
            IncomingEdges = new List<Edge>();
            OutgoingEdges = new List<Edge>();
            Mutex = new List<PlanningGraphNode>();
        }

        public void addIncomingEdge(Edge edge)
        {
            IncomingEdges.Add(edge);
        }

        public void addOutgoingEdge(Edge edge)
        {
            OutgoingEdges.Add(edge);
        }

        //adds the mutex to the mutex list
        public void addMutex(PlanningGraphNode mutex)
        {
            //if(!Mutex.Contains(mutex))
                Mutex.Add(mutex);
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

        //if at least one effect of this action is negated by the other action they are mutex
        public bool EffectNegatedBy(ActionNode actionNode)
        {
            foreach (Literal literal in this.Action.Effects)
            {
                foreach (Literal otherLiteral in actionNode.Action.Effects)
                {
                    if(literal.negationOf(otherLiteral))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        //if at least one precondition of this action is negated by the other action they are mutex
        public bool PreconditionNegatedBy(ActionNode actionNode)
        {
            foreach (Literal literal in this.Action.PreConditions)
            {
                foreach (Literal otherLiteral in actionNode.Action.Effects)
                {
                    if (literal.negationOf(otherLiteral))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            ActionNode node = obj as ActionNode;

            if (node == null)
                return false;
            return this.Action.Equals(node.Action);
        }

        public override int GetHashCode()
        {
            return this.Layer.GetHashCode();
        }
    }
}
