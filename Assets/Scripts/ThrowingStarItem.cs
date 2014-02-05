using UnityEngine;
using System.Collections;

public class ThrowingStarItem : ItemScript {
	
	private GameObject starPrefab;

	public override bool isAutomatic() {
		return false;
	}

	protected override void Start() {
		starPrefab = (GameObject) Resources.Load("/Prefabs/Items/ThrowingStar");
		base.Start();
	}

	public override void deploy() {
		// TODO check for sprit power
		Instantiate(starPrefab, GameObject.Find("Ryu").transform.position, Quaternion.identity);
	}

	public override void onPickedUp() {
		// TODO show sprite in the ui layer
		GetComponent<SpriteRenderer>().sprite = null;
		rigidbody2D.Sleep();
		collider2D.isTrigger = true;
	}
}