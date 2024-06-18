using UnityEngine;

namespace Core.Player
{
    public sealed class PlayerBehaviour : MonoBehaviour
    {
        #region Encapsulation
        internal PlayerSetInputs Inputs { get => _inputs; }
        internal PlayerMovement Movement { get => _movement; }

        public PlayerEquipment Equipment { get => _equipment; }
        #endregion

        [Header("Classes")]
        [SerializeField] private PlayerSetInputs _inputs;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerEquipment _equipment;
    }
}
