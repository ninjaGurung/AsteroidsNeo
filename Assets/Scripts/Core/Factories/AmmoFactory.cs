/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 19th Jul 2025
 */

using UnityEngine;
using SuperLaggy.AsteroidsNeo.Configurations;

namespace SuperLaggy.AsteroidsNeo.Core.Factories
{
    /// <summary>
    /// A factory responsible for creating ammo instances. It uses an object pool to manage ammo GameObjects.
    /// </summary>
    public class AmmoFactory : MonoBehaviour
    {
        [Tooltip("The ScriptableObject defining the ammo to be created.")]
        public AmmoSO AmmoConfig;
        [Tooltip("The object pool that contains the ammo prefabs.")]
        public ObjectPool AmmoPool;
        [Tooltip("The initial number of ammo to create.")]
        public int InitialPoolSize = 20;

        void OnEnable()
        {
            GameEvents.OnPrepareNewGame += HandlePrepareNewGame;
        }

        void OnDisable()
        {
            GameEvents.OnPrepareNewGame -= HandlePrepareNewGame;
        }

        void Start()
        {
            if (AmmoConfig == null || AmmoPool == null)
            {
                Debug.LogError("[AmmoFactory] Factory dependencies are not set!");
                return;
            }

            AmmoPool.Initialize(AmmoConfig.AmmoPrefab, InitialPoolSize);
        }

        /// <summary>
        /// Retrieves an ammo from the pool.
        /// </summary>
        public GameObject Create()
        {
            if (AmmoPool == null)
            {
                Debug.LogError("[AmmoFactory] AmmoPool is not assigned in the AmmoFactory.");
                return null;
            }

            return AmmoPool.Get();
        }

        private void HandlePrepareNewGame()
        {
            if (AmmoPool != null)
            {
                Debug.Log("[AmmoFactory] Returning all active ammo to pool.");
                AmmoPool.ReturnAll();
            }
        }
    }
}