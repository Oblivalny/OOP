using System;
using System.Collections.Generic;

namespace BinarySearchTree.BinaryTree
{
    [Serializable]
    public class TreeNode<TKey, TValue>
    {
        public KeyValuePair<TKey, TValue> KeyValuePair { get; set; }
        public TreeNode<TKey, TValue> Left, Right;

        public TreeNode(KeyValuePair<TKey, TValue> keyValuePair)
        {
            KeyValuePair = keyValuePair;
        }
    }
}