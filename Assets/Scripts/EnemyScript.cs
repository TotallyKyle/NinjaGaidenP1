using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	protected bool frozen = false;

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

	void OnDestroy() {
		EnemyController.GetInstance().UnregisterEnemy(this);
	}
}
