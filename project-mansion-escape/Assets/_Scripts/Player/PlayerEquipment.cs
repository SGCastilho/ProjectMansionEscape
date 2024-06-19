using Core.ScriptableObjects;
using UnityEngine;

namespace Core.Player
{
    public sealed class PlayerEquipment : MonoBehaviour
    {
        #region Encapsulation
        internal EquipmentData EquipData { get => _currentEquipedWeapon; }
        #endregion

        public delegate GameObject EquipingWeapon(ref string poolingKey, Vector2 poolingPosistion, Transform poolingParent);
        public event EquipingWeapon OnEquipingWeapon;

        [Header("Settings")]
        [SerializeField] private EquipmentData _currentEquipedWeapon;
        [Space(12)]
        [SerializeField] private Transform _weaponPivot;

        //DEBBUG
        [SerializeField] private EquipmentData _debugEquip;

        void Update()
        {
            if(Input.GetKeyUp(KeyCode.E))
            {
                EquipWeapon(ref _debugEquip);
            }

            if(Input.GetKeyUp(KeyCode.F))
            {
                DesequipWeapon();
            }
        }
        //DEBBUG

        private string _currentWeaponKey;
        private GameObject _currentEquipedWeaponGameObject;

        public void EquipWeapon(ref EquipmentData equipmentData)
        {
            if(_currentEquipedWeapon != null)
            {
                DesequipWeapon();
            }

            _currentEquipedWeapon = equipmentData;
            _currentWeaponKey = _currentEquipedWeapon.Key;

            _currentEquipedWeaponGameObject = OnEquipingWeapon?.Invoke(ref _currentWeaponKey, Vector2.zero, _weaponPivot);
        }

        public void DesequipWeapon()
        {
            _currentEquipedWeapon = null;
            _currentWeaponKey = string.Empty;

            _currentEquipedWeaponGameObject.SetActive(false);
            _currentEquipedWeaponGameObject.transform.parent = null;
            _currentEquipedWeaponGameObject.transform.position = Vector2.zero;
            _currentEquipedWeaponGameObject = null;
        }
    }
}
