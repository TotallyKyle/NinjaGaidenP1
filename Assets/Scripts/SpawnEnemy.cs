using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour
{
    //Enemy Prefab
    public Transform enemy;

    void Update()
    {
        Transform enemySpawned;
        GameObject camera = GameObject.Find("Main Camera");
        if ((Mathf.Abs(camera.transform.position.x - transform.position.x) < .05f) && (transform.childCount < 1))
        {
            enemySpawned = Instantiate(enemy, new Vector3(transform.position.x + 7.4f, transform.position.y, 0), Quaternion.identity) as Transform;
            enemySpawned.parent = transform;
        }
    }
}
