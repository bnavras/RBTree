using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RadBlackTree;

namespace RadBlackTree
{
    public class Node : IComparable<Node>
    {
        public int Value { get; set; }
        public Node RightChild { get; set; }
        public Node LeftChild { get; set; }
        public Color Color { get; set; }
        public Node(int value, Color color) { Value = value; Color = color; }
        public int CompareTo(Node node)
        {
            return Value.CompareTo(node.Value);
        }
    }
    public enum Color { Red, Black }
}
