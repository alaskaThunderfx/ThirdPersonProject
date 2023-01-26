using UnityEngine;

namespace StateMachines.Enemy
{
    public abstract class EnemyBaseState : State
    {
        protected EnemyStateMachine stateMachine;

        public EnemyBaseState(EnemyStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        
        protected void Move(float deltaTime)
        {
            Move(Vector3.zero, deltaTime);
        }

        protected void Move(Vector3 motion, float deltaTime)
        {
            stateMachine.CharacterController.Move(
                (motion + stateMachine.ForceReceiver.Movement) * deltaTime);
        }

        protected bool IsInChaseRange()
        {
            var playerDistanceSquare = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

            return playerDistanceSquare <= stateMachine.PlayerChasingRange * stateMachine.PlayerChasingRange;
        }
    }
}
