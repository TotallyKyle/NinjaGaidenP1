using UnityEngine;
using System.Collections;

public class ThrowingStarItem : ItemScript {
	
	private Object starPrefab;
	private GameData gameData;
	private Vector3 offset = new Vector3(0.5f, 0f, 0f);

	public override bool isAutomatic() {
		return false;
	}

	protected override void Start() {
		starPrefab = Resources.Load("ThrowingStar");
		gameData = GameObject.Find("Game HUD").GetComponent<GameData>();
		base.Start();
	}

	public override void deploy() {
		if (gameData.spiritData < 3) {
			return;
		}
		Ryu ryu = (Ryu) GameObject.Find("Ryu").GetComponent<Ryu>();
		if (!ryu.facingRight)
			offset.x *= -1;
		Instantiate(starPrefab, ryu.transform.position + offset, Quaternion.identity);
		if (!ryu.facingRight)
			offset.x *= -1;
		gameData.spiritData -= 3;
	}

	public override void onPickedUp() {
		// TODO show sprite in the ui layer
		GetComponent<SpriteRenderer>().sprite = null;
		rigidbody2D.Sleep();
		collider2D.isTrigger = true;
	}
}