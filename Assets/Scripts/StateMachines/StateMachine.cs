using UnityEngine;

namespace StateMachines
{
    public abstract class StateMachine : MonoBehaviour
    {
        private State _currentState;

        // Unity built-in methods
        private void Update()
        {
            _currentState?.Tick(Time.deltaTime);
        }

        // Public methods
        public void SwitchState(State newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }
    }
}
