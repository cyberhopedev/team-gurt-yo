using NUnit.Framework;

/// <summary>
/// Tests gameplay logic for battles.
/// </summary>
public class BattleTests
{
    /// <summary>
    /// Tests the player taking damage from an enemy when in battle
    /// </summary>
    [Test]
    public void PlayerTakesDamage()
    {
        // Create player battler and set health
        var player = new PlayerBattler();
        player.currentHP = 100;

        // Call take damage and ensure that the player's health is updated
        player.TakeDamage(5);
        Assert.AreEqual(95, player.currentHP);
    }

    /// <summary>
    /// Tests that the player dies when their health reaches 0
    /// </summary>
    [Test]
    public void PlayerDiesWhenHPZero()
    {
        // Create player battler and set health
        var player = new PlayerBattler();
        player.currentHP = 100;

        // Take enough damage to die and ensure that the system knows it's dead
        player.TakeDamage(100);
        Assert.IsTrue(player.IsDead());
    }
    
    /// <summary>
    /// Tests the enemy taking damage from an enemy when in battle
    /// </summary>
    [Test]
    public void EnemyTakesDamage()
    {
        
        // Create enemy and set health
        var enemy = new goblins();
        enemy.currentHP = 100;

        // Call take damage and ensure that the enemy's health is updated
        enemy.TakeDamage(5);
        Assert.AreEqual(95, enemy.currentHP);
    }

    /// <summary>
    /// Tests that the enemy dies when their health reaches 0
    /// </summary>
    [Test]
    public void EnemyDiesWhenHPZero()
    {
        // Create enemy and set health
        var enemy = new goblins();
        enemy.currentHP = 100;

        // Take enough damage to die and ensure that the system knows it's dead
        enemy.TakeDamage(100);
        Assert.IsTrue(enemy.IsDead());
    }
}