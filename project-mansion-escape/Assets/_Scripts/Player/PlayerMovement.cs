using UnityEngine;

namespace Core.Player
{
    public sealed class PlayerMovement : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private PlayerBehaviour _behaviour;
        [Space(6)]
        [SerializeField] private Rigidbody2D _rb2D;

        [Header("Settings")]
        [SerializeField] [Range(100f, 600f)] private float _walkSpeed = 200f;
        [Space(12)]
        [SerializeField] [Range(200f, 800f)] private float _sprintSpeed = 400f;
        [SerializeField] [Range(1f, 6f)] private float _sprintBreath = 4f;
        [SerializeField] [Range(6f, 10f)] private float _breathRecuperation = 8f;

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
        }

        private void Update()
        {
            _movement = _behaviour.Inputs.HorizontalAxis * Vector2.right;

            SprintCounters();
        }

        private void SprintCounters()
        {
            if (_isSprinting && !_sprintInCouldown)
            {
                _currentSprintBreath += Time.deltaTime;
                if (_currentSprintBreath >= _sprintBreath)
                {
                    _sprintInCouldown = true;

                    EndSprint();
                }
            }

            if (!_isSprinting && _currentSprintBreath > 0 && !_sprintInCouldown)
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
            if(!_sprintInCouldown) 
            {
                _isSprinting = true;
                _currentSpeed = _sprintSpeed;
            }
        }

        internal void EndSprint()
        {
            _isSprinting = false;
            _currentSpeed = _walkSpeed;
        }
    }
}
