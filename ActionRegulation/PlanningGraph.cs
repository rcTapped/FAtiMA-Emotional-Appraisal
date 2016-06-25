using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRegulation
{
    class PlanningGraph
    {
        List<Literal> initState;
        List<PlanningGraphNode> nodeList;
        List<Edge> edgeList;
        List<Action> actionList;
        Goal goal;

        public PlanningGraph()
        {
            initState = null;
            nodeList = new List<PlanningGraphNode>();
            edgeList = new List<Edge>();
            actionList = new List<Action>();
            goal = null;
        }

        //even layers are literal layers, uneven layers are action layers
        public void buildPlanningGraph()
        {
            bool solutionFound = false;
            int layerCount = 0;
            List<LiteralNode> literalLayer = new List<LiteralNode>();       //keep track of current literal layer nodes
            List<LiteralNode> previousLiteralLayer;                         //keep track of previous literal layer nodes
            List<PlanningGraphNode> actionLayer;                            //keep track of current action layer nodes
            List<LiteralNode> maintenanceActions;                           //keep track of maintenance actions

            //create initial literal layer from initial state
            foreach (Literal literal in initState)
            {
                LiteralNode newNode = new LiteralNode(layerCount, literal);

                literalLayer.Add(newNode);
                nodeList.Add(newNode);
            }

            while (layerCount < 4 && !solutionFound)  // create layers 0 through 4 of the graph plan unless a solution is found earlier
            {
                layerCount++;

                /////////////////////////////////////// action layer (uneven) ////////////////////////////////////////////////////

                actionLayer = new List<PlanningGraphNode>();

                //for each action that is satisfied in the previous literal layer add the action node to the action layer and
                //the edge between the literal nodes that satisfy the action node's preconditions
                foreach (Action action in actionList)
                {
                    // check if all the preconditions of the action are met in the previous literal layer
                    if (!action.PreConditions.Except(literalLayer.Select(l => l.Literal), new LiteralComparer()).Any())
                    {
                        List<LiteralNode> metConditions = new List<LiteralNode>();

                        ActionNode actionNode = new ActionNode(layerCount, action);
                        actionLayer.Add(actionNode);
                        nodeList.Add(actionNode);
                        
                        //determine which literal nodes satisfy which the preconditions of the action
                        foreach (Literal literal in action.PreConditions)
                        {
                            foreach (LiteralNode literalNode in literalLayer)
                            {
                                if (literalNode.Literal.Equals(literal))
                                {
                                    metConditions.Add(literalNode);
                                    break;
                                }
                            }
                        }

                        //create an edge between each literal node that satisfies a precondition of the action
                        foreach (LiteralNode literalNode in metConditions)
                        {
                            Edge newEdge = new Edge(literalNode, actionNode);
                            literalNode.addOutgoingEdge(newEdge);
                            actionNode.addIncomingEdge(newEdge);
                        }
                    }
                }

                layerCount++;

                ///////////////////////////////////////// literal layer (even) /////////////////////////////////////////////////////

                previousLiteralLayer = literalLayer;
                literalLayer = new List<LiteralNode>();
                maintenanceActions = new List<LiteralNode>();

                //add literals that are the effects of actions in previous action layer
                foreach (ActionNode actionNode in actionLayer)
                {
                    foreach (Literal literal in actionNode.Action.Effects)
                    {
                        bool newNode = true;
                        LiteralNode literalNode = new LiteralNode(layerCount, literal);

                        //check if effect resulting literal already exists
                        foreach (LiteralNode existingLit in literalLayer)
                        {
                            if(existingLit.Literal.Equals(literal))
                            {
                                literalNode = existingLit;
                                newNode = false;
                                break;
                            }
                        }

                        if (newNode)
                        {
                            literalLayer.Add(literalNode);
                            nodeList.Add(literalNode);
                        }

                        Edge newEdge = new Edge(actionNode, literalNode);
                        actionNode.addOutgoingEdge(newEdge);
                        literalNode.addIncomingEdge(newEdge);
                    }
                }

                //add literals that where in the previous literal layer as maintenance actions
                foreach (LiteralNode literalNode in previousLiteralLayer)
                {
                    bool nodeAlreadyExists = false;
                    LiteralNode newNode = new LiteralNode(layerCount, literalNode.Literal);     //same literal new layer

                    //if literal layer already has this literal then link maintenance action edge to this node instead
                    foreach (LiteralNode node in literalLayer)
                    {
                        if (node.Equals(literalNode))
                        {
                            newNode = node;
                            nodeAlreadyExists = true;
                            break;
                        }
                    }

                    if (!nodeAlreadyExists)
                    {
                        literalLayer.Add(newNode);
                        nodeList.Add(newNode);
                    }

                    Edge newEdge = new Edge(literalNode, newNode);
                    maintenanceActions.Add(literalNode);
                    literalNode.addOutgoingEdge(newEdge);
                    newNode.addIncomingEdge(newEdge);
                }

                //action mutex time!

                //compare each action to each other action
                foreach (ActionNode node in actionLayer)
                {
                    //maintence actions mutexes
                    foreach (LiteralNode maintenance in maintenanceActions)
                    {
                        if(node.EffectNegatedBy(maintenance))
                        {
                            node.addMutex(maintenance);
                            maintenance.addMutex(node);
                        }
                    }

                    foreach (ActionNode otherNode in actionLayer)
                    {
                        if (!node.Equals(otherNode) && otherNode is ActionNode)
                        {
                            //inconsistent effects of actions
                            if (node.EffectNegatedBy(otherNode))
                                node.addMutex(otherNode);

                            //interference of actions
                            if (node.PreconditionNegatedBy(otherNode))
                            {
                                node.addMutex(otherNode);
                                otherNode.addMutex(node);
                            }

                            //competing needs
                            foreach (Edge edge in node.IncomingEdges)
                            {
                                //action node incoming edges should always be from literal nodes
                                LiteralNode literalNode = (LiteralNode)edge.From;

                                foreach (Edge otherEdge in otherNode.IncomingEdges)
                                {
                                    LiteralNode otherLiteralNode = (LiteralNode)otherEdge.From;

                                    if (literalNode.Mutex.Contains(otherLiteralNode))
                                    {
                                        node.addMutex(otherNode);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //end of action mutex time

                //literal mutex time!

                foreach (LiteralNode literalNode in literalLayer)
                {
                    foreach(LiteralNode otherLiteralNode in literalLayer)
                    {
                        if(!literalNode.Equals(otherLiteralNode))
                        {
                            //negation
                            if (literalNode.Literal.negationOf(otherLiteralNode.Literal))
                            {
                                literalNode.addMutex(otherLiteralNode);
                            }

                            //inconsistent support
                            int count = 0;

                            foreach (Edge edge in literalNode.IncomingEdges)
                            {
                                PlanningGraphNode node = edge.From;
                                bool consistent = false;

                                foreach (Edge otherEdge in otherLiteralNode.IncomingEdges)
                                {
                                    PlanningGraphNode otherNode = otherEdge.From;

                                    if (!node.Mutex.Contains(otherNode))
                                    {
                                        consistent = true;
                                        break;
                                    }
                                }
                                if(!consistent)
                                    count++;                                
                            }
                            if (count >= literalNode.IncomingEdges.Count)
                            {
                                literalNode.addMutex(otherLiteralNode);
                                otherLiteralNode.addMutex(literalNode);
                            }
                        }
                    }
                }

                //end of literal mutex time

                //check for a possible solution
                List<LiteralNode> goalList = new List<LiteralNode>();

                foreach(Literal literal in goal.SuccessConditions)
                {
                    foreach(LiteralNode literalNode in literalLayer)
                    {
                        if (literal.Equals(literalNode.Literal) && (literalNode.Mutex.Except(goalList).Count() == literalNode.Mutex.Count))
                            goalList.Add(literalNode);
                    }
                }

                //all the literals of the goal success conditions are present in the deepest level of the graph and not mutex
                if(goalList.Count >= goal.SuccessConditions.Count)
                {
                    findPlan(goalList);
                }
            }
        }

        public void findPlan(List<LiteralNode> goalList)
        {
            foreach(LiteralNode node in goalList)
            {

            }
        }

        public void birthdayDinnerExample()
        {
            goal = new Goal("birthdayDinner", new List<Literal>(new Literal[] { new Literal("garbage", false), new Literal("dinner", true), new Literal("present", true) }));

            Action cook = new Action("cook", new List<Literal>(new Literal[] { new Literal("clean", true) }), new List<Literal>(new Literal[] { new Literal("dinner", true) }));
            Action wrap = new Action("wrap", new List<Literal>(new Literal[] { new Literal("quiet", true) }), new List<Literal>(new Literal[] { new Literal("present", true) }));
            Action carry = new Action("carry", new List<Literal>(new Literal[] { new Literal("garbage", true) }), new List<Literal>(new Literal[] { new Literal("garbage", false), new Literal("clean", false) }));
            Action dolly = new Action("dolly", new List<Literal>(new Literal[] { new Literal("garbage", true) }), new List<Literal>(new Literal[] { new Literal("garbage", false), new Literal("quiet", false) }));
            
            actionList.Add(cook);
            actionList.Add(wrap);
            actionList.Add(carry);
            actionList.Add(dolly);

            initState = new List<Literal>(new Literal[] { new Literal("clean", true), new Literal("garbage", true), new Literal("quiet", true) });

            buildPlanningGraph();
        }
            
    }
}
