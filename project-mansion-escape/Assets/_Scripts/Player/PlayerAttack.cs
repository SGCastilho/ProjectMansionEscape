using Core.ScriptableObjects;
using UnityEngine;

namespace Core.Player
{
    public sealed class PlayerAttack : MonoBehaviour
    {
        #region Encapsulation
        internal Transform WeaponParent;
        #endregion

        [Header("Classes")]
        [SerializeField] private PlayerBehaviour _behaviour;

        [Header("Settings")]
        [SerializeField] private bool _isAiming;
        [SerializeField] private Transform _weaponParent;

        public void StartAim()
        {
            _isAiming = true;
            _behaviour.Movement.StartAim();
        }

        public void EndAim()
        {
            _isAiming = false;
            _behaviour.Movement.EndAim();
        }

        public void Attacking()
        {
            if(_behaviour.Equipment.EquipData != null && _isAiming)
            {
                switch(_behaviour.Equipment.EquipData.Type)
                {
                    case EquipmentData.WeaponType.MELEE:
                        MeleeAttacking();
                        break;
                    case EquipmentData.WeaponType.RANGED:
                        RangedAttacking();
                        break;
                }
            }
        }

        private void MeleeAttacking()
        {
            _behaviour.Animation.MeleeAttackAnimation();
        }

        private void RangedAttacking()
        {
            _behaviour.Animation.PistolAttackAnimation();
        }
    }
}
