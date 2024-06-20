using UnityEngine;

namespace Core.Utilities
{
    public sealed class MoveObjectHorizontal : MonoBehaviour
    {
        #region Encapsulation
        public bool MoveRight { get => _moveRight; set => _moveRight = value; }
        #endregion

        [Header("Settings")]
        [SerializeField] [Range(20f, 80f)] private float _bulletSpeed = 40f;
        [SerializeField] private bool _moveRight = true;

        private Transform _transform;

        private void Awake() => CacheVariables();

        private void CacheVariables()
        {
            _transform = transform;
        }

        private void FixedUpdate()
        {
            if(_moveRight)
            {
                _transform.Translate(Vector2.right * _bulletSpeed * Time.deltaTime);
            }
            else
            {
                _transform.Translate(Vector2.left * _bulletSpeed * Time.deltaTime);
            }
        }
    }
}
