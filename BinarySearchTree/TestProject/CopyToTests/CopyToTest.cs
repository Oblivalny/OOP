using System;
using System.Collections.Generic;
using System.Linq;
using BinarySearchTree.BinaryTree;
using NUnit.Framework;

namespace TestProject.CopyToTests
{
    [TestFixture]
    public class CopyToTest
    {
        [Test]
        public void CopyToShortArrayTest()
        {
            var tree = new BinaryTree<int, int>
            {
                {0, default},
                {1, default},
                {2, default}
            };
            var array = new KeyValuePair<int, int>[2];
            var testDelegate = new TestDelegate(() => tree.CopyTo(array, 0));
            Assert.Throws<ArgumentException>(testDelegate);
        }

        [Test]
        public void CopyToNotBeginningOfArrayTest()
        {
            var tree = new BinaryTree<int, int>
            {
                {0, default},
                {1, default},
                {2, default}
            };
            var array = new KeyValuePair<int, int>[3];
            var testDelegate = new TestDelegate(() => tree.CopyTo(array, 1));
            Assert.Throws<ArgumentException>(testDelegate);
        }

        [Test]
        public void CopyToStartOfArrayTest()
        {
            var tree = new BinaryTree<int, int>
            {
                {0, default},
                {1, default},
                {2, default}
            };
            var array = new KeyValuePair<int, int>[3];
            tree.CopyTo(array, 0);
            Assert.AreEqual(tree, array);
        }

        [Test]
        public void CopyToArrayTest()
        {
            var tree = new BinaryTree<int, int>
            {
                {0, default},
                {1, default},
                {2, default}
            };
            var array = new KeyValuePair<int, int>[6];
            tree.CopyTo(array, 2);
            Assert.AreEqual(tree, array.Skip(2).Take(tree.Count));
        }
    }
}