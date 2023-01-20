using UnityEngine;
using UnityEngine.TextCore.Text;

namespace StateMachines.Player
{
    public class PlayerStateMachine : StateMachine
    {
        // Public variables
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }

        // Unity built-in methods
        private void Start()
        {
            SwitchState(new PlayerTestState(this));
        }
        
        // Private methods
        
    }
}