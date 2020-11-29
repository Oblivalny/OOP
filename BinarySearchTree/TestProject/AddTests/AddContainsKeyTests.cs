using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BinarySearchTree.BinaryTree;
using NUnit.Framework;

namespace TestProject.AddTests
{
    [TestFixture]
    public class AddContainsKeyTests
    {
        private readonly Random _rnd = new Random();

        private void TestAddContainsKey<T>(IEnumerable<T> keys) where T : IComparable
        {
            var tree = new BinaryTree<T, int>();
            var listKeyValuePair = keys.Distinct()
                                       .Select(key => new KeyValuePair<T, int>(key, _rnd.Next()))
                                       .ToList();
            foreach (var keyValuePair in listKeyValuePair)
            {
                tree.Add(keyValuePair);
            }
            var expected = listKeyValuePair.Select(keyValuePair => keyValuePair.Key);
            foreach (var comparable in expected)
            {
                Assert.IsTrue(tree.ContainsKey(comparable));
            }
        }

        [Test]
        public void IntegerAddContainsKeyTest()
        {
            TestAddContainsKey(Enumerable.Range(0, 100));
        }

        [Test]
        public void StringAddContainsKeyTest()
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
            TestAddContainsKey(keys);
        }

        [Test]
        public void ContainsKeyWithEmptyTreeTest()
        {
            var tree = new BinaryTree<int, int>();
            Assert.IsFalse(tree.ContainsKey(default));
            var stringTree = new BinaryTree<string, string>();
            Assert.IsFalse(stringTree.ContainsKey(default));
        }

        [Test]
        public void ClearContainsKeyTest()
        {
            var tree = new BinaryTree<int, int>
            {
                {0, default},
                {1, default},
                {2, default}
            };
            tree.Clear();
            Assert.IsFalse(tree.ContainsKey(default));
        }
    }
}
