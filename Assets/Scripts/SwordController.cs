using UnityEngine;
using System.Collections;

public class SwordController : MonoBehaviour {

	private int swordLayer;
	private int enemyLayer;

	void Start() {
		swordLayer = LayerMask.NameToLayer("Sword");
		enemyLayer = LayerMask.NameToLayer("Enemies");
		retractSword();
	}

	public void extendSword() {
		Physics2D.IgnoreLayerCollision(swordLayer, enemyLayer, false);
		setScaleX(0.95f);
	}

	public void retractSword() {
		Physics2D.IgnoreLayerCollision(swordLayer, enemyLayer, true);
		setScaleX(0f);
	}

	private void setScaleX(float scaleX) {
		Vector3 scale = transform.localScale;
		scale.x = scaleX;
		transform.localScale = scale;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == enemyLayer) {
			Destroy(other.gameObject);
		}
	}
}
