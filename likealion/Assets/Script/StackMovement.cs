using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StackMovement : MonoBehaviour
{
    [NonSerialized] public float speed = 3.0f;

    [SerializeField] private float speed2 = 3.0f;
    
    private Stack<Vector3> position_stack = new Stack<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Vector3.zero;
        
        if(Input.GetKey(KeyCode.W)) pos+= transform.forward;
        if(Input.GetKey(KeyCode.S)) pos-= transform.forward;
        if(Input.GetKey(KeyCode.D)) pos+= transform.right;
        if(Input.GetKey(KeyCode.A)) pos-= transform.right;

        if (Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.A))
        {
            pos = Vector3.zero;
            // Debug.Log(transform.position);
            position_stack.Push(transform.position);
            // Debug.Log($"Pushed : {transform.position}");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(position_stack.Count > 0 )
                transform.position = position_stack.Pop();
            else
            {
                Debug.Log("Empty stack");
            }
        }
        
        transform.position += pos.normalized * speed2 * Time.deltaTime;
           
    }

    
}
