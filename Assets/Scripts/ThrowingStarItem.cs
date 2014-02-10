using UnityEngine;
using System.Collections;

public class ThrowingStarItem : ItemScript {

	private Ryu ryu;
	private Object starPrefab;
	private Vector3 offset = new Vector3(0.5f, 0f, 0f);

	public override bool IsAutomatic() {
		return false;
	}

	protected override void Start() {
		starPrefab = Resources.Load("ThrowingStar");
		base.Start();
	}

	public override void Deploy() {
		if (GameData.spiritData < 3) {
			return;
		}
		float dir = ryu.facingRight ? 1 : -1;

		Instantiate(starPrefab, ryu.transform.position + dir * offset, Quaternion.identity);

		GameData.spiritData -= 3;
	}

	public override void OnPickedUp() {
		// TODO show sprite in the ui layer
		ryu = (Ryu) GameObject.Find("Ryu").GetComponent<Ryu>();
		GetComponent<SpriteRenderer>().sprite = null;
		rigidbody2D.isKinematic = true;
		collider2D.enabled = false;
	}
}