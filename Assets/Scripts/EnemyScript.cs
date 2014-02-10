using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public int score = 100;

	protected bool frozen = false;

	protected Vector3 offset = new Vector3(0f, 1f, 0f);

	void Awake() {
		EnemyController.GetInstance().RegisterEnemy(this);
	}

	public void Freeze() {
		frozen = true;
		rigidbody2D.Sleep();
		rigidbody2D.isKinematic = true;
	}

	public void Unfreeze() {
		frozen = false;
		rigidbody2D.WakeUp();
		rigidbody2D.isKinematic = false;
	}

	public virtual void Die() {
		Instantiate(Resources.Load("Explosion"), transform.position + offset, Quaternion.identity);
		GameData.scoreData += score;
		Destroy(gameObject);
	}

	void OnDestroy() {
		EnemyController.GetInstance().UnregisterEnemy(this);
	}
}
