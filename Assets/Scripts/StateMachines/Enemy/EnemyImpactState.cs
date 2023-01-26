using UnityEngine;

namespace StateMachines.Enemy
{
    public class EnemyImpactState : EnemyBaseState
    {
        private readonly int _impactHash = Animator.StringToHash("Impact");
        private const float CrossFadeDuration = 0.1f;
        private float _duration = 1;

        public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.Animator.CrossFadeInFixedTime(_impactHash, CrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
            
            _duration -= deltaTime;

            if (_duration <= 0)
            {
                stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            }
        }

        public override void Exit()
        {
            
        }
    }
}
