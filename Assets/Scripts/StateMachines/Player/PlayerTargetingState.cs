using Unity.VisualScripting;
using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerTargetingState : PlayerBaseState
    {
        private readonly int _targetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");

        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.InputReader.CancelEvent += OnCancel;

            stateMachine.Animator.Play(_targetingBlendTreeHash);
        }

        public override void Tick(float deltaTime)
        {
            if (stateMachine.Targeter.CurrentTarget == null)
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
                return;
            }

            var movement = CalculateMovement();
            
            Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);

            FaceTarget();
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

        private Vector3 CalculateMovement()
        {
            var movement = new Vector3();

            movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
            movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

            return movement;
        }
    }
}