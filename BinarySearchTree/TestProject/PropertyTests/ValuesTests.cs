using System.Collections.Generic;
using System.Linq;
using BinarySearchTree.BinaryTree;
using NUnit.Framework;

namespace TestProject.PropertyTests
{
    [TestFixture]
    public class ValuesTests
    {
        [Test]
        public void ValuesWithEmptyTreeTest()
        {
            var tree = new BinaryTree<int, int>();
            Assert.AreEqual(new List<int>(), tree.Values);
        }

        [Test]
        public void ValuesTest()
        {
            var values = Enumerable.Range(0, 100);
            var tree = GetFilledTree(values);
            Assert.AreEqual(values, tree.Values);
        }

        [Test]
        public void ValuesClearTest()
        {
            var values = Enumerable.Range(0, 100);
            var tree = GetFilledTree(values);
            tree.Clear();
            Assert.AreEqual(new List<int>(), tree.Values);
        }

        private BinaryTree<int, int> GetFilledTree(IEnumerable<int> values)
        {
            var tree = new BinaryTree<int, int>();
            var keys = values.Distinct()
                             .Select(item => new KeyValuePair<int, int>(item, item));
            foreach (var keyValuePair in keys)
            {
                tree.Add(keyValuePair);
            }
            return tree;
        }
    }
}
