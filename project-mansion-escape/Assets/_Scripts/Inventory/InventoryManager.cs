using System;
using System.Collections.Generic;
using Core.ScriptableObjects;
using UnityEngine;

namespace Core.Inventory
{
    public sealed class InventoryManager : MonoBehaviour
    {
        internal class InventoryItem 
        {
            #region Encapsulation
            internal ItemData Data { get => _itemData; }
            internal int Amount { get => _itemAmount; set => _itemAmount = value; }
            #endregion

            private ItemData _itemData;
            private int _itemAmount;

            internal InventoryItem(ItemData data)
            {
                _itemData = data;
                _itemAmount = 1;
            }
        }

        public delegate void EquipWeapon(EquipmentData equipmentData);
        public event EquipWeapon OnEquipWeapon;

        public Action OnDesequipWeapon;

        [Header("Settings")]
        [SerializeField] private int _inventoryOcuppedSlots;
        [SerializeField] private int _inventoryMaxSlots = 12;

        private Dictionary<string, InventoryItem> _inventory;

        private string _currentEquipedWeapon;

        //DEBUG
        [SerializeField] private ItemData itemAdd;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                AddItem(ref itemAdd);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                RemoveItem(itemAdd.Key);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                //FAZER FUNÇÃO DE USAR ITEM
            }
        }
        //DEBUG

        private void Awake()
        {
            _inventoryOcuppedSlots = 0;
            _inventory = new Dictionary<string, InventoryItem>();
        }

        public void AddItem(ref ItemData itemData)
        {
            if(_inventoryOcuppedSlots >= _inventoryMaxSlots) return;

            if(_inventory.ContainsKey(itemData.Key))
            {
                if(_inventory[itemData.Key].Amount < _inventory[itemData.Key].Data.SlotLimit)
                {
                    _inventory[itemData.Key].Amount++;

                    _inventoryOcuppedSlots++;

                    Debug.Log($"New item added to inventory, {itemData.Name} now is {_inventory[itemData.Key].Amount}");
                }
            }
            else
            {
                InventoryItem newItem = new InventoryItem(itemData);

                _inventory.Add(newItem.Data.Key, newItem);

                _inventoryOcuppedSlots++;

                Debug.Log($"New item added to inventory, {newItem.Data.Name}");
            }
        }

        public void RemoveItem(string itemKey)
        {
            if(_inventory.ContainsKey(itemKey))
            {
                if(_currentEquipedWeapon == itemKey)
                {
                    DesequipWeapon();
                }

                _inventory[itemKey].Amount--;

                _inventoryOcuppedSlots--;

                Debug.LogWarning($"A item has been removed from your inventory, {_inventory[itemKey].Data.Name}");

                if(_inventoryOcuppedSlots < 0)
                {
                    _inventoryOcuppedSlots = 0; 
                }

                if(_inventory[itemKey].Amount < 1)
                {
                    Debug.LogWarning($"All itens has been removed from your inventory, {_inventory[itemKey].Data.Name}");

                    _inventory.Remove(itemKey);
                }
            }
        }

        public void RemoveItem(string itemKey, int amountToRemove)
        {
            if(_inventory.ContainsKey(itemKey))
            {
                if(_currentEquipedWeapon == itemKey)
                {
                    DesequipWeapon();
                }

                _inventory[itemKey].Amount -= amountToRemove;

                _inventoryOcuppedSlots -= amountToRemove;

                Debug.LogWarning($"A item has been removed from your inventory, {_inventory[itemKey].Data.Name}");

                if(_inventoryOcuppedSlots < 0)
                {
                    _inventoryOcuppedSlots = 0; 
                }

                if(_inventory[itemKey].Amount < 1)
                {
                    Debug.LogWarning($"All itens has been removed from your inventory, {_inventory[itemKey].Data.Name}");

                    _inventory.Remove(itemKey);
                }
            }
        }

        public void EquipAction(string itemKey)
        {
            if(_inventory.ContainsKey(itemKey))
            {
                if(_inventory[itemKey].Data.Type == ItemType.EQUIPABLE)
                {
                    _currentEquipedWeapon = itemKey;

                    OnEquipWeapon?.Invoke(_inventory[itemKey].Data.WeaponToEquip);

                    Debug.Log($"Weapon has been equiped, {_inventory[itemKey].Data.Name}");
                }
            }
            else
            {
                Debug.LogWarning($"No weapon founded, {itemKey}");
            }
        }

        public void DesequipWeapon()
        {
            _currentEquipedWeapon = string.Empty;

            OnDesequipWeapon?.Invoke();

            Debug.LogWarning("Weapon has been unequiped");
        }
    }
}
