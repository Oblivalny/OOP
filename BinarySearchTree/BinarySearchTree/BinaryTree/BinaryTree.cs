using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace BinarySearchTree.BinaryTree
{
    public class BinaryTree<TKey, TValue> : IDictionary<TKey, TValue>
        where TKey : IComparable
    {
        private TreeNode<TKey, TValue> _root;
        public int Count { get; private set; }
        public bool IsReadOnly => false;
        public ICollection<TKey> Keys => this.Select(node => node.Key).ToList();
        public ICollection<TValue> Values => this.Select(node => node.Value).ToList();

        public BinaryTree()
        {
            Count = 0;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Count++;
            if (_root == null)
            {
                _root = new TreeNode<TKey, TValue>(item);
                return;
            }
            var node = new TreeNode<TKey, TValue>(item);
            var parent = GetParentCurrentNode(item.Key, null);
            if (item.Key.CompareTo(parent.KeyValuePair.Key) < 0)
            {
                parent.Left = node;
            }
            else if (item.Key.CompareTo(parent.KeyValuePair.Key) > 0)
            {
                parent.Right = node;
            }
            else
            {
                Count--;
                throw new ArgumentException($"An item with the same key has already been added. Key: {item.Key}");
            }
        }

        public void Clear()
        {
            Count = 0;
            _root = null;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            var node = Search(item.Key);
            return node != null && node.KeyValuePair.Value.Equals(item.Value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array.Length < arrayIndex + Count)
            {
                throw new ArgumentException(
                    "The length of the current array is not enough to copy the elements of the collection!");
            }
            foreach (var keyValuePair in this)
            {
                array[arrayIndex] = keyValuePair;
                arrayIndex++;
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public void Add(TKey key, TValue value)
        {
            var keyValuePair = new KeyValuePair<TKey, TValue>(key, value);
            Add(keyValuePair);
        }

        public bool ContainsKey(TKey key)
        {
            var node = Search(key);
            return node != null;
        }

        public bool Remove(TKey key)
        {
            var node = Search(key);
            if (node == null)
            {
                return false;
            }
            Count--;
            var countChildren = GetCountChildren(node);
            var parent = GetParentCurrentNode(node.KeyValuePair.Key, node);
            switch (countChildren)
            {
                case 0:
                    DeletingWithoutChildren(node, parent);
                    break;
                case 1:
                    DeletingWithChild(node, parent);
                    break;
                case 2:
                    DeletingWithChildren(node);
                    break;
            }
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var node = Search(key);
            value = default;
            if (node != null)
            {
                value = node.KeyValuePair.Value;
                return true;
            }
            return false;
        }

        public void Serialize(Stream serializationStream)
        {
            if (_root == null)
            {
                return;
            }
            var formatter = new BinaryFormatter();
            formatter.Serialize(serializationStream, _root);
        }

        public void Deserialize(Stream serializationStream)
        {
            var formatter = new BinaryFormatter();
            var obj = formatter.Deserialize(serializationStream);
            if (obj is TreeNode<TKey, TValue> root)
            {
                _root = root;
                Count = Keys.Count;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                var node = Search(key);
                if (node == null)
                {
                    throw new KeyNotFoundException($"The given key '{key}' was not present in the BinaryTree.");
                }
                return node.KeyValuePair.Value;
            }
            set
            {
                var node = Search(key);
                if (node == null)
                {
                    Add(key, value);
                }
                else
                {
                    node.KeyValuePair = new KeyValuePair<TKey, TValue>(key, value);
                }
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return GetItemsRecursively(_root)
                  .Select(node => node.KeyValuePair)
                  .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private TreeNode<TKey, TValue> GetParentCurrentNode(TKey key, TreeNode<TKey, TValue> node)
        {
            var parent = _root;
            while (true)
            {
                var compareTo = key.CompareTo(parent.KeyValuePair.Key);
                if (compareTo < 0 && parent.Left != node)
                {
                    parent = parent.Left;
                }
                else if (compareTo > 0 && parent.Right != node)
                {
                    parent = parent.Right;
                }
                else
                {
                    break;
                }
            }
            return parent;
        }

        private int GetCountChildren(TreeNode<TKey, TValue> node)
        {
            var count = node.Left == null ? 0 : 1;
            if (node.Right != null)
            {
                count++;
            }
            return count;
        }

        private void DeletingWithChildren(TreeNode<TKey, TValue> node)
        {
            var current = node.Right;
            while (current.Left != null)
            {
                current = current.Left;
            }
            var parent = GetParentCurrentNode(current.KeyValuePair.Key, current);
            (current.KeyValuePair, node.KeyValuePair) = (node.KeyValuePair, current.KeyValuePair);
            var countChildren = GetCountChildren(current);
            if (countChildren == 0)
            {
                DeletingWithoutChildren(current, parent);
            }
            else
            {
                DeletingWithoutChildren(current, parent);
            }
        }

        private void DeletingWithChild(TreeNode<TKey, TValue> node, TreeNode<TKey, TValue> parent)
        {
            if (node == _root)
            {
                if (node.Left == null)
                {
                    _root = node.Right;
                    node.Right = null;
                }
                else
                {
                    _root = node.Left;
                    node.Left = null;
                }
            }
            else
            {
                Replace(node, parent, node.Right);
            }
        }

        private void DeletingWithoutChildren(TreeNode<TKey, TValue> node, TreeNode<TKey, TValue> parent)
        {
            if (node == _root)
            {
                _root = null;
            }
            else
            {
                Replace(node, parent, null);
            }
        }

        private void Replace(TreeNode<TKey, TValue> current, TreeNode<TKey, TValue> parent, TreeNode<TKey, TValue> node)
        {
            if (parent.Left == current)
            {
                parent.Left = node;
            }
            else
            {
                parent.Right = node;
            }
        }

        private IEnumerable<TreeNode<TKey, TValue>> GetItemsRecursively(TreeNode<TKey, TValue> current)
        {
            if (current?.Left != null)
            {
                foreach (var node in GetItemsRecursively(current.Left))
                {
                    yield return node;
                }
            }
            if (current != null)
            {
                yield return current;
            }
            if (current?.Right != null)
            {
                foreach (var node in GetItemsRecursively(current.Right))
                {
                    yield return node;
                }
            }
        }

        private TreeNode<TKey, TValue> Search(TKey key)
        {
            var current = _root;
            while (current != null)
            {
                if (key.CompareTo(current.KeyValuePair.Key) == 0)
                {
                    return current;
                }
                current = key.CompareTo(current.KeyValuePair.Key) < 0
                              ? current.Left
                              : current.Right;
            }
            return null;
        }
    }
}
