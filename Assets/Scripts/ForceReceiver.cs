using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private CharacterController controller;
    [SerializeField] private float drag = 0.3f;

    // Private variables
    private Vector3 _dampingVelocity;
    private Vector3 _impact;
    private float _verticalVelocity;
    
    public Vector3 Movement => _impact + Vector3.up * _verticalVelocity;
    
    // Unity built-in methods
    private void Update()
    {
        if (_verticalVelocity < 0 && controller.isGrounded)
        {
            _verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, drag);
    }

    public void AddForce(Vector3 force)
    {
        _impact += force;
    }
}
