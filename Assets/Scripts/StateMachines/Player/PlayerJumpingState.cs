using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerJumpingState : PlayerBaseState
    {
        private readonly int _jumpHash = Animator.StringToHash("Jump");
        private const float CrossFadeDuration = 0.1f;

        private Vector3 _momentum;

        public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);

            _momentum = stateMachine.CharacterController.velocity;
            _momentum.y = 0;
            
            stateMachine.Animator.CrossFadeInFixedTime(_jumpHash, CrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            Move(_momentum, deltaTime);

            if (stateMachine.CharacterController.velocity.y <= 0)
            {
                stateMachine.SwitchState(new PlayerFallingState(stateMachine));
                return;
            }
            
            FaceTarget();
        }

        public override void Exit()
        {
        }
    }
}