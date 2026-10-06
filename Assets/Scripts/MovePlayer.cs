using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction jumpAction;

    private Vector2 moveValue;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();

        Debug.Log(moveValue);
        Debug.Log(jumpAction.WasPerformedThisFrame());
    }
}
