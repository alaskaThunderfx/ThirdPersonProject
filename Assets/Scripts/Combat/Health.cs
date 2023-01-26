using System;
using UnityEngine;

namespace Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;

        private int _health;

        public event Action OnTakeDamage;

        private void Start()
        {
            _health = maxHealth;
        }

        public void DealDamage(int damage)
        {
            if (_health == 0) return;

            _health = Mathf.Max(_health - damage, 0);
            
            OnTakeDamage?.Invoke();
            
            Debug.Log(_health);
        }
    }
}