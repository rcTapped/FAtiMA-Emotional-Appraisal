using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRegulation
{
    class Edge
    {
        public PlanningGraphNode From { get; set; }
        public PlanningGraphNode To { get; set; }

        public Edge()
        {
            From = null;
            To = null;
        }

        public Edge(PlanningGraphNode from, PlanningGraphNode to)
        {
            From = from;
            To = to;
        }
    }

    //class ActionEdge : Edge
    //{
    //    public Action Action { get; set; }

    //    public ActionEdge(Action action) : base()
    //    {
    //        Action = action;
    //        From = null;
    //        To = null;
    //    }

    //    public ActionEdge(Action action, Node from, Node to) : base(from, to)
    //    {
    //        Action = action;
    //        From = from;
    //        To = to;
    //    }
    //}
}
