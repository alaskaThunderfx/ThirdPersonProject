using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerStateMachine : StateMachine
    {
        // Unity built-in methods
        private void Start()
        {
            SwitchState(new PlayerTestState(this));
        }
    }
}

