using UnityEngine;
using System.Collections;

public class ItemContainerScript : MonoBehaviour {

	public Transform item;

	public AudioClip fireClip;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.layer == LayerMask.NameToLayer("Sword") || collider.tag == "SpecialItem") {
			if (collider.tag == "SpecialItem")
				AudioSource.PlayClipAtPoint(fireClip, transform.position);
			Instantiate(item, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
}
