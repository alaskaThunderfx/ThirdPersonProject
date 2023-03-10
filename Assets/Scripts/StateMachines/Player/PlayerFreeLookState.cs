using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerFreeLookState : PlayerBaseState
    {
        // Private variables
        private bool _shouldFade;
        private readonly int _freeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
        private readonly int _freeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
        private const float AnimatorDampTime = 0.1f;
        private const float CrossFadeDuration = 0.1f;

        public PlayerFreeLookState(PlayerStateMachine stateMachine, bool shouldFade = true) : base(stateMachine)
        {
            _shouldFade = shouldFade;
        }

        public override void Enter()
        {
            stateMachine.InputReader.TargetEvent += OnTarget;
            stateMachine.InputReader.JumpEvent += OnJump;
            
            stateMachine.Animator.SetFloat(_freeLookSpeedHash, 0);

            if (_shouldFade)
            {
                stateMachine.Animator.CrossFadeInFixedTime(_freeLookBlendTreeHash, CrossFadeDuration);
            }
            else
            {
                stateMachine.Animator.Play(_freeLookBlendTreeHash);
            }
            
        }

        public override void Tick(float deltaTime)
        {
            if (stateMachine.InputReader.IsAttacking)
            {
                stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
                return;
            }
            
            var movement = CalculateMovement();

            Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

            if (stateMachine.InputReader.MovementValue == Vector2.zero)
            {
                stateMachine.Animator.SetFloat(_freeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
                return;
            }

            stateMachine.Animator.SetFloat(_freeLookSpeedHash, 1, AnimatorDampTime, deltaTime);

            FaceMovementDirection(movement, deltaTime);
        }

        public override void Exit()
        {
            stateMachine.InputReader.TargetEvent -= OnTarget;
            stateMachine.InputReader.JumpEvent -= OnJump;
        }

        // Private methods
        private void OnTarget()
        {
            if (!stateMachine.Targeter.SelectTarget()) return;

            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }

        private void OnJump()
        {
            stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
        }

        private Vector3 CalculateMovement()
        {
            var forward = stateMachine.MainCameraTransform.forward;
            var right = stateMachine.MainCameraTransform.right;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            return forward * stateMachine.InputReader.MovementValue.y +
                   right * stateMachine.InputReader.MovementValue.x;
        }

        private void FaceMovementDirection(Vector3 movement, float deltaTime)
        {
            stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation,
                Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationDamping);
        }
    }
}