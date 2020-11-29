using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BinarySearchTree.BinaryTree;
using NUnit.Framework;

namespace TestProject.AddTests
{
    [TestFixture]
    public class AddTests
    {
        private readonly Random _rnd = new Random();

        private void TestAddKeyValuePairs<T>(IEnumerable<T> keys) where T : IComparable
        {
            var tree = new BinaryTree<T, int>();
            var listKeyValuePair = keys.Distinct()
                                       .Select(key => new KeyValuePair<T, int>(key, _rnd.Next()))
                                       .ToList();
            foreach (var keyValuePair in listKeyValuePair)
            {
                tree.Add(keyValuePair);
            }
            var result = tree.ToList();
            var expected = listKeyValuePair.OrderBy(keyValuePair => keyValuePair.Key).ToList();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void EmptyTest()
        {
            var keys = new List<int>();
            TestAddKeyValuePairs(keys);
        }

        [Test]
        public void IntegerAddTest()
        {
            var keys = new List<int>();
            for (var i = 0; i < 100; i++)
            {
                keys.Add(_rnd.Next(1, 1000));
            }
            TestAddKeyValuePairs(keys);
        }

        [Test]
        public void StringAddTest()
        {
            var keys = new List<string>();
            for (var i = 0; i < 100; i++)
            {
                var length = _rnd.Next(1, 10);
                var str = new StringBuilder();
                for (var k = 0; k < length; k++)
                {
                    str.Append((char) _rnd.Next(65, 123));
                }
                keys.Add(str.ToString());
            }
            TestAddKeyValuePairs(keys);
        }

        [Test]
        public void AlreadyAddedKeyTest()
        {
            var tree = new BinaryTree<int, int>
            {
                new KeyValuePair<int, int>(0, 0)
            };
            var testDelegate = new TestDelegate(() => tree.Add(0, 1));
            Assert.Throws<ArgumentException>(testDelegate);
        }

        [Test]
        public void AddOverloadTest()
        {
            var keyValuePairTree = new BinaryTree<int, int>();
            var keyValueTree = new BinaryTree<int, int>();
            var listKeyValuePair = Enumerable.Range(0, 100)
                                             .Select(key => new KeyValuePair<int, int>(key, _rnd.Next()))
                                             .ToList();
            foreach (var keyValuePair in listKeyValuePair)
            {
                keyValuePairTree.Add(keyValuePair);
                keyValueTree.Add(keyValuePair.Key, keyValuePair.Value);
            }
            const string message =
                "The results of the work of Add(KeyValuePair<TKey, TValue> keyValuePair) and Add(TKey key, TValue value) methods are different";
            Assert.AreEqual(keyValueTree.ToList(), keyValueTree.ToList(), message);
        }
    }
}
