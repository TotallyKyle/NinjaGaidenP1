using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
public abstract class ItemScript : MonoBehaviour {

	private bool pickedUp = false;

	public abstract bool isAutomatic();

	public abstract void deploy();

	public abstract void onPickedUp();

	public void pickUp() {
		pickedUp = true;
		if (isAutomatic()) {
			deploy();
		}
		onPickedUp();
	}

	void Start() {
		Invoke("destroyItem", 5);
	}

	public void destroyItem() {
		if (!pickedUp)
			Destroy(this.gameObject);
	}
}
