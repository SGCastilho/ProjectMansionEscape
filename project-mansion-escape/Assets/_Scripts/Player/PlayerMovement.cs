using UnityEngine;

namespace Core.Player
{
    public sealed class PlayerMovement : MonoBehaviour
    {
        #region Encapsulation
        internal bool AimingRightSide { get => _aimingRightSide; }
        #endregion

        [Header("Classes")]
        [SerializeField] private PlayerBehaviour _behaviour;
        [Space(6)]
        [SerializeField] private Rigidbody2D _rb2D;

        [Header("Settings")]
        [SerializeField] private Transform _graphicsPivot;
        [Space(12)]
        [SerializeField] [Range(100f, 600f)] private float _walkSpeed = 200f;
        [SerializeField] [Range(60f, 200f)] private float _aimSpeed = 100f;
        [Space(12)]
        [SerializeField] [Range(200f, 800f)] private float _sprintSpeed = 400f;
        [SerializeField] [Range(1f, 6f)] private float _sprintBreath = 4f;
        [SerializeField] [Range(6f, 10f)] private float _breathRecuperation = 8f;

        private bool _isAiming;
        private bool _aimingRightSide;
        private bool _isSprinting;
        private bool _sprintInCouldown;

        private float _currentSpeed;
        private float _currentSprintBreath;
        private float _currentBreathRecuperation;

        private Vector2 _movement;

        private void OnEnable() => SetupVariables();

        private void SetupVariables()
        {
            _currentSpeed = _walkSpeed;
        }

        private void FixedUpdate() => Movement();

        private void Movement()
        {
            _rb2D.velocity = new Vector2(_movement.x * _currentSpeed * Time.deltaTime, _rb2D.velocity.y);

            FlipGraphics();
        }

        private void FlipGraphics()
        {
            if (!_behaviour.Attack.IsAiming)
            {
                if (_movement.x > 0)
                {
                    _aimingRightSide = true;
                    _graphicsPivot.localScale = new Vector2(1f, 1f);
                }
                else if (_movement.x < 0)
                {
                    _aimingRightSide = false;
                    _graphicsPivot.localScale = new Vector2(-1f, 1f); ;
                }
            }
        }

        private void Update()
        {
            _movement = _behaviour.Inputs.HorizontalAxis * Vector2.right;

            SprintCounters();
        }

        private void SprintCounters()
        {
            if (_isSprinting && !_isAiming && !_sprintInCouldown)
            {
                _currentSprintBreath += Time.deltaTime;
                if (_currentSprintBreath >= _sprintBreath)
                {
                    _sprintInCouldown = true;

                    EndSprint();
                }
            }

            if (!_isSprinting && _isAiming &&_currentSprintBreath > 0 && !_sprintInCouldown)
            {
                _currentSprintBreath -= Time.deltaTime;
                if (_currentSprintBreath <= 0)
                {
                    _currentSprintBreath = 0;
                }
            }

            if (_sprintInCouldown)
            {
                _currentBreathRecuperation += Time.deltaTime;
                if (_currentBreathRecuperation >= _breathRecuperation)
                {
                    _currentSprintBreath = 0;
                    _currentBreathRecuperation = 0;

                    _sprintInCouldown = false;
                }
            }
        }

        internal void StartSprint()
        {
            if(!_sprintInCouldown && !_isAiming) 
            {
                _isSprinting = true;
                _currentSpeed = _sprintSpeed;
            }
        }

        internal void EndSprint()
        {
            if(!_isAiming)
            {
                _isSprinting = false;
                _currentSpeed = _walkSpeed;
            }
        }

        internal void StartAim()
        {
            _isAiming = true;
            _currentSpeed = _aimSpeed;
        }

        internal void EndAim()
        {
            _isAiming = false;

            if(_isSprinting)
            {
                _currentSpeed = _sprintSpeed;
            }
            else
            {
                _currentSpeed = _walkSpeed;
            }
        }

        internal bool CheckPlayerSide()
        {
            if (_movement.x > 0)
            {
                return true;
            }
            else if (_movement.x < 0)
            {
                return false;
            }

            return true;
        }
    }
}
