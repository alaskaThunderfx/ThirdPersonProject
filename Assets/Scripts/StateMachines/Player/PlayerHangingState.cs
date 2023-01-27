using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerHangingState : PlayerBaseState
    {
        private Vector3 _ledgeForward;
        private Vector3 _closestPoint;
        private readonly int _hangingHash = Animator.StringToHash("Hanging");
        private const float CrossFadeDuration = 0.1f;

        public PlayerHangingState(PlayerStateMachine stateMachine, Vector3 ledgeForward, Vector3 closestPoint) : base(
            stateMachine)
        {
            _ledgeForward = ledgeForward;
            _closestPoint = closestPoint;
        }

        public override void Enter()
        {
            stateMachine.transform.rotation = Quaternion.LookRotation(_ledgeForward, Vector3.up);
            stateMachine.CharacterController.enabled = false;
            stateMachine.transform.position = _closestPoint- (stateMachine.LedgeDetector.transform.position - stateMachine.transform.position);
            stateMachine.CharacterController.enabled = true;

            stateMachine.Animator.CrossFadeInFixedTime(_hangingHash, CrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            if (stateMachine.InputReader.MovementValue.y > 0f)
            {
                stateMachine.SwitchState(new PlayerPullUpState(stateMachine));
            }

            else if (stateMachine.InputReader.MovementValue.y < 0f)
            {
                stateMachine.CharacterController.Move(Vector3.zero);
                stateMachine.ForceReceiver.Reset();
                stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            }
        }

        public override void Exit()
        {
        }
    }
}