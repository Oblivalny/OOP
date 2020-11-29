using System.Collections.Generic;
using System.Linq;
using BinarySearchTree.BinaryTree;
using NUnit.Framework;

namespace TestProject.IEnumerableTests
{
    [TestFixture]
    public class TestIEnumerable
    {
        [Test]
        public void EmptyTreeTest()
        {
            var tree = new BinaryTree<int, int>();
            using var enumerator = tree.GetEnumerator();
            Assert.IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void EnumerableTest()
        {
            var tree = new BinaryTree<int, int>();
            var listKeyValuePairs = Enumerable.Range(0, 100)
                                              .Select(key => new KeyValuePair<int, int>(key, default));
            foreach (var keyValuePair in listKeyValuePairs)
            {
                tree.Add(keyValuePair);
            }
            Assert.AreEqual(listKeyValuePairs, tree);
        }
    }
}