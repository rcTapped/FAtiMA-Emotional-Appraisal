using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRegulation
{
    class BasicGraph
    {
        //Node initNode;
        //List<Node> nodeList;
        //List<ActionEdge> ActionEdgeList;

        //public BasicGraph()
        //{
        //    initNode = null;
        //    nodeList = new List<Node>();
        //    ActionEdgeList = new List<ActionEdge>();
        //}

        //public void CakeExample()
        //{
        //    initNode = new Node(new List<string>(new string[] { "have(cake)" }));

        //    Goal haveCakeAndEatenGoal = new Goal("have(cake)^eaten(cake)", new List<string>(new string[] { "have(cake)", "eaten(cake)" }));
        //    Node haveCakeAndEatenNode = new Node(haveCakeAndEatenGoal);
        //    nodeList.Add(haveCakeAndEatenNode);

        //    List<string> preConditions1 = new List<string>(new string[] { "have(cake)" });
        //    List<string> effects1 = new List<string>(new string[] { "eaten(cake)" });
        //    Action eatCakeAction = new Action("eatCake", preConditions1, effects1);
        //    ActionEdge eatCakeActionEdge = new ActionEdge(eatCakeAction);
        //    ActionEdgeList.Add(eatCakeActionEdge);

        //    List<string> preConditions2 = new List<string>();
        //    List<string> effects2 = new List<string>(new string[] { "have(cake)" });
        //    Action bakeCakeAction = new Action("bakeCake", preConditions2, effects2);
        //    ActionEdge bakeCakeActionEdge = new ActionEdge(bakeCakeAction);
        //    ActionEdgeList.Add(bakeCakeActionEdge);

        //    List<string> preConditions3 = new List<string>();
        //    List<string> effects3 = new List<string>(new string[] { "have(cake)" });
        //    Action orderCakeAction = new Action("orderCake", preConditions2, effects2);
        //    ActionEdge orderCakeActionEdge = new ActionEdge(orderCakeAction);
        //    ActionEdgeList.Add(orderCakeActionEdge);

        //    buildGraph(initNode, 5);
        //    displayGraph(initNode);
        //}

        //public void buildGraph(Node currentNode, int depth)
        //{
        //    depth--;

        //    //check for actions whose preconditions are met in state of current node
        //    foreach (ActionEdge ActionEdge in ActionEdgeList)
        //    {
        //        //check if preconditions of action is subset of current state AND if effects arent a subset of current state
        //        if (!ActionEdge.Action.PreConditions.Except(currentNode.State).Any() && ActionEdge.Action.Effects.Except(currentNode.State).Any())
        //        {
        //            ActionEdge newActionEdge = new ActionEdge(ActionEdge.Action);
        //            newActionEdge.From = currentNode;

        //            //add outgoing ActionEdge to current node
        //            currentNode.addOutgoingEdge(newActionEdge);

        //            //create node for resulting state
        //            List<string> newState = currentNode.State.Except(newActionEdge.Action.PreConditions).ToList();
        //            newState.AddRange(newActionEdge.Action.Effects);

        //            Node nextState = new Node(newState);
        //            nextState.addIncomingEdge(newActionEdge);
        //            newActionEdge.To = nextState;

        //            if (depth >= 1)
        //                buildGraph(nextState, depth);
        //        }
        //    }
        //}

        //public void displayGraph(Node currentNode)
        //{
        //    if (currentNode.outEdges.Any())
        //    {
        //        foreach (ActionEdge ActionEdge in currentNode.outEdges)
        //        {
        //            Console.Write(" node state: ");
        //            currentNode.State.ForEach(Console.Write);
        //            Console.Write(" outgoing ActionEdge: " + ActionEdge.Action.Name + " --> ");

        //            displayGraph(ActionEdge.To);
        //        }
        //    }
        //    else
        //    {
        //        Console.Write(" node state: ");
        //        currentNode.State.ForEach(Console.Write);
        //        Console.WriteLine();
        //    }
        //}
    }
}
