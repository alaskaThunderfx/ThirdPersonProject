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
            var movement = new Vector3();
            
            movement.x = stateMachine.InputReader.MovementValue.x;
            movement.y = 0;
            movement.z = stateMachine.InputReader.MovementValue.y;
            
            stateMachine.CharacterController.Move(movement * (stateMachine.FreeLookMovementSpeed * deltaTime));

            if (stateMachine.InputReader.MovementValue == Vector2.zero) return;

            stateMachine.transform.rotation = Quaternion.LookRotation(movement);
        }

        public override void Exit()
        {
        }
    }
}