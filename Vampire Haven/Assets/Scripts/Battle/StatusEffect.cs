using UnityEngine;

/// <summary>
/// Lightweight per-turn effect attached to a Battler. Lives on the unit until
/// turnsRemaining hits 0. We DON'T use ScriptableObjects here because
/// instances need mutable state (turnsRemaining ticks down each round).
/// </summary>
[System.Serializable]
public class StatusEffect
{
    public StatusType type;
    public int        magnitude;       // damage per tick, or +N stacks for bleed/poison
    public int        turnsRemaining;
    public Sprite     icon;            // shown in BattleUI

    public StatusEffect(StatusType t, int mag, int turns, Sprite ic = null)
    {
        type = t; magnitude = mag; turnsRemaining = turns; icon = ic;
    }
}