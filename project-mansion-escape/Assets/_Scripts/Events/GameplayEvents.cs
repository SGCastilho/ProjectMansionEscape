using Core.UI;
using Core.Player;
using Core.Managers;
using Core.Inventory;
using UnityEngine;

namespace Core.Events
{
    public sealed class GameplayEvents : MonoBehaviour
    {
        [Header("UI Classes")]
        [SerializeField] private PlayerUI _playerUI;
        [SerializeField] private InventoryUI _inventoryUI;

        [Header("Player Classes")]
        [SerializeField] private PlayerBehaviour _behaviour;

        [Header("Managers Classes")]
        [SerializeField] private ObjectPoolingManager _poolingManager;
        [Space(12)]
        [SerializeField] private InventoryManager _inventoryManager;

        private void OnEnable()
        {
            _behaviour.Status.OnChangeHealth += _playerUI.RefreshHealthBar;

            _behaviour.Equipment.OnEquipingWeapon += _poolingManager.SpawnPooling;

            _behaviour.Attack.OnShooting += _poolingManager.SpawnPooling;

            _inventoryManager.OnEquipWeapon += _behaviour.Equipment.EquipWeapon;
            _inventoryManager.OnDesequipWeapon += _behaviour.Equipment.DesequipWeapon;

            _inventoryManager.OnUseItem += _behaviour.Status.AddHealth;

            _inventoryManager.OnItemAdd += _inventoryUI.AddItem;
            _inventoryUI.OnUseItem += _inventoryManager.UseAction;
            _inventoryUI.OnEquipItem += _inventoryManager.EquipAction;
            _inventoryUI.OnDiscardItem += _inventoryManager.RemoveItem;
            _inventoryUI.OnUnequipItem += _inventoryManager.DesequipWeapon;
            _inventoryManager.OnUnequipWeapon += _inventoryUI.UnequipUI;
        }

        private void OnDisable()
        {
            _behaviour.Status.OnChangeHealth -= _playerUI.RefreshHealthBar;

            _behaviour.Equipment.OnEquipingWeapon -= _poolingManager.SpawnPooling;

            _behaviour.Attack.OnShooting -= _poolingManager.SpawnPooling;

            _inventoryManager.OnEquipWeapon -= _behaviour.Equipment.EquipWeapon;
            _inventoryManager.OnDesequipWeapon -= _behaviour.Equipment.DesequipWeapon;

            _inventoryManager.OnUseItem -= _behaviour.Status.AddHealth;

            _inventoryManager.OnItemAdd -= _inventoryUI.AddItem;
            _inventoryUI.OnUseItem -= _inventoryManager.UseAction;
            _inventoryUI.OnEquipItem -= _inventoryManager.EquipAction;
            _inventoryUI.OnDiscardItem -= _inventoryManager.RemoveItem;
            _inventoryUI.OnUnequipItem -= _inventoryManager.DesequipWeapon;
            _inventoryManager.OnUnequipWeapon -= _inventoryUI.UnequipUI;
        }
    }
}
