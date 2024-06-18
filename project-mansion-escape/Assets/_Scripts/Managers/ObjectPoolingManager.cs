using System.Collections.Generic;
using UnityEngine;

namespace Core.Managers
{
    public sealed class ObjectPoolingManager : MonoBehaviour
    {
        [System.Serializable]
        internal class Pool
        {
            #region Encapsulation
            internal string Key { get => _poolKey; }
            internal int MaxInstances { get => _poolMaxInstances; }
            internal GameObject Prefab { get => _poolPrefab; }
            #endregion

            [Header("Classes")]
            #region Editor Variable
                #if UNITY_EDITOR
                [SerializeField] private string _poolName = "Pool Name";
                #endif
            #endregion
            [SerializeField] private string _poolKey;
            [SerializeField] private int _poolMaxInstances;
            [SerializeField] private GameObject _poolPrefab;
        }

        [Header("Settings")]
        [SerializeField] private List<Pool> _poolList = new List<Pool>();

        private Dictionary<string, Queue<GameObject>> _poolDictionary;

        private void Awake() => SetupPooling();

        private void SetupPooling()
        {
            _poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in _poolList)
            {
                Queue<GameObject> _poolQueue = new Queue<GameObject>();

                for (int i = 0; i < pool.MaxInstances; i++)
                {
                    GameObject instance = Instantiate(pool.Prefab, Vector3.zero, Quaternion.identity);

                    instance.name = pool.Prefab.name + $"_{i}";
                    instance.SetActive(false);

                    _poolQueue.Enqueue(instance);
                }

                _poolDictionary.Add(pool.Key, _poolQueue);
            }
        }

        public GameObject SpawnPooling(ref string poolingKey, Vector2 poolingPosistion)
        {
            GameObject instance = _poolDictionary[poolingKey].Dequeue();

            instance.SetActive(false);

            instance.transform.position = poolingPosistion;

            instance.SetActive(true);

            _poolDictionary[poolingKey].Enqueue(instance);

            return instance;
        }

        public GameObject SpawnPooling(ref string poolingKey, Vector2 poolingPosistion, Quaternion poolingRotation)
        {
            GameObject instance = _poolDictionary[poolingKey].Dequeue();

            instance.SetActive(false);

            instance.transform.position = poolingPosistion;
            instance.transform.rotation = poolingRotation;

            instance.SetActive(true);

            _poolDictionary[poolingKey].Enqueue(instance);

            return instance;
        }

        public GameObject SpawnPooling(ref string poolingKey, Transform poolingParent)
        {
            GameObject instance = _poolDictionary[poolingKey].Dequeue();

            instance.SetActive(false);
            instance.transform.parent = null;

            instance.transform.parent = poolingParent;

            instance.SetActive(true);

            _poolDictionary[poolingKey].Enqueue(instance);

            return instance;
        }
        
        public GameObject SpawnPooling(ref string poolingKey, Vector2 poolingPosistion, Transform poolingParent)
        {
            GameObject instance = _poolDictionary[poolingKey].Dequeue();

            instance.SetActive(false);
            instance.transform.parent = null;

            instance.transform.parent = poolingParent;

            instance.transform.localPosition = poolingPosistion;

            instance.SetActive(true);

            _poolDictionary[poolingKey].Enqueue(instance);

            return instance;
        }

        public GameObject SpawnPooling(ref string poolingKey, Vector2 poolingPosistion, Quaternion poolingRotation, Transform poolingParent)
        {
            GameObject instance = _poolDictionary[poolingKey].Dequeue();

            instance.SetActive(false);
            instance.transform.parent = null;

            instance.transform.localPosition = poolingPosistion;
            instance.transform.localRotation = poolingRotation;
            instance.transform.parent = poolingParent;

            instance.SetActive(true);

            _poolDictionary[poolingKey].Enqueue(instance);

            return instance;
        }
    }
}
