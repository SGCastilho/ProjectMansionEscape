using UnityEngine;

namespace Core.ScriptableObjects
{
    public enum ItemType { USABLE, EQUIPABLE }

    [CreateAssetMenu(menuName = "New Data/Item Data")]
    public sealed class ItemData : ScriptableObject
    {
        #region Encapsulation
        public string Key { get => _itemKey; }

        public string Name { get => _itemName; }
        public Sprite Sprite { get => _itemSprite; }

        public int SlotLimit { get => _itemSlotLimit; }
        public ItemType Type { get => _itemType; }

        public string Description { get => _itemDescription; }

        public EquipmentData WeaponToEquip { get => _weaponToEquip; }

        public int HealthRecovery { get => _healthRecovery; }
        #endregion

        [Header("Settings")]
        [SerializeField] private string _itemKey = "item_key";
        [Space(12)]
        [SerializeField] private string _itemName = "Item Name";
        [SerializeField] private Sprite _itemSprite;
        [Space(12)]
        [SerializeField] private int _itemSlotLimit = 1;
        [SerializeField] private ItemType _itemType = ItemType.USABLE;
        [Space(20)]
        [SerializeField] [Multiline(6)] private string _itemDescription = "Put your item description here.";

        [Header("Equipable Settings")]
        [SerializeField] private EquipmentData _weaponToEquip;

        [Header("Usable Settings")]
        [SerializeField] private int _healthRecovery = 20;

        #region Editor Variables
        #if UNITY_EDITOR
        [Space (20)]

        [SerializeField] [Multiline(6)] private string _devNotes = "Put your dev notes here.";
        #endif
        #endregion
    }
}
