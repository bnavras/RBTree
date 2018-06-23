using System;
using System.Collections.Generic;
using System.Linq;

namespace RadBlackTree
{
    public class RBTree
    {
        private Node root;
        public void Add(int value)
        {
            if (root == null) root = new Node(value, Color.Black);
            else InsertNode(null, null, root, root, new Node(value, Color.Red));
        }
        public IEnumerable<int> GetValues()
        {
            return GetValues(root).Select(node => node.Value);
        }
        public Node Find(int value)
        {
            return Find(root, value);
        }
        public bool Remove(int value)
        {
            Node currentNode = root, parentNode = root;
            while (currentNode?.Value != value)
            {
                if (currentNode == null) return false;
                parentNode = currentNode;
                currentNode = currentNode.Value > value ? currentNode.LeftChild : currentNode.RightChild;
            }
            var currentIsLeft = parentNode.LeftChild == currentNode;
            var currentIsRoot = root == currentNode;
            if (currentNode.LeftChild == null && currentNode.RightChild == null)
            {
                if (currentIsRoot) root = null;
                else if (currentIsLeft) parentNode.LeftChild = null;
                else parentNode.RightChild = null;
            }
            else if (currentNode.LeftChild == null)
            {
                if (currentIsRoot) root = currentNode.RightChild;
                if (currentIsLeft) parentNode.LeftChild = currentNode.RightChild;
                else parentNode.RightChild = currentNode.RightChild;
            }
            else if (currentNode.RightChild == null)
            {
                if (currentIsRoot) root = currentNode.LeftChild;
                if (currentIsLeft) parentNode.LeftChild = currentNode.LeftChild;
                else parentNode.RightChild = currentNode.LeftChild;
            }
            else
            {
                var successor = GetSuccessor(currentNode.RightChild);
                if (currentNode.RightChild.LeftChild != null)
                    successor.RightChild = currentNode.RightChild;
                successor.LeftChild = currentNode.LeftChild;
                if (currentIsRoot) root = successor;
                if (currentIsLeft) parentNode.LeftChild = successor;
                else parentNode.RightChild = successor;
            }
            throw new NotImplementedException();
            //return true;
        }
        private void LeftRotate(Node parent, Node topNode)
        {
            var newTopNode = topNode.RightChild;
            if (topNode == parent.LeftChild) parent.LeftChild = topNode.RightChild;
            else if (topNode == root) root = topNode.RightChild;
            else parent.RightChild = topNode.RightChild;

            topNode.RightChild = topNode.RightChild?.LeftChild;
            newTopNode.LeftChild = topNode;
        }
        private void RightRotate(Node parent, Node topNode)
        {
            var newTopNode = topNode.LeftChild;
            if (topNode == parent.LeftChild) parent.LeftChild = topNode.LeftChild;
            else if (topNode == root) root = topNode.LeftChild;
            else parent.RightChild = topNode.LeftChild;

            topNode.LeftChild = topNode.LeftChild.RightChild;
            newTopNode.RightChild = topNode;
        }
        //TODO: refactor
        private void InsertNode(Node grandGranGrandparent, Node granGrandparent,
                                 Node grandparent, Node parentNode, Node newNode)
        {
            if (parentNode.Color == Color.Black &&
                parentNode.LeftChild?.Color == Color.Red &&
                parentNode.RightChild?.Color == Color.Red)
            {
                parentNode.LeftChild.Color = Color.Black;
                parentNode.RightChild.Color = Color.Black;
                if (root != parentNode) parentNode.Color = Color.Red;
                if (parentNode.Color == Color.Red && grandparent.Color == Color.Red)
                {
                    if (granGrandparent.LeftChild == grandparent && grandparent.LeftChild == parentNode)
                    {
                        granGrandparent.Color = granGrandparent.Color == Color.Red ? Color.Black : Color.Red;
                        grandparent.Color = grandparent.Color == Color.Red ? Color.Black : Color.Red;
                        RightRotate(grandGranGrandparent, granGrandparent);
                    }
                    else if (granGrandparent.RightChild == grandparent && grandparent.RightChild == parentNode)
                    {
                        granGrandparent.Color = granGrandparent.Color == Color.Red ? Color.Black : Color.Red;
                        grandparent.Color = grandparent.Color == Color.Red ? Color.Black : Color.Red;
                        LeftRotate(grandGranGrandparent, granGrandparent);
                    }
                    else
                    {
                        granGrandparent.Color = granGrandparent.Color == Color.Red ? Color.Black : Color.Red;
                        parentNode.Color = parentNode.Color == Color.Red ? Color.Black : Color.Red;
                        if (grandparent.LeftChild == parentNode)
                        {
                            RightRotate(granGrandparent, grandparent);
                            LeftRotate(grandGranGrandparent, granGrandparent);
                        }
                        else
                        {
                            LeftRotate(granGrandparent, grandparent);
                            RightRotate(grandGranGrandparent, granGrandparent);
                        }
                    }
                }
            }

            if (newNode.CompareTo(parentNode) < 0)
            {

                if (parentNode.LeftChild == null)
                {
                    parentNode.LeftChild = newNode;
                    if (parentNode.Color == Color.Red && grandparent.LeftChild == parentNode)
                    {
                        grandparent.Color = grandparent.Color == Color.Red ? Color.Black : Color.Red;
                        parentNode.Color = parentNode.Color == Color.Red ? Color.Black : Color.Red;
                        RightRotate(granGrandparent, grandparent);
                    }
                    else if (parentNode.Color == Color.Red)
                    {
                        grandparent.Color = grandparent.Color == Color.Red ? Color.Black : Color.Red;
                        newNode.Color = parentNode.Color == Color.Red ? Color.Black : Color.Red;
                        RightRotate(grandparent, parentNode);
                        LeftRotate(granGrandparent, grandparent);
                    }
                }
                else InsertNode(granGrandparent, grandparent, parentNode, parentNode.LeftChild, newNode);
            }
            else
            {
                if (parentNode.RightChild == null)
                {
                    parentNode.RightChild = newNode;
                    if (parentNode.Color == Color.Red && grandparent.RightChild == parentNode)
                    {
                        grandparent.Color = grandparent.Color == Color.Red ? Color.Black : Color.Red;
                        parentNode.Color = parentNode.Color == Color.Red ? Color.Black : Color.Red;
                        LeftRotate(granGrandparent, grandparent); 
                    }
                    else if (parentNode.Color == Color.Red)
                    {
                        grandparent.Color = grandparent.Color == Color.Red ? Color.Black : Color.Red;
                        newNode.Color = parentNode.Color == Color.Red ? Color.Black : Color.Red;
                        LeftRotate(grandparent, parentNode);
                        RightRotate(granGrandparent, grandparent);
                    }
                }
                else InsertNode(granGrandparent, grandparent, parentNode, parentNode.RightChild, newNode);
            }
        }
        private Node GetSuccessor(Node parent)
        {
            var successor = parent;
            while (successor.LeftChild != null)
            {
                parent = successor;
                successor = successor.LeftChild;
            }
            if (successor == parent.LeftChild)
                parent.LeftChild = successor.RightChild;
            return successor;
        }
        private Node Find(Node node, int value)
        {
            if (node == null) return null;
            if (node.Value == value) return node;
            return node.Value > value ? Find(node.LeftChild, value) : Find(node.RightChild, value);
        }
        private IEnumerable<Node> GetValues(Node parentNode)
        {
            if (parentNode?.LeftChild != null)
                foreach (var value in GetValues(parentNode.LeftChild))
                    yield return value;

            if (parentNode == null) yield break;
            yield return parentNode;

            if (parentNode.RightChild != null)
                foreach (var value in GetValues(parentNode.RightChild))
                    yield return value;
        }
    }
}