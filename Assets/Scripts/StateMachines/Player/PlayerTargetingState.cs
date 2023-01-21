using Unity.VisualScripting;
using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerTargetingState : PlayerBaseState
    {
        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.InputReader.CancelEvent += OnCancel;
        }

        public override void Tick(float deltaTime)
        {
            Debug.Log(stateMachine.Targeter.CurrentTarget.name);
        }

        public override void Exit()
        {
            stateMachine.InputReader.CancelEvent -= OnCancel;
        }
        
        // Private methods
        private void OnCancel()
        {
            stateMachine.Targeter.Cancel();
            
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }
}

