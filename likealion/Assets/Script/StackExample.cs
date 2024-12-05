using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackNode<T>
{
    public T data;
    public StackNode<T> prev;
}

public class StackCustom<T> where T : new()
{
    public StackNode<T> top;

    public void Push(T data)
    {
        var stackNode = new StackNode<T>();
        stackNode.data = data;
        stackNode.prev = top;
        top = stackNode;
    }

    public T Pop()
    {
        if (top == null)
        {
            return new();
        }
        
        var result = top.data;
        top = top.prev;
        
        return result;
    }

    public T Peek()
    {
        if (top == null)
        {
            return new();
        }
        return top.data;
    }
}
public class StackExample : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        StackCustom<int> stack = new StackCustom<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        stack.Push(4);

        stack.Pop();
        
        Debug.Log(stack.Pop());
        Debug.Log(stack.Peek());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
