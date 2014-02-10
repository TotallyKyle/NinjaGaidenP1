using UnityEngine;
using System.Collections;


[RequireComponent (typeof(Rigidbody2D))]

[RequireComponent (typeof(BoxCollider2D))]

[RequireComponent (typeof(SpriteRenderer))]


public class WindmillStarItem : ItemScript {

	private Ryu ryu;

	private GameObject windmillStar;

	private Object windmillStarPrefab;

	private Vector3 offset = new Vector3(0.5f, 0f, 0f);

	public override bool IsAutomatic() {
		return false;
	}

	protected override void Start () {
		windmillStarPrefab = Resources.Load("WindmillStar");
		base.Start();
	}

	public override void Deploy() {
		if (GameData.spiritData < 5 || windmillStar != null) {
			return;
		}

		float dir = ryu.facingRight ? 1 : -1;

		windmillStar = (GameObject) Instantiate(windmillStarPrefab, ryu.transform.position + dir * offset, Quaternion.identity);

		GameData.spiritData -= 5;
	}

	public override void OnPickedUp() {
		// TODO set sprite in game data
		ryu = GameObject.Find("Ryu").GetComponent<Ryu>();
		GetComponent<SpriteRenderer>().sprite = null;
		rigidbody2D.isKinematic = true;
		collider2D.enabled = false;
	}
}
