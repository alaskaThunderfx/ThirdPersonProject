using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private CharacterController controller;

    // Private variables
    private float verticalVelocity;
    
    public Vector3 Movement => Vector3.up * verticalVelocity;
    
    // Unity built-in methods
    private void Update()
    {
        if (verticalVelocity < 0 && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }
}
