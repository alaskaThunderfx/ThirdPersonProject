using System;
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
        private Camera _mainCamera;
        private List<Target> _targets = new();
        public Target CurrentTarget { get; private set; }

        // Unity built-in methods
        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<Target>(out var target)) return;

            _targets.Add(target);
            target.OnDestroyed += RemoveTarget;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<Target>(out var target)) return;
            
            RemoveTarget(target);
        }

        // Public methods
        public bool SelectTarget()
        {
            if (_targets.Count == 0) return false;

            Target closestTarget = null;
            var closestTargetDistance = Mathf.Infinity;

            foreach (var target in _targets)
            {
                Vector2 viewPos = _mainCamera.WorldToViewportPoint(target.transform.position);
                
                if (!target.GetComponentInChildren<Renderer>().isVisible)
                {
                    continue;
                }

                var toCenter = viewPos - new Vector2(.5f, .5f);
                if (toCenter.sqrMagnitude < closestTargetDistance)
                {
                    closestTarget = target;
                    closestTargetDistance = toCenter.sqrMagnitude;
                }
            }

            if (closestTarget == null) return false;

            CurrentTarget = closestTarget;
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