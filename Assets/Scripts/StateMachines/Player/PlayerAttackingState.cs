namespace StateMachines.Player
{
    public class PlayerAttackingState : PlayerBaseState
    {
        private float _previousFrameTime;
        private bool _alreadyAppliedForce;
        private Attack _attack;

        public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
        {
            _attack = stateMachine.Attacks[attackIndex];
        }

        public override void Enter()
        {
            stateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TransitionDuration);
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
            FaceTarget();

            var normalizedTime = GetNormalizedTime();

            if (normalizedTime < 1)
            {
                if (normalizedTime >= _attack.ForceTime)
                {
                    TryApplyForce();
                }

                if (stateMachine.InputReader.IsAttacking)
                {
                    TryComboAttack(normalizedTime);
                }
            }
            else
            {
                if (stateMachine.Targeter.CurrentTarget != null)
                {
                    stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
                }
                else
                {
                    stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
                }
            }

            _previousFrameTime = normalizedTime;
        }

        public override void Exit()
        {
        }

        private void TryComboAttack(float normalizedTime)
        {
            if (_attack.ComboStateIndex == -1) return;

            if (normalizedTime < _attack.ComboAttackTime) return;

            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, _attack.ComboStateIndex));
        }

        private void TryApplyForce()
        {
            if (_alreadyAppliedForce) return;
            
            stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * _attack.Force);

            _alreadyAppliedForce = true;
        }

        private float GetNormalizedTime()
        {
            var currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
            var nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

            if (stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
            {
                return nextInfo.normalizedTime;
            }

            if (!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
            {
                return currentInfo.normalizedTime;
            }

            return 0;
        }
    }
}