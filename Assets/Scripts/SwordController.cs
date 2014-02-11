using UnityEngine;
using System.Collections;

public class SwordController : MonoBehaviour {

	private int swordLayer;
    private int bossLayer;
	private int enemyLayer;
	private int projectileLayer;

	private BoxCollider2D boxCollider;

	public AudioClip swordClip;

	void Start() {
		boxCollider = GetComponent<BoxCollider2D>();
		swordLayer = LayerMask.NameToLayer("Sword");
		enemyLayer = LayerMask.NameToLayer("Enemies");
        bossLayer = LayerMask.NameToLayer("Boss");
		projectileLayer = LayerMask.NameToLayer("Enemy Projectiles");
		retractSword();
	}

	public void extendSword() {
		Physics2D.IgnoreLayerCollision(swordLayer, enemyLayer, false);
		boxCollider.enabled = true;
		AudioSource.PlayClipAtPoint(swordClip, transform.position, 1.0f);
	}

	public void retractSword() {
		Physics2D.IgnoreLayerCollision(swordLayer, enemyLayer, true);
		boxCollider.enabled = false;
	}

	private void setScaleX(float scaleX) {
		Vector3 scale = transform.localScale;
		scale.x = scaleX;
		transform.localScale = scale;
	}

	public void onCrouchStateChanged(bool crouching) {
		if (crouching) {
			boxCollider.center = new Vector2(boxCollider.center.x, -0.7f);
		} else {
			boxCollider.center = new Vector2(boxCollider.center.x, 0f);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == enemyLayer ||
		    other.gameObject.layer == bossLayer || 
		    other.gameObject.layer == projectileLayer) {
			EnemyScript enemy = other.GetComponent<EnemyScript>();
			if (enemy != null) {
				enemy.Die();
			}
		}
	}
}
