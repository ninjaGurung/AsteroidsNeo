/*
 * Authored by: Mahatva Gurung
 * Date: 19th Jul 2025
 */

using UnityEngine;

namespace SuperLaggy.AsteroidsNeo.Core.Player
{
    /// <summary>
    /// Defines the contract for any class that provides player input.
    /// </summary>
    public interface IInputProvider
    {
        Vector2 MoveInput { get; }
        bool IsShooting { get; }
    }
}