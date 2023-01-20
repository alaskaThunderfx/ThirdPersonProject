using StateMachines.Player;
using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerTestState : PlayerBaseState
    {
        public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
        }

        public override void Tick(float deltaTime)
        {
            var movement = CalculateMovement();

            stateMachine.CharacterController.Move(movement * (stateMachine.FreeLookMovementSpeed * deltaTime));

            if (stateMachine.InputReader.MovementValue == Vector2.zero)
            {
                stateMachine.Animator.SetFloat("FreeLookSpeed", 0, 0.1f, deltaTime);
                return;
            }

            stateMachine.Animator.SetFloat("FreeLookSpeed", 1, 0.1f, deltaTime);
            stateMachine.transform.rotation = Quaternion.LookRotation(movement);
        }

        public override void Exit()
        {
        }
        
        // Private methods
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
    }
}