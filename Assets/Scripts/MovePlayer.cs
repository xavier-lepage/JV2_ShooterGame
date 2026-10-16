using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction jumpAction;
    private CharacterController characterController;

    private Vector2 moveValue;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();


        characterController.Move(new Vector3(0f, -30f, 0f) * Time.deltaTime);
    }
}
