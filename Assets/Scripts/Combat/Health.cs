using System;
using UnityEngine;

namespace Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;

        private int _health;
        private bool _isInvulnerable;

        public event Action OnTakeDamage;
        public event Action OnDie;

        private void Start()
        {
            _health = maxHealth;
        }

        public void SetInvulnerable(bool isInvulnerable)
        {
            _isInvulnerable = isInvulnerable;
        }

        public void DealDamage(int damage)
        {
            if (_health == 0) return;

            if (_isInvulnerable) return;
            
            _health = Mathf.Max(_health - damage, 0);

            OnTakeDamage?.Invoke();
            
            if (_health == 0)
            {
                OnDie?.Invoke();
            }

            Debug.Log(_health);
        }
    }
}