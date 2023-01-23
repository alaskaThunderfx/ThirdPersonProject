using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Combat.Targeting
{
    public class Targeter : MonoBehaviour
    {
        // Serialized fields
        [SerializeField] private CinemachineTargetGroup cineTargetGroup;
        
        // Public variables
        private List<Target> _targets = new();
        public Target CurrentTarget { get; private set; }

        // Unity built-in methods
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<Target>(out var target)) return;

            _targets.Add(target);
            target.OnDestroyed += RemoveTarget;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<Target>(out var target)) return;
            
            _targets.Remove(target);
        }
        
        // Public methods
        public bool SelectTarget()
        {
            if (_targets.Count == 0) return false;

            CurrentTarget = _targets[0];
            cineTargetGroup.AddMember(CurrentTarget.transform, 1, 2);

            return true;
        }

        public void Cancel()
        {
            if (CurrentTarget == null) return;
            cineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }
        
        // Private Methods
        private void RemoveTarget(Target target)
        {
            if (CurrentTarget == target)
            {
                cineTargetGroup.RemoveMember(CurrentTarget.transform);
                CurrentTarget = null;
            }

            target.OnDestroyed -= RemoveTarget;
            _targets.Remove(target);
        }
    }
}