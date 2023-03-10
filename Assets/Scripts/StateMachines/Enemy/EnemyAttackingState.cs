using UnityEngine;

namespace StateMachines.Enemy
{
    public class EnemyAttackingState : EnemyBaseState
    {
        private readonly int _attackHash = Animator.StringToHash("Attack");
        private const float CrossFadeDuration = 0.1f;
        
        public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            FacePlayer();
            
            stateMachine.Weapon.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback);
            
            stateMachine.Animator.CrossFadeInFixedTime(_attackHash, CrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            if (GetNormalizedTime(stateMachine.Animator, "Attack") >= 1)
            {
                stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            }
        }

        public override void Exit()
        {
            
        }
    }
}
