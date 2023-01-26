using UnityEngine;

namespace StateMachines.Enemy
{
    public class EnemyIdleState : EnemyBaseState
    {
        private readonly int _locomotionHash = Animator.StringToHash("Locomotion");
        private readonly int _speedHash = Animator.StringToHash("Speed");
        private const float CrossFadeDuration = 0.1f;
        private const float AnimatorDampTime = 0.1f;

        public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.Animator.CrossFadeInFixedTime(_locomotionHash, CrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
            
            if (IsInChaseRange())
            {
                stateMachine.SwitchState(new EnemyChasingState(stateMachine));
                return;
            }
            
            FacePlayer();
            
            stateMachine.Animator.SetFloat(_speedHash, 0, AnimatorDampTime, deltaTime);
        }

        public override void Exit()
        {
        }
    }
}