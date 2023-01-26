using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class WeaponDamage : MonoBehaviour
    {
        [SerializeField] private Collider myCollider;

        private int _damage;
        private float _knockback;

        private List<Collider> _alreadyCollidedWith = new();

        private void OnEnable()
        {
            _alreadyCollidedWith.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == myCollider) return;

            if (_alreadyCollidedWith.Contains(other)) return;

            _alreadyCollidedWith.Add(other);

            if (other.TryGetComponent<Health>(out var health))
            {
                health.DealDamage(_damage);
            }

            if (other.TryGetComponent(out ForceReceiver forceReceiver))
            {
                var direction = (other.transform.position - myCollider.transform.position).normalized;
                forceReceiver.AddForce(direction * _knockback);
            }
        }

        public void SetAttack(int damage, float knockback)
        {
            _damage = damage;
            _knockback = knockback;
        }
    }
}