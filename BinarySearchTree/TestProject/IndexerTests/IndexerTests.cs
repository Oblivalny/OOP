using System.Collections.Generic;
using System.Linq;
using BinarySearchTree.BinaryTree;
using NUnit.Framework;

namespace TestProject.IndexerTests
{
    [TestFixture]
    public class IndexerTests
    {
        [Test]
        public void GetTest()
        {
            var tree = new BinaryTree<int, int>();
            var listKeyValuePairs = Enumerable.Range(0, 100)
                                              .Select(key => new KeyValuePair<int, int>(key, default))
                                              .ToList();
            foreach (var keyValuePair in listKeyValuePairs)
            {
                tree.Add(keyValuePair);
            }
            foreach (var (key, value) in listKeyValuePairs)
            {
                Assert.AreEqual(value, tree[key]);
            }
        }

        [Test]
        public void GetWhenKeyIsMissingTest()
        {
            var tree = new BinaryTree<int, int>();
            var value = 0;
            var testDelegate = new TestDelegate(() => value = tree[0]);
            Assert.Throws<KeyNotFoundException>(testDelegate);
        }

        [Test]
        public void SetTest()
        {
            var tree = new BinaryTree<int, int>();
            var listKeyValuePairs = Enumerable.Range(0, 100)
                                              .Select(key => new KeyValuePair<int, int>(key, key))
                                              .ToList();
            var expected = listKeyValuePairs.Select(pair => new KeyValuePair<int, int>(pair.Key, default));
            foreach (var keyValuePair in listKeyValuePairs)
            {
                tree.Add(keyValuePair);
            }
            foreach (var (key, _) in listKeyValuePairs)
            {
                tree[key] = default;
            }
            Assert.AreEqual(expected, tree);
        }

        [Test]
        public void SetWhenKeyIsMissingTest()
        {
            var tree = new BinaryTree<int, int>();
            tree[0] = default;
            Assert.IsTrue(tree.ContainsKey(0));
        }
    }
}