using UnityEngine;

/// <summary>
/// Teleports the player to a target Transform when they walk into the trigger,
/// attach to our game objects in order to teleport the player to where they need to be.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class WarpManager : MonoBehaviour
{
    [Tooltip("Empty GameObject placed wherever you want the player to end up. " +
             "Drag it from the Hierarchy.")]
    [SerializeField] private Transform destination;

    [Tooltip("Tiny cooldown so the player can't immediately re-trigger the warp " +
             "if the destination is also inside another trigger.")]
    [SerializeField] private float cooldownSeconds = 0.5f;

    // Static so cooldown survives even if the warp is destroyed/reparented.
    // Time.time advances every frame; we compare against this stamp on each enter.
    private static float _nextWarpAllowedAt = 0f;

    /// <summary>
    /// Unity calls this when another Collider2D enters our trigger collider.
    /// We require both objects to have Collider2Ds AND at least one must have a
    /// Rigidbody2D for triggers to fire — your Player already has one.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // CompareTag is faster than other.tag == "Player" because it avoids
        // allocating a temporary string for the comparison.
        if (!other.CompareTag("Player"))
        {
            return;
        }

        // Cooldown gate: prevent the "ping-pong" loop where exiting the
        // destination immediately re-enters another warp pad.
        if (Time.time < _nextWarpAllowedAt){
            return;
        }
        _nextWarpAllowedAt = Time.time + cooldownSeconds;

        if (destination == null)
        {
            Debug.LogWarning($"WarpManager '{name}' has no destination set; skipping.");
            return;
        }

        // Teleport the player
        other.transform.position = destination.position;
    }
}