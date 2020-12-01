using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using BinarySearchTree.BinaryTree;
using NUnit.Framework;

namespace TestProject.SavingRestoringTests
{
    [TestFixture]
    public class SavingRestoringTests
    {
        private readonly string _projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        private const string CurrentDirectoryName = "SavingRestoringTests";

        [Test]
        public void SaveToNonExistsDirectoryTest()
        {
            var tree = new BinaryTree<int, int>();
            var path = _projectPath + _projectPath;
            var testDelegate = new TestDelegate(() => tree.SaveToFile(path));
            Assert.Throws<DirectoryNotFoundException>(testDelegate);
        }

        [Test]
        public void RestoreFromNonExistsDirectoryTest()
        {
            var tree = new BinaryTree<int, int>();
            var path = _projectPath + _projectPath;
            var testDelegate = new TestDelegate(() => tree.RestoreFromFile(path));
            Assert.Throws<DirectoryNotFoundException>(testDelegate);
        }

        [Test]
        public void RestoreFromNonExistsFileTest()
        {
            var tree = new BinaryTree<int, int>();
            var path = Path.Combine(_projectPath, Path.GetRandomFileName());
            var testDelegate = new TestDelegate(() => tree.RestoreFromFile(path));
            Assert.Throws<FileNotFoundException>(testDelegate);
        }

        [Test]
        public void RestoreFromEmptyFile()
        {
            var tree = new BinaryTree<int, int>();
            var fileName = Path.GetRandomFileName();
            var path = Path.Combine(_projectPath, CurrentDirectoryName, fileName);
            var testDelegate = new TestDelegate(() => tree.RestoreFromFile(path));
            using (var file = File.Create(path)) { }
            try
            {
                Assert.Throws<SerializationException>(testDelegate);
            }
            finally
            {
                File.Delete(path);
            }
        }

        [Test]
        public void SimpleSavingRestoringTest()
        {
            SavingRestoringTree(1);
        }

        [Test]
        public void SavingRestoringTest()
        {
            SavingRestoringTree(10);
        }

        [Test]
        public void BigSavingRestoringTest()
        {
            SavingRestoringTree(1000);
        }

        private void SavingRestoringTree(int count)
        {
            var filePath = Path.Combine(_projectPath, CurrentDirectoryName, Path.GetRandomFileName());
            var tree = GetFilledTree(count);
            var copyOfTree = new BinaryTree<int, int>();
            foreach (var item in tree)
            {
                copyOfTree.Add(item);
            }
            tree.SaveToFile(filePath);
            tree.Clear();
            tree.RestoreFromFile(filePath);
            File.Delete(filePath);
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
