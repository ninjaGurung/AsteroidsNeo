/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 19th Jul 2025
 */

using UnityEngine;

namespace SuperLaggy.AsteroidsNeo.Configurations
{
    /// <summary>
    /// For configuring ammo properties in the game.
    /// </summary>
    [CreateAssetMenu(fileName = "NewAmmoConfig", menuName = "Scriptable Objects/Ammo Configuration")]
    public class AmmoSO : ScriptableObject
    {
        [Header("Prefab")]
        public GameObject AmmoPrefab;
        [Header("Properties")]
        public float Speed = 20f;
        public int Damage = 1;
        public bool IsHeavyAttack = false; //TODO: Implement heavy attack logic in future update
        public float Lifetime = 3f; // Time before the ammo return to pool
    }
}