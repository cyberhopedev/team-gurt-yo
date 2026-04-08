/// <summary>
/// Shared behaviors to help with redundancy/repeated code.
/// </summary
//###################################################################################//

/// <summary>
/// Behavior of something that can be damaged and thus killed, in this case
/// the Player and Enemy
/// </summary
public interface IDamageable
{   
    /// <summary>
    /// Subtracts the amount of damage the current battler takes.
    /// </summary>
    /// <param name="damage">The amount of damage inflicted to the battler</param>
    void TakeDamage(int damage);

    /// <summary>
    /// Reports if the battler is dead or alive, in order to be dead
    /// the battler's health must be 0.
    /// </summary>
    /// <returns>True if the battler is dead, false if otherwise</returns>
    bool IsDead();
}