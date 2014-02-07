using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class SwordController : MonoBehaviour {

	private int swordLayer;
	private int enemyLayer;

    public GameData gameData;

	private BoxCollider2D boxCollider;

	public AudioClip swordClip;

	void Start() {
		boxCollider = GetComponent<BoxCollider2D>();
		swordLayer = LayerMask.NameToLayer("Sword");
		enemyLayer = LayerMask.NameToLayer("Enemies");
		retractSword();
	}

	public void extendSword() {
		Physics2D.IgnoreLayerCollision(swordLayer, enemyLayer, false);
		boxCollider.enabled = true;
		audio.PlayOneShot(swordClip, 1.0f);
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
		if (other.gameObject.layer == enemyLayer) {
			DieScript dieScript = other.GetComponent<DieScript>();
			if (dieScript != null) {
				dieScript.die();
			}
			Destroy(other.gameObject);
            gameData.scoreData+= 100;
		}
	}
}
