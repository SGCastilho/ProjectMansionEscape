using UnityEngine;

namespace Core.Player
{
    public sealed class PlayerStatus : MonoBehaviour
    {
        #region Encapsulation
        public int CurrentHealth { get => _currentHealth; }
        public int MaxHealth { get => _maxHealth; }
        public bool IsDead { get => _isDead; }
        #endregion

        [Header("Classes")]
        [SerializeField] private PlayerBehaviour _behaviour;
        
        [Header("Settings")]
        [SerializeField] private int _currentHealth;
        [Space(12)]
        [SerializeField] private int _maxHealth = 100;

        private bool _isDead;

        private void OnEnable()
        {
            _currentHealth = _maxHealth;
            _isDead = false;
        }

        public void AddHealth(int amount)
        {
            _currentHealth += amount;

            if(_currentHealth > _maxHealth) 
            { 
                _currentHealth = _maxHealth; 
            }
        }

        public void RemoveHealth(int amount)
        {
            _currentHealth -= amount;

            if(_currentHealth <= 0) { PlayerDeath(); }
        }

        private void PlayerDeath()
        {
            _currentHealth = 0;
            _isDead = true;

            _behaviour.Inputs.DisableInputs();
        }
    }
}
