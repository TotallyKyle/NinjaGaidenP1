using UnityEngine;
using System.Collections;

public class ThrowingStarItem : ItemScript {
	
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
		Ryu ryu = (Ryu) GameObject.Find("Ryu").GetComponent<Ryu>();
		if (!ryu.facingRight)
			offset.x *= -1;
		Instantiate(starPrefab, ryu.transform.position + offset, Quaternion.identity);
		if (!ryu.facingRight)
			offset.x *= -1;
		GameData.spiritData -= 3;
	}

	public override void OnPickedUp() {
		// TODO show sprite in the ui layer
		GetComponent<SpriteRenderer>().sprite = null;
		collider2D.enabled = false;
	}
}