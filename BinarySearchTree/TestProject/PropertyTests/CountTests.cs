using System.Linq;
using BinarySearchTree.BinaryTree;
using NUnit.Framework;

namespace TestProject.PropertyTests
{
    [TestFixture]
    public class CountTests
    {
        [Test]
        public void CountWithEmptyTreeTest()
        {
            var tree = new BinaryTree<int, int>();
            Assert.AreEqual(0, tree.Count);
        }

        [Test]
        public void CountTest()
        {
            var tree = GetFilledTree(100);
            Assert.AreEqual(100, tree.Count);
        }

        [Test]
        public void ClearCountTest()
        {
            var tree = GetFilledTree(100);
            tree.Clear();
            Assert.AreEqual(0, tree.Count);
        }

        private BinaryTree<int, int> GetFilledTree(int count)
        {
            var tree = new BinaryTree<int, int>();
            foreach (var key in Enumerable.Range(0, count))
            {
                tree.Add(key, default);
            }
            return tree;
        }
    }
}
