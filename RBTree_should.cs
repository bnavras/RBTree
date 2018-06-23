using System.Linq;
using NUnit.Framework;

namespace RadBlackTree
{
    [TestFixture]
    public class RBTree_should
    {
        [Test]
        public void RootNodeIsBlack()
        {
            var value = 1;
            var tree = new RBTree();
            tree.Add(value);
            Assert.AreEqual(Color.Black, tree.Find(value).Color);
        }
        [Test]
        public void NewNodeIsRed()
        {
            int root = 1, notRoot = 2;
            var tree = new RBTree();
            tree.Add(root);
            tree.Add(notRoot);
            Assert.AreEqual(Color.Red, tree.Find(notRoot).Color);
        }
        [Test]
        public void InsertWorksCorrectly()
        {
            var tree = GetTree(5, 4, 6, 7, 1);
            var root = tree.Find(5);

            Assert.AreEqual(5, root.Value);
            Assert.AreEqual(4, root.LeftChild.Value);
            Assert.AreEqual(6, root.RightChild.Value);
            Assert.AreEqual(7, root.RightChild.RightChild.Value);
            Assert.AreEqual(1, root.LeftChild.LeftChild.Value);

            Assert.True(Color.Black == root.Color);
            Assert.True(Color.Black == root.RightChild.Color);
            Assert.True(Color.Black == root.LeftChild.Color);
            Assert.True(Color.Red == root.RightChild.RightChild.Color);
            Assert.True(Color.Red == root.LeftChild.LeftChild.Color);
        }

        [Test]
        public void TreeIsOrdered()
        {
            var values = new[] {2, 9, 4, 0, 10, 6, 30, 12, 15};
            var tree = GetTree(values);

            var valueFromTree = tree.GetValues().ToList();
            Assert.True(values.Length == valueFromTree.Count);
            Assert.That(valueFromTree, Is.Ordered);
        }

        [Test]
        public void FindWork()
        {
            var values = new[] { 2, 9, 0, 10, 8 };
            var tree = GetTree(values);

            foreach (var value in values)
                Assert.AreEqual(value, tree.Find(value).Value);
        }
        [Test]
        public void SimpleLeftRotateWorksCorrectly()
        {
            var tree = GetTree(1,2,3);
            var root = tree.Find(2);
            Assert.AreEqual(Color.Black, root.Color);
            Assert.True(1 == root.LeftChild.Value);
            Assert.True(3 == root.RightChild.Value);
        }
        [Test]
        public void SimpleRightRotateWorksCorrectly()
        {
            var tree = GetTree(3, 2, 1);
            var root = tree.Find(2);
            Assert.AreEqual(Color.Black, root.Color);
            Assert.True(1 == root.LeftChild.Value);
            Assert.True(3 == root.RightChild.Value);
        }

        [Test]
        public void RightRotateWorksCorrectly()
        {
            var tree = GetTree(50, 30, 80, 60, 90, 95, 85, 94);
            var root = tree.Find(80);
            Assert.AreEqual(Color.Black, root.Color);
            Assert.True(50 == root.LeftChild.Value);
            Assert.True(90 == root.RightChild.Value);
        }
        [Test]
        public void LeftRotateWorksCorrectly()
        {
            var tree = GetTree(50, 30, 75, 20, 35, 25, 19, 26);
            
            var root = tree.Find(30);
            Assert.AreEqual(Color.Black, root.Color);
            Assert.True(20 == root.LeftChild.Value);
            Assert.True(50 == root.RightChild.Value);
        }

        [Test]
        public void LeftRightRotatesWorksCorrectly()
        {
            var tree = GetTree(50, 30, 75, 20, 35, 34, 37, 38);
            var root = tree.Find(35);
            Assert.AreEqual(Color.Black, root.Color);
            Assert.True(30 == root.LeftChild.Value);
            Assert.True(50 == root.RightChild.Value);

            var oldRoot = tree.Find(50);
            Assert.True(oldRoot.LeftChild.Value == 37);
        }
        [Test]
        public void RightLeftRotatesWorksCorrectly()
        {
            var tree = GetTree(50, 30, 75, 90, 65, 60, 70, 71);
            var root = tree.Find(65);
            Assert.AreEqual(Color.Black, root.Color);
            Assert.AreEqual(50, root.LeftChild.Value);
            Assert.AreEqual(75, root.RightChild.Value);

            var oldRoot = tree.Find(50);
            Assert.AreEqual(60, oldRoot.RightChild.Value);
        }

        private RBTree GetTree(params int[] values)
        {
            var tree = new RBTree();
            foreach (var value in values)
                tree.Add(value);
            return tree;
        }
    }
}