using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController {

	private static EnemyController sInstance;

	public static EnemyController GetInstance() {
		if (sInstance == null)
			sInstance = new EnemyController();
		return sInstance;
	}

	private List<EnemyScript> mEnemies;
	private List<SpawnEnemy> mSpawnPoints;

	private EnemyController() {
		mEnemies = new List<EnemyScript>();
		mSpawnPoints = new List<SpawnEnemy>();
	}

	public void RegisterEnemy(EnemyScript enemy) {
		mEnemies.Add(enemy);
	}

	public void RegisterSpawnPoint(SpawnEnemy spawnPoint) {
		mSpawnPoints.Add(spawnPoint);
	}

	public void UnregisterEnemy(EnemyScript enemy) {
		mEnemies.Remove(enemy);
	}

	public void UnregisterSpawnPoint(SpawnEnemy spawnPoint) {
		mSpawnPoints.Remove(spawnPoint);
	}

	public void SetEnemiesFrozen(bool frozen) {
		foreach (EnemyScript enemy in mEnemies) {
			if (frozen)
				enemy.Freeze();
			else
				enemy.Unfreeze();
		}
		foreach (SpawnEnemy spawnPoint in mSpawnPoints) {
			spawnPoint.spawning = !frozen;
		}
	}
}
