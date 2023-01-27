using System;
using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerTargetingState : PlayerBaseState
    {
        private readonly int _targetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
        private readonly int _targetingForwardTreeHash = Animator.StringToHash("TargetingForward");
        private readonly int _targetingRightTreeHash = Animator.StringToHash("TargetingRight");
        private const float CrossFadeDuration = 0.1f;

        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.InputReader.CancelEvent += OnCancel;
            stateMachine.InputReader.DodgeEvent += OnDodge;
            stateMachine.InputReader.JumpEvent += OnJump;

            stateMachine.Animator.CrossFadeInFixedTime(_targetingBlendTreeHash, CrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            if (stateMachine.InputReader.IsAttacking)
            {
                stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
                return;
            }

            if (stateMachine.InputReader.IsBlocking)
            {
                stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
                return;
            }

            if (stateMachine.Targeter.CurrentTarget == null)
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
                return;
            }

            var movement = CalculateMovement(deltaTime);

            Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);

            UpdateAnimator(deltaTime);

            FaceTarget();
        }

        public override void Exit()
        {
            stateMachine.InputReader.CancelEvent -= OnCancel;
            stateMachine.InputReader.DodgeEvent -= OnDodge;
            stateMachine.InputReader.JumpEvent -= OnJump;
        }


        // Private methods
        private void OnCancel()
        {
            stateMachine.Targeter.Cancel();

            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }

        private void OnDodge()
        {
            if (stateMachine.InputReader.MovementValue == Vector2.zero) return;
            
            stateMachine.SwitchState(new PlayerDodgingState(stateMachine, stateMachine.InputReader.MovementValue));
        }

        private void OnJump()
        {
            stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
        }

        private Vector3 CalculateMovement(float deltaTime)
        {
            var movement = new Vector3();

            movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
            movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
            
            return movement;
        }

        private void UpdateAnimator(float deltaTime)
        {
            if (stateMachine.InputReader.MovementValue.y == 0)
            {
                stateMachine.Animator.SetFloat(_targetingForwardTreeHash, 0, 0.1f, deltaTime);
            }
            else
            {
                float value = stateMachine.InputReader.MovementValue.y > 0 ? 1 : -1;
                stateMachine.Animator.SetFloat(_targetingForwardTreeHash, value, 0.1f, deltaTime);
            }

            if (stateMachine.InputReader.MovementValue.x == 0)
            {
                stateMachine.Animator.SetFloat(_targetingRightTreeHash, 0, 0.1f, deltaTime);
            }
            else
            {
                float value = stateMachine.InputReader.MovementValue.x > 0 ? 1 : -1;
                stateMachine.Animator.SetFloat(_targetingRightTreeHash, value, 0.1f, deltaTime);
            }
        }
    }
}