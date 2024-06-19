using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Player
{
    public sealed class PlayerSetInputs : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private PlayerBehaviour _behaviour;

        internal float HorizontalAxis {get; private set;}

        private bool _sprintStarted;
        private bool _sprintEnded;
        private bool _aimStarted;
        private bool _aimEnded;

        public void GetHorizontalAxis(InputAction.CallbackContext value)
        {
            HorizontalAxis = value.ReadValue<float>();
        }

        public void SetSprintAction(InputAction.CallbackContext value)
        {
            _sprintStarted = value.started;
            _sprintEnded = value.canceled;

            if(_sprintStarted)
            {
                _behaviour.Movement.StartSprint();
            }

            if(_sprintEnded)
            {
                _behaviour.Movement.EndSprint();
            }
        }

        public void SetAimAction(InputAction.CallbackContext value)
        {
            _aimStarted = value.started;
            _aimEnded = value.canceled;

            if(_aimStarted)
            {
                _behaviour.Attack.StartAim();
            }

            if(_aimEnded)
            {
                _behaviour.Attack.EndAim();
            }
        }

        public void AttackAction(InputAction.CallbackContext value)
        {
            _behaviour.Attack.Attacking();
        }
    }
}
