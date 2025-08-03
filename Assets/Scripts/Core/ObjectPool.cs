/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 18th Jul 2025
 */

namespace SuperLaggy.AsteroidsNeo.Core
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// A generic object pool for recycling GameObjects.
    /// </summary>
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject prefabToPool;
        private Queue<GameObject> objectsQueue = new Queue<GameObject>();
        private List<GameObject> activeObjectsList = new List<GameObject>();

        /// <summary>
        /// Initializes the pool with a specific prefab and fills it.
        /// This should be called by the factory that uses this pool.
        /// </summary>
        public void Initialize(GameObject _prefab, int _initialSize)
        {
            prefabToPool = _prefab;
            // Clear any existing objects if re-initializing
            while (objectsQueue.Count < _initialSize)
            {
                CreateAndEnqueueNewObject();
            }
        }

        /// <summary>
        /// Gets an object from the pool. If the pool is empty, it creates a new one.
        /// </summary>
        /// <returns>An active GameObject from the pool.</returns>
        public GameObject Get()
        {
            if (objectsQueue.Count == 0)
            {
                CreateAndEnqueueNewObject();
            }

            GameObject _pooledObject = objectsQueue.Dequeue();
            activeObjectsList.Add(_pooledObject);
            _pooledObject.SetActive(true);
            return _pooledObject;
        }

        /// <summary>
        /// Returns an object to the pool, making it available for reuse.
        /// </summary>
        /// <param name="_objectToReturn">The GameObject to return to the pool.</param>
        public void Return(GameObject _objectToReturn)
        {
            activeObjectsList.Remove(_objectToReturn);
            _objectToReturn.SetActive(false);
            objectsQueue.Enqueue(_objectToReturn);
        }

        /// <summary>
        /// Returns all active objects to the pool.
        /// </summary>
        public void ReturnAll()
        {
            List<GameObject> _objectsToReturn = new List<GameObject>(activeObjectsList);
            foreach (GameObject _obj in _objectsToReturn)
            {
                Return(_obj);
            }
        }

        /// <summary>
        /// Instantiates a new object from the prefab and adds it to the queue.
        /// </summary>
        private void CreateAndEnqueueNewObject()
        {
            GameObject _newObj = Instantiate(prefabToPool);
            _newObj.SetActive(false);
            // This line is important! The pooled object needs to know who its pool is.
            _newObj.GetComponent<IPoolable>()?.SetPool(this);
            objectsQueue.Enqueue(_newObj);
        }
    }

    /// <summary>
    /// An interface for objects that can be managed by an ObjectPool.
    /// </summary>
    public interface IPoolable
    {
        void SetPool(ObjectPool _pool);
    }
}