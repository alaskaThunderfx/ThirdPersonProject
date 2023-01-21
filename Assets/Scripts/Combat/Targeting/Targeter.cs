using System;
using System.Collections.Generic;
using UnityEngine;

namespace Combat.Targeting
{
    public class Targeter : MonoBehaviour
    {
        // Public variables
        public List<Target> targets = new List<Target>();

        // Unity built-in methods
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<Target>(out var target)) return;

            targets.Add(target);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<Target>(out var target)) return;
            
            targets.Remove(target);
        }
    }
}