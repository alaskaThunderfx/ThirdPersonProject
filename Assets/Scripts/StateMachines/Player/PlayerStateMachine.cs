using UnityEngine;
using UnityEngine.Serialization;

namespace StateMachines.Player
{
    public class PlayerStateMachine : StateMachine
    {
        // Public variables
        [field: SerializeField] public InputReader InputReader { get; private set; }

        // Unity built-in methods
        private void Start()
        {
            SwitchState(new PlayerTestState(this));
        }
        
        // Private methods
        
    }
}