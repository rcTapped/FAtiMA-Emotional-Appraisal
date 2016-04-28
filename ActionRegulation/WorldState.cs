using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRegulation
{
    class WorldState
    {
        public List<string> State { get; set; }

        public WorldState()
        {
            State = new List<string>();
        }

        public void addState(string state)
        {
            State.Add(state);
        }

        public void removeState(string state)
        {
            State.Remove(state);
        }

        public bool containsState(string state)
        {
            return State.Contains(state);
        }
    }
}
