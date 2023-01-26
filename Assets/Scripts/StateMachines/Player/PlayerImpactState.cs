using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerImpactState : PlayerBaseState
    {
        private readonly int _impactHash = Animator.StringToHash("Impact");
        private const float CrossFadeDuration = 0.1f;
        private float _duration = 1;

        public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
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
             ReturnToLocomotion();
         }
        }

        public override void Exit()
        {
            
        }
    }
}