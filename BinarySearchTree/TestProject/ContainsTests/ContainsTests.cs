using System.Collections.Generic;
using System.Linq;
using BinarySearchTree.BinaryTree;
using NUnit.Framework;

namespace TestProject.ContainsTests
{
    [TestFixture]
    public class ContainsTests
    {
        [Test]
        public void ContainsWithEmptyTreeTest()
        {
            var tree = new BinaryTree<int, int>();
            Assert.IsFalse(tree.Contains(new KeyValuePair<int, int>(0, 0)));
        }

        [Test]
        public void ContainsTest()
        {
            var tree = new BinaryTree<int, int>();
            var keys = Enumerable.Range(0, 100).ToList();
            foreach (var key in keys)
            {
                tree.Add(key, default);
            }
            foreach (var key in keys)
            {
                Assert.IsTrue(tree.Contains(new KeyValuePair<int, int>(key, default)));
            }
        }

        [Test]
        public void NotContainsTest()
        {
            var tree = new BinaryTree<int, int>();
            var keys = Enumerable.Range(1, 100).ToList();
            foreach (var key in keys)
            {
                tree.Add(key, default);
            }
            foreach (var key in keys)
            {
                Assert.IsFalse(tree.Contains(new KeyValuePair<int, int>(0, key)));
            }
        }
    }
}