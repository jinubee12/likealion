using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<T>
{
    public T Data;
    public Node<T> Next { get; set; }

    public Node(T data)
    {
        Data = data;
        Next = null;
    }
}

public class LinkedListCustom<T>
{
    public Node<T> Head { get; set; }

    public void Add(T data)
    {
        Node<T> newNode = new Node<T>(data);
        if(Head == null) Head = newNode;
        else
        {
            Node<T> current = Head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            
            current.Next = newNode;
        }
    }

    public void Traverse()
    {
        Node<T> current = Head;
        while (current != null)
        {
            Debug.Log(current.Data);
            current = current.Next;
        }
    }
}
public class ListExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LinkedListCustom<int> list = new LinkedListCustom<int>();
        
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
/*
        list.AddFirst(0);
        
        var enumerator = list.GetEnumerator();
        
        int findIndex = 3;
        int currentIndex = 0;

        while (enumerator.MoveNext())
        {
            if (currentIndex == findIndex)
            {
                Debug.Log(enumerator.Current);
                break;
            }
            currentIndex++;
        }
        LinkedListNode<int> node = list.Find(findIndex);
        Debug.Log(node.Value);
*/
        list.Traverse();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
