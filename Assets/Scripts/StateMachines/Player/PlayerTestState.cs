using StateMachines.Player;
using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerTestState : PlayerBaseState
    {
        private float _timer = 0;

        public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.InputReader.JumpEvent += OnJump;
        }

        public override void Tick(float deltaTime)
        {
            _timer += deltaTime;

            Debug.Log(_timer);
        }

        public override void Exit()
        {
            stateMachine.InputReader.JumpEvent -= OnJump;
        }

        private void OnJump()
        {
            stateMachine.SwitchState(new PlayerTestState(stateMachine));
        }
    }
}