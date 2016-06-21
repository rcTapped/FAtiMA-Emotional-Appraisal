using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRegulation
{
    class Edge
    {
        public Action Action { get; set; }
        public Node From { get; set; }
        public Node To { get; set; }

        public Edge(Action action)
        {
            Action = action;
            From = null;
            To = null;
        }

        public Edge(Action action, Node from, Node to)
        {
            Action = action;
            From = from;
            To = to;
        }
    }
}
