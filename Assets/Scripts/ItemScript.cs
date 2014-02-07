using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
public abstract class ItemScript : MonoBehaviour {

	public bool pickedUp = false;

	public AudioClip pickUpClip;

	public abstract bool isAutomatic();

	public abstract void deploy();

	public abstract void onPickedUp();

	public void pickUp() {
		AudioSource.PlayClipAtPoint(pickUpClip, transform.position, 1.0f);
		pickedUp = true;
		if (isAutomatic()) {
			deploy();
		}
		onPickedUp();
	}

	protected virtual void Start() {
		Invoke("destroyItem", 5);
	}

	public void destroyItem() {
		if (!pickedUp)
			Destroy(this.gameObject);
	}
}
