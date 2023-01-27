using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerFallingState : PlayerBaseState
    {
        private readonly int _fallHash = Animator.StringToHash("Fall");
        private const float CrossFadeDuration = 0.1f;

        private Vector3 _momentum;

        public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            _momentum = stateMachine.CharacterController.velocity;
            _momentum.y = 0;

            stateMachine.Animator.CrossFadeInFixedTime(_fallHash, CrossFadeDuration);

            stateMachine.LedgeDetector.OnLedgeDetect += HandleLedgeDetect;
        }


        public override void Tick(float deltaTime)
        {
            Move(_momentum, deltaTime);

            if (stateMachine.CharacterController.isGrounded)
            {
                ReturnToLocomotion();
            }

            FaceTarget();
        }

        public override void Exit()
        {
            stateMachine.LedgeDetector.OnLedgeDetect -= HandleLedgeDetect;
        }

        private void HandleLedgeDetect(Vector3 ledgeForward)
        {
            stateMachine.SwitchState(new PlayerHangingState(stateMachine, ledgeForward));
        }
    }
}