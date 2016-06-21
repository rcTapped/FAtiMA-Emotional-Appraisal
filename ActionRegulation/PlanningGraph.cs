using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRegulation
{
    class PlanningGraph
    {
        Node initNode;
        List<Node> nodeList;
        List<Edge> edgeList;

        public PlanningGraph()
        {
            initNode = null;
            nodeList = new List<Node>();
            edgeList = new List<Edge>();
        }

        public void CakeExample()
        {
            initNode = new Node(new List<string>(new string[] { "have(cake)" }));

            Goal haveCakeAndEatenGoal = new Goal("have(cake)^eaten(cake)", new List<string>(new string[] { "have(cake)", "eaten(cake)" }));
            Node haveCakeAndEatenNode = new Node(haveCakeAndEatenGoal);
            nodeList.Add(haveCakeAndEatenNode);

            List<string> preConditions1 = new List<string>(new string[] { "have(cake)" });
            List<string> effects1 = new List<string>(new string[] { "eaten(cake)" });
            Action eatCakeAction = new Action("eatCake", preConditions1, effects1);
            Edge eatCakeEdge = new Edge(eatCakeAction);
            edgeList.Add(eatCakeEdge);
            
            List<string> preConditions2 = new List<string>();
            List<string> effects2 = new List<string>(new string[] { "have(cake)" });
            Action bakeCakeAction = new Action("bakeCake", preConditions2, effects2);
            Edge bakeCakeEdge = new Edge(bakeCakeAction);
            edgeList.Add(bakeCakeEdge);

            buildGraph(initNode, 5);
            displayGraph(initNode);
        }

        public void buildGraph(Node currentNode, int depth)
        {
            depth--;

            //check for actions whose preconditions are met in state of current node
            foreach(Edge edge in edgeList)
            {
                //check if preconditions of action is subset of current state AND if effects arent a subset of current state
                if(!edge.Action.PreConditions.Except(currentNode.State).Any() && edge.Action.Effects.Except(currentNode.State).Any())
                {
                    Edge newEdge = new Edge(edge.Action);
                    newEdge.From = currentNode;

                    //add outgoing edge to current node
                    currentNode.addOutgoingEdge(newEdge);

                    //create node for resulting state
                    List<string> newState = currentNode.State.Except(newEdge.Action.PreConditions).ToList();
                    newState.AddRange(newEdge.Action.Effects);

                    Node nextState = new Node(newState);
                    nextState.addIncomingEdge(newEdge);
                    newEdge.To = nextState;

                    if(depth >= 1)
                        buildGraph(nextState, depth);
                }
            }
        }

        public void displayGraph(Node currentNode)
        {
            Console.Write(" node state: ");
            currentNode.State.ForEach(Console.Write);

            if (currentNode.outEdges.Any())
            {
                foreach (Edge edge in currentNode.outEdges)
                {
                    Console.Write(" outgoing edge: " + edge.Action.Name + " --> ");

                    displayGraph(edge.To);
                }
            } else
            {
                Console.WriteLine();
            }
        }
    }
}
