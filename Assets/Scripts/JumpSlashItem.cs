using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider2D))]
[RequireComponent (typeof(SpriteRenderer))]
public class JumpSlashItem : ItemScript {

	public override bool IsAutomatic() {
		return false;
	}

	public override void Deploy() {
		collider2D.enabled = true;
	}

	public void EndSlash() {
		collider2D.enabled = false;
		GameData.spiritData -= 5;
	}

	public override void OnPickedUp() {
		GameData.currentItem = GameData.ITEM_JUMP_SLASH;
		GetComponent<SpriteRenderer>().sprite = null;
		Destroy(rigidbody2D);
		collider2D.isTrigger = true;
		((BoxCollider2D) collider2D).size = new Vector2(2f, 2f);
		collider2D.enabled = false;
		gameObject.layer = LayerMask.NameToLayer("Player");
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.layer == LayerMask.NameToLayer("Enemies")) {
			EnemyScript enemy = collider.GetComponent<EnemyScript>();
			if (enemy != null) {
				enemy.Die();
			}
		}
	}
}
