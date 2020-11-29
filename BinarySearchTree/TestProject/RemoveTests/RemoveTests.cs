using System;
using System.Linq;
using BinarySearchTree.BinaryTree;
using NUnit.Framework;

namespace TestProject.RemoveTests
{
    [TestFixture]
    public class RemoveTests
    {
        private readonly Random _rnd = new Random();

        [Test]
        public void RemoveAllTest()
        {
            var tree = new BinaryTree<int, int>();
            var keys = Enumerable.Range(0, 100).ToList();
            foreach (var key in keys)
            {
                tree.Add(key, default);
            }
            foreach (var key in keys)
            {
                Assert.IsTrue(tree.Remove(key));
            }
        }

        [Test]
        public void RemoveKeyTest()
        {
            var tree = new BinaryTree<int, int>();
            var keys = Enumerable.Range(0, 100).ToList();
            foreach (var key in keys)
            {
                tree.Add(key, default);
            }
            var index = _rnd.Next(0, 101);
            Assert.IsTrue(tree.Remove(keys[index]));
            Assert.IsFalse(tree.ContainsKey(index));
        }

        [Test]
        public void RemoveKeyWhichIsNotPresent()
        {
            var tree = new BinaryTree<int, int>();
            Assert.IsFalse(tree.Remove(0));
        }
    }
}
