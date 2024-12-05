using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute();
    void Undo();
}
public class CommandManager : MonoBehaviour
{
    private Stack<ICommand> undoStack = new Stack<ICommand>();
    private Stack<ICommand> redoStack = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        undoStack.Push(command);
        redoStack.Clear();
    }

    public void Undo()
    {
        if (undoStack.Count > 0)
        {
            ICommand command = undoStack.Pop();
            command.Undo();
            redoStack.Push(command);
            
        }
    }

    public void Redo()
    {
        if (redoStack.Count > 0)
        {
            ICommand command = redoStack.Pop();
            command.Execute();
            undoStack.Push(command);
        }
    }

    public float Speed = 3.0f;
    public float RotateSpeed = 3.0f;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 movePos = Vector3.zero;
        Vector3 deltaRot = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movePos += transform.forward;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            movePos -= transform.forward;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            movePos -= transform.right;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            movePos += transform.right;
        }

        if (Input.GetKey(KeyCode.R))
        {
            deltaRot += transform.right * (Time.deltaTime * RotateSpeed);
        }
        
        if (Input.GetKey(KeyCode.Q))
        {
            deltaRot -= transform.right * (Time.deltaTime * RotateSpeed);
        }
        
        // 움직였던 정보를 기록하기 위해 키를 땔때마다 위치를 기록한다.
        if (Input.GetKeyDown(KeyCode.W) || 
            Input.GetKeyDown(KeyCode.S) || 
            Input.GetKeyDown(KeyCode.A) || 
            Input.GetKeyDown(KeyCode.D))
        {
            var moveCommand = new MoveCommand(transform, transform.position);
            ExecuteCommand(moveCommand);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Quaternion newRotation = transform.rotation;
            var rotateCommand = new RotateCommand(transform, newRotation);
            ExecuteCommand(rotateCommand);
        }

        // 왔던 포지션으로 되돌아가는 코드
        if (Input.GetKeyDown(KeyCode.Space))
        {
        }
        
        Vector3 addtivePosition = movePos.normalized * Speed * Time.deltaTime;
        transform.position += addtivePosition;
        transform.rotation = Quaternion.LookRotation(transform.forward + deltaRot, Vector3.up); ;
    }
}

public class MoveCommand : ICommand
{
    private Transform _transform;
    private Vector3 _oldPosition;
    private Vector3 _newPosition;

    public MoveCommand(Transform transform, Vector3 newPosition)
    {
        _transform = transform;
        _oldPosition = _transform.position;
        _newPosition = newPosition;
    }

    public void Execute()
    {
        _transform.position = _newPosition;
    }

    public void Undo()
    {
        _transform.position = _oldPosition;
    }
}

public class RotateCommand : ICommand
{
    private Transform _transform;
    private Quaternion _oldRotation;
    private Quaternion _newRotation;
    
    public RotateCommand(Transform transform, Quaternion newRotation)
    {
        // 이동하려는 트랜스폼 객체를 참조한다.
        _transform = transform;
        // 언두할때 돌아갈 회전값을 저장한다.
        _oldRotation = _transform.rotation;
        // excute시에 셋팅될 회전 값을 저장한다.
        _newRotation = newRotation;
    }
    
    public void Execute()
    {
        // newPosition으로 갱신힌다.
        _transform.rotation = _newRotation;
    }

    public void Undo()
    {
        // oldPosition으로 undo한다.
        _transform.rotation = _oldRotation;
    }
}


