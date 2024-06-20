using UnityEngine;

namespace Core.Player
{
    public sealed class PlayerAnimation : MonoBehaviour
    {
        #region Encapsulation
        internal bool IsPistolAttacked { get => _isPistolAttacked; }
        #endregion

        private const string MELEE_ATTACK_KEY = "IsMeleeAttacking";
        private const string PISTOL_ATTACK_KEY = "IsPistolAttacking";

        [Header("Classes")]
        [SerializeField] private Animator _playerAnimator;

        [Header("Settings")]
        [SerializeField] [Range(1f, 2f)] private float _meleeAttackCouldown = 1.2f;
        [SerializeField] [Range(1f , 2f)] private float _pistolAttackCouldown = 1.4f;

        private bool _isMeleeAttacked;
        private bool _isPistolAttacked;
        private float _currentMeleeAttackCouldown;
        private float _currentPistolAttackCouldown;

        private void Update()
        {
            if(_isMeleeAttacked)
            {
                _currentMeleeAttackCouldown += Time.deltaTime;
                if(_currentMeleeAttackCouldown >= _meleeAttackCouldown)
                {
                    _currentMeleeAttackCouldown = 0;

                    _isMeleeAttacked = false;
                }
            }

            if(_isPistolAttacked)
            {
                _currentPistolAttackCouldown += Time.deltaTime;
                if(_currentPistolAttackCouldown >= _pistolAttackCouldown)
                {
                    _currentPistolAttackCouldown = 0;

                    _isPistolAttacked = false;
                }
            }
        }

        internal void MeleeAttackAnimation()
        {
            if(!_isMeleeAttacked)
            {
                _playerAnimator.SetTrigger(MELEE_ATTACK_KEY);

                _isMeleeAttacked = true;
            }
        }

        internal void PistolAttackAnimation()
        {
            if(!_isPistolAttacked)
            {
                _playerAnimator.SetTrigger(PISTOL_ATTACK_KEY);

                _isPistolAttacked = true;
            }
        }
    }
}
