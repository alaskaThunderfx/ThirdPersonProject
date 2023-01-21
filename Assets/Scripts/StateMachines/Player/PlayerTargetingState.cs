using Unity.VisualScripting;

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
            
        }

        public override void Exit()
        {
            stateMachine.InputReader.CancelEvent -= OnCancel;
        }
        
        // Private methods
        private void OnCancel()
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }
}

