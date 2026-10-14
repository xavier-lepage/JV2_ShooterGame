using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private CharacterController characterController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        characterController.Move(Vector3.down);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
    }
}
