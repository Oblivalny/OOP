using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BinarySearchTree.BinaryTree;
using NUnit.Framework;

namespace TestProject.AddTests
{
    [TestFixture]
    public class AddTryGetValueTests
    {
        private readonly Random _rnd = new Random();

        private void TestAddTryGetValue<T>(IEnumerable<T> keys) where T : IComparable
        {
            var tree = new BinaryTree<T, int>();
            var listKeyValuePair = keys.Distinct()
                                       .Select(key => new KeyValuePair<T, int>(key, _rnd.Next()))
                                       .ToList();
            foreach (var keyValuePair in listKeyValuePair)
            {
                tree.Add(keyValuePair);
            }

            foreach (var (key, value) in listKeyValuePair)
            {
                Assert.IsTrue(tree.TryGetValue(key, out var val));
                Assert.AreEqual(value, val);
            }
        }

        [Test]
        public void AddIntTryGetValueTest()
        {
            TestAddTryGetValue(Enumerable.Range(0, 100).Select(item => _rnd.Next(10000)));
        }

        [Test]
        public void AddStringTryGetValueTest()
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
            TestAddTryGetValue(keys);
        }

        [Test]
        public void TryGetValueWithEmptyTreeTest()
        {
            var tree = new BinaryTree<int, int>();
            Assert.IsFalse(tree.TryGetValue(0, out _));
        }
    }
}