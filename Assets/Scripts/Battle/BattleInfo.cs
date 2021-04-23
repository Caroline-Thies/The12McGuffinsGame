using System.Collections;
using System.Collections.Generic;

public class BattleInfo {

    public string comingFromScene;
    
    // This information should ideally be paired up in a dictionary
    // or something. However, dictionaries don't guarantee order.
    // As long as you don't manipulate these properties from the outside
    // it should be fine ;)
    public List<string> enemyIdentifiers = new List<string>();
    public List<Unit> enemyUnits = new List<Unit>();

    // The index starts out at -1 to indicate that no
    // enemy is being fought yet.
    public int index = -1;

    public BattleInfo(string comingFromScene) {
        this.comingFromScene = comingFromScene;
    }

    // Initialize a new battle against a single unit.
    // This is just for convenience.
    public BattleInfo(string comingFromScene, string enemyIdentifier, Unit enemyUnit) {
        this.comingFromScene = comingFromScene;
        Add(enemyIdentifier, enemyUnit);
    }

    // Add a new Unit with the given enemy identifier to the battle
    public void Add(string enemyIdentifier, Unit enemyUnit) {
        enemyIdentifiers.Add(enemyIdentifier);
        enemyUnits.Add(enemyUnit);
    }

    // Returns the next unit the player needs to fight
    // or null if there are none left.
    public Unit NextUnit() {
        index += 1;

        if (index < enemyUnits.Count) {
            return enemyUnits[index];
        } else {
            return null;
        }
    }

    public void MarkAllEnemiesAsDead() {
        foreach (string identifier in enemyIdentifiers) {
            Enemy.deadEnemies.Add(identifier);
        }
    }

}