using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class EnemyGroup : MonoBehaviour
{

    bool shouldDelete = false;

    void Start()
    {
        Enemy[] enemiesInGroup = GetComponentsInChildren<Enemy>();
        int enemiesDead = 0;

        foreach (Enemy enemy in enemiesInGroup) {
            Collider2D enemyCollider = enemy.gameObject.GetComponent<Collider2D>();
            enemyCollider.enabled = false;

            if (Enemy.deadEnemies.Contains(enemy.getEnemyID())) {
                enemiesDead += 1;
            }
        }

        if (enemiesDead == enemiesInGroup.Length) {
            shouldDelete = true;
        }
    }

    void Update() {
        if (shouldDelete) {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if (shouldDelete) {
			return;
		}
		
		if (other.gameObject.CompareTag("Player")) {
            string sceneName = SceneManager.GetActiveScene().name;
            BattleInfo battleInfo = new BattleInfo(sceneName);

            Enemy[] enemiesInGroup = GetComponentsInChildren<Enemy>();

            foreach (Enemy enemy in enemiesInGroup) {
                battleInfo.Add(enemy.getEnemyID(), enemy.unit);
            }

			BattleSystem.currentBattle = battleInfo;

			LoadNewArea loadArea = gameObject.AddComponent<LoadNewArea>();
			loadArea.sceneToLoad = "BattleArea";
			loadArea.LoadArea();
		}
    }

}
