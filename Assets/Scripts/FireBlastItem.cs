using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(BoxCollider2D))]
[RequireComponent (typeof(SpriteRenderer))]
public class FireBlastItem : ItemScript {

	private Ryu ryu;
	private Object fireWheelPrefab;

	private const float wheelSppedX  = 5.0f / 16f * 60f;
	private const float wheelSppedY1 = 5.0f / 16f * 60f;
	private const float wheelSppedY2 = 3.0f / 16f * 60f;
	private const float wheelSppedY3 = 1.0f / 16f * 60f;

	public AudioClip blastClip;

	protected override void Start () {
		fireWheelPrefab = Resources.Load("FireBlast");
		base.Start();
	}

	public override bool IsAutomatic() {
		return false;
	}
	
	public override void Deploy() {
		if (GameData.spiritData < 5) {
			return;
		}

		Vector3 position = ryu.transform.position;

		GameObject fire1 = Instantiate(fireWheelPrefab, position, Quaternion.identity) as GameObject;
		GameObject fire2 = Instantiate(fireWheelPrefab, position, Quaternion.identity) as GameObject;
		GameObject fire3 = Instantiate(fireWheelPrefab, position, Quaternion.identity) as GameObject;

		AudioSource.PlayClipAtPoint(blastClip, ryu.transform.position);

		float dir = ryu.facingRight ? 1f : -1f;

		fire1.rigidbody2D.velocity = new Vector2(dir * wheelSppedX, wheelSppedY1);
		fire2.rigidbody2D.velocity = new Vector2(dir * wheelSppedX, wheelSppedY2);
		fire3.rigidbody2D.velocity = new Vector2(dir * wheelSppedX, wheelSppedY3);

		GameData.spiritData -= 5;
	}

	public override void OnPickedUp() {
		// TODO set sprite in game data
		ryu = (Ryu) GameObject.Find("Ryu").GetComponent<Ryu>();
		GetComponent<SpriteRenderer>().sprite = null;
		rigidbody2D.isKinematic = true;
		collider2D.enabled = false;
	}
}
