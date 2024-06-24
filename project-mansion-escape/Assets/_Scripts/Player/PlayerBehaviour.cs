using UnityEngine;

namespace Core.Player
{
    public sealed class PlayerBehaviour : MonoBehaviour
    {
        #region Encapsulation
        internal PlayerSetInputs Inputs { get => _inputs; }
        internal PlayerMovement Movement { get => _movement; }
        internal PlayerAnimation Animation { get => _animation; }

        public PlayerStatus Status { get => _status; }
        public PlayerAttack Attack { get => _attack; }
        public PlayerEquipment Equipment { get => _equipment; }
        #endregion

        [Header("Classes")]
        [SerializeField] private PlayerStatus _status;
        [SerializeField] private PlayerSetInputs _inputs;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerAttack _attack;
        [SerializeField] private PlayerAnimation _animation;
        [SerializeField] private PlayerEquipment _equipment;
    }
}
