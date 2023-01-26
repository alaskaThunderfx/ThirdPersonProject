using System;
using Unity.VisualScripting;
using UnityEngine;

namespace StateMachines.Enemy
{
    public class EnemyStateMachine : StateMachine
    {
        [field: SerializeField] public Animator Animator { get; private set; }

        private void Start()
        {
            SwitchState(new EnemyIdleState(this));
        }
    }
}