using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace StateMachines.Player
{
    public class PlayerPullUpState : PlayerBaseState
    {
        private readonly int _pullUpHash = Animator.StringToHash("PullUp");
        private readonly Vector3 _offset = new Vector3(0, 2.325f, .65f);
        private const float CrossFadeDuration = 0.1f;

        public PlayerPullUpState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.Animator.CrossFadeInFixedTime(_pullUpHash, CrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            if (GetNormalizedTime(stateMachine.Animator, "Climbing") < 1) return;

            stateMachine.CharacterController.enabled = false;
            stateMachine.transform.Translate(_offset, Space.Self);
            stateMachine.CharacterController.enabled = true;

            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine, false));
        }

        public override void Exit()
        {
            stateMachine.CharacterController.Move(Vector3.zero);
            stateMachine.ForceReceiver.Reset();
        }
    }
}