using UnityEngine;

namespace Core.ScriptableObjects
{
    [CreateAssetMenu(menuName = "New Data/Equipment Data")]
    public sealed class EquipmentData : ScriptableObject
    {
        #region Encapsulation
        public string Key { get => _equipmentKey; }
        public WeaponType Type { get => _weaponType; }
        #endregion

        public enum WeaponType { MELEE, RANGED }

        [Header("Settings")]
        [SerializeField] private string _equipmentKey = "equipment_key";
        [Space(12)]
        [SerializeField] private WeaponType _weaponType = WeaponType.MELEE;
    }
}
