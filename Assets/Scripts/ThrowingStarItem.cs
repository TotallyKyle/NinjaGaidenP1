using UnityEngine;
using System.Collections;

public class ThrowingStarItem : ItemScript {

	private Sprite sprite;
	private SpriteRenderer spriteRenderer;

	private ThrowingStar star;

	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public override bool isAutomatic() {
		return false;
	}

	public override void deploy() {
		// TODO check for sprit power
		Instantiate(star, GameObject.Find("Ryu").transform.position, Quaternion.identity);
	}

	public override void onPickedUp() {
		// TODO show sprite in the ui layer
		sprite = spriteRenderer.sprite;
		spriteRenderer.sprite = null;
		rigidbody2D.Sleep();
		collider2D.isTrigger = true;
	}
}