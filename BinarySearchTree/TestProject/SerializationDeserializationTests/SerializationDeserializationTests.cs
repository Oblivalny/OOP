using System.IO;
using System.Linq;
using BinarySearchTree.BinaryTree;
using NUnit.Framework;

namespace TestProject.SerializationDeserializationTests
{
    [TestFixture]
    public class SerializationDeserializationTests
    {
        [Test]
        public void NotAnEmptyStreamTest()
        {
            var memoryStream = new MemoryStream();
            var tree = new BinaryTree<int, int>
            {
                {0, default},
                {1, default},
                {2, default},
            };
            tree.Serialize(memoryStream);
            Assert.IsTrue(memoryStream.Length > 0);
        }

        [Test]
        public void SimpleSerializeDeserializeTest()
        {
            SerializeDeserializeTree(1);
        }

        [Test]
        public void SerializeDeserializeTest()
        {
            SerializeDeserializeTree(10);
        }

        [Test]
        public void BigSerializeDeserializeTest()
        {
            SerializeDeserializeTree(1000);
        }

        private void SerializeDeserializeTree(int count)
        {
            var tree = GetFilledTree(count);
            var copyOfTree = new BinaryTree<int, int>();
            var memoryStream = new MemoryStream();
            foreach (var item in tree)
            {
                copyOfTree.Add(item);
            }
            tree.Serialize(memoryStream);
            tree.Clear();
            memoryStream.Seek(0, SeekOrigin.Begin);
            tree.Deserialize(memoryStream);
            Assert.AreEqual(copyOfTree, tree);
        }

        private BinaryTree<int, int> GetFilledTree(int count)
        {
            var tree = new BinaryTree<int, int>();
            foreach (var key in Enumerable.Range(0, count))
            {
                tree.Add(key, default);
            }
            return tree;
        }
    }
}
