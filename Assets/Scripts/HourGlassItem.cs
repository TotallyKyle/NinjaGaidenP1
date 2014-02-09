using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
public class HourGlassItem : ItemScript {

	public AudioClip tickClip;

	private int timer = 5;

	public override bool IsAutomatic() {
		return true;
	}
	
	public override void Deploy() {
		EnemyController.GetInstance().SetEnemiesFrozen(true);
		InvokeRepeating("CountDown", 1, 1);
	}
	
	public override void OnPickedUp() {
		GetComponent<SpriteRenderer>().sprite = null;
		collider2D.enabled = false;
	}

	private void CountDown() {
		AudioSource.PlayClipAtPoint(tickClip, transform.position);
		if (--timer == 0) {
			CancelInvoke();
			EnemyController.GetInstance().SetEnemiesFrozen(false);
			Destroy(this.gameObject);
		}
	}
}
