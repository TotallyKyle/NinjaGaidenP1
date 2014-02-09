using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(BoxCollider2D))]
[RequireComponent (typeof(SpriteRenderer))]
public class FireWheelItem : ItemScript {

	private int timer = 8;
	private GameObject fireWheel;
	private Object fireWheelPrefab;
	private Ryu ryu;

	public AudioClip tickClip;

	public override bool IsAutomatic() {
		return true;
	}

	protected override void Start() {
		ryu = GameObject.Find("Ryu").GetComponent<Ryu>();
		fireWheelPrefab = Resources.Load("FireWheel");
		base.Start();
	}

	public override void Deploy() {
		fireWheel = Instantiate(fireWheelPrefab, ryu.transform.position, Quaternion.identity) as GameObject;
		fireWheel.transform.parent = ryu.transform;
		InvokeRepeating("CountDown", 1, 1);
	}

	public override void OnPickedUp() {
		// TODO set power up sprite in game data
		GetComponent<SpriteRenderer>().sprite = null;
		collider2D.enabled = false;
	}

	private void CountDown() {
		AudioSource.PlayClipAtPoint(tickClip, fireWheel.transform.position);
		if (--timer == 0) {
			CancelInvoke();
			Destroy(fireWheel.gameObject);
			Destroy(gameObject);
			ryu.item = null;
			// TODO remove sprite from game data
		}
	}
}
