using Combat;
using Combat.Targeting;
using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerStateMachine : StateMachine
    {
        // Serialized field variables
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Targeter Targeter { get; private set; }
        [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
        [field: SerializeField] public Attack[] Attacks { get; private set; }
        [field: SerializeField] public WeaponDamage Weapon { get; private set; }
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
        [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
        [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
        [field: SerializeField] public float RotationDamping { get; private set; }
        [field: SerializeField] public float DodgeDuration { get; private set; }
        [field: SerializeField] public float DodgeLength { get; private set; }
        [field: SerializeField] public float DodgeCooldown { get; private set; }
        
        // Public variables
        public Transform MainCameraTransform { get; private set; }
        public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;

        // Unity built-in methods
        private void Start()
        {
            if (Camera.main != null) MainCameraTransform = Camera.main.transform;

            SwitchState(new PlayerFreeLookState(this));
        }

        private void OnEnable()
        {
            Health.OnTakeDamage += HandleTakeDamage;
            Health.OnDie += HandleDie;
        }

        private void OnDisable()
        {
            Health.OnTakeDamage -= HandleTakeDamage;
            Health.OnDie -= HandleDie;
        }
        
        // Public methods
        public void SetDodgetime(float dodgeTime)
        {
            PreviousDodgeTime = dodgeTime;
        }
        
        // Private methods
        private void HandleDie()
        {
            SwitchState(new PlayerDeadState(this));
        }

        private void HandleTakeDamage()
        {
            SwitchState(new PlayerImpactState(this));
        }
        
        
    }
}