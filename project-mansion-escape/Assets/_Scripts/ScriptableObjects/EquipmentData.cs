using UnityEngine;

namespace Core.ScriptableObjects
{
    [CreateAssetMenu(menuName = "New Data/Equipment Data")]
    public sealed class EquipmentData : ScriptableObject
    {
        #region Encapsulation
        public string Key { get => _equipmentKey; }
        
        public WeaponType Type { get => _weaponType; }
        public int Duration { get => _weaponDuration; }
        #endregion

        public enum WeaponType { MELEE, RANGED }

        [Header("Settings")]
        [SerializeField] private string _equipmentKey = "equipment_key";
        [Space(12)]
        [SerializeField] private WeaponType _weaponType = WeaponType.MELEE;
        [SerializeField] private int _weaponDuration = 16;
        
        #region Editor Variables
        #if UNITY_EDITOR
        [Space (20)]

        [SerializeField] [Multiline(6)] private string _devNotes = "Put your dev notes here.";
        #endif
        #endregion
    }
}
