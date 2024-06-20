using Core.Utilities;
using Core.ScriptableObjects;
using UnityEngine;

namespace Core.Player
{
    public sealed class PlayerAttack : MonoBehaviour
    {
        #region Encapsulation
        internal bool IsAiming { get => _isAiming; }

        internal Transform WeaponParent;
        #endregion

        public delegate GameObject Shooting(ref string poolingKey, Vector2 poolingPosistion);
        public event Shooting OnShooting;
        
        [Header("Classes")]
        [SerializeField] private PlayerBehaviour _behaviour;

        [Header("Settings")]
        [SerializeField] private bool _isAiming;
        [SerializeField] private Transform _weaponParent;
        [Space(12)]
        [SerializeField] private string _bulletInstanceKey = "projectile_bullet_default_weapon";
        [SerializeField] private Transform _bulletSpawnPoint;

        private MoveObjectHorizontal _projectileInstance;

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
            if(!_behaviour.Animation.IsPistolAttacked)
            {
                _behaviour.Animation.PistolAttackAnimation();

                GameObject projectile = OnShooting?.Invoke(ref _bulletInstanceKey, _bulletSpawnPoint.position);
                _projectileInstance = projectile.GetComponent<MoveObjectHorizontal>();

                _projectileInstance.MoveRight = _behaviour.Movement.AimingRightSide;
            }
        }
    }
}
