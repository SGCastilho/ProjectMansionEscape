using Core.Player;
using Core.Managers;
using Core.Inventory;
using UnityEngine;

namespace Core.Events
{
    public sealed class GameplayEvents : MonoBehaviour
    {
        [Header("Player Classes")]
        [SerializeField] private PlayerBehaviour _behaviour;

        [Header("Managers Classes")]
        [SerializeField] private ObjectPoolingManager _poolingManager;
        [Space(12)]
        [SerializeField] private InventoryManager _inventoryManager;

        private void OnEnable()
        {
            _behaviour.Equipment.OnEquipingWeapon += _poolingManager.SpawnPooling;

            _behaviour.Attack.OnShooting += _poolingManager.SpawnPooling;

            _inventoryManager.OnEquipWeapon += _behaviour.Equipment.EquipWeapon;
            _inventoryManager.OnDesequipWeapon += _behaviour.Equipment.DesequipWeapon;
        }

        private void OnDisable()
        {
            _behaviour.Equipment.OnEquipingWeapon -= _poolingManager.SpawnPooling;

            _behaviour.Attack.OnShooting -= _poolingManager.SpawnPooling;

            _inventoryManager.OnEquipWeapon -= _behaviour.Equipment.EquipWeapon;
            _inventoryManager.OnDesequipWeapon -= _behaviour.Equipment.DesequipWeapon;
        }
    }
}
