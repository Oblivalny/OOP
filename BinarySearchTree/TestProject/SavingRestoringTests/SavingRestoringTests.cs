using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Runtime.Serialization;
using BinarySearchTree.BinaryTree;
using NUnit.Framework;

namespace TestProject.SavingRestoringTests
{
    [TestFixture]
    public class SavingRestoringTests
    {
        private const string FileName = "tree";

        [Test]
        public void SaveToNonExistsDirectoryTest()
        {
            var fileSystem = new MockFileSystem();
            var path = GetNonExistsDirectory(fileSystem);
            var tree = new BinaryTree<int, int>(fileSystem);
            var testDelegate = new TestDelegate(() => tree.SaveToFile(path));
            Assert.Throws<DirectoryNotFoundException>(testDelegate);
        }

        [Test]
        public void RestoreFromNonExistsDirectoryTest()
        {
            var fileSystem = new MockFileSystem();
            var path = GetNonExistsDirectory(fileSystem);
            var tree = new BinaryTree<int, int>();
            var testDelegate = new TestDelegate(() => tree.RestoreFromFile(path));
            Assert.Throws<DirectoryNotFoundException>(testDelegate);
        }

        private string GetNonExistsDirectory(IMockFileDataAccessor fileSystem)
        {
            var dir = fileSystem.AllDirectories.FirstOrDefault();
            var nonExistsDir = Path.GetRandomFileName();
            var path = Path.Combine(dir, nonExistsDir, FileName);
            return path;
        }

        [Test]
        public void RestoreFromNonExistsFileTest()
        {
            var fileSystem = new MockFileSystem();
            var dir = fileSystem.AllDirectories.FirstOrDefault();
            var path = Path.Combine(dir, FileName);
            var tree = new BinaryTree<int, int>(fileSystem);
            var testDelegate = new TestDelegate(() => tree.RestoreFromFile(path));
            Assert.Throws<FileNotFoundException>(testDelegate);
        }

        [Test]
        public void RestoreFromEmptyFile()
        {
            var fileSystem = new MockFileSystem();
            var dir = fileSystem.AllDirectories.FirstOrDefault();
            var path = Path.Combine(dir, FileName);
            fileSystem.AddFile(path, string.Empty);
            var tree = new BinaryTree<int, int>(fileSystem);
            var testDelegate = new TestDelegate(() => tree.RestoreFromFile(path));
            Assert.Throws<SerializationException>(testDelegate);
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
            var fileSystem = new MockFileSystem();
            var dir = fileSystem.AllDirectories.FirstOrDefault();
            var path = Path.Combine(dir, FileName);
            var tree = GetFilledTree(count, fileSystem);
            var copyOfTree = new BinaryTree<int, int>();
            foreach (var item in tree)
            {
                copyOfTree.Add(item);
            }
            tree.SaveToFile(path);
            tree.Clear();
            tree.RestoreFromFile(path);
            Assert.AreEqual(copyOfTree, tree);
        }

        private BinaryTree<int, int> GetFilledTree(int count, IFileSystem fileSystem)
        {
            var tree = new BinaryTree<int, int>(fileSystem);
            foreach (var key in Enumerable.Range(0, count))
            {
                tree.Add(key, default);
            }
            return tree;
        }
    }
}
