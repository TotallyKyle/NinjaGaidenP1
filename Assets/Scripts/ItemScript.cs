using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
public abstract class ItemScript : MonoBehaviour {

	public bool pickedUp = false;

	public AudioClip pickUpClip;

	public abstract bool IsAutomatic();

	public abstract void Deploy();

	public abstract void OnPickedUp();

	public void PickUp() {
		PickUp(true);
	}

	public void PickUp(bool withSound) {
		if (withSound)
			AudioSource.PlayClipAtPoint(pickUpClip, transform.position, 1.0f);
		pickedUp = true;
		if (IsAutomatic()) {
			Deploy();
		}
		OnPickedUp();
	}

	protected virtual void Start() {
		Invoke("DestroyItem", 5);
	}

	public void DestroyItem() {
		if (!pickedUp)
			Destroy(this.gameObject);
	}
}
