using UnityEngine;
using System.Collections;

public class ThrowingStarItem : ItemScript {
	
	private Object starPrefab;

	private Vector3 offset = new Vector3(0.5f, 1f, 0f);

	public override bool isAutomatic() {
		return false;
	}

	protected override void Start() {
		starPrefab = Resources.Load("ThrowingStar");
		base.Start();
	}

	public override void deploy() {
		// TODO check for sprit power
		Ryu ryu = (Ryu) GameObject.Find("Ryu").GetComponent<Ryu>();
		if (!ryu.facingRight)
			offset.x *= -1;
		Instantiate(starPrefab, ryu.transform.position + offset, Quaternion.identity);
		if (!ryu.facingRight)
			offset.x *= -1;
	}

	public override void onPickedUp() {
		// TODO show sprite in the ui layer
		GetComponent<SpriteRenderer>().sprite = null;
		rigidbody2D.Sleep();
		collider2D.isTrigger = true;
	}
}