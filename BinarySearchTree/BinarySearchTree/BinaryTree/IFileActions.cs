using System;
using System.Collections.Generic;
using System.Text;

namespace BinarySearchTree.BinaryTree
{
    public interface IFileActions
    {
        void SaveToFile(string path);
        void RestoreFromFile(string path);
    }
}