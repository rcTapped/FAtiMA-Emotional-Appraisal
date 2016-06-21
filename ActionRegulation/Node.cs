using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRegulation
{
    class Node
    {
        public Goal Goal { get; set; }
        public List<string> State { get; set; }
        public List<Edge> inEdges { get; set; }
        public List<Edge> outEdges { get; set; }

        public Node(Goal goal)
        {
            Goal = goal;
            State = goal.SuccessConditions;
            inEdges = new List<Edge>();
            outEdges = new List<Edge>();
        }

        public Node(List<string> state)
        {
            Goal = null;
            State = state;
            inEdges = new List<Edge>();
            outEdges = new List<Edge>();
        }

        public Node(Goal goal, List<Edge> incomingEdges, List<Edge> outgoingEdges)
        {
            Goal = goal;
            inEdges = incomingEdges;
            outEdges = outgoingEdges;
        }

        public Node(List<string> state, List<Edge> incomingEdges, List<Edge> outgoingEdges)
        {
            Goal = null;
            State = state;
            inEdges = incomingEdges;
            outEdges = outgoingEdges;
        }

        public void addIncomingEdge(Edge edge)
        {
            inEdges.Add(edge);
        }

        public void removeIncomingEdge(Edge edge)
        {
            if (inEdges.Contains(edge))
                inEdges.Remove(edge);
            else
                Console.WriteLine("No such incoming edge present for removal");
        }

        public void addOutgoingEdge(Edge edge)
        {
            outEdges.Add(edge);
        }

        public void removeOutgoingEdge(Edge edge)
        {
            if (outEdges.Contains(edge))
                outEdges.Remove(edge);
            else
                Console.WriteLine("No such outgoing edge present for removal");
        }
    }
}
