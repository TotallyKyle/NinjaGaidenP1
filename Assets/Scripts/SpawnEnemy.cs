using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {
    //Enemy Prefab
    public Transform enemy;

    //States
    public bool offCamera = true;

	public bool spawning = true;

	void Start() {
		EnemyController.GetInstance().RegisterSpawnPoint(this);
	}

    void Update() {
		if (!spawning) return;

        Transform enemySpawned;
        GameObject camera = GameObject.Find("Main Camera");
        float relativePosition = camera.transform.position.x - transform.position.x;
        if ((Mathf.Abs(relativePosition) < 26 / 3) && offCamera && (transform.childCount < 1)) {
            enemySpawned = Instantiate(enemy, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as Transform;
            enemySpawned.parent = transform;
            offCamera = false;
        } else if ((Mathf.Abs(relativePosition) > 26 / 3))
            offCamera = true;
    }

	void OnDestroy() {
		EnemyController.GetInstance().UnregisterSpawnPoint(this);
	}
}
