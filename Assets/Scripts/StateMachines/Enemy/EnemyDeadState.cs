using UnityEngine;

namespace StateMachines.Enemy
{
    public class EnemyDeadState : EnemyBaseState
    {
        public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.Ragdoll.ToggleRagdoll(true);
            stateMachine.Weapon.gameObject.SetActive(false);
            Object.Destroy(stateMachine.Target);
        }

        public override void Tick(float deltaTime)
        {
            // toggle ragdoll
            stateMachine.Weapon.gameObject.SetActive(false);
        }

        public override void Exit()
        {
            // toggle ragdoll
            stateMachine.Weapon.gameObject.SetActive(false);
        }
    }
}
