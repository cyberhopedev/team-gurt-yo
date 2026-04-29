/// <summary>
/// Enums that will be used by the project.
/// </summary>
//###################################################################################//
// States that the turn based battle can be in
public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST, ESCAPED}

// Types of units within the game (Player, Enemy)
public enum UnitType {PLAYER, ENEMY}

// Types of "abilities" associated with each starter class
public enum AbilityType
{
    Block,    // Tank   — gain armor this turn
    Stealth,  // Rogue  — skip turn, attack twice next turn with crit
    Mark,     // Archer — deal 150% damage next turn to marked target
    Swipe,    // Fighter — hit enemy for half damage (AOE concept simplified to single-target)
}

