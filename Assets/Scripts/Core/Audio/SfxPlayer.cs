/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 22nd Jul 2025
 */

using System.Collections;
using UnityEngine;

namespace SuperLaggy.AsteroidsNeo.Core.Audio
{
    /// <summary>
    /// A reusable component that plays a single audio clip and then returns itself to an object pool.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class SfxPlayer : MonoBehaviour, IPoolable
    {
        private AudioSource audioSource;
        private ObjectPool myPool;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Plays the given audio clip once.
        /// </summary>
        public void Play(AudioClip _clip)
        {
            //audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(_clip);

            StartCoroutine(ReturnToPoolRoutine(_clip.length));
        }

        private IEnumerator ReturnToPoolRoutine(float _delay)
        {
            yield return new WaitForSeconds(_delay);
            myPool?.Return(this.gameObject);
        }

        public void SetPool(ObjectPool _pool)
        {
            myPool = _pool;
        }
    }
}