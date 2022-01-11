using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.Core.BehavioralPatterns.Iterator
{
    public class Node<T>
    {
        public T Value;
        public Node<T> Left, Right;
        public Node<T> Parent;

        public Node(T value)
        {
            Value = value;
        }

        public Node(T value, Node<T> left, Node<T> right)
        {
            Value = value;
            Left = left;
            Right = right;

            left.Parent = right.Parent = this;
        }
    }

    public class InOrderIterator<T>
    {
        public Node<T> Current { get; set; }
        private readonly Node<T> _root;
        private bool _yieldedStart;

        public InOrderIterator(Node<T> root)
        {
            this._root = Current = root;
            while (Current.Left != null)
                Current = Current.Left;
        }

        public void Reset()
        {
            Current = _root;
            _yieldedStart = true;
        }

        public bool MoveNext()
        {
            if (!_yieldedStart)
            {
                _yieldedStart = true;
                return true;
            }

            if (Current.Right != null)
            {
                Current = Current.Right;
                while (Current.Left != null)
                    Current = Current.Left;
                return true;
            }
            else
            {
                var p = Current.Parent;
                while (p != null && Current == p.Right)
                {
                    Current = p;
                    p = p.Parent;
                }
                Current = p;
                return Current != null;
            }
        }
    }

    public class BinaryTree<T>
    {
        private readonly Node<T> _root;

        public BinaryTree(Node<T> root)
        {
            this._root = root;
        }

        public InOrderIterator<T> GetEnumerator()
        {
            return new InOrderIterator<T>(_root);
        }

        public IEnumerable<Node<T>> NaturalInOrder
        {
            get
            {
                IEnumerable<Node<T>> TraverseInOrder(Node<T> current)
                {
                    if (current.Left != null)
                    {
                        foreach (var left in TraverseInOrder(current.Left))
                            yield return left;
                    }
                    yield return current;
                    if (current.Right != null)
                    {
                        foreach (var right in TraverseInOrder(current.Right))
                            yield return right;
                    }
                }
                foreach (var node in TraverseInOrder(_root))
                    yield return node;
            }
        }
    }

    public class Demo
    {
        public static void Main()
        {
            //   1
            //  / \
            // 2   3

            // in-order:  213
            // preorder:  123
            // postorder: 231

            var root = new Node<int>(1,
              new Node<int>(2), new Node<int>(3));

            // C++ style
            var it = new InOrderIterator<int>(root);

            while (it.MoveNext())
            {
                Console.Write(it.Current.Value);
                Console.Write(',');
            }

            Console.WriteLine();

            // C# style
            var tree = new BinaryTree<int>(root);

            Console.WriteLine(string.Join(",", tree.NaturalInOrder.Select(x => x.Value)));

            // duck typing!
            foreach (var node in tree)
                Console.WriteLine(node.Value);
        }
    }
}
