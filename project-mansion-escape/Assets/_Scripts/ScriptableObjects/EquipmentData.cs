
using UnityEngine;

namespace Core.ScriptableObjects
{
    [CreateAssetMenu(menuName = "New Data/Equipment Data")]
    public sealed class EquipmentData : ScriptableObject
    {
        #region Encapsulation
        public string Key { get => _equipmentKey; }
        #endregion

        [Header("Settings")]
        [SerializeField] private string _equipmentKey = "equipment_key";
    }
}
