using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    private CharacterController characterController;
    private InputAction moveAction;
    private InputAction jumpAction;
    private Vector2 moveValue;
    private bool isGameOver = false;

    private Vector3 direction;
    private float rotationSmoothingDuration = 0.1f;
    private float rotationSpeed;

    private float gravity = 80.0f;
    private float jumpStrength = 42.0f;
    private float verticalVelocity = 0f;

    [SerializeField] private float moveSpeed;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (isGameOver) return;
        moveValue = moveAction.ReadValue<Vector2>();

        MoveHorizontally();
        MoveVertically();

        characterController.Move(direction);
    }

    private void MoveHorizontally()
    {
        direction = new Vector3(moveValue.x, 0f, moveValue.y);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, rotationSmoothingDuration);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            float tempSpeed = moveSpeed;
            if (!characterController.isGrounded) tempSpeed /= 2;

            direction.x = direction.x * tempSpeed * Time.deltaTime;
            direction.z = direction.z * tempSpeed * Time.deltaTime;
        }
        else
        {
            direction = Vector3.zero;
        }
    }

    private void MoveVertically()
    {
        if (jumpAction.WasPerformedThisFrame())
        {
            if (characterController.isGrounded) verticalVelocity = jumpStrength;
        }

        verticalVelocity -= gravity * Time.deltaTime;
        direction.y = verticalVelocity * Time.deltaTime;
    }

    public void disableControl()
    {
        isGameOver = true;
    }
}