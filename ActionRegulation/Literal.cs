using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRegulation
{
    class Literal
    {
        public string Name { get; set; }
        public bool Value { get; set; }

        public Literal(string name, bool value)
        {
            Name = name;
            Value = value;
        }

        public override bool Equals(object obj)
        {
            Literal literal = obj as Literal;

            if (literal == null)
                return false;
            return (this.Name == literal.Name) && (this.Value == literal.Value);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }

    public class LiteralComparer : IEqualityComparer<Literal>
    {
        bool IEqualityComparer<Literal>.Equals(Literal x, Literal y)
        {
            return (x.Name.Equals(y.Name)) && (x.Value.Equals(y.Value));
        }

        int IEqualityComparer<Literal>.GetHashCode(Literal obj)
        {
            if (Object.ReferenceEquals(obj, null))
                return 0;
            return obj.Name.GetHashCode();
        }
    }
}
