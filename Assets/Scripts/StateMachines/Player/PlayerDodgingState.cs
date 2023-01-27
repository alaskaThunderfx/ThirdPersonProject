using UnityEditor;
using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerDodgingState : PlayerBaseState
    {
        private readonly int _dodgeBlendTreeHash = Animator.StringToHash("DodgeBlendTree");
        private readonly int _dodgeForwardHash = Animator.StringToHash("DodgeForward");
        private readonly int _dodgeRightHash = Animator.StringToHash("DodgeRight");
        private const float CrossFadeDuration = 0.1f;
        private float _remainingDodgeTime;
        private Vector3 _dodgingDirectionInput;
        
        public PlayerDodgingState(PlayerStateMachine stateMachine, Vector3 dodgingDirectionInput) : base(stateMachine)
        {
            _dodgingDirectionInput = dodgingDirectionInput;
        }

        public override void Enter()
        {
            _remainingDodgeTime = stateMachine.DodgeDuration;
            
            stateMachine.Animator.SetFloat(_dodgeForwardHash, _dodgingDirectionInput.y);
            stateMachine.Animator.SetFloat(_dodgeRightHash, _dodgingDirectionInput.x);
            stateMachine.Animator.CrossFadeInFixedTime(_dodgeBlendTreeHash, CrossFadeDuration);
            
            stateMachine.Health.SetInvulnerable(true);
        }

        public override void Tick(float deltaTime)
        {
            var movement = new Vector3();
            
            movement += stateMachine.transform.right * _dodgingDirectionInput.x * stateMachine.DodgeLength /
                        stateMachine.DodgeDuration;
            movement += stateMachine.transform.forward * _dodgingDirectionInput.y * stateMachine.DodgeLength /
                        stateMachine.DodgeDuration;
            
            Move(movement, deltaTime);
            
            FaceTarget();

            _remainingDodgeTime -= deltaTime;

            if (_remainingDodgeTime <= 0)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
        }

        public override void Exit()
        {
            stateMachine.Health.SetInvulnerable(false);
        }
    }
}
