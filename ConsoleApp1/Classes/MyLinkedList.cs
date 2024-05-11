using System.Collections;

public interface INode<K>
{
    K Value { get; set; }
}

public class MyLinkedList<T> : IEnumerable<T>
{
    private class Node<K> : INode<K>
    {
        #region Properties
        public K Value { get; set; }
        public Node<K> Next { get; set; }
        public Node<K> Prev { get; set; }
        #endregion

        #region Methods - Constructors
        public Node()
        {
            Value = default;
            Next = null;
            Prev = null;
        }

        public Node(K value)
        {
            Value = value;
            Next = null;
            Prev = null;
        }

        public Node(K value, Node<K> prev, Node<K> next)
        {
            Value = value;
            Prev = prev;
            Next = next;
        }
        #endregion
    }

    public class MyLinkedListEnumerator : IEnumerator<T>
    {
        private MyLinkedList<T> _myLinkedList;
        private Node<T> _currentNode;
        private int index = 0;

        public T Current
        {
            get
            {
                if (_currentNode != null && _currentNode.Value != null)
                {
                    return _currentNode.Value;
                }

                return default(T);
            }
        }

        object IEnumerator.Current { get { return Current; } }

        public MyLinkedListEnumerator(MyLinkedList<T> myLinkedList)
        {
            _myLinkedList = myLinkedList;
            _currentNode = _myLinkedList._head;
        }

        public bool MoveNext()
        {
            if (index == 0 && _currentNode != null)
            {
                index++;

                return true;
            }

            _currentNode = _currentNode.Next;
            if (_currentNode == null)
            {
                return false;
            }

            index++;

            return true;
        }

        public void Reset()
        {
            _currentNode = _myLinkedList._head;
            index = 0;
        }

        public void Dispose()
        {

        }
    }

    #region Properties
    public int Count { get { return _nodeCount; } }
    #endregion

    #region Variables
    private int _nodeCount = 0;
    private Node<T> _head = null;
    private Node<T> _tail = null;
    #endregion

    #region Methods - Constructors
    public MyLinkedList()
    {
        _head = null;
        _tail = null;
    }
    #endregion

    #region Methods - IEnumerable
    public IEnumerator<T> GetEnumerator()
    {
        return new MyLinkedListEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerable<T> GetEnumeratorReverse()
    {
        Node<T> node = _tail;
        while (node != null)
        {
            yield return node.Value;
            node = node.Prev;
        }
    }
    #endregion

    #region Methods - private
    private Node<T> GetNode(int index)
    {
        if (index < 0 || index >= _nodeCount)
        {
            throw new IndexOutOfRangeException($"Index: {index}, node count: {_nodeCount}");
        }

        int counter = 0;
        Node<T> node = _head;

        while (node != null && counter < index)
        {
            node = node.Next;
            counter++;
        }

        if (node == null)
        {
            throw new IndexOutOfRangeException($"Linkedlist is potentially broken. Unable to get node at index({index}), node count: {_nodeCount}, stopped at index: {counter}");
        }

        return node;
    }

    private Node<T> GetNodeReverse(int reverseIndex)
    {
        if (reverseIndex < 0 || reverseIndex >= _nodeCount)
        {
            throw new IndexOutOfRangeException($"Index: {reverseIndex}, node count: {_nodeCount}");
        }

        int counter = 0;
        Node<T> node = _tail;

        while (node != null && counter < reverseIndex)
        {
            node = node.Prev;
            counter++;
        }

        if (node == null)
        {
            throw new IndexOutOfRangeException($"Linkedlist is potentially broken. Unable to get node at reverse index({reverseIndex}), node count: {_nodeCount}, stopped at index: {counter}");
        }

        return node;
    }

    private void InsertNode(Node<T> newNode, Node<T> currNode)
    {
        Node<T> prevNode = currNode.Prev;

        if (prevNode != null)
        {
            prevNode.Next = newNode;
            newNode.Prev = prevNode;
            newNode.Next = currNode;
            currNode.Prev = newNode;
        }
        else
        {
            _head = newNode;
            newNode.Next = currNode;
            currNode.Prev = newNode;
        }

        _nodeCount++;
    }

    private void InsertNodeReverse(Node<T> newNode, Node<T> currNode)
    {
        Node<T> nextNode = currNode.Next;

        if (nextNode != null)
        {
            nextNode.Prev = newNode;
            newNode.Next = nextNode;
            nextNode.Prev = currNode;
            currNode.Next = newNode;
        }
        else
        {
            _tail = newNode;
            newNode.Prev = currNode;
            currNode.Next = newNode;
        }

        _nodeCount++;
    }


    private void RemoveNode(Node<T> node)
    {
        Node<T> prevNode = node.Prev;
        Node<T> nextNode = node.Next;

        if (prevNode == null && nextNode == null)
        {
            _head = null;
            _tail = null;
        }
        else if (prevNode == null && nextNode != null)
        {
            _head = nextNode;
            nextNode.Prev = null;
        }
        else if (prevNode != null && nextNode == null)
        {
            _tail = prevNode;
            prevNode.Next = null;
        }
        else if (prevNode != null && nextNode != null)
        {
            prevNode.Next = nextNode;
            nextNode.Prev = prevNode;
        }

        ClearNode(node);

        _nodeCount--;
    }

    private void ClearNode(Node<T> node)
    {
        node.Value = default(T);
        node.Prev = null;
        node.Next = null;
    }
    #endregion

    #region Methods - public
    public T Get(int index)
    {
        Node<T> node = GetNode(index);

        return node.Value;
    }

    public T GetReverse(int reverseIndex)
    {
        Node<T> node = GetNodeReverse(reverseIndex);

        return node.Value;
    }

    public void AddHead(T value)
    {
        Node<T> newNode = new Node<T>(value);

        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            Node<T> nextNode = _head;
            nextNode.Prev = newNode;
            newNode.Next = nextNode;
            _head = newNode;
        }

        _nodeCount++;
    }

    public void AddTail(T value)
    {
        Node<T> newNode = new Node<T>(value);

        if (_tail == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            Node<T> prevNode = _tail;
            prevNode.Next = newNode;
            newNode.Prev = prevNode;
            _tail = newNode;
        }

        _nodeCount++;
    }

    public void Insert(T value, int index)
    {
        Node<T> newNode = new Node<T>(value);
        Node<T> currNode = GetNode(index);

        InsertNode(newNode, currNode);
    }

    public void InsertReverse(T value, int index)
    {
        Node<T> newNode = new Node<T>(value);
        Node<T> currNode = GetNodeReverse(index);

        InsertNode(newNode, currNode);
    }

    public bool RemoveHead()
    {
        if (_nodeCount == 0 || _head == null)
        {
            return false;
        }

        RemoveNode(_head);

        return true;
    }

    public bool RemoveTail()
    {
        if (_nodeCount == 0 || _tail == null)
        {
            return false;
        }

        RemoveNode(_tail);

        return true;
    }

    public bool Remove(T value)
    {
        Node<T> node = _head;
        while (node != null)
        {
            if (node != null && node.Value != null && node.Value.Equals(value))
            {
                break;
            }
            node = node.Next;
        }

        if (node is null)
        {
            return false;
        }

        RemoveNode(node);

        return true;
    }

    public bool RemoveReverse(T value)
    {
        Node<T> node = _tail;
        while (node != null)
        {
            if (node != null && node.Value != null && node.Value.Equals(value))
            {
                break;
            }
            node = node.Prev;
        }

        if (node is null)
        {
            return false;
        }

        RemoveNode(node);

        return true;
    }

    public void RemoveAt(int index)
    {
        Node<T> node = GetNode(index);

        RemoveNode(node);
    }

    public void RemoveAtReverse(int reverseIndex)
    {
        Node<T> node = GetNodeReverse(reverseIndex);

        RemoveNode(node);
    }

    public void Clear()
    {
        Node<T> node = _head;
        while (node != null)
        {
            Node<T> removeMe = node;
            node = node.Next;

            ClearNode(removeMe);
        }

        _head = null;
        _tail = null;
        _nodeCount = 0;
    }

    public void PrintAllNodes()
    {
        string s = "";
        int index = 0;

        Node<T> node = _head;
        while (node != null)
        {
            s += $"index[{index}]: {node.Value?.ToString()}";

            node = node.Next;
            if (node != null)
            {
                s += "\n";
                index++;
            }
        }

        if (string.IsNullOrEmpty(s))
        {
            Console.WriteLine($"Linkedlist is empty!\n-------------");
            return;
        }

        Console.WriteLine($"Linkedlist content:\n{s}\n-------------");
    }
    #endregion
}
