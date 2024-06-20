using Core.Player;
using Core.Managers;
using UnityEngine;

namespace Core.Events
{
    public sealed class GameplayEvents : MonoBehaviour
    {
        [Header("Player Classes")]
        [SerializeField] private PlayerBehaviour _behaviour;

        [Header("Managers Classes")]
        [SerializeField] private ObjectPoolingManager _poolingManager;

        private void OnEnable()
        {
            _behaviour.Equipment.OnEquipingWeapon += _poolingManager.SpawnPooling;

            _behaviour.Attack.OnShooting += _poolingManager.SpawnPooling;
        }

        private void OnDisable()
        {
            _behaviour.Equipment.OnEquipingWeapon -= _poolingManager.SpawnPooling;

            _behaviour.Attack.OnShooting -= _poolingManager.SpawnPooling;
        }
    }
}
