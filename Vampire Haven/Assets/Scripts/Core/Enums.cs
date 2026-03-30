/// <summary>
/// Enums that will be used by the project.
/// </summary>
//###################################################################################//
// States that the turn based battle can be in
public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST, ESCAPED}

// Types of units within the game (Player, Enemy)
public enum UnitType {PLAYER, ENEMY}