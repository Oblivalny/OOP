using System.Collections.Generic;
using System.Linq;
using BinarySearchTree.BinaryTree;
using NUnit.Framework;

namespace TestProject.PropertyTests
{
    [TestFixture]
    public class KeysTests
    {
        [Test]
        public void KeysWithEmptyTreeTest()
        {
            var tree = new BinaryTree<int, int>();
            Assert.AreEqual(new List<int>(), tree.Keys);
        }

        [Test]
        public void KeysTest()
        {
            var keys = Enumerable.Range(0, 100);
            var tree = GetFilledTree(keys);
            Assert.AreEqual(keys, tree.Keys);
        }

        [Test]
        public void KeysClearTest()
        {
            var keys = Enumerable.Range(0, 100);
            var tree = GetFilledTree(keys);
            tree.Clear();
            Assert.AreEqual(new List<int>(), tree.Keys);
        }

        private BinaryTree<int, int> GetFilledTree(IEnumerable<int> keys)
        {
            var tree = new BinaryTree<int, int>();
            foreach (var key in keys)
            {
                tree.Add(key, default);
            }
            return tree;
        }
    }
}
